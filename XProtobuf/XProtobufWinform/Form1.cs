﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using XProtobufWinform.Model;

namespace XProtobufWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void listBoxEnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            // enum 
            var message = GetSelectedMessage(listBoxEnum);
            UpdateRichText(richTextBoxEnum, message, "Can not find Message who's name is " + listBoxEnum.Text);
        }

        private void listBoxMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // message
            var message = GetSelectedMessage(listBoxMessage);
            UpdateRichText(richTextBoxMessage, message, "Can not find Message who's name is " + listBoxMessage.Text);
        }

        private void ToolStripMenuItemAddEnum_Click(object sender, EventArgs e)
        {
            var dialog = new EnumEditDialog(null);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                var message = dialog.m_Message;
                EditorMessageManager.singleton.AddMessage(message);
                Command.SaveMessageData();
                listBoxEnum.Items.Add(message.Name);
            }
        }

        private void ToolStripMenuItemModifyEnum_Click(object sender, EventArgs e)
        {
            var message = GetSelectedMessage(listBoxEnum);
            if(message == null)
            {
                MessageBox.Show("Has not selected Enum");
                return;
            }
            
            // editor message
            var dialog = new EnumEditDialog(message.Clone());
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                EditorMessageManager.singleton.UpdateMessage(dialog.m_Message, message);
                Command.SaveMessageData();
                UpdateRichText(richTextBoxEnum, message);
            }
        }

        private void UpdateRichText(RichTextBox rich, EditorMessage message, string default_text = "???")
        {
            if(message != null)
            {
                rich.Text = message.ToDescriptionString();
            }
            else
            {
                rich.Text = default_text;
            }
        }

        private void ToolStripMenuItemDeleteEnum_Click(object sender, EventArgs e)
        {
            var message = GetSelectedMessage(listBoxEnum);
            if(message == null)
            {
                MessageBox.Show("Has not select Enum");
                return;
            }
            if(EditorMessageManager.singleton.CheckMessageHasBeDepends(message))
            {
                MessageBox.Show("Enum [" + listBoxEnum.Text + "] has be used by some message, check it.");
                return;
            }
            EditorMessageManager.singleton.DeleteMessage(message);
            Command.SaveMessageData();
            UpdateRichText(richTextBoxEnum, message, "");
        }

        EditorMessage GetSelectedMessage(ListBox list)
        {
            if(list.SelectedItems.Count  == 0)
            {
                return null;
            }
            return EditorMessageManager.singleton.GetByName(list.SelectedItem.ToString());
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab == tabPageEnum)
            {

            }
            else if(tabControl1.SelectedTab == tabPageMessage)
            {

            }
            else if(tabControl1.SelectedTab == tabPageSetting)
            {

            }
        }

        private void buttonSearchEnum_Click(object sender, EventArgs e)
        {
            RefreshMessageBoxNames(textBoxSearchEnum.Text, true);
        }

        void RefreshMessageBoxNames(string txt, bool is_enum)
        {
            txt = txt.Trim();
            List<string> names = new List<string>();
            names.Clear();
            foreach(var f in EditorMessageManager.singleton.m_editor_messages.Values)
            {
                if (is_enum && f.m_enum == null) continue;
                if (is_enum == false && f.m_message == null) continue;
                var name = f.Name;
                if(txt.Length == 0 || name.ToLower().IndexOf(txt.ToLower()) >= 0)
                {
                    names.Add(name);
                }
            }

            ListBox box = is_enum ? listBoxEnum : listBoxMessage;
            box.Items.Clear();
            box.Items.AddRange(names.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshMessageBoxNames(textBoxSearchEnum.Text, false);
        }

        private void textBoxSearchEnum_TextChanged(object sender, EventArgs e)
        {
            RefreshMessageBoxNames(textBoxSearchEnum.Text, true);
        }

        private void tabPageSetting_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tabPageSetting_DragDrop(object sender, DragEventArgs e)
        {
            var files = ((string[])e.Data.GetData(DataFormats.FileDrop));//文件路径+文件名
            if(files.Length > 0)
            {
                LoadConf(files[0]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var work_dir = System.IO.Directory.GetCurrentDirectory() + "/XProtobufConf";
            System.IO.Directory.CreateDirectory(work_dir);
            LoadConf(work_dir);
        }

        private bool _is_loading_conf = false;
        private void LoadConf(string file)
        {
            _is_loading_conf = true;
            Command.LoadConf(file);
            RefreshTabSettingPage();
            RefreshMessageBoxNames("", true);
            RefreshMessageBoxNames("", false);
            _is_loading_conf = false;
        }

        private void RefreshTabSettingPage()
        {
            textBoxCurrentWorkDir.Text = EditorConf.singleton.m_conf_file;
            textBoxNameSpace.Text = EditorMessageManager.singleton.NameSpace;
            textBoxProtoBinFilePath.Text = EditorConf.singleton.m_ProtoBinFilePath;
            textBoxProtoFileOutPath.Text = EditorConf.singleton.m_ProtoFileOutPath;
            textBoxCSharpFileOutPath.Text = EditorConf.singleton.m_CSharpFileOutPath;
        }

        private void UpdateTabSettingPage(object sender, EventArgs e)
        {
            if(_is_loading_conf == false)
            {
                EditorConf.singleton.m_conf_file = textBoxCurrentWorkDir.Text.Trim();
                EditorConf.singleton.m_ProtoBinFilePath = textBoxProtoBinFilePath.Text.Trim();
                EditorConf.singleton.m_ProtoFileOutPath = textBoxProtoFileOutPath.Text.Trim();
                EditorConf.singleton.m_CSharpFileOutPath = textBoxCSharpFileOutPath.Text.Trim();
                Command.SaveConfData();
                {
                    var name = textBoxNameSpace.Text.Trim();
                    if (name != EditorMessageManager.singleton.NameSpace)
                    {
                        EditorMessageManager.singleton.NameSpace = name;
                        Command.SaveMessageData();
                    }
                }
            }
        }

        private void textBoxSearchMessage_TextChanged(object sender, EventArgs e)
        {
            RefreshMessageBoxNames(textBoxSearchEnum.Text, false);
        }

        private void ToolStripMenuItemAddMessage_Click(object sender, EventArgs e)
        {
            // New Message
            var dialog = new MessageEditDialog(null);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                var message = dialog.m_Message;
                EditorMessageManager.singleton.AddMessage(message);
                Command.SaveMessageData();
                listBoxMessage.Items.Add(message.Name);
            }
        }

        private void ToolStripMenuItemModifyMessage_Click(object sender, EventArgs e)
        {
            // Modify Message
            var message = GetSelectedMessage(listBoxMessage);
            if (message == null)
            {
                MessageBox.Show("Has not selected Message");
                return;
            }

            // editor message
            var dialog = new MessageEditDialog(message.Clone());
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                EditorMessageManager.singleton.UpdateMessage(dialog.m_Message, message);
                Command.SaveMessageData();
                UpdateRichText(richTextBoxMessage, message);
            }
        }

        private void ToolStripMenuItemDeleteMessage_Click(object sender, EventArgs e)
        {
            // Delete Message
            var message = GetSelectedMessage(listBoxMessage);
            if (message == null)
            {
                MessageBox.Show("Has not select Message");
                return;
            }
            if (EditorMessageManager.singleton.CheckMessageHasBeDepends(message))
            {
                MessageBox.Show("Message [" + listBoxMessage.Text + "] has be used by some message, check it.");
                return;
            }
            EditorMessageManager.singleton.DeleteMessage(message);
            Command.SaveMessageData();
            UpdateRichText(richTextBoxMessage, message, "");
        }

        private void genCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Command.GenCode();
        }

        private void RichTextSelect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RichTextBox txt_box = (RichTextBox)sender;
                string txt = txt_box.SelectedText.Trim();
                var message = EditorMessageManager.singleton.GetByName(txt);

                if (message != null)
                {
                    MenuItem menu_item;
                    if (message.m_enum != null)
                    {
                        // enum
                        menu_item = new MenuItem("Enum: " + txt);
                        menu_item.Click += new EventHandler((object sender_, EventArgs e_) =>
                        {
                            tabControl1.SelectedTab = tabPageEnum;
                            listBoxEnum.Text = txt;
                            UpdateRichText(txt_box, message);
                        });

                    }
                    else if(message.m_message != null)
                    {
                        // message
                        menu_item = new MenuItem("Message: " + txt);
                        menu_item.Click += new EventHandler((object sender_, EventArgs e_) =>
                        {
                            tabControl1.SelectedTab = tabPageMessage;
                            listBoxMessage.Text = txt;
                            UpdateRichText(txt_box, message);
                        });
                    }
                    else
                    {
                        throw new Exception();
                    }

                    ContextMenu menu = new ContextMenu();

                    menu.MenuItems.Add(menu_item);
                    txt_box.ContextMenu = menu;
                }
                else
                {
                    txt_box.ContextMenu = null;
                }
            }
        }
    }
}
