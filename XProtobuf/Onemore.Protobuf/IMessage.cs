using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onemore.Protobuf
{
    public interface IMessage
    {
        void WriteTo(OutputStream _output);
        void ReadFrom(InputStream _input);
        int CalculateSize();
        int GetSize();
    }
}
