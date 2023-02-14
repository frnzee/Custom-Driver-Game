using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public partial class GameManager : MonoBehaviour
{
    private const int SpeedModifierForUI = 10;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _lapTime;
    [SerializeField] private TextMeshProUGUI _lapCounter;
    [SerializeField] private TextMeshProUGUI _gameTime;
    [SerializeField] private TextMeshProUGUI _bestTime;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _score;

    [SerializeField] private FinishLine _finishLine;
    [SerializeField] private CountDown _startCounter;
    [SerializeField] private GameObject _startMenu;

    private float _timeValue;
    private float _lapTimeValue;
    private float _bestTimeValue = Mathf.Infinity;
    private int _lastLapCount = 0;
    private int _scoreValue = 0;

    public GameState CurrentGameState { get; private set; }

    private void Start()
    {
        _startMenu.SetActive(true);
        CurrentGameState = GameState.None;
    }

    public void StartGame()
    {
        _startMenu.SetActive(false);
        CurrentGameState = GameState.Countdown;
        _startCounter.LevelStart();
        Ambulance.Instance.ShowMoveControls();
    }

    public void StartRace()
    {
        CurrentGameState = GameState.Game;
    }

    private void Update()
    {
        if (CurrentGameState == GameState.Game)
        {
            _timeValue += Time.deltaTime;
            _lapTimeValue += Time.deltaTime;

            _gameTime.text = $"Game Time: {_timeValue:0.0}";
            _lapTime.text = $"Lap Time: {_lapTimeValue:0.0}";

            _speed.text = $"SPEED: {Ambulance.Instance.CurrentSpeed * SpeedModifierForUI:00}";

            UpdateLapCount();
        }
    }

    private void UpdateLapCount()
    {
        if (_finishLine.LapCounter > _lastLapCount)
        {
            UpdateBestLapTime();
        }

        _lastLapCount = _finishLine.LapCounter;
        _lapCounter.text = $"Laps: {_lastLapCount}";
    }

    private void UpdateBestLapTime()
    {
        if (_lapTimeValue < _bestTimeValue)
        {
            _bestTimeValue = _lapTimeValue;
            _lapTimeValue = 0f;
        }

        _bestTime.text = $"Best time: {_bestTimeValue:0.0}";
    }

    public void UpdateScore()
    {
        ++_scoreValue;
        _score.text = "Score: " + _scoreValue.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("DriverScene");
    }
}
