using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    public class LinkedList
    {
        private Element? _head;

        //initialises an empty list
        public LinkedList()
        {
            _head = null;
        }

        public bool IsEmpty()
        {
            return _head == null;
        }

        //add a new element at the head of the list
        public void PushFront(int data)
        {
            //create the new element from data
            Element newElement = new Element(data);

            //make new element next value the current head
            newElement.Next = _head;

            //make new element the head of the list
            _head = newElement;
        }

        public void PushBack(int data)
        {
            //creates new element checks if list is empty
            Element newElement = new Element(data);
            if (_head == null)
            {
                _head = newElement;
            }
            //traverses to the end of the list and adds the new element
            Element current = _head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newElement;
        }
        //remove an element from the front of the list
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
        public bool GetFront(ref int data)
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
                currentElement = currentElement.Next;
            }

            Console.Write("\b\b \b"); // over-write the comma after the last element with a space, then back the cursor up.
        }
        // Contains - returns true when data is contained in the list
        public bool Contains(int data)
        {
            Element? currentElement = _head;
            bool found = false;

            while (currentElement != null && !found) // stop when we have either reached the end, or found what we're looking for
            {
                if (currentElement.Data == data)
                {
                    found = true; // flag when we gave found the element
                }
                currentElement = currentElement.Next;
            }

            return found;
        }

        // RemoveFirst - removes the first occurrance of data from the list
        public void RemoveFirst(int data)
        {
            if (_head != null && _head.Data == data) // data is the first element in the list
            {
                _head = _head.Next; // the same as PopFront
            }
            else
            {
                Element? currentElement = _head;
                Element? previousElement = _head;

                bool found = false;
                // find element to remove if it exists
                while (currentElement != null && !found)
                {
                    if (currentElement.Data == data)
                    {
                        found = true;
                    }
                    else
                    {
                        // move the references along...
                        previousElement = currentElement;
                        currentElement = currentElement.Next;
                    }
                }

                // If the data is found, we know current is pointing to it.
                // Also, it is not the first element, so we know previous must be
                // pointing to the element before.

                // remove the data if we found it
                if (found)
                {
                    previousElement.Next = currentElement.Next;
                }
            }
        }

        public List<T> ToList()
        {
            var result = new List<T>();
            Element? current = _head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }
            return result;
        }
    }
}