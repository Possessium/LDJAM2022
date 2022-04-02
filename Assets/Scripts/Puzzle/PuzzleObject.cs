using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleObject : MonoBehaviour
{
    public static PuzzleObject Instance;

    List<PuzzlePiecePosition> piecePositions;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       piecePositions = FindObjectsOfType<PuzzlePiecePosition>().ToList();
    }

    public void RemovePiecePosition(PuzzlePiecePosition _piece)
    {
        if(piecePositions.Contains(_piece))
            piecePositions.Remove(_piece);

        if(piecePositions.Count == 0)
            FinishPuzzle();
    }

    private void FinishPuzzle()
    {
        Debug.Log("End");
        GameManager.Instance.LoadNewPuzzle();
    }
}
