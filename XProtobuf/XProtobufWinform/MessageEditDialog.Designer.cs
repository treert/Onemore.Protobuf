namespace XProtobufWinform
{
    partial class MessageEditDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listDataField = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataEditMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddField = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModifyField = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteField = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.dataEditMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDataName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(435, 575);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Message名字";
            // 
            // txtDataName
            // 
            this.txtDataName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDataName.Location = new System.Drawing.Point(177, 3);
            this.txtDataName.Name = "txtDataName";
            this.txtDataName.Size = new System.Drawing.Size(171, 21);
            this.txtDataName.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button2.Location = new System.Drawing.Point(204, 549);
            this.button2.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "取消修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.listDataField);
            this.groupBox1.Location = new System.Drawing.Point(3, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBox1.Size = new System.Drawing.Size(429, 483);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message 的成员变量";
            // 
            // listDataField
            // 
            this.listDataField.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listDataField.ContextMenuStrip = this.dataEditMenuStrip;
            this.listDataField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDataField.FullRowSelect = true;
            this.listDataField.GridLines = true;
            this.listDataField.Location = new System.Drawing.Point(10, 17);
            this.listDataField.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.listDataField.Name = "listDataField";
            this.listDataField.Size = new System.Drawing.Size(409, 463);
            this.listDataField.TabIndex = 4;
            this.listDataField.UseCompatibleStateImageBehavior = false;
            this.listDataField.View = System.Windows.Forms.View.Details;
            this.listDataField.DoubleClick += new System.EventHandler(this.ModifyField_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "索引";
            this.columnHeader1.Width = 98;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "变量名";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 145;
            // 
            // dataEditMenuStrip
            // 
            this.dataEditMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddField,
            this.ToolStripMenuItemModifyField,
            this.ToolStripMenuItemDeleteField});
            this.dataEditMenuStrip.Name = "dataEditMenuStrip";
            this.dataEditMenuStrip.Size = new System.Drawing.Size(101, 70);
            // 
            // ToolStripMenuItemAddField
            // 
            this.ToolStripMenuItemAddField.Name = "ToolStripMenuItemAddField";
            this.ToolStripMenuItemAddField.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemAddField.Text = "添加";
            this.ToolStripMenuItemAddField.Click += new System.EventHandler(this.ToolStripMenuItemAddField_Click);
            // 
            // ToolStripMenuItemModifyField
            // 
            this.ToolStripMenuItemModifyField.Name = "ToolStripMenuItemModifyField";
            this.ToolStripMenuItemModifyField.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemModifyField.Text = "修改";
            this.ToolStripMenuItemModifyField.Click += new System.EventHandler(this.ModifyField_Click);
            // 
            // ToolStripMenuItemDeleteField
            // 
            this.ToolStripMenuItemDeleteField.Name = "ToolStripMenuItemDeleteField";
            this.ToolStripMenuItemDeleteField.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemDeleteField.Text = "删除";
            this.ToolStripMenuItemDeleteField.Click += new System.EventHandler(this.ToolStripMenuItemDeleteField_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(96, 549);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类型名";
            this.columnHeader2.Width = 156;
            // 
            // MessageEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 575);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MessageEditDialog";
            this.Text = "MessageEditDialog";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.dataEditMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDataName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listDataField;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip dataEditMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddField;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifyField;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteField;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}