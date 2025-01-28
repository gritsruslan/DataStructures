

using DataStructures.BinaryTree;
using DataStructures.HashTable;

var bst = new BinarySearchTree<int>();

for (int i = 0; i < 5; i++)
	bst.Add(i);

Stack<int> c = new Stack<int>();
c.Push(1);

for (int i = 5 - 1; i >= 0; i--)
{
	Console.WriteLine($"{i} : {bst.Search(i)}");
}


