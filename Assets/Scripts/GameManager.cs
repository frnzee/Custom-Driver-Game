using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        None,
        Game,
        Win
    }

    [SerializeField] private GameObject _startMenu;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;

    [SerializeField] private TextMeshProUGUI _startCounter;
    [SerializeField] private TextMeshProUGUI _lapTime;
    [SerializeField] private TextMeshProUGUI _lapCounter;
    [SerializeField] private TextMeshProUGUI _gameTime;
    [SerializeField] private TextMeshProUGUI _bestTime;
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private FinishLine _finishLine;

    private GameState _currentGameState;

    private float _timeValue;
    private float _lapTimeValue;
    private float _currentSpeed;

    private int _lapCount = 0;

    private void Awake()
    {
        _startMenu.SetActive(true);
        _currentGameState = GameState.None;
    }

    private void Start()
    {
    }

    public void StartGame()
    {
        _startMenu.SetActive(false);
        _currentGameState = GameState.Game;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("DriverScene");
    }

    private void UpdateLapCount()
    {

    }
    private void Update()
    {
        if (_currentGameState == GameState.Game)
        {
            _timeValue += Time.deltaTime;
            _lapTimeValue += Time.deltaTime;

            _gameTime.text = "Game Time: " + _timeValue.ToString("0.0");
            _lapTime.text = "Lap Time: " + _lapTimeValue.ToString("0.0");

            _speed.text = "SPEED: " + (Ambulance.Instance.CurrentSpeed * 10).ToString("00");
        }
    }
}
