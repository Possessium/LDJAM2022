using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleObject : MonoBehaviour
{
    public static PuzzleObject Instance;

    [SerializeField] private PieceMaterial currentMaterial;
    public PieceMaterial CurrentMaterial { get { return currentMaterial; } }

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
        {
            switch (currentMaterial)
            {
                case PieceMaterial.ceramic:
                    AudioController.Instance.Play("ceramicPose");
                    break;
                case PieceMaterial.metal:
                    AudioController.Instance.Play("metalPose");
                    break;
            }
            piecePositions.Remove(_piece);
        }

        if(piecePositions.Count == 0)
            FinishPuzzle();
    }

    private void FinishPuzzle()
    {
        GameManager.Instance.LoadNewPuzzle();
    }
}

public enum PieceMaterial
{
    ceramic,
    metal
}