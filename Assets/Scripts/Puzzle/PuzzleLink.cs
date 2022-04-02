using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLink : MonoBehaviour
{
    [SerializeField] private PuzzlePiece referencedPiece;

    private void Update()
    {
        if(referencedPiece)
            transform.position = referencedPiece.transform.position;
    }
}
