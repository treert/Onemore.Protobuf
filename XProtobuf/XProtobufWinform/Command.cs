using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using XProtobufWinform.Model;
using Onemore.Protobuf.CodeGenerate;
namespace XProtobufWinform
{
    static class Command
    {
        public static void LoadConf(string file)
        {
            if(System.IO.Directory.Exists(file))
            {
                file = System.IO.Path.Combine(file, "XProto.proto.conf").Replace('\\','/');
            }
            if(file.EndsWith(".conf") == false)
            {
                MessageBox.Show("file type need be *.proto.conf \n current is "+file);
                return;
            }
            if(file == EditorConf.singleton.m_conf_file)
            {
                MessageBox.Show("Is same with current conf.");
                return;
            }
            else if(string.IsNullOrEmpty(EditorConf.singleton.m_conf_file) == false)
            {
                if(MessageBox.Show("Save current Data", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveAllData();
                }
            }

            EditorConf.ReadFromFile(file);

            EditorMessageManager.singleton.ReadFromFile(EditorConf.singleton.m_ProtoBinFilePath);
        }

        public static void SaveAllData()
        {
            if(string.IsNullOrEmpty(EditorConf.singleton.m_conf_file) == false)
            {
	            EditorConf.singleton.SaveToFile();
	            EditorMessageManager.singleton.SaveToFile(EditorConf.singleton.m_ProtoBinFilePath);
            }
        }

        public static bool SaveMessageData()
        {
            if (string.IsNullOrEmpty(EditorConf.singleton.m_ProtoBinFilePath) == false)
            {
                EditorMessageManager.singleton.SaveToFile(EditorConf.singleton.m_ProtoBinFilePath);
                return true;
            }
            else
            {
                MessageBox.Show("Goto setting tab, drag conf file(dir) to the tab, or set path");
                return false;
            }
        }

        public static void SaveConfData()
        {
            if (string.IsNullOrEmpty(EditorConf.singleton.m_conf_file) == false)
            {
                EditorConf.singleton.SaveToFile();
            }
        }

        public static void GenCode()
        {
            if(SaveMessageData())
            {
                var mgr = EditorMessageManager.singleton.GetMessageManager();
                if (string.IsNullOrEmpty(EditorConf.singleton.m_ProtoFileOutPath) == false)
                {
                    GenProto.GenProto2(mgr, EditorConf.singleton.m_ProtoFileOutPath);
                }
                if(string.IsNullOrEmpty(EditorConf.singleton.m_CSharpFileOutPath) == false)
                {
                    GenCSharp.GenCode(mgr, EditorConf.singleton.m_CSharpFileOutPath);
                }
                MessageBox.Show("Generate Code OK");
            }
        }
    }
}
