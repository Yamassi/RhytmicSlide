using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] ScoreController _scoreController;
    [SerializeField] RhythmSliderController _rhythmSliderController;
    [SerializeField] HealthController _healthController;

    public void Init()
    {
        _scoreController.Init();
        // _rhythmSliderController.Init();
        _healthController.Init();
        EventManager.OnPlayerHitMatch += AddScore;
        EventManager.OnPlayerHitMisMatch += RemoveScore;
    }

    public void AddScore()
    {
        _scoreController.AddScore();
    }
    public void RemoveScore()
    {
        _scoreController.RemoveScore();
    }
}
