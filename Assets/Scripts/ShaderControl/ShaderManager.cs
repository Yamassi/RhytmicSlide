using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager
{
    private static List<ColorBlinkShaderController> _colorBlinkShaderControllers = new List<ColorBlinkShaderController>();
    public static void Init()
    {
        foreach (ColorBlinkShaderController colorBlinkShaderController in _colorBlinkShaderControllers)
        {
            colorBlinkShaderController.Init();
        }
    }
    public static void AddToColorBlinkShadersList(ColorBlinkShaderController colorBlinkShader)
    {
        _colorBlinkShaderControllers.Add(colorBlinkShader);
    }
    public static void RemoveFromColorBlinkShadersList(ColorBlinkShaderController colorBlinkShader)
    {
        _colorBlinkShaderControllers.Remove(colorBlinkShader);
    }

}
