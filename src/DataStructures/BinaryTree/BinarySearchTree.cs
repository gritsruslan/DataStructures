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

		return Root.Remove(value);
	}

	public int Height()
	{
		if (Root is null)
			return 0;

		return Height() + 1;
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
		return node is null;
	}

	public int Height()
	{
		int leftHeight = 0;
		int rightHeight = 0;

		if (Left is not null)
		{
			leftHeight++;
			leftHeight += Left.Height();
		}

		if (Right is not null)
		{
			rightHeight++;
			rightHeight += Right.Height();
		}

		return Math.Max(rightHeight, leftHeight);
	}

	public List<T> InOrder(List<T> list)
	{
		Left?.InOrder(list);
		list.Add(Value);
		Right?.InOrder(list);

		return list;
	}

	//TODO
	public BinarySearchTreeNode<T>? DeleteNode(T item)
	{
		if (Value.CompareTo(item) == 1)
			return Left?.DeleteNode(item) ?? null;

		if (Value.CompareTo(item) == -1)
			return Right?.DeleteNode(item) ?? null;

		if (Left is null)
			return Right;

		if (Right is null)
			return Left;

		var suggestion = FindSuggestion();

		Value = suggestion.Value;
		Right = Right.DeleteNode(Value);

		return this;
	}

	//TODO
	private BinarySearchTreeNode<T> FindSuggestion()
	{
		var current = Right!;

		while (current.Left != null)
			current = current.Left;

		return current;
	}

	public void Add(T value)
	{
		if (Left is null)
		{
			Left = new BinarySearchTreeNode<T>(value);
			return;
		}

		if (Right is null)
		{
			Right = new BinarySearchTreeNode<T>(value);
			return;
		}

		if(Right.Value.CompareTo(value) == 1)
			Right.Add(value);
		else
			Left.Add(value);
	}

	//TODO
	public void Add2(T value)
	{
		if (Value.CompareTo(value) == 0) {

			if(Left is null)
				Left = new BinarySearchTreeNode<T>(value);
			else Left.Add2(value);

			return;
		}

		if (Right is null)
			Right = new BinarySearchTreeNode<T>(value);
		else
			Right.Add2(value);
	}

	public bool Search(T value)
	{
		if (Left is null && Right is null)
			return false;

		if (Value.CompareTo(value) == 0)
			return true;

		if (Left is not null && Left.Value.CompareTo(value) == 1)
			return Left.Search(value);

		if (Right is not null && Right.Value.CompareTo(value) == 1)
			return Right.Search(value);

		return false;
	}
}