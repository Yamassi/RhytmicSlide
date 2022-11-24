using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthController : MonoBehaviour
{
    [SerializeField] string _text;
    [SerializeField] int _health;
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] TextMeshProUGUI _textMeshProNumber;
    public void Init()
    {
        _textMeshPro.text = _text;
        _textMeshProNumber.text = _health.ToString();

        EventManager.OnPlayerHitMisMatch += SubtractHealth;
    }

    private void SubtractHealth()
    {
        _health--;
        _textMeshProNumber.text = (_health.ToString());
        if (_health <= 0)
        {
            _health = 0;
            _textMeshProNumber.text = (_health.ToString());
            EventManager.GameOver();
        }
    }

}
