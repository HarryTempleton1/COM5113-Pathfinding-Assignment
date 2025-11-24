using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    internal class Stack<T> : LinkedList<T>
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();
        public Stack()
        {
            _list = new LinkedList<T>();
        }

        public void PushStack(T data)
        {
            _list.PushFront(data);
        }

        public void PopStack()
        {
            _list.PopFront();
        }
    }
}
