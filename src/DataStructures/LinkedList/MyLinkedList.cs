using System.Collections;

namespace DataStructures.LinkedList;

public class MyLinkedList<T> : ICollection<T>
{
	private MyLinkedListNode<T>? _first;

	private MyLinkedListNode<T>? _last;

	public T First
	{
		get
		{
			ThrowIfLinkedListIsEmpty();
			return _first!.Value;
		}
	}

	public T Last
	{
		get
		{
			ThrowIfLinkedListIsEmpty();
			return _last!.Value;
		}
	}
	public bool IsEmpty => Count == 0;

	public int Count { get; private set; }

	public bool IsReadOnly => false;

	public MyLinkedList() {}
	public MyLinkedList(IEnumerable<T> source)
	{
		foreach (var item in source)
			AddLast(item);
	}

	public void Add(T item) => AddLast(item);

	public void AddAfter(T itemToFind, T itemToAdd)
	{
		ThrowIfLinkedListIsEmpty();

		var ( _ , current) = FindNode(itemToFind);

		if (current == null)
			throw new InvalidOperationException();

		var newNode = new MyLinkedListNode<T>(itemToAdd);

		if (current.Next is null)
		{
			current.Next = newNode;
			_last = newNode;
		}
		else
		{
			newNode.Next = current.Next;
			current.Next = newNode;
		}
		Count++;
	}

	public void AddBefore(T itemToFind, T itemToAdd)
	{
		ThrowIfLinkedListIsEmpty();

		var (previous, current) = FindNode(itemToFind);

		if (current is null)
			throw new InvalidOperationException();

		if (previous is null)
		{
			AddFirst(itemToAdd);
			return;
		}

		var newNode = new MyLinkedListNode<T>(itemToAdd, current);
		previous.Next = newNode;
		Count++;
	}

	public void AddFirst(T item)
	{
		if (IsEmpty)
		{
			var newNode = new MyLinkedListNode<T>(item);
			_first = newNode;
			_last = newNode;
		}
		else
		{
			_first = new MyLinkedListNode<T>(item, _first);
		}

		Count++;
	}

	public void AddLast(T item)
	{
		if (IsEmpty)
		{
			var newNode = new MyLinkedListNode<T>(item);
			_first = newNode;
			_last = newNode;
		}
		else
		{
			var newNode = new MyLinkedListNode<T>(item);
			_last!.Next = newNode;
			_last = newNode;
		}

		Count++;
	}

	public void RemoveFirst()
	{
		ThrowIfLinkedListIsEmpty();

		if (Count == 1)
		{
			_first = null;
			_last = null;
		}
		else
			_first = _first!.Next;

		Count--;
	}

	public void RemoveLast()
	{
		ThrowIfLinkedListIsEmpty();

		if (Count == 1)
		{
			_first = null;
			_last = null;
		}
		else
		{
			var current = _first;

			while (current!.Next != _last)
				current = current.Next;

			var previous = current;

			previous.Next = null;
			_last = previous;
		}

		Count--;
	}

	public bool Remove(T item)
	{
		var (previous, current) = FindNode(item);

		if (current == null)
			return false;

		if (current == _first)
		{
			_first = _first!.Next;
		}
		else if (current == _last)
		{
			previous!.Next = null;
			_last = previous;
		}
		else
		{
			previous!.Next = current.Next;
		}

		Count--;
		return true;
	}

	public bool Remove(Predicate<T> predicate)
	{
		var (previous, current) = FindNode(predicate);

		if (current is null)
			return false;

		if (current == _first)
		{
			_first = _first.Next;
		}
		else if (current == _last)
		{
			previous!.Next = null;
			_last = previous;
		}
		else
		{
			previous!.Next = current.Next;
		}

		Count--;
		return true;
	}

	public void Clear()
	{
		_first = null;
		_last = null;
		Count = 0;
	}

	public bool Contains(T item)
	{
		var (_, node) = FindNode(item);

		return node is not null;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		int arrayCapacity = array.Length - arrayIndex;

		if (arrayCapacity < Count)
			throw new InvalidOperationException();

		if(IsEmpty)
			return;

		var current = _first;
		var index = 0;

		while (current is not null)
		{
			array[arrayIndex + index] = current.Value;
			current = current.Next;
			index++;
		}
	}

	private (MyLinkedListNode<T>? previousNode, MyLinkedListNode<T>? currentNode) FindNode(T itemToFind)
	{
		Predicate<T> predicate = item =>
		{
			if (item is null)
				return itemToFind is null;

			return item.Equals(itemToFind);
		};

		return FindNode(predicate);
	}

	// Returns (null , null) if linkedList is empty
	// Returns (node, null) if element didnt find
	// Returns (null, node) if firstNode
	// Returns (node, node) if not firstNode
	private (MyLinkedListNode<T>? previousNode, MyLinkedListNode<T>? currentNode) FindNode(Predicate<T> predicate)
	{
		if (IsEmpty)
			return (null, null);

		MyLinkedListNode<T>? previous = null;
		MyLinkedListNode<T>? current = _first;

		do
		{
			if(predicate.Invoke(current!.Value))
				break;

			previous = current;
			current = current.Next;

		} while (current is not null);

		return (previous, current);
	}

	private void ThrowIfLinkedListIsEmpty()
	{
		if (IsEmpty)
			throw new InvalidOperationException();
	}

	public IEnumerator<T> GetEnumerator()
	{
		if(IsEmpty)
			yield break;

		var current = _first;

		while (current is not null)
		{
			yield return current.Value;
			current = current.Next;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public static implicit operator MyLinkedList<T>(T[] array) => new(array);
}