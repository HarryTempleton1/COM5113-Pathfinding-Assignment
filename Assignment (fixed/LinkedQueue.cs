using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    internal class LinkedQueue<T> : IQueue<T>
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();
        public LinkedQueue()
        {
            _list = new LinkedList<T>();
        }
        public void Enqueue(T data)
        {
            _list.PushBack(data); 
        }

        
        public bool Dequeue(ref T data)
        {
            bool result = _list.GetFront(ref data);
            _list.PopFront();
            return result;
        }


        public bool Peek(ref T data)
        {
            bool result = _list.GetFront(ref data);
            _list.PrintList();
            return result;
        }

        public void Clear()
        {

        }
    }
}
