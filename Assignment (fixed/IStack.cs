using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    internal interface IStack<T>
    {
        public interface IQueue<T>
        {
            void PushStack(T value);
            bool PopStack(ref T value);
            bool IsEmpty(ref T value);
        }
    }
}
