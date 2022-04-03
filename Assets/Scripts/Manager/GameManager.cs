using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<PuzzleObject> puzzleObjectsEasy;
    [SerializeField] private List<PuzzleObject> puzzleObjectsMedium;
    [SerializeField] private List<PuzzleObject> puzzleObjectsHard;
    [SerializeField] private List<PuzzleObject> puzzleObjectsHardCopy;

    private PuzzleObject currentPuzzleObject;
    private Difficulty currentDifficulty = Difficulty.easy;

    [SerializeField] private PuzzleMask puzzleMask;
    [SerializeField] private float valueAugmentation = .05f;
    [SerializeField] private float gameDuration = 60f;
    [SerializeField] private float pauseDuration = 1f;

    [SerializeField] private float winValueDestinyStringRemove = 15;

    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject dedMenu;

    private float currentGameDuration = 0;
    private float stringValue;

    public bool IsPaused { get; private set; } = false;
    public bool isLoadingLevel = false;
    private int score;
    private int highScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnPuzzle();
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreText.text = highScore.ToString();
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if (IsPaused || isLoadingLevel)
            return;

        currentGameDuration += Time.deltaTime * valueAugmentation;
        stringValue = Mathf.MoveTowards(0, 1, currentGameDuration / gameDuration);
        puzzleMask.SetStringValue(stringValue);
    }

    public void LoadNewPuzzle()
    {
        StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        puzzleMask.StartCoroutine(puzzleMask.LerpStringCoroutine(Mathf.Clamp((currentGameDuration - winValueDestinyStringRemove) / gameDuration, 0, gameDuration), pauseDuration));

        isLoadingLevel = true;
        float _timer = 0;

        while (_timer < pauseDuration)
        {
            yield return new WaitForEndOfFrame();

            if(isLoadingLevel && !IsPaused)
                _timer += Time.deltaTime;
        }

        if (currentPuzzleObject)
        {
            score++;
            scoreText.text = score.ToString();

            if(score > highScore)
                highScoreText.text = score.ToString();

            Destroy(currentPuzzleObject.gameObject);
            currentPuzzleObject = null;
            currentGameDuration -= winValueDestinyStringRemove;
            currentGameDuration = Mathf.Clamp(currentGameDuration, 0, gameDuration);
        }

        yield return new WaitForEndOfFrame();

        SpawnPuzzle();
    }

    public void SpawnPuzzle()
    {
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

                puzzleObjectsHardCopy.Add(puzzleObjectsHard[_index]);
                puzzleObjectsHard.RemoveAt(_index);
                if (puzzleObjectsHard.Count == 0)
                {
                    puzzleObjectsHard.AddRange(puzzleObjectsHardCopy);
                    puzzleObjectsHardCopy.Clear();
                }
                break;
        }

        isLoadingLevel = false;

        puzzleMask.GetAllPieces();
    }

    public void EndGame()
    {
        IsPaused = true;

        pauseMenu.SetActive(false);
        dedMenu.SetActive(true);

        int _high = PlayerPrefs.GetInt("highScore");
        if (_high < score)
            PlayerPrefs.SetInt("highScore", score);

    }

    public void InputPause(UnityEngine.InputSystem.InputAction.CallbackContext _ctx)
    {
        if (dedMenu.activeInHierarchy)
            return;

        if(_ctx.started)
        {
            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        IsPaused = true;
        currentPuzzleObject.gameObject.SetActive(false);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        IsPaused = false;
        currentPuzzleObject.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() => Application.Quit();
}

public enum Difficulty
{
    easy,
    medium,
    hard
}