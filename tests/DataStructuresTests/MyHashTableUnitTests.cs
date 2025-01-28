using DataStructures.HashTable;
using FluentAssertions;

namespace DataStructuresTests;

public class MyHashTableUnitTests
{
    [Fact]
    public void ShouldCorrectAddNewElement()
    {
        var hashTable = new MyHashTable<string, int>();

        hashTable.AddOrUpdate("one", 1);

        hashTable.HasKey("one").Should().BeTrue();
        hashTable.GetValue("one").Should().Be(1);
    }

    [Fact]
    public void ShouldCorrectUpdateElement()
    {
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        hashTable.AddOrUpdate("one", 2);

        hashTable.GetValue("one").Should().Be(2);
    }

    [Fact]
    public void ShouldCorrectReturnValueByKey()
    {
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);
        hashTable.AddOrUpdate("two", 2);

        var value = hashTable.GetValue("two");

        value.Should().Be(2);
    }

    [Fact]
    public void ShouldThrowExceptionWhenKeyDoesNotExist()
    {
        var hashTable = new MyHashTable<string, int>();

        Assert.Throws<ArgumentException>(() => hashTable.GetValue("nonexistent"));
    }

    [Fact]
    public void ShouldCorrectCheckKeyExist()
    {
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        var hasKey = hashTable.HasKey("one");

        hasKey.Should().BeTrue();
    }

    [Fact]
    public void ShouldReturnFalseIfKeyDoesNotExist()
    {
        var hashTable = new MyHashTable<string, int>();

        var hasKey = hashTable.HasKey("nonexistent");

        hasKey.Should().BeFalse();
    }

    [Fact]
    public void ShouldCorrectTryGetValueIfValueExist()
    {
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        var success = hashTable.TryGetValue("one", out var value);

        success.Should().BeTrue();
        value.Should().Be(1);
    }

    [Fact]
    public void ShouldCorrectTryGetValueIfValueDoesNotExist()
    {
        var hashTable = new MyHashTable<string, int>();

        var success = hashTable.TryGetValue("nonexistent", out var value);

        success.Should().BeFalse();
        value.Should().Be(default);
    }

    [Fact]
    public void ShouldCorrectRemoveAnExistingElement()
    {
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        var removed = hashTable.Remove("one");

        removed.Should().BeTrue();
        hashTable.HasKey("one").Should().BeFalse();
    }

    [Fact]
    public void ShouldThrowExceptionIfKeyDoesNotExistWhileRemoving()
    {
        var hashTable = new MyHashTable<string, int>();

        Assert.Throws<ArgumentException>(() => hashTable.Remove("nonexistent"));
    }

    [Fact]
    public void ShouldCorrectExtendCapacity()
    {
        var hashTable = new MyHashTable<int, string>();

        for (int i = 0; i < 10; i++)
            hashTable.AddOrUpdate(i, $"Value {i}");

        var initialCapacity = 5;

        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
        hashTable.GetValue(5).Should().Be("Value 5");
        hashTable.GetValue(9).Should().Be("Value 9");
    }

    [Fact]
    public void ShouldIndexerCorrectWorking()
    {
        var hashTable = new MyHashTable<string, int>();

        hashTable["test"] = 123;
        var value = hashTable["test"];

        value.Should().Be(123);
    }
}