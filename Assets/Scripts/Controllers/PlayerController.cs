using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float _timeToJump;
    [Range(0, 10)]
    [SerializeField] float _speed;
    ColorBlinkShaderController _colorBlinkShaderController;
    InputController _inputController;
    StepObserver _stepObserver;
    static Vector3 moveUpOffset = new Vector3(0, 0, 1);
    static Vector3 moveDownOffset = new Vector3(0, 0, -1);
    static Vector3 moveLeftOffset = new Vector3(-1, 0, 0);
    static Vector3 moveRightOffset = new Vector3(1, 0, 0);
    public Vector3 LastPosition;
    public Vector3 FinalPosition;

    public void Init(StepObserver stepObserver)
    {
        _colorBlinkShaderController = GetComponent<ColorBlinkShaderController>();
        ShaderManager.AddToColorBlinkShadersList(_colorBlinkShaderController);

        _inputController = GetComponent<InputController>();
        _stepObserver = stepObserver;


        EventManager.OnSwipeUp += MoveToUp;
        EventManager.OnSwipeDown += MoveToDown;
        EventManager.OnSwipeLeft += MoveToLeft;
        EventManager.OnSwipeRight += MoveToRight;
    }

    public void MoveToUp()
    {
        StartCoroutine(SmoothMoveTo(moveUpOffset));
    }
    public void MoveToDown()
    {
        StartCoroutine(SmoothMoveTo(moveDownOffset));
    }
    public void MoveToLeft()
    {
        StartCoroutine(SmoothMoveTo(moveLeftOffset));
    }
    public void MoveToRight()
    {
        StartCoroutine(SmoothMoveTo(moveRightOffset));
    }

    IEnumerator SmoothMoveTo(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        Vector3 finalDestination = transform.position + destination;
        LastPosition = transform.position;
        FinalPosition = finalDestination;
        float time = 0;
        while (time < _timeToJump)
        {
            time = time + Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(startPosition, finalDestination, time);
            yield return null;
        }
        finalDestination = new Vector3(Mathf.Round(finalDestination.x), Mathf.Round(finalDestination.y), Mathf.Round(finalDestination.z));
        transform.position = finalDestination;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AIController>(out AIController aIController)) EventManager.AIHit(aIController);

    }

}
