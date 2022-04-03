using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMask : MonoBehaviour
{
    private PuzzleObject currentChild;
    private Vector3 offsetChild;

    [SerializeField, Range(0, 1)] float value = 0;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(.05f, 10, 7), new Vector3(.05f, .15f, 7), value * 10);
        if(currentChild)
            currentChild.transform.localPosition = new Vector3(0, -transform.position.y / .94f, 0) + offsetChild;
    }

    public void GetAllPieces()
    {
        currentChild = FindObjectOfType<PuzzleObject>();
        currentChild.transform.parent = transform;
        offsetChild = currentChild.transform.position;
    }

    public void SetStringValue(float _value)
    {
        value = _value;

        if (value == 1)
            GameManager.Instance.EndGame();
    }

    
    public IEnumerator LerpStringCoroutine(float _value, float _time)
    {
        float _startValue = value;
        float _elapsedTime = 0;

        while(value != _value)
        {
            _elapsedTime += Time.deltaTime;
            SetStringValue(Mathf.MoveTowards(value, _value, Time.deltaTime * (_time / 2)));
            yield return new WaitForEndOfFrame();
        }
    }
}
