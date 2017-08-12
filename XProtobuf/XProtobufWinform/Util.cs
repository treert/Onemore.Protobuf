using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
namespace XProtobufWinform
{
    class Util
    {
        public static void VariableNameValidation_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            TextBox textbox = (sender as TextBox);
            if (textbox.Text.Count() == 0)
            {
                if (!char.IsControl(c) && !char.IsLetter(c))
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!char.IsControl(c) && !char.IsLetterOrDigit(c) && c != '_')
                {
                    e.Handled = true;
                }
            }
        }

        public static void NumberValidation_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!char.IsControl(c) && !char.IsDigit(c))
            {
                e.Handled = true;
            }
        }

        private static MemoryStream _memory_for_clone = new MemoryStream();
        public static T Clone<T>(T obj)
        {
            _memory_for_clone.Position = 0;
            XSerialize.XBinarySerializer2.singleton.Serialize(_memory_for_clone, obj);
            _memory_for_clone.Position = 0;
            return XSerialize.XBinarySerializer2.singleton.Deserialize<T>(_memory_for_clone);
        }
    }
}
