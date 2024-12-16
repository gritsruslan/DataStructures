using System.Collections;
using DataStructures.Stack;
using FluentAssertions;

namespace DataStructuresTests;

public class MyStackUnitTests
{
	// Constructor
	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(25)]
	public void ShouldCorrectCreateWithCapacity(int capacity)
	{
		var st = new MyStack<int>(capacity);
		st.Capacity.Should().Be(capacity);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(-100)]
	public void ShouldThrowExceptionIfCapacityIsWrong(int capacity)
	{
		Action act = () => new MyStack<int>(capacity);
		act.Should().Throw<ArgumentOutOfRangeException>();
	}

	// Resize
	[Theory]
	[InlineData(1, 2)]
	[InlineData(5, 8)]
	[InlineData(100, 150)]
	public void ShouldCorrectResizeArray(int capacity, int nextCapacity)
	{
		var st = new MyStack<int>(capacity);

		for (int i = 0; i < capacity + 1; i++)
			st.Push(i);

		st.Capacity.Should().Be(nextCapacity);
	}

	// Push and Pop
	[Fact]
	public void ShouldCorrectPushAndPop()
	{
		var st = new MyStack<int>();

		for (int i = 0; i < 10; i++)
			st.Push(i);

		for (int i = 9; i >= 0; i--)
		{
			st.Pop().Should().Be(i);
			st.Count.Should().Be(i);
		}

		st.IsEmpty.Should().BeTrue();
	}

	// Peek
	[Fact]
	public void ShouldCorrectPeekElem()
	{
		var st = new MyStack<int>([0, 1, 2, 3, 4, 5]);

		for (int i = 5; i >= 0; i--)
		{
			st.Peek().Should().Be(i);

			if(i != 0)
				st.Pop();
		}
	}

	// TryPop
	[Fact]
	public void ShouldCorrectTryPop()
	{
		var st = new MyStack<int>([1]);

		st.TryPop(out var item1).Should().BeTrue();
		item1.Should().Be(1);

		st.TryPop(out var item2).Should().BeFalse();
		item2.Should().Be(0);
	}

	// TryPeek
	[Fact]
	public void ShouldCorrectTryPeek()
	{
		var st = new MyStack<int>([52]);
		st.TryPeek(out var item1).Should().BeTrue();
		item1.Should().Be(52);

		var st2 = new MyStack<int>();
		st2.TryPeek(out var item2).Should().BeFalse();
		item2.Should().Be(0);
	}

	[Fact]
	//CopeTo
	public void ShouldCorrectCopyTo()
	{
		var array = new int[10];
		var st = new MyStack<int>([0, 1, 2, 3]);

		st.CopyTo(array, 3);

		for (int i = 3; i < 7; i++)
			array[i].Should().Be(7 - i - 1);
	}
}