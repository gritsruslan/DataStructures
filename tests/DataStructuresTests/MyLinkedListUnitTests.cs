using DataStructures.LinkedList;
using FluentAssertions;

namespace DataStructuresTests;

public class MyLinkedListUnitTests
{

	//Constructor
	[Fact]
	public void ShouldCorrectCreate()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];

		ml.Count.Should().Be(6);
		ml.First.Should().Be(1);
		ml.Last.Should().Be(6);
	}

	//AddFirst
	[Fact]
	public void ShouldCorrectAddFirst()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];

		ml.AddFirst(52);

		ml.First.Should().Be(52);
		ml.Count.Should().Be(7);

		MyLinkedList<int> ml2 = [1,2,3,4,5,6];
		ml2.AddFirst(52);
		ml.First.Should().Be(52);
		ml.Last.Should().Be(6);
		ml.Count.Should().Be(7);
	}

	//AddLast
	[Fact]
	public void ShouldCorrectAddLast()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];

		ml.AddLast(52);

		ml.Last.Should().Be(52);
		ml.Count.Should().Be(7);

		MyLinkedList<int> ml2 = [1,2,3,4,5,6];
		ml2.AddLast(52);
		ml.First.Should().Be(1);
		ml.Last.Should().Be(52);
		ml.Count.Should().Be(7);
	}

	//AddAfter
	[Fact]
	public void ShouldCorrectAddAfter()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];
		ml.AddAfter(4, 10);

		ml.Count.Should().Be(7);
		ml.Contains(5).Should().BeTrue();
	}

	//AddBefore
	[Fact]
	public void ShouldCorrectAddBefore()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];
		ml.AddAfter(4, 10);

		ml.Count.Should().Be(7);
		ml.Contains(10).Should().BeTrue();
	}


	//RemoveFirst
	[Fact]
	public void ShouldCorrectRemoveFirst()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];
		ml.RemoveFirst();
		ml.First.Should().Be(2);
		ml.Count.Should().Be(5);
	}

	//RemoveLast
	public void ShouldCorrectRemoveLast()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];
		ml.RemoveLast();
		ml.Last.Should().Be(5);
		ml.Count.Should().Be(5);
	}

	//Remove
	[Fact]
	public void ShouldCorrectRemove()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];
		ml.Remove(3).Should().BeTrue();
		ml.Count.Should().Be(5);

		ml.Remove(52).Should().BeFalse();
		ml.Count.Should().Be(5);
	}

	//Contains
	[Fact]
	public void ShouldCorrectContains()
	{
		MyLinkedList<int> ml = [1,2,3,4,5,6];
		ml.Contains(3).Should().BeTrue();
		ml.Contains(52).Should().BeFalse();
	}

	//CopyTo
	[Fact]
	public void ShouldCorrectCopyTo()
	{
		var arr = new int[10];

		MyLinkedList<int> ml = [0,1,2,3,4,5];

		ml.CopyTo(arr, 3);

		for (int i = 3; i < 3 + ml.Count; i++)
			arr[i].Should().Be(i - 3);
	}
}