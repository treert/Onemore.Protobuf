# XProtobuf
implement a subset of protobuf, current target is to support dymanic proto for c#.

## Doc

Has no doc. Just open XProtobuf.sln , then run XprotobufWinform.

0. Onemore.Protobuf: the lib
    1. core: InputStream.cs, OutputStream.cs and OutputStream.ComputeSize.cs, WireFormat.cs
    2. for dynamic message: MessageInfo.cs, FieldFormat.cs, Node.cs 
1. ConsoleText: simple test
2. XProtobufWinform: then editor, can editor message, can gen *.proto and *.cs.
2. XSerialize: serialize tool, use in save file and **`Util.Clone<T>(T obj)`**.