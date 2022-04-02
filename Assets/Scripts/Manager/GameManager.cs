using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<PuzzleObject> puzzleObjectsEasy;
    [SerializeField] private List<PuzzleObject> puzzleObjectsMedium;
    [SerializeField] private List<PuzzleObject> puzzleObjectsHard;

    private PuzzleObject currentPuzzleObject;

    private Difficulty currentDifficulty = Difficulty.easy;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadNewPuzzle();
    }

    public void LoadNewPuzzle()
    {
        if(currentPuzzleObject)
        {
            Destroy(currentPuzzleObject.gameObject);
            currentPuzzleObject = null;
        }

        StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForEndOfFrame();

        int _index = 0;

        switch (currentDifficulty)
        {
            case Difficulty.easy:
                _index = Random.Range(0, puzzleObjectsEasy.Count);
                currentPuzzleObject = Instantiate(puzzleObjectsEasy[_index]);
                puzzleObjectsEasy.RemoveAt(_index);
                break;
            case Difficulty.medium:
                _index = Random.Range(0, puzzleObjectsMedium.Count);
                currentPuzzleObject = Instantiate(puzzleObjectsMedium[_index]);
                puzzleObjectsEasy.RemoveAt(_index);
                break;
            case Difficulty.hard:
                _index = Random.Range(0, puzzleObjectsHard.Count);
                currentPuzzleObject = Instantiate(puzzleObjectsHard[_index]);
                break;
        }

    }
}

public enum Difficulty
{
    easy,
    medium,
    hard
}