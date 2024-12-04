namespace DataStructures.BitVector;

public class BitVector
{
	// Bit container
	private uint[] _data;

	public readonly int Size;

	// Create BitVector with specified size
	public BitVector(int size)
	{
		Size = size;
		int arraySize = (int)Math.Ceiling((float)size / sizeof(uint));
		_data = new uint[arraySize];
	}

	public bool this[int index]
	{
		get => IsSetBit(index);
		set
		{
			if(value)
				SetBit(index);
			else
				UnsetBit(index);
		}
	}

	// Set the bit at the specified index to 1
	public void SetBit(int index)
	{
		ThrowIfIndexOutOfRange(index);

		var (row, column) = GetBitCoordinates(index);

		_data[row] |= (uint) 1 << column;
	}

	// Set the bit at the specified index to 0
	public void UnsetBit(int index)
	{
		ThrowIfIndexOutOfRange(index);

		var (row, column) = GetBitCoordinates(index);

		_data[row] ^= (uint) 1 << column;
	}

	// Inverse the bit at the specified index
	public void InverseBit(int index)
	{
		ThrowIfIndexOutOfRange(index);
		var (row, column) = GetBitCoordinates(index);

		_data[row] &= (uint)~(1 << column);
	}

	// Return true if the bit at specified index is 1, else return false
	public bool IsSetBit(int index)
	{
		ThrowIfIndexOutOfRange(index);
		var (row, column) = GetBitCoordinates(index);

		return (_data[row] & (1 << column)) == 1;
	}

	private (int, int) GetBitCoordinates(int index)
	{
		int row = index / sizeof(uint);
		int column = index % sizeof(uint);
		return (row, column);
	}

	private void ThrowIfIndexOutOfRange(int index)
	{
		if (index < 0 || index >= Size)
			throw new IndexOutOfRangeException($"BitVector size is {Size}, but the element with index {index} is requested");
	}
}