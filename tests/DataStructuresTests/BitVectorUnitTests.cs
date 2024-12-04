using DataStructures.BitVector;
using FluentAssertions;

namespace DataStructuresTests;

public class BitVectorUnitTests
{
	//[]
	[Fact]
	public void ShouldCorrectSetBitAndUnsetByArrayIndex()
	{
		var bv = new BitVector(10);

		for(int i = 0; i < 10; i++)
			bv[i].Should().BeFalse();

		bv[8] = true;

		bv[8].Should().BeTrue();

		for (int i = 0; i < 10; i++)
			bv[i] = true;

		for (int i = 0; i < 10; i++)
			bv[i].Should().BeTrue();

		bv[5] = false;
		bv[5].Should().BeFalse();
	}

	[Theory]
	[MemberData(nameof(GetBitVectorsIndexesData))]
	public void ShouldThrowOrNotExceptionSize(BitVector bitVector, int index, bool shouldThrow)
	{
		if (shouldThrow)
			bitVector.Invoking(bv => bv.IsSetBit(index)).Should().Throw<IndexOutOfRangeException>();
		else
			bitVector.Invoking(bv => bv.IsSetBit(index)).Should().NotThrow();
	}

	public static IEnumerable<object[]> GetBitVectorsIndexesData()
	{
		yield return [new BitVector(1), 0, false];
		yield return [new BitVector(1), 1, true];
		yield return [new BitVector(1488), 1200, false];
		yield return [new BitVector(52), 81, true];
	}

	// SetBit
	[Fact]
	public void ShouldCorrectSetBitByFunction()
	{
		var bv = new BitVector(30);
		bv[12].Should().BeFalse();
		bv.SetBit(12);
		bv[12].Should().BeTrue();
	}

	// UnsetBit
	[Fact]
	public void ShouldCorrectUnsetBit()
	{
		var bv = new BitVector(30);
		bv.SetBit(12);
		bv.UnsetBit(12);
		bv[12].Should().BeFalse();
	}

	// InverseBit
	[Fact]
	public void ShouldCorrectInverseBit()
	{
		var bv = new BitVector(10);
		bv.InverseBit(8);
		bv[8].Should().BeTrue();
		bv.InverseBit(8);
		bv[8].Should().BeFalse();
	}

	// IsSetBit
	[Fact]
	public void ShouldCorrectRecognizeIsSetBit()
	{
		var bv = new BitVector(60);
		bv.InverseBit(52);
		bv.IsSetBit(52).Should().BeTrue();
		bv.InverseBit(52);
		bv.IsSetBit(52).Should().BeFalse();
	}
}