using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmSliderController : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] Slider _sliderMirror;
    [SerializeField] float _TimerMax;
    Coroutine _sliderSmoothPlay;
    public void Init()
    {
        EventManager.Sound += SliderPlay;

        _slider.maxValue = _TimerMax;
        _sliderMirror.maxValue = _TimerMax;
    }

    void SliderPlay()
    {
        if (_sliderSmoothPlay != null) StopCoroutine(_sliderSmoothPlay);
        _sliderSmoothPlay = StartCoroutine(SliderToMaxThenToMin());
    }
    IEnumerator SliderToMaxThenToMin()
    {
        while (_slider.value < _TimerMax)
        {
            _slider.value += 0.1f;
            _sliderMirror.value += 0.1f;
            yield return null;
        }
        while (_slider.value > 0)
        {
            _slider.value -= 0.1f;
            _sliderMirror.value -= 0.1f;
            yield return null;
        }
    }

}
