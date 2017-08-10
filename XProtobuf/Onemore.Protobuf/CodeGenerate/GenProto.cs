using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onemore.Protobuf.CodeGenerate
{
    public static class GenProto
    {
        public static string GenProto3(MessageManager mananger)
        {
            _buffer.Clear();
            _is_proto2 = false;
            GenMessageManager(mananger);
            return _buffer.ToString();
        }

        public static string GenProto2(MessageManager mananger)
        {
            _buffer.Clear();
            _is_proto2 = true;
            GenMessageManager(mananger);
            return _buffer.ToString();
        }

        static StringBuilder _buffer = new StringBuilder();
        private static void AppendFormat(string format, params object[] args)
        {
            if (args.Length > 0)
            {
                _buffer.AppendFormat(format, args);
            }
            else
            {
                _buffer.Append(format);
            }

        }
        private static void Append(int indent, string format, params object[] args)
        {
            _buffer.Append(' ', indent * 4);
            if (args.Length > 0) // WTF
            {
                _buffer.AppendFormat(format, args);
            }
            else
            {
                _buffer.Append(format);
            }
        }
        private static void AppendLine(int indent, string format, params object[] args)
        {
            Append(indent, format, args);
            AppendLine();
        }
        private static void AppendLine()
        {
            _buffer.AppendLine();
        }

        static bool _is_proto2 = false;

        static void GenMessageManager(MessageManager mananger)
        {
            if(_is_proto2 == false)
            {
                AppendLine(0, "syntax = \"proto3\";");
                AppendLine();
            }
            if(string.IsNullOrEmpty(mananger.m_namespace) == false)
            {
                AppendLine(0, "package {0};", mananger.m_namespace);
                AppendLine();
            }
            foreach(var penum in mananger.m_enums.Values)
            {
                GenEnum(penum);
                AppendLine();
            }
            foreach(var message in mananger.m_messages.Values)
            {
                GenMessage(message);
                AppendLine();
            }

        }

        static void GenEnum(PEnum penum)
        {
            AppendLine(0, "enum {0} {{", penum.m_name);
            foreach(var item in penum.m_items)
            {
                AppendLine(1, "{0} = {1};", item.Key, item.Value);
            }
            AppendLine(0, "}");
        }

        static void GenMessage(MessageInfo message)
        {
            AppendLine(0, "message {0} {{", message.m_name);
            foreach(var field in message.m_fields.Values.OrderBy(item => item.m_index))
            {
                GenField(field);
            }
            AppendLine(0, "}");
        }

        static void GenField(FieldInfo field)
        {
            if(field.m_is_array)
            {
                if(_is_proto2 && FieldFormat.CanBePacked(field.m_type))
                {
                    AppendLine(1, "repeated {0} {1} = {2} [packed=true];"
                        , field.m_type_name, field.m_name, field.m_index);
                }
                else
                {
                    AppendLine(1, "repeated {0} {1} = {2};"
                        , field.m_type_name, field.m_name, field.m_index);
                }
            }
            else
            {
                AppendLine(1, "{0} {1} = {2};"
                        , field.m_type_name, field.m_name, field.m_index);
            }
        }
    }
}
