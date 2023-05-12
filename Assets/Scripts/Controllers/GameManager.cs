using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private int startingLives = 3;
    [SerializeField] private Home[] homes;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private int scorePerHome = 100;
    [SerializeField] private int scorePerClear = 1000;
    [SerializeField] private int scorePerRow = 10;
    [SerializeField] private int scorePerTime = 20;

    [SerializeField] private int timeToOccupyHome = 60;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text livesText;

    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip levelClearSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip homeOccupiedSound;

    [SerializeField] private AudioClip gameMusic;

    private int _score;
    private int _lives;
    private int _time;

    private int _homesOccupied = 0;

    private bool _hasDiedThisLevel = false;
    private int _levelsClearedWithoutDying = -1;

    private IPlayerDataService _playerDataService;
    private IAudioService _audioService;

    private void Start()
    {
        _playerDataService = ServiceLocator.Instance.Get<IPlayerDataService>();
        _audioService = ServiceLocator.Instance.Get<IAudioService>();
        _audioService.PlayMusic(gameMusic);
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void UpdatePlayerData()
    {
        PlayerData playerData = _playerDataService.GetCurrentPlayerData();
        playerData.score = _score;
        playerData.lives = _lives;
        if (_score > playerData.highScore)
        {
            playerData.highScore = _score;
        }
        playerData.homesOccupied = GetOccupiedHomes();

        _playerDataService.SaveData(playerData);
    }

    private bool[] GetOccupiedHomes()
    {
        bool[] occupiedHomes = new bool[homes.Length];
        for (int i = 0; i < homes.Length; i++)
        {
            occupiedHomes[i] = homes[i].enabled;
        }
        return occupiedHomes;
    }

    public void NewGame()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        // if we have 0 lives, we're starting a new game
        // otherwise we continue with the current game

        PlayerData playerData = _playerDataService.GetCurrentPlayerData();
        if (playerData.lives == 0)
        {
            SetScore(0);
            SetLives(startingLives);
            _homesOccupied = 0;
        }
        else
        {
            SetScore(playerData.score);
            SetLives(playerData.lives);
            bool[] homesOccupied = playerData.homesOccupied;
            for (int i = 0; i < homesOccupied.Length; i++)
            {
                if (homesOccupied[i])
                {
                    homes[i].enabled = true;
                    _homesOccupied++;
                }
            }
        }
        SetHighScore(playerData.highScore);
        NewLevel();
    }

    private void NewLevel()
    {
        // Reset the level. When the player gets all homes
        foreach (Home home in homes)
        {
            home.enabled = false;
        }
        _homesOccupied = 0;
        _hasDiedThisLevel = false;
        if (_levelsClearedWithoutDying == 5)
        {
            // unlock super_virus achievement
            _playerDataService.UnlockAchievement("super_virus");
        }
        else {
            _levelsClearedWithoutDying++;
        }
        NewRound();
    }

    private void NewRound()
    {
        // When the player occupies a home
        Respawn();
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Respawn()
    {
        playerController.Respawn();

        StopAllCoroutines();
        StartCoroutine(Timer(timeToOccupyHome));
    }

    public void PlayerDeath()
    {
        _audioService.PlaySound(deathSound);
        _hasDiedThisLevel = true;

        _levelsClearedWithoutDying = 0;
        SetLives(_lives - 1);

        if (_lives > 0)
        {
            Invoke(nameof(Respawn), playerController.respawnDelay);
        }
        else
        {
            Invoke(nameof(GameOver), playerController.respawnDelay);
        }
    }

    private void GameOver()
    {
        _audioService.PlaySound(gameOverSound);
        gameOverScreen.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        UpdatePlayerData();
        SceneManager.Instance.LoadMainMenu();
    }

    private IEnumerator Timer(int duration)
    {
        _time = duration;
        timerText.SetText(_time.ToString());
        while (_time > 0)
        {
            yield return new WaitForSeconds(1f);
            timerText.SetText(_time.ToString());
            _time--;
        }

        playerController.Death();
    }

    private void SetScore(int score)
    {
        _score = score;
        scoreText.SetText("Score: " + _score);
        PlayerData playerData = _playerDataService.GetCurrentPlayerData();
        playerData.score = _score;
        if (_score > playerData.highScore)
        {
            SetHighScore(_score);
        }
        _playerDataService.SaveData(playerData);
    }

    private void SetLives(int lives)
    {
        if (lives < 0)
        {
            lives = 0;
        }
        _lives = lives;
        livesText.SetText(_lives.ToString());
        PlayerData playerData = _playerDataService.GetCurrentPlayerData();
        playerData.lives = _lives;
        _playerDataService.SaveData(playerData);
    }

    public void SetHighScore(int score)
    {
        highScoreText.SetText("High Score: " + score);
    }

    public void HomeOccupied()
    {

        // Unlock hackerman achievement
        if (_time >= (timeToOccupyHome - 20))
        {
            _playerDataService.UnlockAchievement("hackerman");
        }

        _homesOccupied++;
        PlayerData playerData = _playerDataService.GetCurrentPlayerData();
        playerData.homesOccupied = GetOccupiedHomes();
        _playerDataService.SaveData(playerData);

        _audioService.PlaySound(homeOccupiedSound);
        SetScore(_score + scorePerHome);

        int bonus = _time * scorePerTime;

        playerController.gameObject.SetActive(false);
        if (IsAllHomesOccupied())
        {
            // unlock sneaky virus achievement
            if (!_hasDiedThisLevel)
            {
                _playerDataService.UnlockAchievement("sneaky_virus");
            }

            _audioService.PlaySound(levelClearSound);
            SetScore(_score + scorePerClear);
            SetLives(_lives + 1);
            Invoke(nameof(NewLevel), playerController.respawnDelay);
        }
        else
        {
            Invoke(nameof(NewRound), playerController.respawnDelay);
        }
    }

    private bool IsAllHomesOccupied()
    { 
        return _homesOccupied == homes.Length;
    }

    public void RowIncremented()
    {
        SetScore(_score + scorePerRow);
    }
}