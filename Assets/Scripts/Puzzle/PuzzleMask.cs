using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMask : MonoBehaviour
{

    private PuzzleObject currentChild;

    [SerializeField, Range(0, 1)] float value = 0;


    private void Update()
    {
        transform.position = Vector3.MoveTowards(new Vector3(0, 10, 0), Vector3.zero, value * 10);
        if(currentChild)
            currentChild.transform.localPosition = new Vector3(0, -transform.position.y, 0);
    }

    public void GetAllPieces()
    {
        currentChild = FindObjectOfType<PuzzleObject>();
        currentChild.transform.parent = transform;
    }

    public void SetStringValue(float _value)
    {
        value = _value;
    }
}
