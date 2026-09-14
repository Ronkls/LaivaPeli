using System.Collections;
using System.Collections.Generic;

namespace GA.Collections
{
	public class LinkedList<T> : ICollection<T>
	{
		protected class Node
		{
			public T Value { get; set; }
			public Node Next { get; set; }
			public Node Previous { get; set; }

			public Node() : this(default(T))
			{
			}

			public Node(T value, Node next = null, Node previous = null)
			{
				Value = value;
				Next = next;
				Previous = previous;
			}
		}

		/// <summary>
		/// The head of the linked list. When the list is empty, this will be null.
		/// </summary>
		protected Node Head { get; set; } = null;

		/// <summary>
		/// The Tail of the linked list. When the list is empty, this will be null.
		/// </summary>
		protected Node Tail { get; set; } = null;

		public int Count { get; private set; } = 0;

		public virtual bool IsReadOnly => false;

		/// <summary>
		/// Method not used anymore. Replaced by the AddFirst and AddLast methods. If the method is used, it acts as an AddLast method.
		/// </summary>
		public void Add(T item)
		{
			AddLast(item);

			// Old add method used for single linked list.
			// Previously this added a new node to the end of the list so the AddLast method is a newer version of this method

			// if (IsReadOnly)
			// {
			// 	throw new System.NotSupportedException("The collection is read-only.");
			// }

			// Node node = new Node(item);

			// if (Head == null)
			// {
			// 	Head = node;
			// }
			// else
			// {
			// 	Node current = Head;
			// 	while (current.Next != null)
			// 	{
			// 		current = current.Next;
			// 	}

			// 	current.Next = node;
			// }

			// Count++;
		}

		/// <summary>
		/// Adds a new node to the beginning of the linked list.
		/// If the list is empty, the new node becomes both the head and the tail.
		/// </summary>
		public void AddFirst(T item)
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("The collection is read-only.");
			}

			Node newNode = new Node(item);

			if (Head == null)
			{
				Head = newNode;
				Tail = newNode;
			}
			else
			{
				newNode.Next = Head;
				Head.Previous = newNode;
				Head = newNode;
			}

			Count++;
		}

		/// <summary>
		/// Adds a new node to the end of the linked list.
		/// If the list is empty, the new node becomes both the head and the tail.
		/// </summary>
		public void AddLast(T item)
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("The collection is read-only.");
			}

			Node newNode = new Node(item);

			if (Head == null)
			{
				Head = newNode;
				Tail = newNode;
			}
			else
			{
				Tail.Next = newNode;
				newNode.Previous = Tail;
				Tail = newNode;
			}

			Count++;
		}

		public void Clear()
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("The collection is read-only.");
			}

			Head = null;
			Tail = null;
			Count = 0;
		}

		public bool Contains(T item)
		{
			Node current = Head;
			while (current != null)
			{
				if (EqualityComparer<T>.Default.Equals(current.Value, item))
				{
					return true;
				}

				current = current.Next;
			}

			return false;
		}

		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			throw new System.NotImplementedException("Not nesessary for this example :D");
		}

		public IEnumerator<T> GetEnumerator()
		{
			Node current = Head;
			while (current != null)
			{
				yield return current.Value;
				current = current.Next;
			}
		}

		public bool Remove(T item)
		{
			if (IsReadOnly)
			{
				throw new System.NotSupportedException("This collection is read-only");
			}

			Node current = Head;

			while (current != null)
			{
				if (EqualityComparer<T>.Default.Equals(current.Value, item))
				{
					if (current.Previous != null)
					{
						current.Previous.Next = current.Next;
					}
					else
					{
						Head = current.Next;
					}

					if (current.Next != null)
					{
						current.Next.Previous = current.Previous;
					}
					else
					{
						Tail = current.Previous;
					}


					Count--;

					return true;
				}

				current = current.Next;
			}

			return false;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}