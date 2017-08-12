using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onemore.Protobuf.CodeGenerate
{
    public static class GenProto
    {
        public static string GenProto3(MessageManager manager, string file = null)
        {
            _is_proto2 = false;
            return InnerGenProto(manager, file);
        }

        public static string GenProto2(MessageManager manager, string file = null)
        {
            _is_proto2 = true;
            return InnerGenProto(manager, file);
        }

        static string InnerGenProto(MessageManager manager, string file)
        {
            _buffer.Clear();
            GenMessageManager(manager);
            var content = _buffer.ToString();
            if (string.IsNullOrEmpty(file) == false)
            {
                File.WriteAllText(file, content, _utf8);
            }
            return content;
        }

        static Encoding _utf8 = new UTF8Encoding(false, true);// utf-8 without bom
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

        static void GenMessageManager(MessageManager manager)
        {
            if(_is_proto2 == false)
            {
                AppendLine(0, "syntax = \"proto3\";");
            }
            AppendLine(0, "// Auto generate by Onemore.Protobuf. Do not edit!");
            AppendLine();

            if(string.IsNullOrEmpty(manager.m_namespace) == false)
            {
                AppendLine(0, "package {0};", manager.m_namespace);
                AppendLine();
            }
            foreach(var penum in manager.m_enums.Values)
            {
                GenEnum(penum);
                AppendLine();
            }
            foreach(var message in manager.m_messages.Values)
            {
                GenMessage(message);
                AppendLine();
            }

        }

        static void GenEnum(PEnum penum)
        {
            AppendLine(0, "enum {0} {{", penum.m_name);
            foreach(var item in penum.m_items.OrderBy(item_ => item_.Value))
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
                if(_is_proto2 && field.m_is_packed)
                {
                    AppendLine(1, "repeated {0} {1} = {2} [packed=true];", field.m_type_name, field.m_name, field.m_index);
                }
                else
                {
                    AppendLine(1, "repeated {0} {1} = {2};", field.m_type_name, field.m_name, field.m_index);
                }
            }
            else
            {
                if(_is_proto2)
                {
                    AppendLine(1, "optional {0} {1} = {2};", field.m_type_name, field.m_name, field.m_index);
                }
                else
                {
                    AppendLine(1, "{0} {1} = {2};", field.m_type_name, field.m_name, field.m_index);
                }
            }
        }
    }
}
