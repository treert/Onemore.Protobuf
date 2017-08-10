using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Onemore.Protobuf;
using Onemore.Protobuf.CodeGenerate;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 构造一个
            // Message A{ 
            //     int a = 1;
            //     B b = 2;
            //     repeated int c = 3[packed=true];
            // }
            // Message B{
            //     string bb = 1;
            // }

            MessageInfo ma = new MessageInfo();
            FieldInfo faa = new FieldInfo();
            FieldInfo fab = new FieldInfo();
            FieldInfo fac = new FieldInfo();
            ma.m_name = "A";
            ma.m_fields.Add("a", faa);
            ma.m_fields.Add("b", fab);
            ma.m_fields.Add("c", fac);

            faa.m_name = "a";
            faa.m_type = FieldFormat.FieldType.Int32;
            faa.m_type_name = "int32";
            faa.m_index = 1;

            fab.m_name = "b";
            fab.m_type = FieldFormat.FieldType.Message;
            fab.m_type_name = "B";
            fab.m_index = 2;

            fac.m_name = "c";
            fac.m_type = FieldFormat.FieldType.Int32;
            fac.m_type_name = "int32";
            fac.m_index = 3;
            fac.m_is_array = true;

            MessageInfo mb = new MessageInfo();
            FieldInfo fbbb = new FieldInfo();
            mb.m_name = "B";
            mb.m_fields.Add("bb", fbbb);

            fbbb.m_name = "bb";
            fbbb.m_type = FieldFormat.FieldType.String;
            fbbb.m_type_name = "string";
            fbbb.m_index = 1;

            MessageManager mananger = new MessageManager();
            mananger.m_messages.Add(ma.m_name, ma);
            mananger.m_messages.Add(mb.m_name, mb);
            mananger.InitFieldsBeforeUse();

            var a = MessageNode.Create(ma);
            a.GetFieldNode<Int32Node>("a").value = 1111;
            a.GetFieldNode<MessageNode>("b").GetFieldNode<StringNode>("bb").value = "bbbbbb";
            var ac = a.GetFieldNode<RepeatedNode>("c");
            ac.AddNewNode<Int32Node>().value = 31;
            ac.AddNewNode<Int32Node>().value = 32;
            ac.AddNewNode<Int32Node>().value = 33;

            using (var memory = new MemoryStream())
            {
                a.WriteTo(memory);
                var b = MessageNode.Create(ma);
                memory.Position = 0;
                b.ReadFrom(memory);
                Console.WriteLine(b.GetFieldNode<MessageNode>("b").GetFieldNode("bb").ConvertToObj());
                Console.WriteLine(b.GetFieldNode<RepeatedNode>("c").Count());
                Console.WriteLine(b.GetFieldNode<RepeatedNode>("c").GetArrayNode(2).ConvertToObj());
            }

            Console.WriteLine(GenProto.GenProto3(mananger));
            mananger.m_namespace = "X";
            Console.WriteLine(GenProto.GenProto2(mananger));
        }
    }
}
