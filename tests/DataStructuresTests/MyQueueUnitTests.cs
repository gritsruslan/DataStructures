using DataStructures.Queue;
using FluentAssertions;

namespace DataStructuresTests;

public class MyQueueUnitTests
{
	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(25)]
	public void ShouldCorrectCreateWithCapacity(int capacity)
	{
		var qe = new MyQueue<int>(capacity);
		qe.Capacity.Should().Be(capacity);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(-100)]
	public void ShouldThrowExceptionIfCapacityIsWrong(int capacity)
	{
		Action act = () => new MyQueue<int>(capacity);
		act.Should().Throw<ArgumentOutOfRangeException>();
	}


	// Enqueue
	[Fact]
	public void ShouldCorrectEnqueueWithResize()
	{
		var qe = new MyQueue<int>(5);

		for (int i = 0; i < 6; i++)
			qe.Enqueue(i);

		qe.Capacity.Should().BeGreaterThan(5);
		qe.Count.Should().Be(6);
	}

	// Dequeue

	[Fact]
	public void ShouldCorrectEnqueue()
	{
		MyQueue<int> qe = new[]{ 0, 1, 2, 3, 4 };

		for (int i = 0; i < 5; i++)
			qe.Dequeue().Should().Be(i);

		qe.Count.Should().Be(0);
	}

	[Fact]
	public void ShouldCorrectTryPeek()
	{
		MyQueue<int> qe = new[] { 52 };
		qe.TryPeek(out var item1).Should().BeTrue();
		item1.Should().Be(52);

		MyQueue<int> qe2 = new MyQueue<int>();
		qe2.TryPeek(out var item2).Should().BeFalse();
		item2.Should().Be(0);
	}

	// TryDequeue
	[Fact]
	public void ShouldCorrectTryDequeue()
	{
		MyQueue<int> qe = new[] { 52 };
		qe.TryDequeue(out var item1).Should().BeTrue();
		item1.Should().Be(52);
		qe.IsEmpty.Should().BeTrue();

		MyQueue<int> qe2 = new MyQueue<int>();
		qe2.TryDequeue(out var item2).Should().BeFalse();
		item2.Should().Be(0);
	}


	// enumerator
	[Fact]
	public void EnumeratorShouldCorrectWorks()
	{
		MyQueue<int> qe = new[] { 0, 1, 2, 3, 4, 5 };

		int index = 0;
		foreach (var item in qe)
			item.Should().Be(index++);

	}
}