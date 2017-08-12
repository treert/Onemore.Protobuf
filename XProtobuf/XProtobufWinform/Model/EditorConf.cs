using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XSerialize;

namespace XProtobufWinform.Model
{
    class EditorConf
    {
        public static EditorConf singleton = new EditorConf();

        [NonSerialized]
        public string m_conf_file;

        public string m_ProtoBinFilePath;
        public string m_ProtoFileOutPath;
        public string m_CSharpFileOutPath;

        public static void ReadFromFile(string file)
        {
            Action read_func = () =>
            {
                var content = File.ReadAllText(file);
                // sorry for this shit code
                singleton = XXmlSerializer.singleton.DeserializeFromString<EditorConf>(content);
                singleton.m_conf_file = file;
            };

            file = file.Replace('\\', '/');
            if (File.Exists(file))
            {
                read_func();
            }
            else
            {
                // new 
                singleton.m_conf_file = file;
                var dir = Path.GetDirectoryName(file).Replace('\\','/');
                singleton.m_ProtoBinFilePath = dir + "/XProto.proto.bin";
                singleton.m_ProtoFileOutPath = dir + "/XProto.proto";
                singleton.m_CSharpFileOutPath = dir + "/XProto.cs";

                singleton.SaveToFile();
            }
        }

        public void SaveToFile()
        {
            if(string.IsNullOrEmpty(m_conf_file) == false)
            {
                var content = XXmlSerializer.singleton.SerializeToString<EditorConf>(this);
                File.WriteAllText(m_conf_file, content);
            }
        }
    }
}
