using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onemore.XProtobuf
{
    public class Node
    {
        public virtual void WriteTo(OutputStream output)
        {
            throw new NotImplementedException();
        }

        public virtual void ReadFrom(InputStream input)
        {
            throw new NotImplementedException();
        }

        public virtual void ComputeSize()
        {
            throw new NotImplementedException();
        }

        public virtual object ConvertToObj()
        {
            throw new NotImplementedException();
        }

        protected int _size = 0;
        public int GetComputeSize()
        {
            return _size;
        }

        public static Node CreateNodeByFieldInfo(FieldInfo field_info, bool check_array = true)
        {
            if (check_array && field_info.m_is_array)
            {
                return new RepeatedNode(field_info);
            }
            switch (field_info.m_type)
            {
                case FieldFormat.FieldType.InValid:
                    throw new NotSupportedException();
                case FieldFormat.FieldType.Int32:
                    return new Int32Node();
                case FieldFormat.FieldType.UInt32:
                    return new UInt32Node();
                case FieldFormat.FieldType.Int64:
                    return new Int64Node();
                case FieldFormat.FieldType.UInt64:
                    return new UInt64Node();
                case FieldFormat.FieldType.Bool:
                    return new BoolNode();
                case FieldFormat.FieldType.Enum:
                    return new EnumNode();
                case FieldFormat.FieldType.SInt32:
                    return new SInt32Node();
                case FieldFormat.FieldType.SInt64:
                    return new SInt64Node();
                case FieldFormat.FieldType.String:
                    return new StringNode();
                case FieldFormat.FieldType.Bytes:
                    return new BytesNode();
                case FieldFormat.FieldType.Message:
                    return new MessageNode(field_info.m_message);
                case FieldFormat.FieldType.Fixed64:
                    return new Fixed64Node();
                case FieldFormat.FieldType.SFixed64:
                    return new SFixed64Node();
                case FieldFormat.FieldType.Double:
                    return new DoubleNode();
                case FieldFormat.FieldType.Fixed32:
                    return new Fixed32Node();
                case FieldFormat.FieldType.SFixed32:
                    return new SFixed32Node();
                case FieldFormat.FieldType.Float:
                    return new FloatNode();
                default:
                    throw new Exception("assert");
            }
        }

    }

    public class MessageNode :Node
    {
        Dictionary<string, Node> _fields = new Dictionary<string, Node>();
        MessageInfo _message;

        public MessageNode(MessageInfo message)
        {
            _message = message;
        }

        public bool Has(string name)
        {
            return _fields.ContainsKey(name);
        }

        public bool HasDefine(string name)
        {
            return _message.m_fields.ContainsKey(name);
        }

        /// <summary>
        /// if HasDefine(name) is false, will throw Exception
        /// if Has(name) is false, will create new Node
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Node GetFieldNode(string name)
        {
            if(Has(name))
            {
                return _fields[name];
            }
            FieldInfo field_info = null;
            if(_message.m_fields.TryGetValue(name, out field_info))
            {
                return Node.CreateNodeByFieldInfo(field_info);
            }
            else
            {
                throw new Exception(string.Format("{0} has not define a field named {1}", _message.m_name, name));
            }
        }

        /// <summary>
        /// if type is not right, will cause Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetFieldNode<T>(string name)where T:Node
        {
            return (T)GetFieldNode(name);
        }

        public T Get<T>(string name)
        {
            return (T)GetFieldNode(name).ConvertToObj();
        }

        public override void ReadFrom(InputStream input)
        {
            
        }

        public override void WriteTo(OutputStream output)
        {
            output.WriteLength(_inner_size);
            foreach (var item in _fields)
            {
                var field = item.Value;
                var name = item.Key;
                var field_info = _message.m_fields[name];
                output.WriteTag(field_info.m_tag);
                field.WriteTo(output);
            }
        }

        private int _inner_size = 0;
        public override void ComputeSize()
        {
            _inner_size = 0;
            foreach (var item in _fields)
            {
                var field = item.Value;
                var name = item.Key;
                var field_info = _message.m_fields[name];
                field.ComputeSize();
                if (field_info.m_is_array)
                {
                    _inner_size += field.GetComputeSize();// ReatedNode write tag it's self
                }
                else
                {
                    _inner_size += OutputStream.ComputeTagSize(field_info.m_tag);
                    _inner_size += field.GetComputeSize();
                }
            }
            _size = _inner_size + OutputStream.ComputeLengthSize(_inner_size);
        }
    }

    public class RepeatedNode: Node
    {
        List<Node> _items = new List<Node>();
        FieldInfo _field_info;

        public RepeatedNode(FieldInfo field_info)
        {
            _field_info = field_info;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public Node AddNewNode()
        {
            var node = Node.CreateNodeByFieldInfo(_field_info, false);
            _items.Add(node);
            return node;
        }
        
        public T AddNewNode<T>() where T:Node
        {
            return (T)AddNewNode();
        }

        public Node GetArrayNode(int idx)
        {
            return _items[idx];
        }

        public T GetArrayNode<T>(int idx) where T:Node
        {
            return (T)GetArrayNode(idx);
        }

        public int Count()
        {
            return _items.Count;
        }

        public T Get<T>(int idx)
        {
            return (T)GetArrayNode(idx).ConvertToObj();
        }

        private int _inner_size = 0;
        public override void ComputeSize()
        {
            int tag_size = OutputStream.ComputeTagSize(_field_info.m_tag);
            if (_field_info.m_is_packed)
            {
                _inner_size = 0;
                foreach (var node in _items)
                {
                    // assert((node is RepeatedNode) == false)
                    node.ComputeSize();
                    _inner_size += node.GetComputeSize();
                }
                _size = tag_size + _inner_size + OutputStream.ComputeLengthSize(_inner_size);
            }
            else
            {
                foreach (var node in _items)
                {
                    // assert((node is RepeatedNode) == false)
                    node.ComputeSize();
                    _size += tag_size;
                    _size += node.GetComputeSize();
                }
            }
        }

        public override void ReadFrom(InputStream input)
        {
            base.ReadFrom(input);
        }

        public override void WriteTo(OutputStream output)
        {
            base.WriteTo(output);
        }
    }

    public class ValueNode<T> : Node
    {
        public T value;
        public override object ConvertToObj()
        {
            return value;
        }
    }

    public class Int32Node : ValueNode<int>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteInt32(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadInt32();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeInt32Size(value);
        }
    }

    public class UInt32Node : ValueNode<uint>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteUInt32(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadUInt32();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeUInt32Size(value);
        }
    }

    public class Int64Node : ValueNode<long>
    {


        public override void WriteTo(OutputStream output)
        {
            output.WriteInt64(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadInt64();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeInt64Size(value);
        }
    }

    public class UInt64Node : ValueNode<ulong>
    {


        public override void WriteTo(OutputStream output)
        {
            output.WriteUInt64(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadUInt64();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeUInt64Size(value);
        }
    }

    public class SInt32Node : ValueNode<int>
    {


        public override void WriteTo(OutputStream output)
        {
            output.WriteSInt32(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadSInt32();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeSInt32Size(value);
        }
    }

    public class SInt64Node : ValueNode<long>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteSInt64(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadSInt64();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeSInt64Size(value);
        }
    }

    public class BoolNode : ValueNode<bool>
    {


        public override void WriteTo(OutputStream output)
        {
            output.WriteBool(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadBool();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeBoolSize(value);
        }
    }

    public class EnumNode : ValueNode<int>
    {

        public override void WriteTo(OutputStream output)
        {
            output.WriteEnum(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadEnum();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeEnumSize(value);
        }
    }

    public class Fixed64Node : ValueNode<ulong>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteFixed64(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadFixed64();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeFixed64Size(value);
        }
    }

    public class SFixed64Node : ValueNode<long>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteSFixed64(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadSFixed64();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeSFixed64Size(value);
        }
    }

    public class DoubleNode : ValueNode<double>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteDouble(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadDouble();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeDoubleSize(value);
        }
    }

    public class StringNode : ValueNode<string>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteString(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadString();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeStringSize(value);
        }
    }

    public class BytesNode : ValueNode<byte[]>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteBytes(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadBytes();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeBytesSize(value);
        }
    }

    public class Fixed32Node : ValueNode<uint>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteFixed32(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadFixed32();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeFixed32Size(value);
        }
    }

    public class SFixed32Node : ValueNode<int>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteSFixed32(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadSFixed32();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeSFixed32Size(value);
        }
    }

    public class FloatNode : ValueNode<float>
    {



        public override void WriteTo(OutputStream output)
        {
            output.WriteFloat(value);
        }

        public override void ReadFrom(InputStream input)
        {
            value = input.ReadFloat();
        }

        public override void ComputeSize()
        {
            _size = OutputStream.ComputeFloatSize(value);
        }
    }
}
