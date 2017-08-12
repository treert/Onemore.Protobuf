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
    public partial class EnumEditDialog : Form
    {
        public EditorMessage m_Message;
        public bool m_IsAddNew = true;

        public EnumEditDialog(EditorMessage message)
        {
            InitializeComponent();
            m_IsAddNew = message == null;
            if(m_IsAddNew)
            {
                m_Message = EditorMessage.NewEnum();
            }
            else
            {
                txtBoxEnumName.Text = message.Name;
                txtBoxEnumName.Enabled = false;
                foreach(var f in message.m_enum.m_items.OrderBy(t_ => t_.Value))
                {
                    ListViewItem item = new ListViewItem(f.Key);
                    item.SubItems.Add(f.Value.ToString());
                    listViewEnum.Items.Add(item);
                }
                m_Message = message;
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtBoxEnumName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.VariableNameValidation_KeyPress(sender, e);
        }

        private void listViewEnum_DoubleClick(object sender, EventArgs e)
        {
            // modify Enum value

            if (listViewEnum.SelectedItems.Count == 0) return;

            var item = listViewEnum.SelectedItems[0];
            var dialog = new EnumValueEditDialog(m_Message, item.SubItems[0].Text);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                item.SubItems[0].Text = dialog.m_EnumName;
                item.SubItems[1].Text = dialog.m_EnumValue;
            }
        }

        private void listViewEnum_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtBoxEnumName.Text.Trim();
            if(name.Length == 0)
            {
                MessageBox.Show("Name can not be empty.");
                return;
            }
            if(m_IsAddNew)
            {
                if(EditorMessageManager.singleton.GetByName(name) != null)
                {
                    MessageBox.Show("Name ["+name+"] aready exist.");
                    return;
                }
                m_Message.m_enum.m_name = name;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new EnumValueEditDialog(m_Message, null);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                var item = new ListViewItem(dialog.m_EnumName);
                item.SubItems.Add(dialog.m_EnumValue);
                listViewEnum.Items.Add(item);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewEnum.SelectedItems)
            {
                m_Message.m_enum.m_items.Remove(item.Text);
                item.Remove();
            }
        }
    }
}
