using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepObserver : MonoBehaviour
{
    Game _game;
    PlayerController _playerController;
    AIManager _aIManager;
    AudioManager _audioManager;
    bool _isHitMatchWithMusic;
    int _count;
    public void Init(Game game, PlayerController playerController, AIManager aIManager, AudioManager audioManager)
    {
        _game = game;
        _playerController = playerController;
        _aIManager = aIManager;
        _audioManager = audioManager;

        EventManager.OnAIHit += AIHit;

        EventManager.OnSwipeUp += AllAIFollowPlayer;
        EventManager.OnSwipeDown += AllAIFollowPlayer;
        EventManager.OnSwipeLeft += AllAIFollowPlayer;
        EventManager.OnSwipeRight += AllAIFollowPlayer;

        EventManager.Sound += HitMatchWithMusic;
        EventManager.Silent += HitMisMatchWithMusic;
    }
    void AllAIFollowPlayer()
    {
        StartCoroutine(AllAIFollowPlayerAfter());
    }
    void AllAIRandomPath()
    {
        _aIManager.AllAIRandomPath();
    }
    void AIHit(AIController aIController)
    {
        if (_isHitMatchWithMusic)
        {
            print("+");
            aIController.DeInit();
            _aIManager.RemoveFromAIManager(aIController);
            EventManager.PlayerHitMatch();

        }
        else
        {
            print("-");
            aIController.DeInit();
            _aIManager.RemoveFromAIManager(aIController);
            EventManager.PlayerHitMisMatch();
        }
        _count++;
        if (_count > 20)
        {
            _audioManager.FirstExtendedTrackVolumeUp();
        }
        else if (_count > 40)
        {
            _audioManager.SecondExtendedTrackVolumeUp();
        }
    }

    void HitMatchWithMusic()
    {
        _isHitMatchWithMusic = true;
    }
    void HitMisMatchWithMusic()
    {
        _isHitMatchWithMusic = false;
    }

    IEnumerator AllAIFollowPlayerAfter()
    {
        yield return new WaitForSeconds(0.1f);
        _aIManager.AllAIFollowPlayer(_playerController.FinalPosition);
    }
}
