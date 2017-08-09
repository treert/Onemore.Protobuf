using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Onemore.XProtobuf
{
    /// <summary>
    /// > github.com/google/protobuf/cshape/**/CodedInputStream.cs
    /// </summary>
    public sealed class InputStream
    {
        /// <summary>
        /// Reads a double field from the stream.
        /// </summary>
        public double ReadDouble()
        {
            return BitConverter.Int64BitsToDouble((long)ReadRawLittleEndian64());
        }

        /// <summary>
        /// Reads a float field from the stream.
        /// </summary>
        public float ReadFloat()
        {
            byte[] rawBytes = ReadRawBytes(4);
            if (!BitConverter.IsLittleEndian)
            {
                rawBytes = rawBytes.Reverse().ToArray();
            }
            return BitConverter.ToSingle(rawBytes, 0);
        }

        /// <summary>
        /// Reads a uint64 field from the stream.
        /// </summary>
        public ulong ReadUInt64()
        {
            return ReadRawVarint64();
        }

        /// <summary>
        /// Reads an int64 field from the stream.
        /// </summary>
        public long ReadInt64()
        {
            return (long)ReadRawVarint64();
        }

        /// <summary>
        /// Reads an int32 field from the stream.
        /// </summary>
        public int ReadInt32()
        {
            return (int)ReadRawVarint32();
        }

        /// <summary>
        /// Reads a fixed64 field from the stream.
        /// </summary>
        public ulong ReadFixed64()
        {
            return ReadRawLittleEndian64();
        }

        /// <summary>
        /// Reads a fixed32 field from the stream.
        /// </summary>
        public uint ReadFixed32()
        {
            return ReadRawLittleEndian32();
        }

        /// <summary>
        /// Reads a bool field from the stream.
        /// </summary>
        public bool ReadBool()
        {
            return ReadRawVarint32() != 0;
        }

        /// <summary>
        /// Reads a string field from the stream.
        /// </summary>
        public string ReadString()
        {
            int length = ReadLength();
            // No need to read any data for an empty string.
            if (length == 0)
            {
                return "";
            }
            return System.Text.Encoding.UTF8.GetString(ReadRawBytes(length), 0, length);
        }

        /// <summary>
        /// Reads a bytes field value from the stream.
        /// </summary>   
        public byte[] ReadBytes()
        {
            int length = ReadLength();
            return ReadRawBytes(length);
        }

        /// <summary>
        /// Reads a uint32 field value from the stream.
        /// </summary>   
        public uint ReadUInt32()
        {
            return ReadRawVarint32();
        }

        /// <summary>
        /// Reads an enum field value from the stream.
        /// </summary>   
        public int ReadEnum()
        {
            // Currently just a pass-through, but it's nice to separate it logically from WriteInt32.
            return (int)ReadRawVarint32();
        }

        /// <summary>
        /// Reads an sfixed32 field value from the stream.
        /// </summary>   
        public int ReadSFixed32()
        {
            return (int)ReadRawLittleEndian32();
        }

        /// <summary>
        /// Reads an sfixed64 field value from the stream.
        /// </summary>   
        public long ReadSFixed64()
        {
            return (long)ReadRawLittleEndian64();
        }

        /// <summary>
        /// Reads an sint32 field value from the stream.
        /// </summary>   
        public int ReadSInt32()
        {
            return DecodeZigZag32(ReadRawVarint32());
        }

        /// <summary>
        /// Reads an sint64 field value from the stream.
        /// </summary>   
        public long ReadSInt64()
        {
            return DecodeZigZag64(ReadRawVarint64());
        }

        /// <summary>
        /// Reads a length for length-delimited data.
        /// </summary>
        /// <remarks>
        /// This is internally just reading a varint, but this method exists
        /// to make the calling code clearer.
        /// </remarks>
        public int ReadLength()
        {
            return (int)ReadRawVarint32();
        }

        /// <summary>
        /// Read one byte from the input.
        /// modify by om
        /// </summary>
        internal byte ReadRawByte()
        {
            // todo@om 
            return 0;
        }


        internal byte[] ReadRawBytes(int size)
        {
            return new byte[0];
        }

        /// <summary>
        /// Reads a raw varint32 from the stream.
        /// </summary>
        internal uint ReadRawVarint32()
        {
            return (uint)ReadRawVarint64();
        }

        /// <summary>
        /// Reads a raw varint from the stream.
        /// </summary>
        internal ulong ReadRawVarint64()
        {
            int shift = 0;
            ulong result = 0;
            while (shift < 64)
            {
                byte b = ReadRawByte();
                result |= (ulong)(b & 0x7F) << shift;
                if ((b & 0x80) == 0)
                {
                    return result;
                }
                shift += 7;
            }
            throw new Exception("InvalidProtocolBufferException.MalformedVarint");
            //throw InvalidProtocolBufferException.MalformedVarint();
        }

        /// <summary>
        /// Reads a 32-bit little-endian integer from the stream.
        /// </summary>
        internal uint ReadRawLittleEndian32()
        {
            uint b1 = ReadRawByte();
            uint b2 = ReadRawByte();
            uint b3 = ReadRawByte();
            uint b4 = ReadRawByte();
            return b1 | (b2 << 8) | (b3 << 16) | (b4 << 24);
        }

        /// <summary>
        /// Reads a 64-bit little-endian integer from the stream.
        /// </summary>
        internal ulong ReadRawLittleEndian64()
        {
            ulong b1 = ReadRawByte();
            ulong b2 = ReadRawByte();
            ulong b3 = ReadRawByte();
            ulong b4 = ReadRawByte();
            ulong b5 = ReadRawByte();
            ulong b6 = ReadRawByte();
            ulong b7 = ReadRawByte();
            ulong b8 = ReadRawByte();
            return b1 | (b2 << 8) | (b3 << 16) | (b4 << 24)
                   | (b5 << 32) | (b6 << 40) | (b7 << 48) | (b8 << 56);
        }

        /// <summary>
        /// Decode a 32-bit value with ZigZag encoding.
        /// </summary>
        /// <remarks>
        /// ZigZag encodes signed integers into values that can be efficiently
        /// encoded with varint.  (Otherwise, negative values must be 
        /// sign-extended to 64 bits to be varint encoded, thus always taking
        /// 10 bytes on the wire.)
        /// </remarks>
        internal static int DecodeZigZag32(uint n)
        {
            return (int)(n >> 1) ^ -(int)(n & 1);
        }

        /// <summary>
        /// Decode a 32-bit value with ZigZag encoding.
        /// </summary>
        /// <remarks>
        /// ZigZag encodes signed integers into values that can be efficiently
        /// encoded with varint.  (Otherwise, negative values must be 
        /// sign-extended to 64 bits to be varint encoded, thus always taking
        /// 10 bytes on the wire.)
        /// </remarks>
        internal static long DecodeZigZag64(ulong n)
        {
            return (long)(n >> 1) ^ -(long)(n & 1);
        }
    }
}
