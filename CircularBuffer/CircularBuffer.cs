using System;
using System.Collections.Generic;

namespace CircularBuffer
{
    public class CircularBuffer<T> : ICircularBuffer<T>
    {
        //Für Threadsafety, lockt Consume und Produce
        public static object mutex = new object();

        public int Capacity { get { return buffer.Length; } }

        public int Count { get; private set; } = 0;

        public bool IsEmpty => Count == 0;

        public bool IsFull => Count == Capacity;

        private T[] buffer;

        //Konstruktor
        public CircularBuffer(int capacity)
        {
            buffer = new T[capacity];
        }

        public void Clear()
        {
            Count = 0;
        }

        public T Consume()
        {
            lock(mutex)
            {
                if (IsEmpty)
                {
                    throw new BufferUnderflowException("Couldn't remove element. Buffer is empty");
                }

                T _temp = buffer[0];
                for (int i = 1; i < Count; i++)
                {
                    buffer[i - 1] = buffer[i];
                }
                --Count;
                return _temp;
            }
        }

        public void Produce(T newElement)
        {
            lock(mutex)
            {
                if (IsFull)
                {
                    throw new BufferOverflowException("Couldn't add element. Buffer is full");
                }

                buffer[Count] = newElement;
                ++Count;
            }
        }

        public int ProduceAll(IEnumerable<T> collection)
        {
            int _added = 0;

            foreach (T element in collection)
            {
                try
                {
                    Produce(element);
                    ++_added;
                }
                catch
                {
                    return _added;
                }
            }
            return _added;
        }

        public void ConsumeAll(Action<T> action)
        {
            while (!IsEmpty)
            {
                for (int i = 0; i < Count; i++)
                {
                    action(Consume());
                }
            }
        }
    }
}
