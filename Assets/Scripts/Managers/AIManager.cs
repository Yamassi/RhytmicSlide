using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] List<AIController> _aIControllers = new List<AIController>();
    [SerializeField] PathAlgorithm _pathFinder;

    public void Init(PlayerController playerController)
    {
        foreach (AIController aIController in _aIControllers)
        {
            aIController.Init();
        }
        _pathFinder.Init(this, _aIControllers);
    }
    public void AllAIFollowPlayer(Vector3 playerPos)
    {
        foreach (AIController aIController in _aIControllers)
        {
            Vector2 playerPosition = new Vector2(playerPos.x, playerPos.z);
            Vector2 aIPosition = new Vector2(aIController.transform.position.x, aIController.transform.position.z);
            List<Vector2> path = new List<Vector2>();

            path = _pathFinder.FindPath(aIPosition, playerPosition);

            aIController.SetNewPath(path);

            if (path.Count == 1)
            {
                _pathFinder.UpdateBusyNodes(aIPosition, path[0]);
            }
            else if (path.Count > 1)
            {
                _pathFinder.UpdateBusyNodes(aIPosition, path[path.Count - 1]);
            }

            aIController.MoveToOneStep();

        }
    }
    public void AllAISetPath(Vector3 playerPos)
    {
        foreach (AIController aIController in _aIControllers)
        {
            Vector2 playerPosition = new Vector2(playerPos.x, playerPos.z);
            Vector2 aIPosition = new Vector2(aIController.transform.position.x, aIController.transform.position.z);
            List<Vector2> path = new List<Vector2>();

            path = _pathFinder.FindPath(aIPosition, playerPosition);

            aIController.SetNewPath(path);
        }
    }
    public void AllAIRandomPath()
    {
        foreach (AIController aIController in _aIControllers)
        { aIController.MoveToRandomSide(); }
    }
    public void AddToAIManager(AIController aIController)
    {
        _aIControllers.Add(aIController);
        _pathFinder.AddToBusyNodes(aIController.transform.position);
    }
    public void RemoveFromAIManager(AIController aIController)
    {
        _aIControllers.Remove(aIController);
        _pathFinder.RemoveFromBusyNodes(new Vector2(Mathf.Round(aIController.transform.position.x), Mathf.Round(aIController.transform.position.z)));
    }

}
