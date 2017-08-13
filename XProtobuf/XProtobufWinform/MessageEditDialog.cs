using Onemore.Protobuf;
using System;
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
    public partial class MessageEditDialog : Form
    {
        public EditorMessage m_Message;
        bool IsAddNew = true;

        public MessageEditDialog(EditorMessage message)
        {
            InitializeComponent();

            if(message == null)
            {
                m_Message = EditorMessage.NewMessage();
                IsAddNew = true;
            }
            else
            {
                IsAddNew = false;
                m_Message = message;
                txtDataName.Text = m_Message.Name;
                txtDataName.Enabled = false;

                foreach(var field in m_Message.m_message.m_fields.Values.OrderBy(t_ => t_.m_index))
                {
                    listDataField.Items.Add(CreateListViewItemByField(field));
                }
            }

            this.DialogResult = DialogResult.Cancel;
        }

        ListViewItem CreateListViewItemByField(FieldInfo field)
        {
            ListViewItem item = new ListViewItem(field.m_index.ToString());
            item.SubItems.Add(field.m_type_name + (field.m_is_array ? "[]" : ""));
            item.SubItems.Add(field.m_name);
            return item;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = txtDataName.Text.Trim();
            if(name.Length == 0)
            {
                MessageBox.Show("Name is Empty.");
                return;
            }

            if(IsAddNew)
            {
                if(EditorMessageManager.singleton.CheckNameAreadyExist(name))
                {
                    MessageBox.Show("Name [" + name + "] aready exist.");
                    return;
                }
                m_Message.m_message.m_name = name;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripMenuItemDeleteField_Click(object sender, EventArgs e)
        {
            // delete field
            foreach (ListViewItem item in listDataField.SelectedItems)
            {
                m_Message.m_message.m_fields.Remove(item.Text);
                item.Remove();
            }
        }

        private void ToolStripMenuItemAddField_Click(object sender, EventArgs e)
        {
            // add field
            var dialog = new MessageFieldEditDialog(m_Message, null);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var item = CreateListViewItemByField(dialog.m_FieldInfo);
                listDataField.Items.Add(item);
            }
        }

        private void ModifyField_Click(object sender, EventArgs e)
        {
            // modify field
            if(listDataField.SelectedItems.Count == 0)
            {
                return;
            }

            var item = listDataField.SelectedItems[0];
            var dialog = new MessageFieldEditDialog(m_Message, item.SubItems[2].Text);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var field = dialog.m_FieldInfo;
                item.SubItems[0].Text = field.m_index.ToString();
                item.SubItems[1].Text = field.m_type_name + (field.m_is_array ? "[]" : "");
                item.SubItems[2].Text = field.m_name;
            }
        }
    }
}
