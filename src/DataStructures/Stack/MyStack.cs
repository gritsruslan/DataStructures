using System.Collections;
using System.Runtime.CompilerServices;
using DataStructures.List;

namespace DataStructures.Stack;

public class MyStack<T> : IEnumerable<T>
{
	private const int DefaultCapacity = 5;

	private T[] _data;

	public int Capacity => _data.Length;

	public bool IsEmpty => Count == 0;

	public int Count { get; private set; }

	public MyStack()
	{
		_data = new T[DefaultCapacity];
	}

	public MyStack(int capacity)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
		_data = new T[capacity];
	}

	public MyStack(IEnumerable<T> source)
	{
		if (source is ICollection<T> coll)
		{
			int colCount = coll.Count;
			if (coll.Count == 0)
			{
				_data = [];
			}
			else
			{
				_data = new T[colCount];
				coll.CopyTo(_data, 0);
			}

			Count = colCount;
		}
		else
		{
			_data = new T[DefaultCapacity];

			using IEnumerator<T> enumerator = source.GetEnumerator();

			while (enumerator.MoveNext())
			{
				Push(enumerator.Current);
				Count++;
			}
		}
	}


	public void Push(T item)
	{
		if(Count == Capacity)
			ExtendCapacity();

		_data[Count] = item;
		Count++;
	}

	public T Pop()
	{
		ThrowIfStackIsEmpty();
		var item = _data[Count - 1];

		// If dataType <T> is reference type or contains reference type - free memory
		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			_data[Count - 1] = default!;

		Count--;
		return item;
	}

	public T Peek()
	{
		ThrowIfStackIsEmpty();
		return _data[Count - 1];
	}

	public bool TryPop(out T item)
	{
		if (IsEmpty)
		{
			item = default!;
			return false;
		}

		item = Pop();
		return true;
	}


	public bool TryPeek(out T item)
	{
		if (IsEmpty)
		{
			item = default!;
			return false;
		}

		item = Peek();
		return true;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		if (arrayIndex < 0 || arrayIndex >= array.Length)
			throw new ArgumentOutOfRangeException(nameof(arrayIndex));

		if (array.Rank > 1)
			throw new ArgumentException("Invalid rank in array to copy from stack!");

		int arrayCapacity = array.Length - arrayIndex;

		if(arrayCapacity < Count)
			throw new ArgumentException("Not enough capacity to copy!");

		for (int i = 0; i < Count; i++)
		{
			array[arrayIndex + i] = _data[Count - 1 - i];
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		for (int i = 0; i < Count - 1; i++)
			yield return _data[i];
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private void ThrowIfStackIsEmpty()
	{
		if (IsEmpty)
			throw new InvalidOperationException("Stack is empty!");
	}

	private void ExtendCapacity()
	{
		if (Capacity == Array.MaxLength)
			throw new InvalidOperationException("Stack is full!");

		uint newArraySize = Capacity < DefaultCapacity ? DefaultCapacity :
			(uint) Math.Ceiling(Capacity * 1.5);

		if (newArraySize > Array.MaxLength)
			newArraySize = (uint) Array.MaxLength;

		T[] newData = new T[newArraySize];

		Array.Copy(_data, newData, Count);
		_data = newData;
	}


	public static implicit operator MyStack<T>(T[] array) => new(array);
}