using Ga = GA.Collections;
using Xunit;

public class PriorityQueueTests
{

    [Fact]
    public void NewQueueIsEmptyTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsConsistent());
    }

    [Fact]
    public void EnqueueIncreaseCountTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Assert.Equal(3, queue.Count);
    }

    [Fact]
    public void SingleItemQueueTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(69);

        Assert.Equal(1, queue.Count);
        Assert.Equal(69, queue.Peek());
        Assert.Equal(69, queue.Dequeue());
        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsConsistent());
    }

    [Fact]
    public void DuplicateValueTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(5);
        queue.Enqueue(3);
        queue.Enqueue(3);
        queue.Enqueue(1);
        queue.Enqueue(5);

        Assert.True(queue.IsConsistent());

        Assert.Equal(1, queue.Dequeue());
        Assert.Equal(3, queue.Dequeue());
        Assert.Equal(3, queue.Dequeue());
        Assert.Equal(5, queue.Dequeue());
        Assert.Equal(5, queue.Dequeue());

        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void NegativeAndZeroValueTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(0);
        queue.Enqueue(-3);
        queue.Enqueue(10);
        queue.Enqueue(-1);
        queue.Enqueue(-0);

        Assert.True(queue.IsConsistent());

        Assert.Equal(-3, queue.Dequeue());
        Assert.Equal(-1, queue.Dequeue());
        Assert.Equal(-0, queue.Dequeue());
        Assert.Equal(0, queue.Dequeue());
        Assert.Equal(10, queue.Dequeue());

        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void EnqueueMaintainPriorityTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(10);

        Assert.True(queue.IsConsistent());

        Assert.Equal(1, queue.Dequeue());
        Assert.Equal(2, queue.Dequeue());
        Assert.Equal(3, queue.Dequeue());
        Assert.Equal(4, queue.Dequeue());
        Assert.Equal(10, queue.Dequeue());

        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void PeekReturnsHighestPriorityItemWithoutRemovingItTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(3);
        queue.Enqueue(1);
        queue.Enqueue(2);


        Assert.Equal(1, queue.Peek());
        Assert.Equal(3, queue.Count);
        Assert.Equal(1, queue.Peek());
    }

    [Fact]
    public void DequeueReturnsItemsInPriorityOrderTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(5);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(1);
        queue.Enqueue(2);

        Assert.Equal(1, queue.Dequeue());
        Assert.Equal(2, queue.Dequeue());
        Assert.Equal(3, queue.Dequeue());
        Assert.Equal(4, queue.Dequeue());
        Assert.Equal(5, queue.Dequeue());

        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void ClearRemovesAllItemsTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        queue.Clear();

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsConsistent());
        Assert.False(queue.Contains(1));
        Assert.False(queue.Contains(2));
        Assert.False(queue.Contains(3));
    }

    [Fact]
    public void ClearEmptyQueueTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Clear();

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsConsistent());
    }

    [Fact]
    public void ContainsExistingAndMissingItemsTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Assert.True(queue.Contains(1));
        Assert.True(queue.Contains(2));
        Assert.True(queue.Contains(3));
        Assert.False(queue.Contains(4));
    }

    [Fact]
    public void ContainsAfterDequeueTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        queue.Dequeue();

        Assert.False(queue.Contains(1));
        Assert.True(queue.Contains(2));
        Assert.True(queue.Contains(3));
    }

    [Fact]
    public void QueueRemainsConsistentTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(10);
        Assert.True(queue.IsConsistent());

        queue.Enqueue(-1);
        Assert.True(queue.IsConsistent());

        queue.Enqueue(6);
        Assert.True(queue.IsConsistent());

        queue.Enqueue(5);
        Assert.True(queue.IsConsistent());

        queue.Dequeue();
        Assert.True(queue.IsConsistent());

        queue.Enqueue(1);
        Assert.True(queue.IsConsistent());

        queue.Dequeue();
        Assert.True(queue.IsConsistent());

        queue.Dequeue();
        Assert.True(queue.IsConsistent());
    }

    [Fact]
    public void EnqueueAfterClearTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(2);
        queue.Enqueue(1);
        queue.Enqueue(3);

        queue.Clear();

        queue.Enqueue(6);
        queue.Enqueue(4);
        queue.Enqueue(5);

        Assert.Equal(4, queue.Dequeue());
        Assert.Equal(5, queue.Dequeue());
        Assert.Equal(6, queue.Dequeue());

        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void DequeueAllItemsLeavesQueueEmptyTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        queue.Enqueue(5);
        queue.Enqueue(1);
        queue.Enqueue(4);
        queue.Enqueue(2);
        queue.Enqueue(3);

        queue.Dequeue();
        queue.Dequeue();
        queue.Dequeue();
        queue.Dequeue();
        queue.Dequeue();

        Assert.Equal(0, queue.Count);
        Assert.True(queue.IsConsistent());
    }

    [Fact]
    public void InteractingWithAnEmptyListThrowsExeptionsTest()
    {
        Ga.PriorityQueue<int> queue = new Ga.PriorityQueue<int>();

        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        Assert.Throws<InvalidOperationException>(() => queue.Peek());
    }
}