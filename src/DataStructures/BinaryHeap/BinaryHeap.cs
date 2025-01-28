namespace DataStructures.BinaryHeap;

public class BinaryHeap<T>
	where T : IComparable<T>
{
	private const int DefaultCapacity = 5;

	private T[] _data;

	private HeapType Type { get; }

	public int Capacity => _data.Length;

	public bool IsEmpty => Count == 0;

	public int Height => (int) Math.Ceiling(Math.Log2(Count));

	public int Count { get; private set; }

	public BinaryHeap(HeapType type, int capacity)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(capacity);
		Type = type;
		_data = new T[capacity];
	}

	public BinaryHeap(HeapType type)
	{
		Type = type;
		_data = new T[DefaultCapacity];
	}

	public void Add(T item)
	{
		if(Count == Capacity)
			ExtendCapacity();

		_data[Count] = item;
		HeapifyUp(Count);
		Count++;
	}

	public T GetRoot()
	{
		ThrowIfEmpty();
		return _data[0];
	}

	public T GetAndDeleteRoot()
	{
		ThrowIfEmpty();

		var rootToReturn = _data[0];
		_data[0] = _data[Count - 1];
		_data[Count - 1] = default!;
		HeapifyDown(0);
		Count--;
		return rootToReturn;
	}

	public bool Remove(T item)
	{
		ThrowIfEmpty();

		var elemIndex = SearchIndex(item);

		if (elemIndex == -1)
			return false;

		_data[elemIndex] = _data[Count - 1];
		_data[Count - 1] = default!;
		Count--;
		HeapifyDown(elemIndex);
		return true;
	}

	private void HeapifyUp(int index)
	{
		if(index == 0)
			return;

		var parentIndex = GetParentIndex(index);
		var current = _data[index];
		var parent = _data[parentIndex];

		if(!Compare(current, parent))
			return;

		Swap(index, parentIndex);
		HeapifyUp(parentIndex);
	}

	private int SearchIndex(T item)
	{
		for (int i = 0; i < Count; i++)
		{
			if (_data[i].CompareTo(item) == 0)
				return i;
		}
		return -1;
	}

	private void HeapifyDown(int index)
	{
		var leftChildIndex = GetLeftChildIndex(index);
		var rightChildIndex = GetRightChildIndex(index);
		var indexOfTheBest = index;

		if (leftChildIndex < Count && Compare(_data[leftChildIndex], _data[indexOfTheBest]))
			indexOfTheBest = leftChildIndex;

		if (rightChildIndex < Count && Compare(_data[rightChildIndex], _data[indexOfTheBest]))
			indexOfTheBest = rightChildIndex;

		if(indexOfTheBest == index)
			return;

		Swap(indexOfTheBest, index);
		HeapifyDown(indexOfTheBest);
	}


	private void Swap(int index1, int index2) =>
		(_data[index1], _data[index2]) = (_data[index2], _data[index1]);

	private bool Compare(T first, T second)
	{
		return Type == HeapType.MaxHeap ?
			first.CompareTo(second) == 1 : first.CompareTo(second) == -1;
	}

	private void ExtendCapacity()
	{
		if (Capacity == Array.MaxLength)
			throw new InvalidOperationException("Heap is full!");

		uint newArraySize = Capacity < DefaultCapacity ? DefaultCapacity :
			(uint)Math.Ceiling(Capacity * 1.5);

		if (newArraySize > Array.MaxLength)
			newArraySize = (uint) Array.MaxLength;

		T[] newData = new T[newArraySize];

		Array.Copy(_data, newData, Count);
		_data = newData;
	}

	private int GetLeftChildIndex(int index) => index * 2 + 1;

	private int GetRightChildIndex(int index) => index * 2 + 2;

	private int GetParentIndex(int index) => (index - 1) / 2;

	private void ThrowIfEmpty()
	{
		if (IsEmpty)
			throw new Exception("Heap is empty!");
	}
}