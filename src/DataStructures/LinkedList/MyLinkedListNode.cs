namespace DataStructures.LinkedList;

public class MyLinkedListNode<T>(T value, MyLinkedListNode<T>? next = null)
{
	public T Value { get; set; } = value;

	public MyLinkedListNode<T>? Next { get; set; } = next;
}