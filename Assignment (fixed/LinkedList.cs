using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment__fixed.Program;

namespace Assignment__fixed
{
    public class LinkedList<T>
    {
        private Element? _head;

        private int _cost = 0; //heuristics
        private int _score = 0; //terrain

        private Element? _predecessor = null;

        private class Element
        {
            public T Data { get; }
            public Element? Next { get; set; }

            public Element? Predecessor {  get; set; }

            public Element(T data, Element? predecessor)
            {
                Data = data;
                Next = null;
                Predecessor = predecessor;
            }
        }

        // Constructor - initialises an empty list
        public LinkedList()
        {
            _head = null;
        }

        // IsEmpty - returns true only when the list is empty
        public bool IsEmpty()
        {
            return _head == null;
        }

        // PushFront - add a new element at the head of the list
        public void PushFront(T data)
        {
            // create the new element from the data supplied
            Element newElement = new Element(data, _predecessor);

            // make new element's next the current head
            newElement.Next = _head;

            // make the new element the head of the list
            _head = newElement;
        }

        public void PushBack(T data)
        {
            Element newElement = new Element(data, _predecessor);

            newElement.Next = null;

            if (_head == null)
            {
                _head = newElement;
            }
            else
            {
                Element current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newElement;
                Ops.count++;

            }
        }

        // PopFront - remove an element from the front of the list
        public void PopFront()
        {
            // popping from an empty list is not illegal, it just has no effect
            if (_head != null)
            {
                _head = _head.Next;
            }
        }

        // GetFront - provide the data stored in the front element, if the list is not empty
        //            note that data is "returned" through the reference parameter
        //            the bool returned indicates success or otherwise
        public bool GetFront(ref T data)
        {
            if (!IsEmpty())
            {
                data = _head.Data;
            }
            return !IsEmpty();
        }

        // PrintList - display the list contents on the console
        public void PrintList()
        {
            Element? currentElement = _head; // an element reference to "walk" along the list

            while (currentElement != null)
            {
                Console.Write(currentElement.Data + ", ");
                currentElement = currentElement;
            }

            Console.Write("\b\b \b"); // over-write the comma after the last element with a space, then back the cursor up.
        }


        // Contains - returns true when data is contained in the list
        public bool Contains(T data)
        {
            Element? currentElement = _head;
            bool found = false;

            while (currentElement != null && !found) // stop when we have either reached the end, or found what we're looking for
            {
                if (EqualityComparer<T>.Default.Equals(currentElement.Data, data))
                {
                    found = true; // flag when we gave found the element
                }
                currentElement = currentElement.Next;
            }

            return found;
        }

        // RemoveFirst - removes the first occurrance of data from the list
        public void RemoveFirst(T data)
        {
            if (_head == null)
                return;

            if (EqualityComparer<T>.Default.Equals(_head.Data, data))
            {
                _head = _head.Next;
                return;
            }

            Element? current = _head;
            Element? previous = null;

            while (current != null && !EqualityComparer<T>.Default.Equals(current.Data, data))
            {
                previous = current;
                current = current.Next;
            }

            if (current != null && previous != null)
                previous.Next = current.Next;
        }
    }
}
