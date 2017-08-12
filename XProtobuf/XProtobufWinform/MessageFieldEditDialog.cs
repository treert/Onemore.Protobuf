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
    public partial class MessageFieldEditDialog : Form
    {
        private EditorMessage _message;
        public FieldInfo m_FieldInfo;

        bool _modify_mode = false;
        string _modify_name;
        int _modify_index;

        private Dictionary<string, FieldFormat.FieldType> _base_types;
        private Dictionary<string, FieldFormat.FieldType> _my_types;
        private List<string> _types = new List<string>();


        public MessageFieldEditDialog(EditorMessage message, string modify_name)
        {
            InitializeComponent();
            checkBox1.Checked = false;

            _base_types = EditorMessageManager.singleton.GetBaseTypes();
            _my_types = EditorMessageManager.singleton.GetDefineTypes();

            UpdateComboxTypeList();

            _message = message;
            _modify_mode = modify_name != null;

            if (_modify_mode)
            {
                m_FieldInfo = _message.m_message.m_fields[modify_name];
                _modify_name = modify_name;
                _modify_index = m_FieldInfo.m_index;
                textBoxName.Text = modify_name;
                textBoxIndex.Text = _modify_index.ToString();
                comboxTypeName.Text = m_FieldInfo.m_type_name;
            }
            else
            {
                m_FieldInfo = new FieldInfo();
                if (_message.m_message.m_fields.Count == 0)
                {
                    textBoxIndex.Text = "1";
                }
                else
                {
                    var index = 0;
                    foreach(var f in _message.m_message.m_fields.Values)
                    {
                        index = Math.Max(index, f.m_index);
                    }
                    textBoxIndex.Text = (index + 1).ToString();
                }
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void UpdateComboxTypeList()
        {
            comboxTypeName.Items.Clear();
            comboxTypeName.Items.AddRange(_base_types.Keys.ToArray());
            if (checkBox1.Checked == false)
            {
                comboxTypeName.Items.AddRange(_my_types.Keys.ToArray());
            }
        }
        
        private bool CheckIndexValid(int index)
        {
            foreach(var f in _message.m_message.m_fields.Values)
            {
                if(index == f.m_index)
                {
                    return false;
                }
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var index_str = textBoxIndex.Text.Trim();
            int index;
            if(Int32.TryParse(index_str, out index) == false)
            {
                MessageBox.Show("index is invalid Int32");
                return;
            }
            var name = textBoxName.Text.Trim();
            if(name.Length == 0)
            {
                MessageBox.Show("name is empty");
                return;
            }
            var name_type = comboxTypeName.Text;
            var type = GetFieldType(name_type);
            if(type == FieldFormat.FieldType.InValid)
            {
                MessageBox.Show("type \""+name_type+"\" is Invalid");
                return;
            }

            if (_modify_mode)
            {
                if (name != _modify_name && _message.m_message.m_fields.ContainsKey(name))
                {
                    MessageBox.Show("Name " + name + " has aready exist.");
                    return;
                }
                if (index != _modify_index && CheckIndexValid(index) == false)
                {
                    MessageBox.Show("Index " + index + " has aready exist.");
                    return;
                }
            }
            else
            {
                if (_message.m_message.m_fields.ContainsKey(name))
                {
                    MessageBox.Show("Name " + name + " has aready exist.");
                    return;
                }
                if (CheckIndexValid(index) == false)
                {
                    MessageBox.Show("Index " + index + " has aready exist.");
                    return;
                }
            }

            m_FieldInfo.m_index = index;
            m_FieldInfo.m_name = name;
            m_FieldInfo.m_type = type;
            m_FieldInfo.m_type_name = name_type;
            m_FieldInfo.m_is_array = radioRepeat.Checked;

            _message.m_message.m_fields[name] = m_FieldInfo;

            this.DialogResult = DialogResult.OK;
            Close();
        }

        FieldFormat.FieldType GetFieldType(string name)
        {
            if(_base_types.ContainsKey(name))
            {
                return _base_types[name];
            }
            if (_my_types.ContainsKey(name))
            {
                return _my_types[name];
            }
            return FieldFormat.FieldType.InValid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateComboxTypeList();
        }
    }
}
