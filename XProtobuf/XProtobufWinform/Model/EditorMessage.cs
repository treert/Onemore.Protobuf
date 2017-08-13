using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Onemore.Protobuf;
using XSerialize;
namespace XProtobufWinform.Model
{
    class EditorMessageManager
    {
        public static EditorMessageManager singleton = new EditorMessageManager();

        MessageManager _message_mgr = new MessageManager();
        XBinarySerializer _serializer = new XBinarySerializer(typeof(MessageManager));

        public Dictionary<string, EditorMessage> m_editor_messages = new Dictionary<string, EditorMessage>();

        public EditorMessage GetByName(string name)
        {
            EditorMessage ret = null;
            m_editor_messages.TryGetValue(name, out ret);
            return ret;
        }

        public bool CheckNameAreadyExist(string name)
        {
            if(GetByName(name) != null)
            {
                return false;
            }
            if(GetBaseTypes().ContainsKey(name))
            {
                return false;
            }

            return true;
        }

        public Dictionary<string, FieldFormat.FieldType> GetBaseTypes()
        {
            Dictionary<string, FieldFormat.FieldType> dic = new Dictionary<string, FieldFormat.FieldType>();
            foreach (FieldFormat.FieldType f in Enum.GetValues(typeof(FieldFormat.FieldType)))
            {
                if (f != FieldFormat.FieldType.Message && f != FieldFormat.FieldType.Enum && f != FieldFormat.FieldType.InValid)
                {
                    dic.Add(FieldFormat.GetFieldTypeName(f), f);
                }
            }
            return dic;
        }

        public Dictionary<string, FieldFormat.FieldType> GetDefineTypes()
        {
            Dictionary<string, FieldFormat.FieldType> dic = new Dictionary<string, FieldFormat.FieldType>();
            foreach(var f in _message_mgr.m_enums.Values)
            {
                dic.Add(f.m_name, FieldFormat.FieldType.Enum);
            }
            foreach (var f in _message_mgr.m_messages.Values)
            {
                dic.Add(f.m_name, FieldFormat.FieldType.Message);
            }
            return dic;
        }

        public void AddMessage(EditorMessage message)
        {
            m_editor_messages.Add(message.Name, message);
            if (message.m_message != null)
            {
                _message_mgr.m_messages.Add(message.Name, message.m_message);
            }
            if (message.m_enum != null)
            {
                _message_mgr.m_enums.Add(message.Name, message.m_enum);
            }
        }

        public void UpdateMessage(EditorMessage new_message, EditorMessage old_message)
        {
            old_message.CopyFrom(new_message);
            if(old_message.m_message != null)
            {
                _message_mgr.m_messages[old_message.m_message.m_name] = old_message.m_message;
            }
            if(old_message.m_enum != null)
            {
                _message_mgr.m_enums[old_message.m_enum.m_name] = old_message.m_enum;
            }
        }

        public void DeleteMessage(EditorMessage message)
        {
            if(CheckMessageHasBeDepends(message))
            {
                // error;
                return;
            }
            m_editor_messages.Remove(message.Name);
            if (message.m_message != null)
            {
                _message_mgr.m_messages.Remove(message.Name);
            }
            if (message.m_enum != null)
            {
                _message_mgr.m_enums.Remove(message.Name);
            }
        }

        public MessageManager GetMessageManager()
        {
            return _message_mgr;
        }

        public string NameSpace
        {
            get
            {
                return _message_mgr.m_namespace;
            }
            set
            {
                _message_mgr.m_namespace = value;
            }
        }

        public void ReadFromFile(string file)
        {
            if(File.Exists(file))
            {
                using (var stream = File.OpenRead(file))
                {
                    _message_mgr = _serializer.Deserialize<MessageManager>(stream);

                    BuildEditorMessage();
                }
            }
            else
            {
                // just save current data
                SaveToFile(file);
            }
        }

        public void SaveToFile(string file)
        {
            using (var stream = File.OpenWrite(file))
            {
                _serializer.Serialize(stream, _message_mgr);
            }
        }

        void BuildEditorMessage()
        {
            foreach(var f in _message_mgr.m_enums.Values)
            {
                var m = EditorMessage.NewEnum(f);
                m_editor_messages.Add(f.m_name, m);
            }

            foreach(var f in _message_mgr.m_messages.Values)
            {
                var m = EditorMessage.NewMessage(f);
                m_editor_messages.Add(f.m_name, m);
            }
        }

        public bool CheckMessageHasBeDepends(EditorMessage message)
        {
            foreach(var m in _message_mgr.m_messages.Values)
            {
                foreach(var f in m.m_fields.Values)
                {
                    if(f.m_type == FieldFormat.FieldType.Enum || f.m_type == FieldFormat.FieldType.Message)
                    {
                        if(f.m_type_name == message.Name)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public SortedSet<string> GetMessageBeDepends(EditorMessage message)
        {
            SortedSet<string> _used_by_dic = new SortedSet<string>();
            foreach (var m in _message_mgr.m_messages.Values)
            {
                foreach (var f in m.m_fields.Values)
                {
                    if (f.m_type == FieldFormat.FieldType.Enum || f.m_type == FieldFormat.FieldType.Message)
                    {
                        if (f.m_type_name == message.Name)
                        {
                            _used_by_dic.Add(m.m_name);
                        }
                    }
                }
            }
            return _used_by_dic;
        }
    }

    public class EditorMessage
    {
        
        public MessageInfo m_message;
        public PEnum m_enum;

        public static EditorMessage NewEnum(PEnum penum = null)
        {
            var obj = new EditorMessage();
            if(penum != null)
            {
                obj.m_enum = penum;
            }
            else
            {
                obj.m_enum = new PEnum();
            }
            return obj;
        }
        public static EditorMessage NewMessage(MessageInfo message = null)
        {
            var obj = new EditorMessage();
            if(message == null)
            {
                obj.m_message = new MessageInfo();
            }
            else
            {
                obj.m_message = message;
            }
            return obj;
        }

        public EditorMessage Clone()
        {
            // shit
            EditorMessage obj = new EditorMessage();
            if(m_message != null)
            {
                obj.m_message = Util.Clone(m_message);
            }
            if(m_enum != null)
            {
                obj.m_enum = Util.Clone(m_enum);
            }
            return obj;
        }

        public void CopyFrom(EditorMessage message)
        {
            if(message.m_message != null)
            {
                m_message = message.m_message;
            }
            if(message.m_enum != null)
            {
                m_enum = message.m_enum;
            }
        }

        public string Name
        {
            get
            {
                if(m_message != null)
                {
                    return m_message.m_name;
                }
                if(m_enum != null)
                {
                    return m_enum.m_name;
                }
                return "null";
            }
        }

        public string NameWithType
        {
            get
            {
                if (m_message != null)
                {
                    return "message " + m_message.m_name;
                }
                if (m_enum != null)
                {
                    return "enum " + m_enum.m_name;
                }
                return "null";
            }
        }

        public string ToDescriptionString()
        {
            if(m_enum != null)
            {
                return ToDescriptionStringForEnum();
            }
            if(m_message != null)
            {
                return ToDescriptionStringForMessage();
            }
            return "error";
        }

        private string ToDescriptionStringForEnum()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(NameWithType);
            sb.AppendLine();
            foreach(var item in m_enum.m_items.OrderBy(t_ => t_.Value))
            {
                sb.AppendFormat("{0} = {1}", item.Key, item.Value);
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine("=================================");
            sb.AppendLine();

            return sb.ToString() + ToStringForUsedBy();
        }

        private string ToDescriptionStringForMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(NameWithType);
            sb.AppendLine();
            foreach(var field in m_message.m_fields.Values.OrderBy(t_ => t_.m_index))
            {
                sb.AppendFormat("{0}. {1}{3} {2}", field.m_index, field.m_type_name, field.m_name, (field.m_is_array ? "[]" : ""));
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine("=================================");
            sb.AppendLine();

            return sb.ToString() + ToStringForUsedBy();
        }

        private string ToStringForUsedBy()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Used By:");
            var dic = EditorMessageManager.singleton.GetMessageBeDepends(this);
            foreach(var name in dic)
            {
                sb.AppendLine("message "+name);
            }
            return sb.ToString();
        }
    }
}
