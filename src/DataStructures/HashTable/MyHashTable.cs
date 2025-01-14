using DataStructures.LinkedList;

namespace DataStructures.HashTable;

public class MyHashTable<TKey, TValue> where TKey : notnull
{

	private MyLinkedList<KeyAndValue>?[] _values;

	public int Capacity => _values.Length;

	private const int DefaultCapacity = 5;

	private const float Factor = 0.75f;
	public int Count { get; private set; }

	private int GetIndex(int hash) => Math.Abs(hash) % Capacity;

	private float GetPercentageOfOccupancy() =>  (float) Count / Capacity;

	private const string InvalidKeyMessage = "There are no value with such a key in hash-table.";

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
		var linkedList = _values[index];

		if (linkedList is null)
		{
			_values[index] = new MyLinkedList<KeyAndValue>();
			_values[index]!.Add(new KeyAndValue(key, value));
			return;
		}

		foreach (var kv in linkedList)
		{
			if (kv.Key.Equals(key))
			{
				kv.Value = value;
				return;
			}
		}

		Count++;
		_values[index]!.Add(new KeyAndValue(key, value));
	}

	public TValue GetValue(TKey key)
	{
		var index = GetIndex(key.GetHashCode());
		var linkedList = _values[index] ?? throw new ArgumentException(InvalidKeyMessage);

		foreach (var keyAndValue in linkedList)
		{
			if (key.Equals(keyAndValue.Key))
				return keyAndValue.Value;
		}

		throw new ArgumentException(InvalidKeyMessage);
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
		var linkedList = _values[index]
		                 ?? throw new ArgumentException(InvalidKeyMessage);
		var result = linkedList.Remove(keyAndValue => keyAndValue.Key.Equals(key));

		if (result) Count--;

		return result;
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

		foreach (var linkedList in oldValues)
		{
			if(linkedList is null)
				continue;

			foreach (var keyAndValue in linkedList)
				AddOrUpdate(keyAndValue.Key, keyAndValue.Value);
		}
	}

	public class KeyAndValue(TKey key, TValue value)
	{
		public TKey Key { get; } = key;

		public TValue Value { get; set; } = value;
	}
}