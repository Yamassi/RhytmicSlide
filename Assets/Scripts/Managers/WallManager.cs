using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] List<ColorBlinkShaderController> _colorBlinkShaderControllers = new List<ColorBlinkShaderController>();
    public void Init()
    {
        foreach (ColorBlinkShaderController colorBlinkShaderController in _colorBlinkShaderControllers)
        {
            colorBlinkShaderController.Init();
        }
    }
}
