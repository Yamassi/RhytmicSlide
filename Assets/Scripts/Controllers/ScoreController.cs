using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    TextMeshProUGUI _scoreText;
    int _score;

    public void Init()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void AddScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }
    public void RemoveScore()
    {
        _score--;
        _scoreText.text = _score.ToString();
        if (_score <= 0)
        {
            _score = 0;
            _scoreText.text = _score.ToString();
        }
    }
}
