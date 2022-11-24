using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float _timeToJump;
    [Range(0, 10)]
    [SerializeField] float _speed;
    [SerializeField] List<Vector2> _pathToTarget = new List<Vector2>();
    ColorBlinkShaderController _colorBlinkShaderController;
    [SerializeField] Color _gizmosColor;
    static Vector3 _moveUpOffset = new Vector3(0, 0, 1);
    static Vector3 _moveDownOffset = new Vector3(0, 0, -1);
    static Vector3 _moveLeftOffset = new Vector3(-1, 0, 0);
    static Vector3 _moveRightOffset = new Vector3(1, 0, 0);
    Vector3[] _moveVectors = new Vector3[4];
    Coroutine _moveCoroutine;

    public void Init()
    {
        _colorBlinkShaderController = GetComponent<ColorBlinkShaderController>();
        ShaderManager.AddToColorBlinkShadersList(_colorBlinkShaderController);
        _colorBlinkShaderController.Init();
        _gizmosColor = Color.gray;

        _moveVectors[0] = _moveUpOffset;
        _moveVectors[1] = _moveDownOffset;
        _moveVectors[2] = _moveLeftOffset;
        _moveVectors[3] = _moveRightOffset;
    }
    public void DeInit()
    {
        print("deinit");
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _colorBlinkShaderController.DeInit();
        ShaderManager.RemoveFromColorBlinkShadersList(_colorBlinkShaderController);
        gameObject.SetActive(false);
    }
    public Vector2 GetNextMove()
    {
        if (_pathToTarget.Count > 1)
        {
            return _pathToTarget[_pathToTarget.Count - 1];
        }
        else
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }
    public void SetNewPath(List<Vector2> path)
    {
        _pathToTarget = path;
    }
    public List<Vector2> GetPath()
    {
        return _pathToTarget;
    }
    public void MoveToOneStep()
    {
        if (_pathToTarget != null)
        {
            if (_pathToTarget.Count == 1)
            {
                // Vector3 destination = new Vector3(_pathToTarget[0].x, 1, _pathToTarget[0].y);
                // _moveCoroutine = StartCoroutine(SmoothMoveTo(destination));
            }
            else if (_pathToTarget.Count > 1)
            {
                Vector3 destination = new Vector3(_pathToTarget[_pathToTarget.Count - 1].x, 1, _pathToTarget[_pathToTarget.Count - 1].y);
                _moveCoroutine = StartCoroutine(SmoothMoveTo(destination));
            }


        }
    }
    IEnumerator SmoothMoveTo(Vector3 destination)
    {
        Vector3 startPosition = transform.position;

        float time = 0;
        while (time < _timeToJump)
        {
            time = time + Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(startPosition, destination, time);
            yield return null;
        }

        transform.position = destination;
    }
    public void MoveToRandomSide()
    {
        StartCoroutine(SmoothMoveToOneSide());
    }
    IEnumerator SmoothMoveToOneSide()
    {
        int randomVector = Random.Range(0, 4);

        Vector3 startPosition = transform.position;
        Vector3 finalDestination = transform.position + _moveVectors[randomVector];

        float time = 0;
        while (time < _timeToJump)
        {
            time = time + Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(startPosition, finalDestination, time);
            yield return null;
        }

        transform.position = finalDestination;
    }

    void OnDrawGizmos()
    {

        if (_pathToTarget != null)
            foreach (var item in _pathToTarget)
            {
                Gizmos.color = _gizmosColor;
                Gizmos.DrawSphere(new Vector3(item.x, 1, item.y), 0.2f);
            }
    }
}
