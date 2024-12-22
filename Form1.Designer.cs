namespace ComponentDatabase
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            textBox_manufacturer = new TextBox();
            label_manufacturer = new Label();
            textBox_manufacturer_part_number = new TextBox();
            label_manufacturer_part_number = new Label();
            label_vendor = new Label();
            textBox_vendor = new TextBox();
            textBox_vendor_part_number = new TextBox();
            label_vendor_part_number = new Label();
            notifyIcon1 = new NotifyIcon(components);
            textBox_description = new TextBox();
            label_description = new Label();
            textBox_search = new TextBox();
            label_search = new Label();
            dataGridView_parts = new DataGridView();
            button_add = new Button();
            button_edit_save = new Button();
            label_sub_location = new Label();
            textBox_sub_location = new TextBox();
            label_location = new Label();
            textBox_location = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView_parts).BeginInit();
            SuspendLayout();
            // 
            // textBox_manufacturer
            // 
            textBox_manufacturer.Font = new Font("Segoe UI", 14F);
            textBox_manufacturer.Location = new Point(1312, 140);
            textBox_manufacturer.Name = "textBox_manufacturer";
            textBox_manufacturer.ReadOnly = true;
            textBox_manufacturer.Size = new Size(654, 45);
            textBox_manufacturer.TabIndex = 2;
            // 
            // label_manufacturer
            // 
            label_manufacturer.AutoSize = true;
            label_manufacturer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label_manufacturer.Location = new Point(1312, 105);
            label_manufacturer.Name = "label_manufacturer";
            label_manufacturer.Size = new Size(171, 32);
            label_manufacturer.TabIndex = 0;
            label_manufacturer.Text = "Manufacturer";
            // 
            // textBox_manufacturer_part_number
            // 
            textBox_manufacturer_part_number.Font = new Font("Segoe UI", 14F);
            textBox_manufacturer_part_number.Location = new Point(1312, 223);
            textBox_manufacturer_part_number.Name = "textBox_manufacturer_part_number";
            textBox_manufacturer_part_number.ReadOnly = true;
            textBox_manufacturer_part_number.Size = new Size(654, 45);
            textBox_manufacturer_part_number.TabIndex = 3;
            // 
            // label_manufacturer_part_number
            // 
            label_manufacturer_part_number.AutoSize = true;
            label_manufacturer_part_number.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label_manufacturer_part_number.Location = new Point(1312, 188);
            label_manufacturer_part_number.Name = "label_manufacturer_part_number";
            label_manufacturer_part_number.Size = new Size(326, 32);
            label_manufacturer_part_number.TabIndex = 0;
            label_manufacturer_part_number.Text = "Manufacturer Part Number";
            // 
            // label_vendor
            // 
            label_vendor.AutoSize = true;
            label_vendor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label_vendor.Location = new Point(1312, 271);
            label_vendor.Name = "label_vendor";
            label_vendor.Size = new Size(96, 32);
            label_vendor.TabIndex = 0;
            label_vendor.Text = "Vendor";
            // 
            // textBox_vendor
            // 
            textBox_vendor.Font = new Font("Segoe UI", 14F);
            textBox_vendor.Location = new Point(1312, 306);
            textBox_vendor.Name = "textBox_vendor";
            textBox_vendor.ReadOnly = true;
            textBox_vendor.Size = new Size(654, 45);
            textBox_vendor.TabIndex = 4;
            // 
            // textBox_vendor_part_number
            // 
            textBox_vendor_part_number.Font = new Font("Segoe UI", 14F);
            textBox_vendor_part_number.Location = new Point(1312, 389);
            textBox_vendor_part_number.Name = "textBox_vendor_part_number";
            textBox_vendor_part_number.ReadOnly = true;
            textBox_vendor_part_number.Size = new Size(654, 45);
            textBox_vendor_part_number.TabIndex = 5;
            // 
            // label_vendor_part_number
            // 
            label_vendor_part_number.AutoSize = true;
            label_vendor_part_number.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label_vendor_part_number.Location = new Point(1312, 354);
            label_vendor_part_number.Name = "label_vendor_part_number";
            label_vendor_part_number.Size = new Size(251, 32);
            label_vendor_part_number.TabIndex = 0;
            label_vendor_part_number.Text = "Vendor Part Number";
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // textBox_description
            // 
            textBox_description.Font = new Font("Segoe UI", 14F);
            textBox_description.Location = new Point(1312, 472);
            textBox_description.Name = "textBox_description";
            textBox_description.ReadOnly = true;
            textBox_description.Size = new Size(654, 45);
            textBox_description.TabIndex = 6;
            // 
            // label_description
            // 
            label_description.AutoSize = true;
            label_description.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label_description.Location = new Point(1312, 437);
            label_description.Name = "label_description";
            label_description.Size = new Size(146, 32);
            label_description.TabIndex = 0;
            label_description.Text = "Description";
            // 
            // textBox_search
            // 
            textBox_search.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_search.Location = new Point(12, 44);
            textBox_search.Name = "textBox_search";
            textBox_search.Size = new Size(1954, 55);
            textBox_search.TabIndex = 1;
            textBox_search.TextChanged += textBox_search_TextChanged;
            textBox_search.MouseDown += textBox_search_MouseDown;
            // 
            // label_search
            // 
            label_search.AutoSize = true;
            label_search.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_search.Location = new Point(12, 9);
            label_search.Name = "label_search";
            label_search.Size = new Size(89, 32);
            label_search.TabIndex = 0;
            label_search.Text = "Search";
            // 
            // dataGridView_parts
            // 
            dataGridView_parts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_parts.Location = new Point(12, 105);
            dataGridView_parts.MultiSelect = false;
            dataGridView_parts.Name = "dataGridView_parts";
            dataGridView_parts.ReadOnly = true;
            dataGridView_parts.RowHeadersWidth = 62;
            dataGridView_parts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_parts.Size = new Size(1294, 1072);
            dataGridView_parts.TabIndex = 0;
            dataGridView_parts.TabStop = false;
            dataGridView_parts.SelectionChanged += dataGridView_parts_SelectionChanged;
            // 
            // button_add
            // 
            button_add.Location = new Point(1312, 1107);
            button_add.Name = "button_add";
            button_add.Size = new Size(200, 70);
            button_add.TabIndex = 0;
            button_add.TabStop = false;
            button_add.Text = "Add";
            button_add.UseVisualStyleBackColor = true;
            button_add.Click += button_add_Click;
            // 
            // button_edit_save
            // 
            button_edit_save.Location = new Point(1518, 1107);
            button_edit_save.Name = "button_edit_save";
            button_edit_save.Size = new Size(200, 70);
            button_edit_save.TabIndex = 0;
            button_edit_save.TabStop = false;
            button_edit_save.Text = "Edit";
            button_edit_save.UseVisualStyleBackColor = true;
            button_edit_save.Click += button_edit_save_Click;
            // 
            // label_sub_location
            // 
            label_sub_location.AutoSize = true;
            label_sub_location.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label_sub_location.Location = new Point(1312, 1021);
            label_sub_location.Name = "label_sub_location";
            label_sub_location.Size = new Size(165, 32);
            label_sub_location.TabIndex = 0;
            label_sub_location.Text = "Sub-Location";
            // 
            // textBox_sub_location
            // 
            textBox_sub_location.Font = new Font("Segoe UI", 14F);
            textBox_sub_location.Location = new Point(1312, 1056);
            textBox_sub_location.Name = "textBox_sub_location";
            textBox_sub_location.ReadOnly = true;
            textBox_sub_location.Size = new Size(654, 45);
            textBox_sub_location.TabIndex = 8;
            // 
            // label_location
            // 
            label_location.AutoSize = true;
            label_location.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label_location.Location = new Point(1312, 938);
            label_location.Name = "label_location";
            label_location.Size = new Size(112, 32);
            label_location.TabIndex = 0;
            label_location.Text = "Location";
            // 
            // textBox_location
            // 
            textBox_location.Font = new Font("Segoe UI", 14F);
            textBox_location.Location = new Point(1312, 973);
            textBox_location.Name = "textBox_location";
            textBox_location.ReadOnly = true;
            textBox_location.Size = new Size(654, 45);
            textBox_location.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1978, 1189);
            Controls.Add(label_location);
            Controls.Add(textBox_location);
            Controls.Add(label_sub_location);
            Controls.Add(textBox_sub_location);
            Controls.Add(button_edit_save);
            Controls.Add(button_add);
            Controls.Add(dataGridView_parts);
            Controls.Add(label_search);
            Controls.Add(textBox_search);
            Controls.Add(label_description);
            Controls.Add(textBox_description);
            Controls.Add(textBox_vendor_part_number);
            Controls.Add(label_vendor_part_number);
            Controls.Add(textBox_vendor);
            Controls.Add(label_vendor);
            Controls.Add(label_manufacturer_part_number);
            Controls.Add(textBox_manufacturer_part_number);
            Controls.Add(label_manufacturer);
            Controls.Add(textBox_manufacturer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Database Viewer";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_parts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_manufacturer;
        private Label label_manufacturer;
        private TextBox textBox_manufacturer_part_number;
        private Label label_manufacturer_part_number;
        private Label label_vendor;
        private TextBox textBox_vendor;
        private TextBox textBox_vendor_part_number;
        private Label label_vendor_part_number;
        private NotifyIcon notifyIcon1;
        private TextBox textBox_description;
        private Label label_description;
        private Label label_search;
        private DataGridView dataGridView_parts;
        private TextBox textBox_search;
        private Button button_add;
        private Button button_edit_save;
        private Label label_sub_location;
        private TextBox textBox_sub_location;
        private Label label_location;
        private TextBox textBox_location;
    }
}
