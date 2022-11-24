using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathAlgorithm : MonoBehaviour
{
    AIManager _aIManager;
    [SerializeField] List<Vector2> _busyNodes = new List<Vector2>();
    public void Init(AIManager aIManager, List<AIController> aIControllers)
    {
        _aIManager = aIManager;
        AddAllStartPositions(aIControllers);
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 finalPos)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Vector2> path = new List<Vector2>();

        Node start = new Node(new Vector2(Mathf.Round(startPos.x), Mathf.Round(startPos.y)));
        Node end = new Node(new Vector2(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y)));

        if (FindNeighbours(start.Position).Contains(end.Position)) return new List<Vector2>();

        if (start.Position == end.Position) return new List<Vector2>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            Node currentNode = openList.OrderBy(x => x.H).FirstOrDefault();

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode.Position == end.Position)
            {
                path.Reverse();
                return path;
            }

            List<Node> neighbours = new List<Node>();

            neighbours = FindNeighbours(currentNode);

            foreach (Node node in neighbours)
            {
                if (closedList.Contains(node)) continue;

                node.H = GetManhattenDistance(end, node);

                if (!openList.Contains(node))
                {
                    openList.Add(node);
                }
            }

            Node pathNode = neighbours.OrderBy(x => x.H).FirstOrDefault();

            path.Add(pathNode.Position);
        }
        return new List<Vector2>();
    }
    public void UpdateBusyNodes(Vector2 busyPositionOld, Vector2 busyPositionNew)
    {

        if (!_busyNodes.Contains(busyPositionNew)) _busyNodes.Add(busyPositionNew);
        _busyNodes.Remove(busyPositionOld);

    }
    public void AddToBusyNodes(Vector2 busyPositionNew)
    {
        if (!_busyNodes.Contains(busyPositionNew)) _busyNodes.Add(busyPositionNew);
    }
    public void RemoveFromBusyNodes(Vector2 position) => _busyNodes.Remove(position);
    public void AddAllStartPositions(List<AIController> allAI)
    {
        for (int i = 0; i < allAI.Count; i++)
        {
            Vector2 newAIPosition = new Vector2(Mathf.Round(allAI[i].transform.position.x), Mathf.Round(allAI[i].transform.position.z));
            Debug.Log(allAI[i]);
            _busyNodes.Add(newAIPosition);
        }
    }

    public int GetManhattenDistance(Node start, Node neighbour)
    {
        return (int)(Mathf.Abs(start.Position.x - neighbour.Position.x) + Mathf.Abs(start.Position.y - neighbour.Position.y));
    }
    List<Node> FindNeighbours(Node node)
    {
        List<Node> result = new List<Node>();

        Node leftNode = new Node(new Vector2(node.Position.x - 1, node.Position.y));
        Node rightNode = new Node(new Vector2(node.Position.x + 1, node.Position.y));
        Node downNode = new Node(new Vector2(node.Position.x, node.Position.y - 1));
        Node upNode = new Node(new Vector2(node.Position.x, node.Position.y + 1));

        result.Add(leftNode);
        result.Add(rightNode);
        result.Add(downNode);
        result.Add(upNode);

        for (int i = 0; i < result.Count; i++)
        {
            if (_busyNodes.Contains(result[i].Position)) result.Remove(result[i]);
        }

        return result;
    }
    List<Vector2> FindNeighbours(Vector2 position)
    {
        List<Vector2> result = new List<Vector2>();

        Vector2 leftNode = new Vector2(position.x - 1, position.y);
        Vector2 rightNode = new Vector2(position.x + 1, position.y);
        Vector2 downNode = new Vector2(position.x, position.y - 1);
        Vector2 upNode = new Vector2(position.x, position.y + 1);

        result.Add(leftNode);
        result.Add(rightNode);
        result.Add(downNode);
        result.Add(upNode);

        return result;
    }
}

public class Node
{
    public Vector2 Position;
    public Node PreviousNode;
    public int G;
    public int H;
    public int F { get { return G + H; } }
    public bool isWalkable = true;

    public Node(Vector2 position)
    {
        Position = position;
    }
}
