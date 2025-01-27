using DataStructures.BinaryTree;
using FluentAssertions;

namespace DataStructuresTests;

public class BinarySearchTreeUnitTests
{

	[Fact]
	public void ShouldCorrectAddElement()
	{
		var bst = new BinarySearchTree<int>();
		bst.IsEmpty.Should().BeTrue();

		for (int i = 0; i < 10; i++)
			bst.Add(i);

		bst.IsEmpty.Should().BeFalse();
		bst.Count.Should().Be(10);
	}

	[Fact]
	public void ShouldCorrectRemoveElement()
	{
		var bst = new BinarySearchTree<int>();

		for (int i = 0; i < 10; i++)
			bst.Add(i);

		bst.Remove(3).Should().BeTrue();
		bst.Count.Should().Be(9);
		bst.Search(3).Should().BeFalse();
	}

	[Fact]
	public void ShouldCorrectSearchElement()
	{
		var bst = new BinarySearchTree<int>();

		for (int i = 0; i < 10; i++)
			bst.Add(i);

		for (int i = 10 - 1; i >= 0; i--)
			bst.Search(i).Should().BeTrue();
	}

	[Fact]
	public void ShouldCorrectInOrder()
	{
		var bst = new BinarySearchTree<int>();

		for (int i = 100 - 1; i >= 0; i--)
			bst.Add(i);

		var inOrderShouldBe = Enumerable.Range(0, 100).ToList();
		var inOrderRes = bst.InOrder();

		inOrderShouldBe.Should().BeEquivalentTo(inOrderRes);
	}
}