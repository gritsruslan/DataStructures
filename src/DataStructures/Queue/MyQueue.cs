using System.Collections;
using System.Runtime.CompilerServices;

namespace DataStructures.Queue;

public class MyQueue<T> : IEnumerable<T>
{
	private const int DefaultCapacity = 5;

	private T[] _data;

	private int _head;

	private int _tail;

	public int Count { get; private set; }

	public bool IsEmpty => Count == 0;

	public int Capacity => _data.Length;

	public MyQueue(int capacity)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
		_data = new T[capacity];
	}

	public MyQueue()
	{
		_data = new T[DefaultCapacity];
	}

	public MyQueue(IEnumerable<T> source)
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
				Enqueue(enumerator.Current);
				Count++;
			}
		}
	}

	public void Enqueue(T item)
	{
		if (Count == Capacity)
			ResizeArray();

		_data[_tail] = item;
		_tail++;
		Count++;
	}

	public T Dequeue()
	{
		ThrowIfQueueIsEmpty();
		T item = _data[_head];


		if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			_data[_head] = default!;

		_head++;
		Count--;

		return item;
	}

	public T Peek()
	{
		ThrowIfQueueIsEmpty();
		return _data[_head];
	}

	public bool TryDequeue(out T item)
	{
		if (IsEmpty)
		{
			item = default!;
			return false;
		}

		item = Dequeue();
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

	private void ThrowIfQueueIsEmpty()
	{
		if(Count == 0)
			throw new InvalidOperationException();
	}

	private void ResizeArray()
	{
		if (Capacity == int.MaxValue)
			throw new Exception();

		uint newArraySize = Capacity == 0 ? DefaultCapacity :
			(uint)(Capacity + Math.Ceiling(Capacity / 2.0));

		if (newArraySize > Array.MaxLength)
			newArraySize = (uint) Array.MaxLength;

		T[] newData = new T[newArraySize];

		Array.Copy(_data, newData, Count);
		_data = newData;
	}

	public IEnumerator<T> GetEnumerator()
	{
		for (int i = _head; i < _tail; i++)
			yield return _data[i];
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public static implicit operator MyQueue<T>(T[] enumerable) => new(enumerable);
}