﻿using System;

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

        public static string GetFieldTypeName(FieldType field_type)
        {
            switch (field_type)
            {
                case FieldType.InValid:
                    throw new NotSupportedException();
                case FieldType.Int32:
                    return "int32";
                case FieldType.UInt32:
                    return "uint32";
                case FieldType.Int64:
                    return "int64";
                case FieldType.UInt64:
                    return "uint64";
                case FieldType.Bool:
                    return "bool";
                case FieldType.Enum:
                    throw new NotSupportedException();
                case FieldType.SInt32:
                    return "sint32";
                case FieldType.SInt64:
                    return "sint64";
                case FieldType.String:
                    return "string";
                case FieldType.Bytes:
                    return "bytes";
                case FieldType.Message:
                    throw new NotSupportedException();
                case FieldType.Fixed64:
                    return "fixed64";
                case FieldType.SFixed64:
                    return "sfixed64";
                case FieldType.Double:
                    return "double";
                case FieldType.Fixed32:
                    return "fixed32";
                case FieldType.SFixed32:
                    return "sfixed32";
                case FieldType.Float:
                    return "float";
                default:
                    throw new Exception();
            }
        }

        public static FieldType ConvertNameToFieldType(string name)
        {
            switch(name)
            {
                default:
                    return FieldType.InValid;
                case "int32":
                    return FieldType.Int32;
                case "uint32":
                    return FieldType.UInt32;
                case "int64":
                    return FieldType.Int64;
                case "uint64":
                    return FieldType.UInt64;
                case "bool":
                    return FieldType.Bool;
                case "sint32":
                    return FieldType.SInt32;
                case "sint64":
                    return FieldType.SInt64;
                case "string":
                    return FieldType.String;
                case "bytes":
                    return FieldType.Bytes;
                case "fixed64":
                    return FieldType.Fixed64;
                case "sfixed64":
                    return FieldType.SFixed64;
                case "double":
                    return FieldType.Double;
                case "fixed32":
                    return FieldType.Fixed32;
                case "sfixed32":
                    return FieldType.SFixed32;
                case "float":
                    return FieldType.Float;
            }
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
