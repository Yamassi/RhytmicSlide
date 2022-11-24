using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour
{
    [SerializeField] StepObserver _stepObserver;
    [SerializeField] PlayerController _playerController;
    [SerializeField] GridManager _gridManager;
    [SerializeField] WallManager _wallManager;
    [SerializeField] AIManager _aIManager;
    [SerializeField] AudioManager _audioManager;
    [SerializeField] UIManager _uIManager;
    [SerializeField] SpawnManager _spawnManager;

    private void Awake()
    {
        _playerController.Init(_stepObserver);
        _gridManager.Init();
        _aIManager.Init(_playerController);
        _spawnManager.Init(_aIManager);
        _audioManager.Init();
        _stepObserver.Init(this, _playerController, _aIManager, _audioManager);

        _uIManager.Init();
        ShaderManager.Init();
    }


}
