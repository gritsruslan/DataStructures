using System.Collections;

namespace DataStructures.List;

// Custom List<T> implementation
public class MyList<T> : IList<T>
{
	private const int DefaultCapacity = 5;

	private T[] _data;

	public int Count { get; private set; }

	public int Capacity => _data.Length;

	public bool IsReadOnly => false;

	public T this[int index]
	{
		get
		{
			ThrowIfIndexOutOfRange(index);
			return _data[index];
		}
		set
		{
			ThrowIfIndexOutOfRange(index);
			_data[index] = value;
		}
	}

	public MyList()
	{
		_data = new T[DefaultCapacity];
	}

	public MyList(int capacity)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
		_data = new T[capacity];
	}

	public MyList(IEnumerable<T> enumerable)
	{
		if (enumerable is ICollection<T> collection)
		{
			int colCount = collection.Count;

			if (collection.Count == 0)
				_data = [];
			else
			{
				_data = new T[colCount];
				collection.CopyTo(_data, 0);
			}

			Count = colCount;
		}
		else
		{
			_data = new T[DefaultCapacity];

			using var enumerator = enumerable.GetEnumerator();

			while (enumerator.MoveNext())
			{
				Add(enumerator.Current);
				Count++;
			}
		}
	}

	public void Add(T item)
	{
		if(Count == Capacity)
			ExtendCapacity();

		_data[Count] = item;
		Count++;
	}

	public void Clear()
	{
		_data = new T[DefaultCapacity];
		Count = 0;
	}

	public bool Contains(T item) => IndexOf(item) != -1;

	public void CopyTo(T[] array, int arrayIndex)
	{
		if (arrayIndex < 0 || arrayIndex >= array.Length)
			throw new ArgumentOutOfRangeException(nameof(arrayIndex));

		if (array.Rank > 1)
			throw new ArgumentException("Invalid rank in array to copy from list!");

		int arrayCapacity = array.Length - arrayIndex;

		if(arrayCapacity < Count)
			throw new ArgumentException("Not enough capacity to copy from list!");

		for (int i = 0; i < Count; i++)
		{
			int currentArrayIndex = arrayIndex + i;

			array[currentArrayIndex] = _data[i];
		}
	}

	public bool Remove(T item)
	{
		int index = IndexOf(item);

		if (index == -1)
			return false;

		RemoveAt(index);

		return true;
	}

	public int IndexOf(T item)
	{
		for (int i = 0; i < Count; i++)
		{
			var currentItem = _data[i];

			if (currentItem == null)
			{
				if (item == null)
					return i;

				continue;
			}

			if (currentItem.Equals(item))
				return i;
		}

		return -1;
	}

	public void Insert(int index, T item)
	{
		ThrowIfIndexOutOfRange(index);

		if(Count == Capacity)
			ExtendCapacity();

		for (int i = Count; i > index; i--)
		{
			_data[i] = _data[i - 1];
		}

		_data[index] = item;
		Count++;
	}

	public void RemoveAt(int index)
	{
		ThrowIfIndexOutOfRange(index);

		for (int i = index + 1; i < Count; i++)
		{
			_data[i - 1] = _data[i];
		}

		Count--;
	}

	private void ExtendCapacity()
	{
		if (Capacity == Array.MaxLength)
			throw new Exception("List is full!");

		uint newArraySize = Capacity < DefaultCapacity ? DefaultCapacity :
			(uint)Math.Ceiling(Capacity * 1.5);

		if (newArraySize > Array.MaxLength)
			newArraySize = (uint) Array.MaxLength;

		var newData = new T[newArraySize];

		Array.Copy(_data, newData, Count);
		_data = newData;
	}
	public IEnumerator<T> GetEnumerator() => new MyListEnumerator(this);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	private void ThrowIfIndexOutOfRange(int index)
	{
		if (index < 0 || index >= Count)
			throw new IndexOutOfRangeException("Index out of range in list!");
	}

	private struct MyListEnumerator : IEnumerator<T>
	{

		public MyListEnumerator(MyList<T> list)
		{
			_list = list;
			_index = -1;
		}

		private MyList<T> _list;

		private int _index = 0;
		public T Current
		{
			get
			{
				if (_index < 0 || _index >= _list.Count)
					throw new InvalidOperationException("Invalid index!");

				return _list[_index];
			}
		}

		object IEnumerator.Current => Current!;

		public bool MoveNext()
		{
			_index++;
			return _index < _list.Count;
		}

		public void Reset()
		{
			_index = -1;
		}

		public void Dispose() { }
	}

	public static implicit operator MyList<T>(T[] enumerable) => new(enumerable);
}