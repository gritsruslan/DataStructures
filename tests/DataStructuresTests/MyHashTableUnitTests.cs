using DataStructures.HashTable;
using FluentAssertions;

namespace DataStructuresTests;

public class MyHashTableUnitTests
{
    [Fact]
    public void AddOrUpdate_AddsNewElement_WhenKeyIsNew()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();

        // Act
        hashTable.AddOrUpdate("one", 1);

        // Assert
        hashTable.HasKey("one").Should().BeTrue();
        hashTable.GetValue("one").Should().Be(1);
    }

    [Fact]
    public void AddOrUpdate_UpdatesExistingElement_WhenKeyExists()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        // Act
        hashTable.AddOrUpdate("one", 2);

        // Assert
        hashTable.GetValue("one").Should().Be(2);
    }

    [Fact]
    public void GetValue_ReturnsCorrectValue_WhenKeyExists()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);
        hashTable.AddOrUpdate("two", 2);

        // Act
        var value = hashTable.GetValue("two");

        // Assert
        value.Should().Be(2);
    }

    [Fact]
    public void GetValue_ThrowsException_WhenKeyDoesNotExist()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => hashTable.GetValue("nonexistent"));
    }

    [Fact]
    public void HasKey_ReturnsTrue_WhenKeyExists()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        // Act
        var hasKey = hashTable.HasKey("one");

        // Assert
        hasKey.Should().BeTrue();
    }

    [Fact]
    public void HasKey_ReturnsFalse_WhenKeyDoesNotExist()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();

        // Act
        var hasKey = hashTable.HasKey("nonexistent");

        // Assert
        hasKey.Should().BeFalse();
    }

    [Fact]
    public void TryGetValue_ReturnsTrueAndCorrectValue_WhenKeyExists()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        // Act
        var success = hashTable.TryGetValue("one", out var value);

        // Assert
        success.Should().BeTrue();
        value.Should().Be(1);
    }

    [Fact]
    public void TryGetValue_ReturnsFalseAndDefaultValue_WhenKeyDoesNotExist()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();

        // Act
        var success = hashTable.TryGetValue("nonexistent", out var value);

        // Assert
        success.Should().BeFalse();
        value.Should().Be(default);
    }

    [Fact]
    public void Remove_ReturnsTrueAndRemovesElement_WhenKeyExists()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();
        hashTable.AddOrUpdate("one", 1);

        // Act
        var removed = hashTable.Remove("one");

        // Assert
        removed.Should().BeTrue();
        hashTable.HasKey("one").Should().BeFalse();
    }

    [Fact]
    public void Remove_ThrowsException_WhenKeyDoesNotExist()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => hashTable.Remove("nonexistent"));
    }

    [Fact]
    public void ExtendCapacity_ExtendsCapacityAndRehashesElements()
    {
        // Arrange
        var hashTable = new MyHashTable<int, string>();

        for (int i = 0; i < 10; i++)
            hashTable.AddOrUpdate(i, $"Value {i}");

        var initialCapacity = 5;

        // Act & Assert
        hashTable.Capacity.Should().BeGreaterThan(initialCapacity);
        hashTable.GetValue(5).Should().Be("Value 5");
        hashTable.GetValue(9).Should().Be("Value 9");
    }

    [Fact]
    public void Indexer_SetAndGetValues()
    {
        // Arrange
        var hashTable = new MyHashTable<string, int>();

        // Act
        hashTable["test"] = 123;
        var value = hashTable["test"];

        // Assert
        value.Should().Be(123);
    }
}