



using DataStructures.HashTable;

var hashTable = new MyHashTable<string, int>();

hashTable.AddOrUpdate("one", 1);
hashTable.AddOrUpdate("one", 2);

Console.WriteLine(hashTable["one"]);
