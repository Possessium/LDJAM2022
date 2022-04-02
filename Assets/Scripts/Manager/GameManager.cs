using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<PuzzleObject> puzzleObjectsEasy;
    [SerializeField] private List<PuzzleObject> puzzleObjectsMedium;
    [SerializeField] private List<PuzzleObject> puzzleObjectsHard;
    private PuzzleObject currentPuzzleObject;
    private Difficulty currentDifficulty = Difficulty.easy;

    [SerializeField] private DestinyString destinyString;
    [SerializeField] private float valueAugmentation = .05f;
    [SerializeField] private float gameDuration = 60f;

    [SerializeField] private float winValueDestinyStringRemove = 15;

    private float currentGameDuration = 0;
    private float stringValue;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadNewPuzzle();
    }

    private void Update()
    {
        currentGameDuration += Time.deltaTime * valueAugmentation;
        stringValue = Mathf.MoveTowards(0, 1, currentGameDuration / gameDuration);
        destinyString.SetStringValue(stringValue);
    }

    public void LoadNewPuzzle()
    {
        if(currentPuzzleObject)
        {
            Destroy(currentPuzzleObject.gameObject);
            currentPuzzleObject = null;
        }

        currentGameDuration -= winValueDestinyStringRemove;

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
                if (puzzleObjectsEasy.Count == 0)
                    currentDifficulty = Difficulty.medium;
                break;
            case Difficulty.medium:
                _index = Random.Range(0, puzzleObjectsMedium.Count);
                currentPuzzleObject = Instantiate(puzzleObjectsMedium[_index]);
                puzzleObjectsMedium.RemoveAt(_index);
                if (puzzleObjectsMedium.Count == 0)
                    currentDifficulty = Difficulty.hard;
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