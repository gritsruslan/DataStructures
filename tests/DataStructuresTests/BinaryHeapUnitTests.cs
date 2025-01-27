using DataStructures.BinaryHeap;
using FluentAssertions;

namespace DataStructuresTests;

public class BinaryHeapUnitTests
{
	[Fact]
	public void ShouldCorrectAddElement()
	{
		var bh = new BinaryHeap<int>(HeapType.MaxHeap);

		for (int i = 0; i < 10; i++)
			bh.Add(i);

		bh.Capacity.Should().Be(10);
		bh.GetRoot().Should().Be(9);
	}

	[Fact]
	public void ShouldCorrectCalculateMinRoot()
	{
		var bh = new BinaryHeap<int>(HeapType.MinHeap);

		for (int i = 0; i < 10; i++)
			bh.Add(i);

		bh.GetRoot().Should().Be(0);
	}

	[Fact]
	public void ShouldCorrectCalculateMaxRoot()
	{
		var bh = new BinaryHeap<int>(HeapType.MaxHeap);

		for (int i = 0; i < 10; i++)
			bh.Add(i);

		bh.GetRoot().Should().Be(9);
	}

	[Fact]
	public void ShouldCorrectCalculateHeight()
	{
		var bh = new BinaryHeap<int>(HeapType.MaxHeap);

		for (int i = 0; i < 10; i++)
			bh.Add(i);

		bh.Height.Should().Be(4);
	}


	[Fact]
	public void ShouldCorrectRemoveElement()
	{
		var bh = new BinaryHeap<int>(HeapType.MaxHeap);

		for (int i = 0; i < 10; i++)
			bh.Add(i);

		bh.Remove(3).Should().BeTrue();
		bh.Count.Should().Be(9);
	}

	[Fact]
	public void ShouldCorrectGetAndDeleteRoot()
	{
		var bh = new BinaryHeap<int>(HeapType.MaxHeap);

		for (int i = 0; i < 10; i++)
			bh.Add(i);

		for (int i = 10 - 1; i >= 0; i--)
			bh.GetAndDeleteRoot().Should().Be(i);

		bh.Count.Should().Be(0);
	}
}