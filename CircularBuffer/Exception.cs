using System;

namespace CircularBuffer
{
    public class BufferOverflowException : ApplicationException
    {
        public BufferOverflowException(string message) : base(message) { }
    }

    public class BufferUnderflowException : ApplicationException
    {
        public BufferUnderflowException(string message) : base(message) { }
    }
}
