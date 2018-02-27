using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGridManager : MonoBehaviour {

	//public Transform m_tPlayerTransform;
	public LayerMask m_ObstacleMask;
	public Vector2 m_vGridSize;
    public List<Node> m_aPath;
    public float m_fHalfNodeWidth;
    public List<Node> _neighbours;
    public Node _playerNode;
	Node[,] m_aGrid;

	float m_fNodeWidth;
	int m_iNumNodesX, m_iNumNodesY;

	private void Awake()
	{
		// How many nodes are in the grid?
		m_fNodeWidth = m_fHalfNodeWidth * 2;
		m_iNumNodesX = Mathf.RoundToInt(m_vGridSize.x / m_fNodeWidth);
		m_iNumNodesY = Mathf.RoundToInt(m_vGridSize.y / m_fNodeWidth);

        CreateGrid();
	}

	void CreateGrid()
	{
		// Create Grid
		m_aGrid = new Node[m_iNumNodesX, m_iNumNodesY];
		Vector3 vGridBotLeft = transform.position - (Vector3.right * m_vGridSize.x / 2) - (Vector3.forward * m_vGridSize.y / 2);

		// Detect if node is blocked
		for (int x = 0; x < m_iNumNodesX; ++x)
		{
			for (int y = 0; y < m_iNumNodesY; ++y)
			{
				Vector3 vNodePos = vGridBotLeft + Vector3.right * (x * m_fNodeWidth + m_fHalfNodeWidth) + Vector3.forward * (y * m_fNodeWidth + m_fHalfNodeWidth);
				bool bIsBlocked = (Physics.CheckSphere(vNodePos, m_fHalfNodeWidth, m_ObstacleMask));

				m_aGrid[x, y] = new Node(bIsBlocked, vNodePos,x,y);
			}
		}
        
    }

    public int GridSize
    {
        get
        {
            return (int)m_vGridSize.x * (int)m_vGridSize.y;
        }
    }

    public Node NodeFromWorldPos(Vector3 vWorldPos)
	{
		float fX = (vWorldPos.x + m_vGridSize.x / 2) / m_vGridSize.x;
		float fY = (vWorldPos.z + m_vGridSize.y / 2) / m_vGridSize.y;

		fX = Mathf.Clamp01(fX);
		fY = Mathf.Clamp01(fY);

		int x = Mathf.RoundToInt((m_iNumNodesX - 1) * fX);
		int y = Mathf.RoundToInt((m_iNumNodesY - 1) * fY);

		return m_aGrid[x, y];
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(m_vGridSize.x, 1, m_vGridSize.y));
		if (m_aGrid != null)
		{
			foreach (Node node in m_aGrid)
			{
				Gizmos.color = (node.m_isBlocked) ? Color.red: Color.white;

                
                if (m_aPath != null)
                {
                    if (m_aPath.Contains(node)) Gizmos.color = Color.blue;
                }

                Gizmos.DrawWireCube(node.m_vPosition, Vector3.one * (m_fNodeWidth - .1f));
			}
            
        }
        
    }


    

    public List<Node> GetNeighbour(Node node)
    {
        List<Node> Nodes = new List<Node>();
        Node _playerNode = node;
        Node neighbour;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;

                int iCheckX = _playerNode.m_iGridx + i;
                int iCheckY = _playerNode.m_iGridy + j;

                if (iCheckX >= 0 && iCheckX < m_vGridSize.x && iCheckY >= 0 && iCheckY < m_vGridSize.y)
                {
                    

                    neighbour = m_aGrid[iCheckX, iCheckY];
                    Nodes.Add(neighbour);
                }
            }

        }
        _neighbours = Nodes;
        return _neighbours;
    }



}
