using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MinHeap<T> where T: IHeapItem<T> {

    T[] items;
    int itemCount;

    public MinHeap(int _gridSize)
    {
        items = new T[_gridSize];
    }


    /// <summary>
    /// Adds item to heap
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {

        item.hIndex = itemCount;
        items[itemCount] = item;
        SortUp(item);
        itemCount++;

    }

    /// <summary>
    /// Removes first item in heap, afterwards sorts downwards and returns new first item
    /// </summary>
    /// <returns></returns>
    public T RemoveFirstItem()
    {
        T firstItem = items[0];
        itemCount--;
        items[0] = items[itemCount];
        items[0].hIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }


    public void UpdateItem(T item)
    {
        SortUp(item);

    }


    private void SortUp(T item)
    {
        int parentIndex = (item.hIndex - 1) / 2;

        while (true)
        {
            T parent = items[parentIndex];
            if (item.CompareTo(parent) > 0)
            {
                Swap(item,parent);
            }else
            {
                break;
            }
        }
        parentIndex = (item.hIndex - 1) / 2;
    }





    /// <summary>
    /// Swaps indexes between 2 items 
    /// </summary>
    /// <param name="itemA"></param>
    /// <param name="itemB"></param>
    private void Swap(T itemA,T itemB)
    {
        items[itemA.hIndex] = itemB;
        items[itemB.hIndex] = itemA;

        int tempIndex = itemA.hIndex;

        itemA.hIndex = itemB.hIndex;
        itemB.hIndex = tempIndex;
    }

    //For getting item count
    public int Count
    {
        get
        {
            return itemCount;
        }
    }

    //Checks if the items are same
    public bool Contains(T item)
    {
        return Equals(items[item.hIndex], item);
    }



    /// <summary>
    /// Sorts items down after calculating left and right child values
    /// </summary>
    /// <param name="item"></param>
    private void SortDown(T item)
    {
        while(true)
        {
            int leftChildIndex = item.hIndex * 2 + 1;
            int rightChildIndex = item.hIndex * 2 + 2;
            int swapIndex = 0;

            if (leftChildIndex< itemCount)
            {
                swapIndex = leftChildIndex;

                if (rightChildIndex < itemCount)
                {
                    if (items[leftChildIndex].CompareTo(items[rightChildIndex])<0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }


                if (item.CompareTo(items[swapIndex])<0)
                {
                    Swap(item, items[swapIndex]);
                } else { return; }

            }
            else
            {
                return;
            }
        }
    }


}
