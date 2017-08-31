#region Copyright notice and license
// Protocol Buffers - Google's data interchange format
// Copyright 2008 Google Inc.  All rights reserved.
// https://developers.google.com/protocol-buffers/
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//     * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
//     * Neither the name of Google Inc. nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

// modify by om

using System;
using System.IO;
using System.Linq;


namespace Onemore.Protobuf
{
    /// <summary>
    /// > github.com/google/protobuf/cshape/**/CodedInputStream.cs
    /// </summary>
    public sealed class InputStream
    {

        public interface IInput
        {
            void ReadBytes(byte[] buffer);
            void ReadBytes(byte[] buffer, int length);
            byte ReadByte();
            long GetPos();
            void Skip(int size);

            bool IsAtEnd();
        }

        public class BytesInput : IInput
        {

            byte[] _buffer = null;
            int _pos = 0;
            int _length = 0;

            public BytesInput() { }

            public BytesInput(byte[] buffer)
            {
                Reset(buffer, 0, buffer.Length);
            }

            public BytesInput(byte[] buffer, int offset, int length)
            {
                Reset(buffer, offset, length);
            }

            public void Reset(byte[] buffer, int offset, int length)
            {
                _buffer = buffer;
                _pos = offset;
                _length = length;
            }

            public void ReadBytes(byte[] buffer)
            {
                if(_pos + buffer.Length > _length)
                {
                    throw new Exception("has not enough data");
                }
                Buffer.BlockCopy(_buffer, _pos, buffer, 0, buffer.Length);
                _pos += buffer.Length;
            }

            public void ReadBytes(byte[] buffer, int length)
            {
                if (_pos + length > _length)
                {
                    throw new Exception("has not enough data");
                }
                Buffer.BlockCopy(_buffer, _pos, buffer, 0, length);
                _pos += length;
            }
            public byte ReadByte()
            {
                if (_pos + 1 > _length)
                {
                    throw new Exception("has not enough data");
                }
                byte ret = _buffer[_pos];
                _pos++;
                return ret;
            }

            public long GetPos()
            {
                return _pos;
            }

            public void Skip(int size)
            {
                if (_pos + size > _length)
                {
                    throw new Exception("has not enough data");
                }
                _pos += size;
            }

            public bool IsAtEnd()
            {
                return _pos >= _length;
            }
        }

        public class StreamInput :IInput
        {
            Stream _stream = null;

            public StreamInput() { }

            public StreamInput(Stream input)
            {
                _stream = input;
            }

            public void Reset(Stream input)
            {
                _stream = input;
            }

            public void ReadBytes(byte[] buffer)
            {
                int len = _stream.Read(buffer, 0, buffer.Length);
                if(len < buffer.Length)
                {
                    throw new Exception("has not enough data");
                }
            }

            public void ReadBytes(byte[] buffer, int length)
            {
                int len = _stream.Read(buffer, 0, length);
                if (len < length)
                {
                    throw new Exception("has not enough data");
                }
            }

            public byte ReadByte()
            {
                int data = _stream.ReadByte();
                if(data == -1)
                {
                    throw new Exception("has not enough data");
                }
                return (byte)data;
            }

            public long GetPos()
            {
                return _stream.Position;
            }

            public void Skip(int size)
            {
                // assert _stream.CanSeek
                long pre_len = _stream.Position;
                _stream.Position += size;
                if(_stream.Position != pre_len + size)
                {
                    throw new Exception("skip stream failed, has no enough data");
                }
            }

            public bool IsAtEnd()
            {
                return _stream.Position >= _stream.Length;
            }
        }

        IInput _input = null;
        const int BufferSize = 4096;
        /// <summary>
        /// Buffer to avoid to many gc, it's better use one InputStream for one thread
        /// </summary>
        byte[] _buffer = new byte[BufferSize];

        public IInput mInput { get { return _input; } set { _input = value; } }

        public InputStream()
        {
        }

        /// <summary>
        /// Creates a new <see cref="InputStream"/> reading data from the given byte array.
        /// </summary>
        public InputStream(byte[] buffer)
        {
            _input = new BytesInput(buffer);
        }

        /// <summary>
        /// Creates a new <see cref="InputStream"/> that reads from the given byte array slice.
        /// </summary>
        public InputStream(byte[] buffer, int offset, int length)
        {
            // assert offset >= 0 and length >=0 and buffer.Length >= offset + length
            // no zuo no die
            _input = new BytesInput(buffer, offset, length);
        }

        /// <summary>
        /// Creates a new <see cref="InputStream"/> that reads from the given stream.
        /// The given stream should CanSeek
        /// </summary>
        /// <param name="input"></param>
        public InputStream(Stream input)
        {
            // assert input.CanSeek
            _input = new StreamInput(input);
        }

        public static implicit operator InputStream(Stream stream)
        {
            return new InputStream(stream);
        }

        public static implicit operator InputStream(byte[] buffer)
        {
            return new InputStream(buffer);
        }

        /// <summary>
        /// Returns the current position in the input stream, or the position in the input buffer
        /// </summary>
        public long Position
        {
            get
            {
                return _input.GetPos();
            }
        }

        /// <summary>
        /// Reads field Tag from the stream.
        /// </summary>
        public uint ReadTag()
        {
            if(_input.IsAtEnd())
            {
                return 0;// this is why index start from 1
            }
            return ReadRawVarint32();
        }

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
            lock(_buffer)
            {
                _input.ReadBytes(_buffer, 4);
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(_buffer, 0, 4);
                }
                return BitConverter.ToSingle(_buffer, 0);
            }
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
            if (length <= 0)
            {
                return "";
            }
            else if(length <= BufferSize)
            {
                lock(_buffer)
                {
                    _input.ReadBytes(_buffer, length);
                    return System.Text.Encoding.UTF8.GetString(_buffer, 0, length);
                }
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
            return _input.ReadByte();
        }


        internal byte[] ReadRawBytes(int size)
        {
            byte[] buffer = new byte[size];
            _input.ReadBytes(buffer);
            return buffer;
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
            throw new Exception("ReadRawVarint64 error");
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


        /// <summary>
        /// Skips the data for the field with the tag we've just read.
        /// This should be called directly after <see cref="ReadTag"/>, when
        /// the caller wishes to skip an unknown field.
        /// </summary>
        /// <remarks>
        /// This method throws <see cref="InvalidProtocolBufferException"/> if the last-read tag was an end-group tag.
        /// If a caller wishes to skip a group, they should skip the whole group, by calling this method after reading the
        /// start-group tag. This behavior allows callers to call this method on any field they don't understand, correctly
        /// resulting in an error if an end-group tag has not been paired with an earlier start-group tag.
        /// </remarks>
        /// <exception cref="InvalidProtocolBufferException">The last tag was an end-group tag</exception>
        /// <exception cref="InvalidOperationException">The last read operation read to the end of the logical stream</exception>
        public void SkipField(WireFormat.WireType wire_type)
        {
            switch (wire_type)
            {
                case WireFormat.WireType.StartGroup:
                    throw new NotSupportedException();
                case WireFormat.WireType.EndGroup:
                    throw new NotSupportedException();
                case WireFormat.WireType.Fixed32:
                    ReadFixed32();
                    break;
                case WireFormat.WireType.Fixed64:
                    ReadFixed64();
                    break;
                case WireFormat.WireType.LengthDelimited:
                    var length = ReadLength();
                    SkipRawBytes(length);
                    break;
                case WireFormat.WireType.Varint:
                    ReadRawVarint32();
                    break;
            }
        }

        /// <summary>
        /// Reads and discards <paramref name="size"/> bytes.
        /// </summary>
        /// <exception cref="InvalidProtocolBufferException">the end of the stream
        /// or the current limit was reached</exception>
        private void SkipRawBytes(int size)
        {
            _input.Skip(size);
        }
    }
}
