using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Star : MonoBehaviour {

    public Transform m_rSeeker, m_tTarget;
    PathGridManager m_Grid;



    private void Awake()
    {
        m_Grid = GetComponent<PathGridManager>();
        
    }

    public int GetDistance(Node a, Node b)
    {
        int iDistX = Mathf.Abs(a.m_iGridx - b.m_iGridx);
        int iDistY = Mathf.Abs(a.m_iGridy - b.m_iGridy);

        if (iDistX > iDistY)
            return 14 * iDistY + 10 * (iDistX - iDistY);
        else
            return 14 * iDistX + 10 * (iDistY - iDistX);
    }

    private void Update()
    {
        FindPath(m_rSeeker.transform.position, m_tTarget.transform.position);
    }

    void FindPath(Vector3 start, Vector3 end)
    {
        Node startNode = m_Grid.NodeFromWorldPos(start);
        Node endNode = m_Grid.NodeFromWorldPos(end);

        //List<Node> openSet = new List<Node>();
        MinHeap<Node> openSet = new MinHeap<Node>(m_Grid.GridSize);
        List<Node> closedSet = new List<Node>();

        openSet.Add(startNode);
        //int fCost = 0;
        
        while (openSet.Count> 0)
        {
            Node _currentNode = openSet.RemoveFirstItem();

            //The following was replaced by heap
            //Node _currentNode = openSet[0];
            //for (int i = 1; i < openSet.Count; i++)
            //{
            //    if (openSet[i].m_ifCost < _currentNode.m_ifCost || openSet[i].m_ifCost == _currentNode.m_ifCost)
            //    {
            //        _currentNode = openSet[i];
            //    }
            //}
            //openSet.Remove(_currentNode);

            closedSet.Add(_currentNode);
        

            if (_currentNode == endNode) {

                RetracePath(startNode, endNode);
                return;
            }


            
            foreach (Node neighbour in m_Grid.GetNeighbour(_currentNode))
            {
                if (neighbour.m_isBlocked || closedSet.Contains(neighbour))
                {
                    continue;
                }
                int NewMovementCost = _currentNode.m_iGCost + GetDistance(_currentNode, neighbour);

                if (NewMovementCost < neighbour.m_iGCost || !openSet.Contains(neighbour))
                {

                    neighbour.m_iGCost = NewMovementCost;

                    neighbour.m_iHCost = GetDistance(neighbour, endNode);

                    neighbour.m_Parent = _currentNode;


                    if (!openSet.Contains(neighbour))
                    {

                        openSet.Add(neighbour);
                    }
                    else
                    {
                        // This was added when heap was implemented, Sorts upwards if openset contains neighbour node
                        openSet.UpdateItem(neighbour);
                    }

                }
            }

        }
    }





    void RetracePath(Node start, Node end)
    {

        List<Node> _path = new List<Node>();
        Node _currentNode = end;

        while (_currentNode != start)
        {
            _path.Add(_currentNode);
            _currentNode = _currentNode.m_Parent;
        }
        _path.Reverse();
        m_Grid.m_aPath = _path;

    }

}
