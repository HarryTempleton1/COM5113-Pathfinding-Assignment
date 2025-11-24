using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    public interface IQueue<T>
    {
        void Enqueue(T value);
        bool Dequeue(ref T value);
        bool Peek(ref T value);
    }

}
