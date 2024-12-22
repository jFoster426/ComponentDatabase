using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace ComponentDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private class Node
        {
            public string Manufacturer { get; set; }
            public string ManufacturerPartNumber { get; set; }
            public string Vendor { get; set; }
            public string VendorPartNumber { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public string SubLocation { get; set; }
        }

        private List<Node> databaseList;

        private const int RW_MODE = 0;
        private const int RO_MODE = 1;
        private int currentMode = RO_MODE;

        private string[] databaseContent;

        private static string databasePath = "database";
        private static string databaseBackupPath = "database-backup";

        private void reLinkDatabase()
        {
            dataGridView_parts.DataSource = null;
            dataGridView_parts.DataSource = databaseList;

            // Don't show first empty column
            dataGridView_parts.RowHeadersVisible = false;

            // Loop through each column in the DataGridView
            foreach (DataGridViewColumn column in dataGridView_parts.Columns)
            {
                // Set the AutoSizeMode for each column to AllCells
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            dataGridView_parts.Refresh();
        }

        private string extractElement(string input)
        {
            input = input.Split('=')[1];
            int startIndex = input.IndexOf('\'') + 1;
            int endIndex = input.IndexOf('\'', startIndex);

            // Check if both quotes are found
            if (startIndex > 0 && endIndex > startIndex)
            {
                return input.Substring(startIndex, endIndex - startIndex);
            }

            return string.Empty;
        }

        static string GetNextBackupFileName(string baseName)
        {
            string directoryPath = Directory.GetCurrentDirectory();
            string pattern = $@"^{databaseBackupPath}-(\d{{5}})\.txt$";
            Regex regex = new Regex(pattern);

            // Get the highest backup number
            int maxNumber = 0;
            foreach (var file in Directory.GetFiles(directoryPath, $"{baseName}*"))
            {
                var match = regex.Match(Path.GetFileName(file));
                if (match.Success && int.TryParse(match.Groups[1].Value, out int number))
                {
                    maxNumber = Math.Max(maxNumber, number);
                }
            }

            // Increment the highest number by 1 and format it as a three-digit number
            int nextNumber = maxNumber + 1;
            return Path.Combine(directoryPath, $"{baseName}-{nextNumber:D5}");
        }

        private void clearTextBoxes()
        {
            textBox_manufacturer.Text = String.Empty;
            textBox_manufacturer_part_number.Text = String.Empty;
            textBox_vendor.Text = String.Empty;
            textBox_vendor_part_number.Text = String.Empty;
            textBox_description.Text = String.Empty;
            textBox_location.Text = String.Empty;
            textBox_sub_location.Text = String.Empty;
        }

        private void setTextBoxesReadOnly(bool readOnly)
        {
            textBox_manufacturer.ReadOnly = readOnly;
            textBox_manufacturer_part_number.ReadOnly = readOnly;
            textBox_vendor.ReadOnly = readOnly;
            textBox_vendor_part_number.ReadOnly = readOnly;
            textBox_description.ReadOnly = readOnly;
            textBox_location.ReadOnly = readOnly;
            textBox_sub_location.ReadOnly = readOnly;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Make a copy of the database file in case something goes terribly wrong
            File.Copy(databasePath + ".txt", GetNextBackupFileName(databaseBackupPath) + ".txt", overwrite: true);

            // Read the contents into string array databaseContent
            databaseContent = System.IO.File.ReadAllLines(databasePath + ".txt");

            // Create databaseList
            databaseList = new List<Node>();

            foreach (string line in databaseContent)
            {
                string line2 = line;
                if (line2.Length < 3)
                {
                    continue;
                }
                if (line2[0] != '{' || line2[^2] != '}' || line2[^1] != ';')
                {
                    // Error
                    MessageBox.Show("Could not parse database");
                }
                line2 = line2.Replace('{', '\0');
                line2 = line2.Replace('}', '\0');
                line2 = line2.Replace(';', '\0');

                string[] lineParts = line2.Split('|');

                Node newNode = new Node();

                foreach (string field in lineParts)
                {
                    if (field.StartsWith("\'Manufacturer\'="))
                    {
                        newNode.Manufacturer = extractElement(field);
                    }
                    if (field.StartsWith("\'Manufacturer Part Number\'="))
                    {
                        newNode.ManufacturerPartNumber = extractElement(field);
                    }
                    if (field.StartsWith("\'Vendor\'="))
                    {
                        newNode.Vendor = extractElement(field);
                    }
                    if (field.StartsWith("\'Vendor Part Number\'="))
                    {
                        newNode.VendorPartNumber = extractElement(field);
                    }
                    if (field.StartsWith("\'Description\'="))
                    {
                        newNode.Description = extractElement(field);
                    }
                    if (field.StartsWith("\'Location\'="))
                    {
                        newNode.Location = extractElement(field);
                    }
                    if (field.StartsWith("\'SubLocation\'="))
                    {
                        newNode.SubLocation = extractElement(field);
                    }
                }
                databaseList.Add(newNode);
            }

            if (databaseList.Count > 0)
            {
                reLinkDatabase();
            }
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            string[] searchParams = textBox_search.Text.Split(' ');

            // Clear the current cell selection to avoid InvalidOperationException
            dataGridView_parts.CurrentCell = null;

            // Loop through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView_parts.Rows)
            {
                bool rowVisible = false;
                // Check each cell in the current row
                foreach (DataGridViewCell cell in row.Cells)
                {
                    bool allSearchParamsPresent = true;
                    foreach (string searchParam in searchParams)
                    {
                        if (cell.Value == null) continue;
                        // TODO: Incorporate Levenshtein distance to filter by typo in part number?
                        if (!cell.Value.ToString().Contains(searchParam, StringComparison.OrdinalIgnoreCase))
                        {
                            allSearchParamsPresent = false;
                            break;
                        }
                    }
                    if (allSearchParamsPresent)
                    {
                        rowVisible = true;
                        break;
                    }
                }
                // Set the row's visibility based on whether the search term was found
                row.Visible = rowVisible;
            }
        }

        private void textBox_search_MouseDown(object sender, MouseEventArgs e)
        {
            // Clear the current cell selection to avoid InvalidOperationException
            dataGridView_parts.CurrentCell = null;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            // Properties are saved in the edit_save function
            // ...

            // If we are in edit mode then save the previous entry that was just added
            if (currentMode == RW_MODE)
            {
                button_edit_save_Click(null, null);
            }

            // Add an element to the databaseContent (string array representing file contents)
            string[] newDatabaseContent = new string[databaseContent.Length + 1];
            Array.Copy(databaseContent, newDatabaseContent, databaseContent.Length);
            databaseContent = newDatabaseContent;

            // Add the new item to the list that we see in the datagridview
            databaseList.Add(new Node());
            reLinkDatabase();
            dataGridView_parts.Rows[^1].Selected = true;
            dataGridView_parts.CurrentCell = dataGridView_parts.Rows[^1].Cells[0];

            // Update the text in the text box (all to be empty)
            clearTextBoxes();

            // User wants to be able to update the data, so switch to edit mode
            setTextBoxesReadOnly(false);

            // Update button text and state
            button_edit_save.Text = "Save";
            currentMode = RW_MODE;
        }

        private void button_edit_save_Click(object sender, EventArgs e)
        {
            if (currentMode == RO_MODE)
            {
                // User wants to be able to update the data
                setTextBoxesReadOnly(false);

                // Update button text and state
                button_edit_save.Text = "Save";
                currentMode = RW_MODE;
                return;
            }

            if (currentMode == RW_MODE)
            {
                // Save the data now
                setTextBoxesReadOnly(true);

                if (dataGridView_parts.CurrentRow != null)
                {
                    int currentRowIndex = dataGridView_parts.CurrentRow.Index;

                    // Update the data
                    databaseList[currentRowIndex].Manufacturer = textBox_manufacturer.Text;
                    databaseList[currentRowIndex].ManufacturerPartNumber = textBox_manufacturer_part_number.Text;
                    databaseList[currentRowIndex].Vendor = textBox_vendor.Text;
                    databaseList[currentRowIndex].VendorPartNumber = textBox_vendor_part_number.Text;
                    databaseList[currentRowIndex].Description = textBox_description.Text;
                    databaseList[currentRowIndex].Location = textBox_location.Text;
                    databaseList[currentRowIndex].SubLocation = textBox_sub_location.Text;

                    dataGridView_parts.Refresh();

                    // Save data to the database file
                    databaseContent[currentRowIndex] = '{' + "\'Manufacturer\'=\'" + databaseList[currentRowIndex].Manufacturer + "\'|" +
                                                    "\'Manufacturer Part Number\'=\'" + databaseList[currentRowIndex].ManufacturerPartNumber + "\'|" +
                                                    "\'Vendor\'=\'" + databaseList[currentRowIndex].Vendor + "\'|" +
                                                    "\'Vendor Part Number\'=\'" + databaseList[currentRowIndex].VendorPartNumber + "\'|" +
                                                    "\'Description\'=\'" + databaseList[currentRowIndex].Description + "\'|" +
                                                    "\'Location\'=\'" + databaseList[currentRowIndex].Location + "\'|" +
                                                    "\'SubLocation\'=\'" + databaseList[currentRowIndex].SubLocation + "\'};";
                    File.WriteAllLines(databasePath + ".txt", databaseContent);
                }

                // Update button text and state
                button_edit_save.Text = "Edit";
                currentMode = RO_MODE;
                return;
            }
        }

        private void dataGridView_parts_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView_parts.CurrentRow;

            if (selectedRow == null)
            {
                clearTextBoxes();
                return;
            }

            textBox_manufacturer.Text = selectedRow.Cells["Manufacturer"].Value?.ToString() ?? String.Empty;
            textBox_manufacturer_part_number.Text = selectedRow.Cells["ManufacturerPartNumber"].Value?.ToString() ?? String.Empty;
            textBox_vendor.Text = selectedRow.Cells["Vendor"].Value?.ToString() ?? String.Empty;
            textBox_vendor_part_number.Text = selectedRow.Cells["VendorPartNumber"].Value?.ToString() ?? String.Empty;
            textBox_description.Text = selectedRow.Cells["Description"].Value?.ToString() ?? String.Empty;
            textBox_location.Text = selectedRow.Cells["Location"].Value?.ToString() ?? String.Empty;
            textBox_sub_location.Text = selectedRow.Cells["SubLocation"].Value?.ToString() ?? String.Empty;
        }
    }
}
