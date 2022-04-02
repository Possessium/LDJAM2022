using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<PuzzleObject> puzzleObjects;

    [SerializeField] private PuzzleObject currentPuzzleObject;

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
        currentPuzzleObject = Instantiate(puzzleObjects[Random.Range(0, puzzleObjects.Count)]);
    }
}
