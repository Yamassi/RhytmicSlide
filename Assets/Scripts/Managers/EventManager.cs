using System;

public static class EventManager
{
    public static Action OnSwipeUp;
    public static Action OnSwipeDown;
    public static Action OnSwipeLeft;
    public static Action OnSwipeRight;
    public static Action Sound;
    public static Action Silent;
    public delegate void EnemyHandler(AIController aIController);
    public static event EnemyHandler OnAIHit;
    public static Action OnPlayerHitMatch;
    public static Action OnPlayerHitMisMatch;
    public static Action OnGameOver;

    public static void SwipeUp() => OnSwipeUp?.Invoke();
    public static void SwipeDown() => OnSwipeDown?.Invoke();
    public static void SwipeLeft() => OnSwipeLeft?.Invoke();
    public static void SwipeRight() => OnSwipeRight?.Invoke();
    public static void SoundHit() => Sound?.Invoke();
    public static void SilentHit() => Silent?.Invoke();
    public static void AIHit(AIController aIController) => OnAIHit?.Invoke(aIController);
    public static void PlayerHitMatch() => OnPlayerHitMatch?.Invoke();
    public static void PlayerHitMisMatch() => OnPlayerHitMisMatch?.Invoke();
    public static void GameOver() => OnGameOver?.Invoke();
}
