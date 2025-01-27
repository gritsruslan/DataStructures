namespace DataStructures.BinaryTree;

public class BinarySearchTree<T>
	where T : IComparable<T>
{
	private BinarySearchTreeNode<T>? Root { get; set; }

	public int Count { get; private set; }

	public bool IsEmpty => Count == 0;

	public void Add(T value)
	{
		if (Root is null)
			Root = new BinarySearchTreeNode<T>(value);
		else
			Root.Add(value);

		Count++;
	}

	public bool Search(T value)
	{
		if (Root is null)
			return false;

		return Root.Search(value);
	}

	public bool Remove(T value)
	{
		if (Root is null)
			return false;

		Count--;
		return Root.Remove(value);
	}

	public List<T> InOrder()
	{
		if (Root is null)
			return [];

		return Root.InOrder(new List<T>(Count));
	}
}

public class BinarySearchTreeNode<T>
	where T : IComparable<T>
{
	public T Value { get; private set; }

	private BinarySearchTreeNode<T>? Left { get; set; }

	private BinarySearchTreeNode<T>? Right { get; set; }


	public BinarySearchTreeNode(T value)
	{
		Value = value;
	}

	public bool Remove(T item)
	{
		var node = DeleteNode(item);
		return node is not null;
	}

	public List<T> InOrder(List<T> list)
	{
		Left?.InOrder(list);
		list.Add(Value);
		Right?.InOrder(list);

		return list;
	}


	public BinarySearchTreeNode<T>? DeleteNode(T value)
	{
		if (Value.CompareTo(value) > 0)
			Left = Left?.DeleteNode(value) ?? null;
		else if (Value.CompareTo(value) < 0)
			Right = Right?.DeleteNode(value) ?? null;
		else
		{
			if (Left is null)
				return Right;

			if (Right is null)
				return Left;

			var suggestion = FindSuggestion();
			Value = suggestion.Value;
			Right = Right.DeleteNode(Value);
		}

		return this;
	}

	private BinarySearchTreeNode<T> FindSuggestion()
	{
		var current = Right!;

		while (current.Left != null)
			current = current.Left;

		return current;
	}

	public void Add(T value)
	{
		if (value.CompareTo(Value) > 0)
		{
			if(Right is null)
				Right = new BinarySearchTreeNode<T>(value);
			else
				Right.Add(value);
		}
		else
		{
			if (Left is null)
				Left = new BinarySearchTreeNode<T>(value);
			else
				Left.Add(value);
		}
	}

	public bool Search(T value)
	{
		if (value.CompareTo(Value) == 0)
			return true;

		if (Left is null && Right is null)
			return false;

		if (value.CompareTo(Value) > 0)
			return Right?.Search(value) ?? false;

		return Left?.Search(value) ?? false;
	}
}