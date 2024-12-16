using System.Collections.Immutable;
using DataStructures.List;
using FluentAssertions;

namespace DataStructuresTests;

public class MyListUnitTests
{

	//DefaultCapacity
	[Fact]
	public void ShouldCreateListWithDefaultCapacity()
	{
		var ls = new MyList<int>();

		ls.Count.Should().Be(0);
		ls.Capacity.Should().Be(5);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(-100)]
	public void ConstructorNegativeOrZeroCapacityShouldThrow(int capacity)
	{
		Action act = () => new MyList<int>(capacity);

		act.Should().Throw<ArgumentOutOfRangeException>();
	}

	// Resize
	[Fact]
	public void ShouldCorrectResizeInternalArray()
	{
		var ls = new MyList<int>(5);

		for (int i = 0; i < 5; i++)
			ls.Add(52);

		ls.Add(100);
		ls.Count.Should().Be(6);

		ls.Capacity.Should().Be(8);
	}

	// foreach
	[Fact]
	public void ShouldCorrectIterateByEnumerator()
	{
		var ls = new MyList<int>(10);
		for (int i = 0; i < 10; i++)
			ls.Add(i);

		int index = 0;

		foreach (var item in ls)
			item.Should().Be(index++);
	}

	// Add
	[Fact]
	public void ShouldCorrectAddElements()
	{
		var ls = new MyList<int>();

		for (int i = 0; i < 10; i++)
		{
			ls.Add(i);
		}

		ls.Count.Should().Be(10);
		ls[0].Should().Be(0);
		ls[9].Should().Be(9);
	}

	// Clear
	[Fact]
	public void ShouldCorrectClearList()
	{
		MyList<int> ls = [1, 2, 3, 4];
		ls.Clear();
		ls.Count.Should().Be(0);
		ls.Invoking(ls => ls[0]).Should().Throw<IndexOutOfRangeException>();
	}

	// Contains
	[Fact]
	public void ShouldCorrectFindOutIsThereAnElementInList()
	{
		MyList<int> ls = [1, 2, 3, 4];
		ls.Contains(0).Should().Be(false);
		ls.Contains(4).Should().Be(true);
	}

	// Remove
	[Fact]
	public void ShouldCorrectRemoveElement()
	{
		MyList<int> ls = [1, 2, 3, 4];

		ls.Remove(0).Should().Be(false);
		ls.Count.Should().Be(4);

		ls.Remove(2).Should().Be(true);
		ls.Count.Should().Be(3);
	}

	// IndexOf
	[Fact]
	public void ShouldCorrentFindOutIndexOfElement()
	{
		MyList<int> ls = [1, 2, 3, 4, 5];

		ls.IndexOf(5).Should().Be(4);
		ls.IndexOf(0).Should().Be(-1);
	}

	// Insert
	[Fact]
	public void ShouldCorrectInsertElement()
	{
		MyList<int> ls = [1, 2, 3, 4, 5];

		ls.Insert(2, 52);
		ls[2].Should().Be(52);
		ls[3].Should().Be(3);
		ls.Count.Should().Be(6);

		ls.Insert(0, 100);
		ls[0].Should().Be(100);
		ls.Count.Should().Be(7);
	}

	//CopyTo
	[Fact]
	public void ShouldCorrectCopyTo()
	{
		var arr = new int[10];
		var ls = new MyList<int>(5);

		for (int i = 0; i < 5; i++)
			ls.Add(i);

		ls.CopyTo(arr, 5);

		for (int i = 5; i < arr.Length; i++)
			arr[i].Should().Be(i - 5);
	}


	//RemoveAt
	[Fact]
	public void ShouldCorrectRemoveElementAtIndex()
	{
		MyList<int> ls = [1, 2, 3, 4, 5];

		ls.Invoking(ls => ls.RemoveAt(-1)).Should().Throw<IndexOutOfRangeException>();
		ls.Count.Should().Be(5);

		ls.RemoveAt(1);
		ls.Count.Should().Be(4);
		ls[1].Should().Be(3);
	}
}