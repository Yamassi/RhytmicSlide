using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder
{
    [SerializeField] List<Vector2> _pathToTarget = new List<Vector2>();
    List<Vector2> _busyNodes = new List<Vector2>();
    List<PathNode> _checkedNodes = new List<PathNode>();
    List<PathNode> _waitingNodes = new List<PathNode>();
    [SerializeField] GameObject _source;
    [SerializeField] GameObject _target;
    [SerializeField] LayerMask _obstacleLayer;
    Vector3 _nodeToCheckPos;
    string[] allMasks = new string[3];

    // private void Update()
    // {
    //     Vector2 sourcePosition = new Vector2(_source.transform.position.x, _source.transform.position.z);
    //     Vector2 targetPosition = new Vector2(_target.transform.position.x, _target.transform.position.z);
    //     _pathToTarget = GetPath(sourcePosition, targetPosition);
    // }

    public void Init()
    {
        allMasks[0] = "Player";
        allMasks[1] = "AI";
        allMasks[2] = "Obstacle";

        _obstacleLayer = LayerMask.GetMask(allMasks);
    }
    public List<Vector2> GetPath(Vector2 source, Vector2 target)
    {
        _pathToTarget.Clear();
        _checkedNodes.Clear();
        _waitingNodes.Clear();

        Vector2 startPosition = new Vector2(Mathf.Round(source.x), Mathf.Round(source.y));
        Vector2 targetPosition = new Vector2(Mathf.Round(target.x), Mathf.Round(target.y));

        if (startPosition == targetPosition) return _pathToTarget;


        PathNode startNode = new PathNode(0, startPosition, targetPosition, null);
        _checkedNodes.Add(startNode);
        _waitingNodes.AddRange(GetNeighbourNodes(startNode));



        while (_waitingNodes.Count > 0)
        {
            PathNode nodeToCheck = _waitingNodes.OrderBy(x => x.F).FirstOrDefault();//.Where(x => x.F == _waitingNodes.Min(y => y.F)).FirstOrDefault(); // Находим минимальный F, минимальная цена пути
            bool walkable = true;

            if (nodeToCheck.Position == targetPosition) // Если нода уже в конце пути
            {
                return CalculatePathFromNode(nodeToCheck);
            }

            foreach (var busyNode in _busyNodes)
            {
                if (nodeToCheck.Position == busyNode)
                {
                    walkable = false;
                }
            }

            if (!walkable) // Если по ноде нельзя ходить
            {
                _checkedNodes.Add(nodeToCheck);
            }
            else if (walkable) // Если ходить можно
            {
                if (!_checkedNodes.Where(x => x.Position == nodeToCheck.Position).Any()) // Если нет похожих node с той же позицией
                {
                    _checkedNodes.Add(nodeToCheck);
                    _waitingNodes.AddRange(GetNeighbourNodes(nodeToCheck));
                }
            }
            _waitingNodes.Remove(nodeToCheck);
        }

        return _pathToTarget;
    }

    List<Vector2> CalculatePathFromNode(PathNode node)
    {
        var path = new List<Vector2>();
        PathNode currentNode = node;

        while (currentNode.PreviousNode != null)
        {
            path.Add(new Vector2(currentNode.Position.x, currentNode.Position.y));
            currentNode = currentNode.PreviousNode;
        }

        return path;
    }

    List<PathNode> GetNeighbourNodes(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();

        neighbours.Add(new PathNode(node.G + 1, new Vector2(node.Position.x - 1, node.Position.y), node.TargetPosition, node));
        neighbours.Add(new PathNode(node.G + 1, new Vector2(node.Position.x + 1, node.Position.y), node.TargetPosition, node));
        neighbours.Add(new PathNode(node.G + 1, new Vector2(node.Position.x, node.Position.y - 1), node.TargetPosition, node));
        neighbours.Add(new PathNode(node.G + 1, new Vector2(node.Position.x, node.Position.y + 1), node.TargetPosition, node));

        return neighbours;
    }
    public void AddBusyNode(Vector2 busyNode) => _busyNodes.Add(busyNode);
    public void RemoveBusyNode(Vector2 busyNode) => _busyNodes.Remove(busyNode);
    public List<Vector2> GetBusyNodes() => _busyNodes;
}

public class PathNode
{
    public Vector2 Position;
    public Vector2 TargetPosition;
    public PathNode PreviousNode;
    public int G; // растояние от старта до ноды
    public int H; // расстояние от ноды до цели
    public int F; // F = G + H
    public PathNode(int g, Vector2 nodePosition, Vector2 targetPosition, PathNode previousNode)
    {
        Position = nodePosition;
        TargetPosition = targetPosition;
        PreviousNode = previousNode;
        G = g;
        H = (int)Mathf.Abs(TargetPosition.x - Position.x) + (int)Mathf.Abs(TargetPosition.y - Position.y);
        F = G + H;
    }
}
