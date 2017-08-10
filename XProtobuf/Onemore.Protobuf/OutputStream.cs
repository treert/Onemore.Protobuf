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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onemore.Protobuf
{
    /// <summary>
    /// > github.com/google/protobuf/cshape/**/CodedOutputStream.cs
    /// </summary>
    public sealed partial class OutputStream
    {
        interface IOutput
        {
            void WriteByte(byte b);
            void WriteBytes(byte[] buffer, int offset, int length);
            void Flush();
        }

        class BytesOutput:IOutput
        {
            byte[] _buffer;
            int _pos = 0;
            int _length = 0;

            public BytesOutput(byte[] buffer)
            {
                _buffer = buffer;
                _length = buffer.Length;
            }

            public BytesOutput(byte[] buffer, int offset, int length)
            {
                _buffer = buffer;
                _pos = offset;
                _length = length;
            }

            public void WriteByte(byte b)
            {
                if(_pos >= _length)
                {
                    throw new Exception("has no enough space");
                }
                _buffer[_pos] = b;
                _pos++;
            }

            public void WriteBytes(byte[] buffer, int offset, int length)
            {
                if (_pos + length > _length)
                {
                    throw new Exception("has no enough space");
                }
                Buffer.BlockCopy(buffer, offset, _buffer, _pos, length);
                _pos += length;
            }

            public void Flush()
            {
                
            }
        }

        class StreamOutput:IOutput
        {
            Stream _stream;

            public StreamOutput(Stream output)
            {
                _stream = output;
            }

            public void WriteByte(byte b)
            {
                _stream.WriteByte(b);
            }

            public void WriteBytes(byte[] buffer, int offset, int length)
            {
                _stream.Write(buffer, offset, length);
            }

            public void Flush()
            {
                _stream.Flush();
            }
        }

        IOutput _output;

        #region Consturction
        public OutputStream(byte[] buffer)
        {
            _output = new BytesOutput(buffer);
        }

        public OutputStream(byte[] buffer, int offset, int length)
        {
            _output = new BytesOutput(buffer, offset, length);
        }

        public OutputStream(Stream output)
        {
            _output = new StreamOutput(output);
        }
        #endregion
        
        public static implicit operator OutputStream(Stream stream)
        {
            return new OutputStream(stream);
        }

        public static implicit operator OutputStream(byte[] buffer)
        {
            return new OutputStream(buffer);
        }

        public void Flush()
        {
            _output.Flush();
        }

        #region Writing of values (not including tags)
        /// <summary>
        /// Writes a double field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteDouble(double value)
        {
            WriteRawLittleEndian64((ulong)BitConverter.DoubleToInt64Bits(value));
        }

        /// <summary>
        /// Writes a float field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteFloat(float value)
        {
            byte[] rawBytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
            {
                rawBytes = rawBytes.Reverse().ToArray();
            }
            WriteRawBytes(rawBytes, 0, 4);
        }

        /// <summary>
        /// Writes a uint64 field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteUInt64(ulong value)
        {
            WriteRawVarint64(value);
        }

        /// <summary>
        /// Writes an int64 field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteInt64(long value)
        {
            WriteRawVarint64((ulong)value);
        }

        /// <summary>
        /// Writes an int32 field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteInt32(int value)
        {
            if (value >= 0)
            {
                WriteRawVarint32((uint)value);
            }
            else
            {
                // Must sign-extend.
                WriteRawVarint64((ulong)value);
            }
        }

        /// <summary>
        /// Writes a fixed64 field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteFixed64(ulong value)
        {
            WriteRawLittleEndian64(value);
        }

        /// <summary>
        /// Writes a fixed32 field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteFixed32(uint value)
        {
            WriteRawLittleEndian32(value);
        }

        /// <summary>
        /// Writes a bool field value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteBool(bool value)
        {
            WriteRawByte(value ? (byte)1 : (byte)0);
        }

        /// <summary>
        /// Writes a string field value, without a tag, to the stream.
        /// The data is length-prefixed.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteString(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                WriteLength(0);
                return;
            }
            int length = System.Text.Encoding.UTF8.GetByteCount(value);
            WriteLength(length);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
            WriteRawBytes(bytes);
        }

        /// <summary>
        /// Write a byte string, without a tag, to the stream.
        /// The data is length-prefixed.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteBytes(byte[] value)
        {
            if (value == null || value.Length == 0)
            {
                WriteLength(0);
                return;
            }
            WriteLength(value.Length);
            WriteRawBytes(value);
        }

        /// <summary>
        /// Writes a uint32 value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteUInt32(uint value)
        {
            WriteRawVarint32(value);
        }

        /// <summary>
        /// Writes an enum value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteEnum(int value)
        {
            WriteInt32(value);
        }

        /// <summary>
        /// Writes an sfixed32 value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteSFixed32(int value)
        {
            WriteRawLittleEndian32((uint)value);
        }

        /// <summary>
        /// Writes an sfixed64 value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteSFixed64(long value)
        {
            WriteRawLittleEndian64((ulong)value);
        }

        /// <summary>
        /// Writes an sint32 value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteSInt32(int value)
        {
            WriteRawVarint32(EncodeZigZag32(value));
        }

        /// <summary>
        /// Writes an sint64 value, without a tag, to the stream.
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteSInt64(long value)
        {
            WriteRawVarint64(EncodeZigZag64(value));
        }

        /// <summary>
        /// Writes a length (in bytes) for length-delimited data.
        /// </summary>
        /// <remarks>
        /// This method simply writes a rawint, but exists for clarity in calling code.
        /// </remarks>
        /// <param name="length">Length value, in bytes.</param>
        public void WriteLength(int length)
        {
            WriteRawVarint32((uint)length);
        }

#endregion

        #region Raw tag writing
        /// <summary>
        /// Encodes and writes a tag.
        /// </summary>
        /// <param name="fieldNumber">The number of the field to write the tag for</param>
        /// <param name="type">The wire format type of the tag to write</param>
        public void WriteTag(int fieldNumber, WireFormat.WireType type)
        {
            WriteRawVarint32(WireFormat.MakeTag(fieldNumber, type));
        }

        /// <summary>
        /// Writes an already-encoded tag.
        /// </summary>
        /// <param name="tag">The encoded tag</param>
        public void WriteTag(uint tag)
        {
            WriteRawVarint32(tag);
        }

        /// <summary>
        /// Writes the given single-byte tag directly to the stream.
        /// </summary>
        /// <param name="b1">The encoded tag</param>
        public void WriteRawTag(byte b1)
        {
            WriteRawByte(b1);
        }

        /// <summary>
        /// Writes the given two-byte tag directly to the stream.
        /// </summary>
        /// <param name="b1">The first byte of the encoded tag</param>
        /// <param name="b2">The second byte of the encoded tag</param>
        public void WriteRawTag(byte b1, byte b2)
        {
            WriteRawByte(b1);
            WriteRawByte(b2);
        }

        /// <summary>
        /// Writes the given three-byte tag directly to the stream.
        /// </summary>
        /// <param name="b1">The first byte of the encoded tag</param>
        /// <param name="b2">The second byte of the encoded tag</param>
        /// <param name="b3">The third byte of the encoded tag</param>
        public void WriteRawTag(byte b1, byte b2, byte b3)
        {
            WriteRawByte(b1);
            WriteRawByte(b2);
            WriteRawByte(b3);
        }

        /// <summary>
        /// Writes the given four-byte tag directly to the stream.
        /// </summary>
        /// <param name="b1">The first byte of the encoded tag</param>
        /// <param name="b2">The second byte of the encoded tag</param>
        /// <param name="b3">The third byte of the encoded tag</param>
        /// <param name="b4">The fourth byte of the encoded tag</param>
        public void WriteRawTag(byte b1, byte b2, byte b3, byte b4)
        {
            WriteRawByte(b1);
            WriteRawByte(b2);
            WriteRawByte(b3);
            WriteRawByte(b4);
        }

        /// <summary>
        /// Writes the given five-byte tag directly to the stream.
        /// </summary>
        /// <param name="b1">The first byte of the encoded tag</param>
        /// <param name="b2">The second byte of the encoded tag</param>
        /// <param name="b3">The third byte of the encoded tag</param>
        /// <param name="b4">The fourth byte of the encoded tag</param>
        /// <param name="b5">The fifth byte of the encoded tag</param>
        public void WriteRawTag(byte b1, byte b2, byte b3, byte b4, byte b5)
        {
            WriteRawByte(b1);
            WriteRawByte(b2);
            WriteRawByte(b3);
            WriteRawByte(b4);
            WriteRawByte(b5);
        }
        #endregion

        #region Underlying writing primitives
        /// <summary>
        /// Writes a 32 bit value as a varint. The fast route is taken when
        /// there's enough buffer space left to whizz through without checking
        /// for each byte; otherwise, we resort to calling WriteRawByte each time.
        /// </summary>
        internal void WriteRawVarint32(uint value)
        {
            while (value > 127)
            {
                WriteRawByte((byte)((value & 0x7F) | 0x80));
                value >>= 7;
            }
            WriteRawByte((byte)value);
        }

        internal void WriteRawVarint64(ulong value)
        {
            while (value > 127)
            {
                WriteRawByte((byte)((value & 0x7F) | 0x80));
                value >>= 7;
            }
            WriteRawByte((byte)value);
        }

        internal void WriteRawLittleEndian32(uint value)
        {
            WriteRawByte((byte)value);
            WriteRawByte((byte)(value >> 8));
            WriteRawByte((byte)(value >> 16));
            WriteRawByte((byte)(value >> 24));
        }

        internal void WriteRawLittleEndian64(ulong value)
        {
            WriteRawByte((byte)value);
            WriteRawByte((byte)(value >> 8));
            WriteRawByte((byte)(value >> 16));
            WriteRawByte((byte)(value >> 24));
            WriteRawByte((byte)(value >> 32));
            WriteRawByte((byte)(value >> 40));
            WriteRawByte((byte)(value >> 48));
            WriteRawByte((byte)(value >> 56));
        }

        internal void WriteRawByte(byte value)
        {
            _output.WriteByte(value);
        }

        internal void WriteRawByte(uint value)
        {
            WriteRawByte((byte)value);
        }

        /// <summary>
        /// Writes out an array of bytes.
        /// </summary>
        internal void WriteRawBytes(byte[] value)
        {
            WriteRawBytes(value, 0, value.Length);
        }

        /// <summary>
        /// Writes out part of an array of bytes.
        /// </summary>
        internal void WriteRawBytes(byte[] value, int offset, int length)
        {
            _output.WriteBytes(value, offset, length);
        }

        #endregion

        /// <summary>
        /// Encode a 32-bit value with ZigZag encoding.
        /// </summary>
        /// <remarks>
        /// ZigZag encodes signed integers into values that can be efficiently
        /// encoded with varint.  (Otherwise, negative values must be 
        /// sign-extended to 64 bits to be varint encoded, thus always taking
        /// 10 bytes on the wire.)
        /// </remarks>
        internal static uint EncodeZigZag32(int n)
        {
            // Note:  the right-shift must be arithmetic
            return (uint)((n << 1) ^ (n >> 31));
        }

        /// <summary>
        /// Encode a 64-bit value with ZigZag encoding.
        /// </summary>
        /// <remarks>
        /// ZigZag encodes signed integers into values that can be efficiently
        /// encoded with varint.  (Otherwise, negative values must be 
        /// sign-extended to 64 bits to be varint encoded, thus always taking
        /// 10 bytes on the wire.)
        /// </remarks>
        internal static ulong EncodeZigZag64(long n)
        {
            return (ulong)((n << 1) ^ (n >> 63));
        }
    }
}
