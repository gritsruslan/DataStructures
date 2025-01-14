



using DataStructures.HashTable;

var hashTable = new MyHashTable<int, string>();

for (int i = 0; i < 10; i++)
{
	hashTable.AddOrUpdate(i, $"Value {i}");
}

for (int i = 0; i < 10; i++)
{
	Console.WriteLine($"{i} : {hashTable.GetValue(i)}");
}
