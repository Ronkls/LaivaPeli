using Ga = GA.Collections;
using Xunit;

public class LinkedListTests
{

	[Fact]
	public void AddFirstTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddFirst(1);
		list.AddFirst(2);
		list.AddFirst(3);

		Assert.Equal(new[] { 3, 2, 1 }, list);
		Assert.Equal(3, list.Count);
	}

	[Fact]
	public void AddLastTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);

		Assert.Equal(new[] { 1, 2, 3 }, list);
		Assert.Equal(3, list.Count);
	}

	[Fact]
	public void AddFirstAndAddLastTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddFirst(2);
		list.AddLast(3);
		list.AddFirst(4);
		list.AddLast(5);

		Assert.Equal(new[] { 4, 2, 1, 3, 5 }, list);
		Assert.Equal(5, list.Count);
	}

	[Fact]
	public void AddAfterClearTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);
		list.AddLast(4);

		list.Clear();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);
		list.AddLast(4);

		Assert.Equal(new[] { 1, 2, 3, 4 }, list);
		Assert.Equal(4, list.Count);
	}

	[Fact]
	public void RemoveFirstTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);

		bool removed = list.Remove(1);

		Assert.True(removed);
		Assert.Equal(new[] { 2, 3 }, list);
		Assert.Equal(2, list.Count);
	}

	[Fact]
	public void RemoveMiddleTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);

		Assert.True(list.Remove(2));
		Assert.Equal(new[] { 1, 3 }, list);
		Assert.Equal(2, list.Count);
	}

	[Fact]
	public void RemoveLastTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);

		Assert.True(list.Remove(3));
		Assert.Equal(new[] { 1, 2 }, list);
		Assert.Equal(2, list.Count);
	}

	[Fact]
	public void RemoveOnlyItemTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);

		Assert.True(list.Remove(1));
		Assert.Empty(list);
	}

	[Fact]
	public void RemoveNonExistingItemTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);

		Assert.False(list.Remove(4));
		Assert.Equal(new[] { 1, 2, 3 }, list);
		Assert.Equal(3, list.Count);
	}

	[Fact]
	public void RemoveDuplicateItemTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(2);
		list.AddLast(3);

		Assert.True(list.Remove(2));
		Assert.Equal(new[] { 1, 2, 3 }, list);
		Assert.Equal(3, list.Count);
	}

	[Fact]
	public void RemoveAndAddAfterRemovalTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);

		list.Remove(1);
		list.AddLast(4);
		list.Remove(3);

		Assert.Equal(new[] { 2, 4 }, list);
		Assert.Equal(2, list.Count);
	}

	[Fact]
	public void ContainsItemTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddLast(1);
		list.AddLast(2);
		list.AddLast(3);

		bool containsItem = list.Contains(2);
		bool doesNotContainItem = list.Contains(4);

		Assert.True(containsItem);
		Assert.False(doesNotContainItem);
	}

	[Fact]
	public void AddAndClearTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.AddFirst(1);
		list.AddFirst(2);
		list.AddFirst(3);
		list.AddLast(4);

		list.Clear();

		Assert.Empty(list);
	}

	[Fact]
	public void ClearEmptyListTest()
	{
		Ga.LinkedList<int> list = new Ga.LinkedList<int>();

		list.Clear();

		Assert.Empty(list);
	}
}