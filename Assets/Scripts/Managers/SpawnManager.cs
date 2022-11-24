using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _prefabAI;
    [SerializeField] int _spawnAmount;
    [SerializeField] int _spawnInterval;
    [SerializeField] Transform _spawnPlace;
    static Vector3 _offset = new Vector3(0, 1, 0);
    AIManager _aIManager;
    public void Init(AIManager aIManager)
    {
        _aIManager = aIManager;
        // EventManager.OnSwipeDown += Spawn;
        // EventManager.OnSwipeUp += Spawn;
        // EventManager.OnSwipeLeft += Spawn;
        // EventManager.OnSwipeRight += Spawn;
        StartCoroutine(SpawnAI());
    }
    public void DeInit()
    {
        StopAllCoroutines();
    }
    void Spawn()
    {
        int randomRow = Random.Range(0, GridManager.GridSize[0]);
        int randomColumn = Random.Range(0, GridManager.GridSize[0]);

        Vector3 position = GridManager.GridArray[randomRow, randomColumn].transform.position + _offset;

        Vector2 newPos = new Vector2(position.x, position.z);


        GameObject newAI = Instantiate(_prefabAI, position, Quaternion.identity, _spawnPlace);
        AIController aIController = newAI.GetComponent<AIController>();
        aIController.Init();
        _aIManager.AddToAIManager(aIController);
    }

    IEnumerator SpawnAI()
    {
        int spawnCount = 0;
        while (spawnCount < _spawnAmount)
        {
            Spawn();
            spawnCount++;
            // print(spawnCount);
            yield return new WaitForSeconds(_spawnInterval);
        }
        yield return null;
    }
}
