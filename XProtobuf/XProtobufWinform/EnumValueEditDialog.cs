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
    public partial class EnumValueEditDialog : Form
    {
        public string m_EnumName;
        public string m_EnumValue;
        EditorMessage _message;
        bool _modify_mode = false;
        string _modify_name;
        int _modify_index;

        public EnumValueEditDialog(EditorMessage message, string modify_name)
        {
            InitializeComponent();

            _message = message;
            _modify_mode = modify_name != null;
            
            if(_modify_mode)
            {
                _modify_name = modify_name;
                _modify_index = _message.m_enum.m_items[modify_name];
                textBoxEnumValueName.Text = modify_name;
                textBoxEnumValue.Text = _modify_index.ToString();
            }
            else
            {
                if(_message.m_enum.m_items.Count == 0)
                {
                    textBoxEnumValue.Text = "0";
                }
                else
                {
                    textBoxEnumValue.Text = (_message.m_enum.m_items.Values.Max() + 1).ToString();
                }
                
            }

            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check then edit
            string name = textBoxEnumValueName.Text.Trim();
            if(name.Length == 0)
            {
                MessageBox.Show("Name can not be empty.");
                return;
            }
            string value_str = textBoxEnumValue.Text.Trim();
            int index = 0;
            if (Int32.TryParse(value_str, out index) == false)
            {
                MessageBox.Show("Enum value shoude be int32.");
                return;
            }
            if(_modify_mode)
            {
                if (name != _modify_name && _message.m_enum.m_items.ContainsKey(name))
                {
                    MessageBox.Show("Name [" + name + "] has aready exist.");
                    return;
                }
                if(index != _modify_index && _message.m_enum.m_items.ContainsValue(index))
                {
                    MessageBox.Show("Value [" + index + "] has aready exist.");
                    return;
                }
                if(name != _modify_name)
                {
                    _message.m_enum.m_items.Remove(_modify_name);
                    _message.m_enum.m_items.Add(name, index);
                }
                else
                {
                    _message.m_enum.m_items[name] = index;
                }
            }
            else
            {
                if (_message.m_enum.m_items.ContainsKey(name))
                {
                    MessageBox.Show("Name [" + name + "] has aready exist.");
                    return;
                }
                if (_message.m_enum.m_items.ContainsValue(index))
                {
                    MessageBox.Show("Value [" + index + "] has aready exist.");
                    return;
                }
                _message.m_enum.m_items.Add(name, index);
            }

            m_EnumName = name;
            m_EnumValue = index.ToString();

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxEnumValueName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.VariableNameValidation_KeyPress(sender, e);
        }

        private void textBoxEnumValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Util.NumberValidation_KeyPress(sender, e);
        }
    }
}
