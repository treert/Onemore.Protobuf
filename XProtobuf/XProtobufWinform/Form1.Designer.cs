namespace XProtobufWinform
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageEnum = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBoxEnum = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonSearchEnum = new System.Windows.Forms.Button();
            this.textBoxSearchEnum = new System.Windows.Forms.TextBox();
            this.listBoxEnum = new System.Windows.Forms.ListBox();
            this.EnumListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddEnum = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModifyEnum = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteEnum = new System.Windows.Forms.ToolStripMenuItem();
            this.genCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageMessage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSearchMessage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxMessage = new System.Windows.Forms.ListBox();
            this.MessageContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAddMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModifyMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.genCodeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabPageSetting = new System.Windows.Forms.TabPage();
            this.textBoxProtoBinFilePath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNameSpace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCSharpFileOutPath = new System.Windows.Forms.TextBox();
            this.textBoxProtoFileOutPath = new System.Windows.Forms.TextBox();
            this.textBoxCurrentWorkDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageEnum.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.EnumListContextMenuStrip.SuspendLayout();
            this.tabPageMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MessageContextMenuStrip.SuspendLayout();
            this.tabPageSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageEnum);
            this.tabControl1.Controls.Add(this.tabPageMessage);
            this.tabControl1.Controls.Add(this.tabPageSetting);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(802, 534);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageEnum
            // 
            this.tabPageEnum.Controls.Add(this.tableLayoutPanel1);
            this.tabPageEnum.Location = new System.Drawing.Point(4, 22);
            this.tabPageEnum.Name = "tabPageEnum";
            this.tabPageEnum.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEnum.Size = new System.Drawing.Size(794, 508);
            this.tabPageEnum.TabIndex = 0;
            this.tabPageEnum.Text = "Enum";
            this.tabPageEnum.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 217F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxEnum, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(788, 502);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // richTextBoxEnum
            // 
            this.richTextBoxEnum.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxEnum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEnum.Location = new System.Drawing.Point(220, 3);
            this.richTextBoxEnum.Name = "richTextBoxEnum";
            this.richTextBoxEnum.Size = new System.Drawing.Size(565, 496);
            this.richTextBoxEnum.TabIndex = 0;
            this.richTextBoxEnum.Text = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(211, 496);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.listBoxEnum, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(211, 496);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonSearchEnum);
            this.panel3.Controls.Add(this.textBoxSearchEnum);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(205, 34);
            this.panel3.TabIndex = 0;
            // 
            // buttonSearchEnum
            // 
            this.buttonSearchEnum.Location = new System.Drawing.Point(151, 3);
            this.buttonSearchEnum.Name = "buttonSearchEnum";
            this.buttonSearchEnum.Size = new System.Drawing.Size(51, 23);
            this.buttonSearchEnum.TabIndex = 1;
            this.buttonSearchEnum.Text = "Search";
            this.buttonSearchEnum.UseVisualStyleBackColor = true;
            this.buttonSearchEnum.Click += new System.EventHandler(this.buttonSearchEnum_Click);
            // 
            // textBoxSearchEnum
            // 
            this.textBoxSearchEnum.Location = new System.Drawing.Point(3, 3);
            this.textBoxSearchEnum.Name = "textBoxSearchEnum";
            this.textBoxSearchEnum.Size = new System.Drawing.Size(142, 21);
            this.textBoxSearchEnum.TabIndex = 0;
            this.textBoxSearchEnum.TextChanged += new System.EventHandler(this.textBoxSearchEnum_TextChanged);
            // 
            // listBoxEnum
            // 
            this.listBoxEnum.ContextMenuStrip = this.EnumListContextMenuStrip;
            this.listBoxEnum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxEnum.FormattingEnabled = true;
            this.listBoxEnum.ItemHeight = 12;
            this.listBoxEnum.Location = new System.Drawing.Point(3, 43);
            this.listBoxEnum.Name = "listBoxEnum";
            this.listBoxEnum.Size = new System.Drawing.Size(205, 450);
            this.listBoxEnum.TabIndex = 1;
            this.listBoxEnum.SelectedIndexChanged += new System.EventHandler(this.listBoxEnum_SelectedIndexChanged);
            // 
            // EnumListContextMenuStrip
            // 
            this.EnumListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddEnum,
            this.ToolStripMenuItemModifyEnum,
            this.ToolStripMenuItemDeleteEnum,
            this.genCodeToolStripMenuItem});
            this.EnumListContextMenuStrip.Name = "enumListContextMenuStrip";
            this.EnumListContextMenuStrip.Size = new System.Drawing.Size(131, 92);
            // 
            // ToolStripMenuItemAddEnum
            // 
            this.ToolStripMenuItemAddEnum.Name = "ToolStripMenuItemAddEnum";
            this.ToolStripMenuItemAddEnum.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemAddEnum.Text = "添加";
            this.ToolStripMenuItemAddEnum.Click += new System.EventHandler(this.ToolStripMenuItemAddEnum_Click);
            // 
            // ToolStripMenuItemModifyEnum
            // 
            this.ToolStripMenuItemModifyEnum.Name = "ToolStripMenuItemModifyEnum";
            this.ToolStripMenuItemModifyEnum.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemModifyEnum.Text = "修改";
            this.ToolStripMenuItemModifyEnum.Click += new System.EventHandler(this.ToolStripMenuItemModifyEnum_Click);
            // 
            // ToolStripMenuItemDeleteEnum
            // 
            this.ToolStripMenuItemDeleteEnum.Name = "ToolStripMenuItemDeleteEnum";
            this.ToolStripMenuItemDeleteEnum.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemDeleteEnum.Text = "删除";
            this.ToolStripMenuItemDeleteEnum.Click += new System.EventHandler(this.ToolStripMenuItemDeleteEnum_Click);
            // 
            // genCodeToolStripMenuItem
            // 
            this.genCodeToolStripMenuItem.Name = "genCodeToolStripMenuItem";
            this.genCodeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.genCodeToolStripMenuItem.Text = "GenCode";
            this.genCodeToolStripMenuItem.Click += new System.EventHandler(this.genCodeToolStripMenuItem_Click);
            // 
            // tabPageMessage
            // 
            this.tabPageMessage.Controls.Add(this.splitContainer1);
            this.tabPageMessage.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessage.Name = "tabPageMessage";
            this.tabPageMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessage.Size = new System.Drawing.Size(794, 508);
            this.tabPageMessage.TabIndex = 1;
            this.tabPageMessage.Text = "Message";
            this.tabPageMessage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxMessage);
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox2);
            this.splitContainer1.Size = new System.Drawing.Size(788, 502);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.listBoxMessage, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(211, 502);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxSearchMessage);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 34);
            this.panel1.TabIndex = 0;
            // 
            // textBoxSearchMessage
            // 
            this.textBoxSearchMessage.Location = new System.Drawing.Point(3, 3);
            this.textBoxSearchMessage.Name = "textBoxSearchMessage";
            this.textBoxSearchMessage.Size = new System.Drawing.Size(147, 21);
            this.textBoxSearchMessage.TabIndex = 0;
            this.textBoxSearchMessage.TextChanged += new System.EventHandler(this.textBoxSearchMessage_TextChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(156, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxMessage
            // 
            this.listBoxMessage.ContextMenuStrip = this.MessageContextMenuStrip;
            this.listBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMessage.FormattingEnabled = true;
            this.listBoxMessage.ItemHeight = 12;
            this.listBoxMessage.Location = new System.Drawing.Point(3, 43);
            this.listBoxMessage.Name = "listBoxMessage";
            this.listBoxMessage.Size = new System.Drawing.Size(205, 456);
            this.listBoxMessage.TabIndex = 1;
            this.listBoxMessage.SelectedIndexChanged += new System.EventHandler(this.listBoxMessage_SelectedIndexChanged);
            // 
            // MessageContextMenuStrip
            // 
            this.MessageContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAddMessage,
            this.ToolStripMenuItemModifyMessage,
            this.ToolStripMenuItemDeleteMessage,
            this.genCodeToolStripMenuItem1});
            this.MessageContextMenuStrip.Name = "dataContextMenuStrip";
            this.MessageContextMenuStrip.Size = new System.Drawing.Size(131, 92);
            // 
            // ToolStripMenuItemAddMessage
            // 
            this.ToolStripMenuItemAddMessage.Name = "ToolStripMenuItemAddMessage";
            this.ToolStripMenuItemAddMessage.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemAddMessage.Text = "添加";
            this.ToolStripMenuItemAddMessage.Click += new System.EventHandler(this.ToolStripMenuItemAddMessage_Click);
            // 
            // ToolStripMenuItemModifyMessage
            // 
            this.ToolStripMenuItemModifyMessage.Name = "ToolStripMenuItemModifyMessage";
            this.ToolStripMenuItemModifyMessage.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemModifyMessage.Text = "修改";
            this.ToolStripMenuItemModifyMessage.Click += new System.EventHandler(this.ToolStripMenuItemModifyMessage_Click);
            // 
            // ToolStripMenuItemDeleteMessage
            // 
            this.ToolStripMenuItemDeleteMessage.Name = "ToolStripMenuItemDeleteMessage";
            this.ToolStripMenuItemDeleteMessage.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemDeleteMessage.Text = "删除";
            this.ToolStripMenuItemDeleteMessage.Click += new System.EventHandler(this.ToolStripMenuItemDeleteMessage_Click);
            // 
            // genCodeToolStripMenuItem1
            // 
            this.genCodeToolStripMenuItem1.Name = "genCodeToolStripMenuItem1";
            this.genCodeToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.genCodeToolStripMenuItem1.Text = "GenCode";
            this.genCodeToolStripMenuItem1.Click += new System.EventHandler(this.genCodeToolStripMenuItem_Click);
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMessage.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.Size = new System.Drawing.Size(573, 502);
            this.richTextBoxMessage.TabIndex = 2;
            this.richTextBoxMessage.Text = "";
            this.richTextBoxMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RichTextSelect_MouseDown);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(573, 502);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.AllowDrop = true;
            this.tabPageSetting.Controls.Add(this.textBoxProtoBinFilePath);
            this.tabPageSetting.Controls.Add(this.label5);
            this.tabPageSetting.Controls.Add(this.textBoxNameSpace);
            this.tabPageSetting.Controls.Add(this.label4);
            this.tabPageSetting.Controls.Add(this.textBoxCSharpFileOutPath);
            this.tabPageSetting.Controls.Add(this.textBoxProtoFileOutPath);
            this.tabPageSetting.Controls.Add(this.textBoxCurrentWorkDir);
            this.tabPageSetting.Controls.Add(this.label3);
            this.tabPageSetting.Controls.Add(this.label2);
            this.tabPageSetting.Controls.Add(this.label1);
            this.tabPageSetting.Location = new System.Drawing.Point(4, 22);
            this.tabPageSetting.Name = "tabPageSetting";
            this.tabPageSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetting.Size = new System.Drawing.Size(794, 508);
            this.tabPageSetting.TabIndex = 2;
            this.tabPageSetting.Text = "Setting";
            this.tabPageSetting.UseVisualStyleBackColor = true;
            this.tabPageSetting.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabPageSetting_DragDrop);
            this.tabPageSetting.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabPageSetting_DragEnter);
            // 
            // textBoxProtoBinFilePath
            // 
            this.textBoxProtoBinFilePath.Location = new System.Drawing.Point(160, 104);
            this.textBoxProtoBinFilePath.Name = "textBoxProtoBinFilePath";
            this.textBoxProtoBinFilePath.Size = new System.Drawing.Size(387, 21);
            this.textBoxProtoBinFilePath.TabIndex = 13;
            this.textBoxProtoBinFilePath.TextChanged += new System.EventHandler(this.UpdateTabSettingPage);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(18, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "ProtoBinFilePath";
            // 
            // textBoxNameSpace
            // 
            this.textBoxNameSpace.Location = new System.Drawing.Point(160, 55);
            this.textBoxNameSpace.Name = "textBoxNameSpace";
            this.textBoxNameSpace.Size = new System.Drawing.Size(387, 21);
            this.textBoxNameSpace.TabIndex = 11;
            this.textBoxNameSpace.TextChanged += new System.EventHandler(this.UpdateTabSettingPage);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(18, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "NameSpace";
            // 
            // textBoxCSharpFileOutPath
            // 
            this.textBoxCSharpFileOutPath.Location = new System.Drawing.Point(160, 182);
            this.textBoxCSharpFileOutPath.Name = "textBoxCSharpFileOutPath";
            this.textBoxCSharpFileOutPath.Size = new System.Drawing.Size(387, 21);
            this.textBoxCSharpFileOutPath.TabIndex = 9;
            this.textBoxCSharpFileOutPath.TextChanged += new System.EventHandler(this.UpdateTabSettingPage);
            // 
            // textBoxProtoFileOutPath
            // 
            this.textBoxProtoFileOutPath.Location = new System.Drawing.Point(160, 146);
            this.textBoxProtoFileOutPath.Name = "textBoxProtoFileOutPath";
            this.textBoxProtoFileOutPath.Size = new System.Drawing.Size(387, 21);
            this.textBoxProtoFileOutPath.TabIndex = 8;
            this.textBoxProtoFileOutPath.TextChanged += new System.EventHandler(this.UpdateTabSettingPage);
            // 
            // textBoxCurrentWorkDir
            // 
            this.textBoxCurrentWorkDir.Enabled = false;
            this.textBoxCurrentWorkDir.Location = new System.Drawing.Point(160, 12);
            this.textBoxCurrentWorkDir.Name = "textBoxCurrentWorkDir";
            this.textBoxCurrentWorkDir.Size = new System.Drawing.Size(387, 21);
            this.textBoxCurrentWorkDir.TabIndex = 7;
            this.textBoxCurrentWorkDir.Text = "drag work dir or xproto.conf to this tab";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(20, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "CurrentWorkConf";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(18, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "CSharpFileOutPath";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(18, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ProtoFileOutPath";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 534);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Onemore.Protobuf";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.tabControl1.ResumeLayout(false);
            this.tabPageEnum.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.EnumListContextMenuStrip.ResumeLayout(false);
            this.tabPageMessage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.MessageContextMenuStrip.ResumeLayout(false);
            this.tabPageSetting.ResumeLayout(false);
            this.tabPageSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageEnum;
        private System.Windows.Forms.TabPage tabPageMessage;
        private System.Windows.Forms.TabPage tabPageSetting;
        private System.Windows.Forms.TextBox textBoxCSharpFileOutPath;
        private System.Windows.Forms.TextBox textBoxProtoFileOutPath;
        private System.Windows.Forms.TextBox textBoxCurrentWorkDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBoxEnum;
        private System.Windows.Forms.TextBox textBoxProtoBinFilePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxNameSpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxSearchMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxMessage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonSearchEnum;
        private System.Windows.Forms.TextBox textBoxSearchEnum;
        private System.Windows.Forms.ListBox listBoxEnum;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.ContextMenuStrip EnumListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddEnum;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifyEnum;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteEnum;
        private System.Windows.Forms.ContextMenuStrip MessageContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAddMessage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModifyMessage;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteMessage;
        private System.Windows.Forms.ToolStripMenuItem genCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genCodeToolStripMenuItem1;
    }
}

