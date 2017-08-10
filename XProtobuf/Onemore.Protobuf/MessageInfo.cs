using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onemore.Protobuf
{
    public class MessageManager
    {
        public string m_namespace;
        public Dictionary<string, MessageInfo> m_messages = new Dictionary<string, MessageInfo>();
        public Dictionary<string, PEnum> m_enums = new Dictionary<string, PEnum>();

        /// <summary>
        /// 1. fill m_message or m_enum for field if need, will throw Exception if does not find according type!!!
        /// 2. set m_tag and m_is_packed
        /// </summary>
        public void InitFieldsBeforeUse()
        {
            foreach(var message in m_messages)
            {
                foreach(var item in message.Value.m_fields)
                {
                    FieldInfo field = item.Value;
                    message.Value.m_index_to_fileds.Add(field.m_index, field);

                    if(field.m_type == FieldFormat.FieldType.Message)
                    {
                        field.m_message = m_messages[field.m_type_name];
                    }
                    else if(field.m_type == FieldFormat.FieldType.Enum)
                    {
                        field.m_enum = m_enums[field.m_type_name];
                    }
                    field.m_is_packed = field.m_is_array && FieldFormat.CanBePacked(field.m_type);
                    if(field.m_is_packed)
                    {
                        field.m_tag = WireFormat.MakeTag(field.m_index, WireFormat.WireType.LengthDelimited);
                    }
                    else
                    {
                        field.m_tag = WireFormat.MakeTag(field.m_index, FieldFormat.GetTagWireTypeByFieldType(field.m_type));
                    }
                }
            }
        }
    }

    public class MessageInfo
    {
        public string m_name;
        public Dictionary<string, FieldInfo> m_fields = new Dictionary<string, FieldInfo>();

        [NonSerialized]
        public Dictionary<int, FieldInfo> m_index_to_fileds = new Dictionary<int, FieldInfo>();
    }

    public class FieldInfo
    {
        public string m_name;// field name
        public FieldFormat.FieldType m_type = FieldFormat.FieldType.InValid;
        public int m_index = 0;// 1. 1~15  2. 16~2047  3. ~(2^29 - 1 | 536,870,911), can not use 19000~19999
        public string m_type_name;
        public bool m_is_array = false;// repeated

        [NonSerialized]
        public MessageInfo m_message;
        [NonSerialized]
        public PEnum m_enum;
        [NonSerialized]
        public uint m_tag;
        [NonSerialized]
        public bool m_is_packed;
    }

    public class PEnum
    {
        public string m_name;
        public Dictionary<string, int> m_items = new Dictionary<string, int>();
    }
}
