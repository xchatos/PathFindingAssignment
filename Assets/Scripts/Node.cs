using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {

    public bool m_isBlocked;
    public Vector3 m_vPosition;

    public int m_iGridx;
    public int m_iGridy;

    public int m_iGCost;
    public int m_iHCost;

    public Node m_Parent;

    private int heapIndex;

    public int m_ifCost
    {
        get { return m_iGCost + m_iHCost; }
    }

    public int hIndex
    {
        get
        {
            return heapIndex;
        }

        set
        {
            heapIndex = value;
        }
    }

    public Node(bool isBlocked, Vector3 vPosition, int x, int y)
    {
        m_isBlocked = isBlocked;
        m_vPosition = vPosition;
        m_iGridx = x;
        m_iGridy = y;
    }

    /// <summary>
    /// Compares the sent nodes f cost
    /// </summary>
    /// <param name="nodeToCompare"></param>
    /// <returns></returns>
    public int CompareTo(Node nodeToCompare)
    {
        int comparison = m_ifCost.CompareTo(nodeToCompare.m_ifCost);
        if ( comparison == 0)
        {
            comparison = m_iHCost.CompareTo(nodeToCompare.m_iHCost);
        }
        return -comparison;
    }
}
