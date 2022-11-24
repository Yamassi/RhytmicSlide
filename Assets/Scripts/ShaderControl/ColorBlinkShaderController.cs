using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlinkShaderController : MonoBehaviour
{
    [SerializeField] bool _isColorChangeOn;
    [SerializeField] Color _color;
    [SerializeField] float _minIntensity;
    [SerializeField] float _maxIntensity;
    [SerializeField] float _colorChangeSpeed;
    [SerializeField] Renderer[] _outlineSizes = new Renderer[0];
    [SerializeField] bool _isSizeChangeOn;
    [SerializeField] float _minSize;
    [SerializeField] float _maxSize;
    [SerializeField] float _sizeChangeSpeed;
    float _currentSize;
    float _currentIntensity;
    Coroutine _colorIntensityToMax;
    Coroutine _colorIntensityToMin;
    Coroutine _shaderSizeToMax;
    Coroutine _shaderSizeToMin;
    bool isInit;
    public void Init()
    {
        if (!isInit)
        {
            EventManager.Sound += ShaderMax;
            isInit = true;
        }
    }

    public void DeInit()
    {
        StopAllCoroutines();
        EventManager.Sound -= ShaderMax;
        ShaderManager.RemoveFromColorBlinkShadersList(this);
        isInit = false;
        gameObject.SetActive(false);
    }
    void ShaderStart()
    {
        if (_isSizeChangeOn)
        {
            _currentSize = _minSize;
            _shaderSizeToMax = StartCoroutine(ShaderSizeToMax());
        }
        if (_isColorChangeOn)
        {
            _currentIntensity = _minIntensity;
            _colorIntensityToMax = StartCoroutine(ColorIntensityToMax());
        }
    }
    void ShaderMax()
    {
        StartCoroutine(ShaderToMax());
    }
    IEnumerator ShaderToMax()
    {
        if (_isSizeChangeOn)
        {
            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetFloat("_Size", _maxSize);
            }
        }
        if (_isColorChangeOn)
        {
            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetColor("_Color", _color);
            }
        }
        yield return new WaitForSeconds(0.2f);
        if (_isSizeChangeOn)
        {
            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetFloat("_Size", _minSize);
            }
        }
        if (_isColorChangeOn)
        {
            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetColor("_Color", Color.white);
            }
        }
    }
    void ShaderMin()
    {

    }
    IEnumerator ShaderToMin()
    {
        if (_isSizeChangeOn)
        {
            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetFloat("_Size", _minSize);
            }
        }
        if (_isColorChangeOn)
        {
            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetColor("_Color", Color.white);
            }
        }
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator ColorIntensityToMax()
    {
        while (_currentIntensity < _maxIntensity)
        {
            _currentIntensity += _colorChangeSpeed;

            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetColor("_Color", _color * _currentIntensity);
            }
            if (_currentIntensity >= _maxIntensity)
            {
                _colorIntensityToMin = StartCoroutine(ColorIntensityToMin());
                StopCoroutine(_colorIntensityToMax);
                break;
            }
            yield return null;
        }
        yield return null;
    }
    IEnumerator ColorIntensityToMin()
    {
        while (_currentIntensity > _minIntensity)
        {
            _currentIntensity -= _colorChangeSpeed;

            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetColor("_Color", _color * _currentIntensity);
            }
            if (_currentIntensity <= _minIntensity)
            {
                _colorIntensityToMax = StartCoroutine(ColorIntensityToMax());
                StopCoroutine(_colorIntensityToMin);
                break;
            }
            yield return null;
        }
        yield return null;
    }
    IEnumerator ShaderSizeToMax()
    {

        while (_currentSize < _maxSize)
        {
            _currentSize += _sizeChangeSpeed;

            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetFloat("_Size", _currentSize);
            }
            if (_currentSize >= _maxSize)
            {
                _shaderSizeToMin = StartCoroutine(ShaderSizeToMin());
                StopCoroutine(_shaderSizeToMax);
                break;
            }
            yield return null;
        }
        yield return null;
    }
    IEnumerator ShaderSizeToMin()
    {
        while (_currentSize > _minSize)
        {
            _currentSize -= _sizeChangeSpeed;

            foreach (Renderer renderer in _outlineSizes)
            {
                renderer.material.SetFloat("_Size", _currentSize);
            }
            if (_currentSize <= _minSize)
            {
                _shaderSizeToMax = StartCoroutine(ShaderSizeToMax());
                StopCoroutine(_shaderSizeToMin);
                break;
            }
            yield return null;
        }
        yield return null;
    }
}
