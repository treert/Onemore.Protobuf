using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WireType = Onemore.Protobuf.WireFormat.WireType;
namespace Onemore.Protobuf
{
    public class Node
    {
        internal virtual void InternalWriteTo(OutputStream output)
        {
            throw new NotImplementedException();
        }

        internal virtual void InternalReadFrom(InputStream input)
        {
            throw new NotImplementedException();
        }

        internal virtual void ComputeSize()
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

        internal static Node CreateNodeByFieldInfo(FieldInfo field_info, bool check_array = true)
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
                var node = Node.CreateNodeByFieldInfo(field_info);
                _fields.Add(name, node);
                return node;
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

        public void WriteTo(OutputStream output)
        {
            if(_size == 0)
            {
                ComputeSize();
            }
            InternalWriteTo(output);
            output.Flush();
        }

        public int CalculateSize()
        {
            ComputeSize();
            return _size;
        }

        public void ReadFrom(InputStream input)
        {
            InternalReadFrom(input);
        }

        public static MessageNode Create(MessageInfo message)
        {
            var node = new MessageNode(message);
            return node;
        }

        internal override void InternalReadFrom(InputStream input)
        {
            int size = input.ReadLength();
            var end_pos = input.Position + size;
            while(input.Position < end_pos)
            {
                uint tag = input.ReadTag();
                WireFormat.WireType wire_type = WireFormat.GetTagWireType(tag);
                int index = WireFormat.GetTagFieldNumber(tag);
                FieldInfo field_info;
                if(_message.m_index_to_fileds.TryGetValue(index, out field_info))
                {

                    Node node;
                    if(_fields.TryGetValue(field_info.m_name, out node) == false)
                    {
                        node = Node.CreateNodeByFieldInfo(field_info);
                        _fields.Add(field_info.m_name, node);
                    }
                    if(field_info.m_is_array)
                    {
                        (node as RepeatedNode).ReadFromForMessageNode(input, wire_type);
                    }
                    else
                    {
                        node.InternalReadFrom(input);
                    }
                }
                else
                {
                    input.SkipField(wire_type);
                }
            }
            if (input.Position != end_pos)
            {
                throw new Exception();
            }
        }

        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteLength(_inner_size);
            foreach (var item in _fields)
            {
                var field = item.Value;
                var name = item.Key;
                var field_info = _message.m_fields[name];
                if(field_info.m_is_array == false)
                {
                    output.WriteTag(field_info.m_tag);
                }
                field.InternalWriteTo(output);
            }
        }

        private int _inner_size = 0;
        internal override void ComputeSize()
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
        internal override void ComputeSize()
        {
            if(_items.Count == 0)
            {
                _size = 0;
                return;
            }
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

        internal void ReadFromForMessageNode(InputStream input, WireFormat.WireType wire_type)
        {
            if(wire_type == WireFormat.WireType.LengthDelimited && _field_info.m_is_packed)
            {
                int len = input.ReadLength();
                var end_pos = input.Position + len;
                while(input.Position < end_pos)
                {
                    Node node = AddNewNode();
                    node.InternalReadFrom(input);
                }
                if(input.Position != end_pos)
                {
                    throw new Exception();
                }
            }
            else
            {
                Node node = AddNewNode();
                node.InternalReadFrom(input);
            }
        }

        internal override void InternalReadFrom(InputStream input)
        {
            base.InternalReadFrom(input);
        }

        internal override void InternalWriteTo(OutputStream output)
        {
            if(_items.Count == 0)
            {
                return;
            }
            if(_field_info.m_is_packed)
            {
                output.WriteTag(_field_info.m_tag);
                output.WriteLength(_inner_size);
                foreach(var node in _items)
                {
                    node.InternalWriteTo(output);
                }
            }
            else
            {
                foreach(var node in _items)
                {
                    output.WriteTag(_field_info.m_tag);
                    node.InternalWriteTo(output);
                }
            }
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



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteInt32(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadInt32();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeInt32Size(value);
        }
    }

    public class UInt32Node : ValueNode<uint>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteUInt32(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadUInt32();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeUInt32Size(value);
        }
    }

    public class Int64Node : ValueNode<long>
    {


        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteInt64(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadInt64();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeInt64Size(value);
        }
    }

    public class UInt64Node : ValueNode<ulong>
    {


        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteUInt64(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadUInt64();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeUInt64Size(value);
        }
    }

    public class SInt32Node : ValueNode<int>
    {


        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteSInt32(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadSInt32();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeSInt32Size(value);
        }
    }

    public class SInt64Node : ValueNode<long>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteSInt64(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadSInt64();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeSInt64Size(value);
        }
    }

    public class BoolNode : ValueNode<bool>
    {


        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteBool(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadBool();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeBoolSize(value);
        }
    }

    public class EnumNode : ValueNode<int>
    {

        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteEnum(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadEnum();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeEnumSize(value);
        }
    }

    public class Fixed64Node : ValueNode<ulong>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteFixed64(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadFixed64();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeFixed64Size(value);
        }
    }

    public class SFixed64Node : ValueNode<long>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteSFixed64(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadSFixed64();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeSFixed64Size(value);
        }
    }

    public class DoubleNode : ValueNode<double>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteDouble(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadDouble();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeDoubleSize(value);
        }
    }

    public class StringNode : ValueNode<string>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteString(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadString();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeStringSize(value);
        }
    }

    public class BytesNode : ValueNode<byte[]>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteBytes(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadBytes();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeBytesSize(value);
        }
    }

    public class Fixed32Node : ValueNode<uint>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteFixed32(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadFixed32();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeFixed32Size(value);
        }
    }

    public class SFixed32Node : ValueNode<int>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteSFixed32(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadSFixed32();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeSFixed32Size(value);
        }
    }

    public class FloatNode : ValueNode<float>
    {



        internal override void InternalWriteTo(OutputStream output)
        {
            output.WriteFloat(value);
        }

        internal override void InternalReadFrom(InputStream input)
        {
            value = input.ReadFloat();
        }

        internal override void ComputeSize()
        {
            _size = OutputStream.ComputeFloatSize(value);
        }
    }
}
