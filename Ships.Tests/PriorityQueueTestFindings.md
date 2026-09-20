# PriorityQueue Unit Test Findings

## Overview

Here a some findings that i found after making / implementing the unit test for the **PriorityQueue**.

## Tests

The test that i made cover:

- A newly created queue has a count of zero.
- **Enqueue** increases **Count**.
- A single item queue can be peeked and dequeued.
- **Duplicate** values are handled correctly.
- **Negative** and **zero** values are handled correctly.
- **Peek** returns the highest priority item without removing it.
- **Dequeue** returns items in ascending priority order.
- **Enqueue** correctly maintains priority when items are inserted in an unsorted order.
- **Clear** removes all items and resets the count.
- **Contains** correctly identifies present and absent values.
- **IsConsistent** reports a valid heap as consistent.
- The queue remains consistent after enqueue and dequeue operations.
- Dequeuing all items eventually leaves the queue empty.
- **Peek** on an empty queue throws **InvalidOperationException**.
- **Dequeue** on an empty queue throws **InvalidOperationException**.

## Findings

Didn't find a whole lot of bugs or defects in the current implementation infact i found none. In almost every test i tested if the queue remained consistent and it sure did even after trying to just enqueue and dequeue a bunch of times didn't break anything.

**Negative** and **Zero** values also worked perfectly correctly with the negative values being at the front and then zero values being behind them.

I also tried to use **-0** as value in a test but that also didn't break anything. After googling it, **-0** is just the same as positive **0**. 

**Peek()** and **Dequeue()** also throw their InvalidOperationException correctly when trying to access these methods with an empty queue.

I did have one test that used **int.Maxvalue** and **int.Minvalue** but also didn't break anything and it worked perfectly so i just scrapped that test.

Im writing these findings as im doing testing and im doing a weird deep dive into **IEEE-754** floating-point representation. For double and float values, -0.0 and +0.0 have different **IEEE-754** representations in memory because their sign bits differ but they are considered numerically equal and C#'s CompereTo treats them as equal so they would have the same priority in a queue.

So in weird way how the current PriorityQueue implementation does have one problem and that is that it does not consider if a floating-point number 0 is positive or negative but we would need to compare the underlying bit representations and i dont think we care about that at all.

The only things that did break were the test that i made **:P**

## Notes

- No changes were made to PriorityQueue.cs.
- Every test was done on an int PriorityQueue.
