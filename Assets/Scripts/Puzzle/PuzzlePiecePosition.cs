using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiecePosition : MonoBehaviour
{
    [SerializeField] private PuzzlePiece referencedPiece;

    public void PlacePiece(PuzzlePiece _piece)
    {
        if (_piece == referencedPiece)
        {
            PuzzleObject.Instance.RemovePiecePosition(this);
            _piece.SetSet();
            _piece.transform.position = transform.position;
            Destroy(_piece);
        }

        else if (_piece)
        {
            _piece.ResetPosition();
        }
    }
}
