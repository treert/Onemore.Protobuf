using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onemore.Protobuf
{
    public static class FieldFormat
    {
        public enum FieldType
        {
            // -1
            InValid = -1,
            // 0 Varint
            Int32, UInt32, Int64, UInt64,
            Bool, Enum,
            SInt32, SInt64,
            // 2 Length Delimited
            String, Bytes, Message,
            // 1 64-bit
            Fixed64, SFixed64, Double,
            // 5 32-bit
            Fixed32, SFixed32, Float,
        }

        private const uint _varint_mask = (1 << (int)(FieldType.Int32)) 
            | (1 << (int)(FieldType.UInt32))
            | (1 << (int)(FieldType.Int64))
            | (1 << (int)(FieldType.UInt64))
            | (1 << (int)(FieldType.SInt64))
            | (1 << (int)(FieldType.SInt32))
            | (1 << (int)(FieldType.Enum))
            | (1 << (int)(FieldType.Bool));

        private const uint _fixed64_mask = (1 << (int)(FieldType.Double))
            | (1 << (int)(FieldType.Fixed64))
            | (1 << (int)(FieldType.SFixed64));

        private const uint _fixed32_mask = (1 << (int)(FieldType.Float))
            | (1 << (int)(FieldType.Fixed32))
            | (1 << (int)(FieldType.SFixed32));

        private const uint _length_delimited_mask = (1 << (int)(FieldType.String))
            | (1 << (int)(FieldType.Bytes))
            | (1 << (int)(FieldType.Message));

        public static WireFormat.WireType GetTagWireTypeByFieldType(FieldType field_type)
        {
            int mask = 1<<(int)field_type;
            if((mask & _varint_mask) != 0)
            {
                return WireFormat.WireType.Varint;
            }
            else if ((mask & _fixed64_mask) != 0)
            {
                return WireFormat.WireType.Fixed64;
            }
            else if ((mask & _fixed32_mask) != 0)
            {
                return WireFormat.WireType.Fixed32;
            }
            else if ((mask & _length_delimited_mask) != 0)
            {
                return WireFormat.WireType.LengthDelimited;
            }
            throw new NotSupportedException("field type has not init, field_type=" + field_type);
        }

        public static bool CanBePacked(FieldType field_type)
        {
            int mask = 1 << (int)field_type;
            if ((mask & _length_delimited_mask) != 0)
            {
                return false;
            }
            return field_type != FieldType.InValid;
        }
    }
}
