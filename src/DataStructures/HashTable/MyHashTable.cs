using DataStructures.LinkedList;

namespace DataStructures.HashTable;

public class MyHashTable<TKey, TValue> where TKey : notnull
{

	private MyLinkedList<KeyAndValue>?[] _values;

	private int Capacity => _values.Length;

	private const int DefaultCapacity = 5;

	private const float Factor = 0.75f;
	public int Count { get; private set; }

	private int GetIndex(int hash) => hash % Capacity;

	private float GetPercentageOfOccupancy() =>  (float) Capacity / Count;

	public MyHashTable()
	{
		_values = new MyLinkedList<KeyAndValue>[DefaultCapacity];
	}

	public TValue this[TKey key]
	{
		get => GetValue(key);
		set => AddOrUpdate(key, value);
	}


	public void AddOrUpdate(TKey key, TValue value)
	{
		if(GetPercentageOfOccupancy() > Factor)
			ExtendCapacity();

		var index = GetIndex(key.GetHashCode());
		var keyAndValue = new KeyAndValue(key, value);

		if (_values[index] is null)
		{
			_values[index] = new MyLinkedList<KeyAndValue>();
			_values[index]!.Add(keyAndValue);
			return;
		}

		_values[index]!.Add(keyAndValue);
	}

	public TValue GetValue(TKey key)
	{
		var index = GetIndex(key.GetHashCode());
		var linkedList = _values[index] ?? throw new Exception();

		foreach (var keyAndValue in linkedList)
		{
			if (key.Equals(keyAndValue.Key))
				return keyAndValue.Value;
		}

		throw new Exception();  // TODO
	}

	public bool HasKey(TKey key) => TryGetValue(key, out _);

	public bool TryGetValue(TKey key, out TValue value)
	{
		var index = GetIndex(key.GetHashCode());
		var linkedList = _values[index];

		if (linkedList is null)
			goto NoElement;

		foreach (var keyAndValue in linkedList)
		{
			if (key.Equals(keyAndValue.Key))
			{
				value = keyAndValue.Value;
				return true;
			}
		}

		NoElement :
		value = default!;
		return false;
	}

	public bool Remove(TKey key)
	{
		var index = GetIndex(key.GetHashCode());
		var linkedList = _values[index] ?? throw new Exception(); // TODO
		return linkedList.Remove(keyAndValue => keyAndValue.Key.Equals(key));
	}

	private void ExtendCapacity()
	{
		if(Capacity == Array.MaxLength)
			return;

		var newCapacity = (uint) (Capacity + Capacity / 2);

		if (newCapacity > Array.MaxLength)
			newCapacity = (uint) Array.MaxLength;

		var oldValues = _values;
		_values = new MyLinkedList<KeyAndValue>[newCapacity];
		Count = 0;

		foreach (var linkedList in _values)
		{
			if(linkedList is null)
				continue;

			foreach (var keyAndValue in linkedList)
				AddOrUpdate(keyAndValue.Key, keyAndValue.Value);
		}
	}

	public readonly struct KeyAndValue(TKey key, TValue value)
	{
		public TKey Key { get; } = key;

		public TValue Value { get; } = value;
	}
}