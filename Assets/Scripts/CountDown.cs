using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private GameObject _levelStartPanel;

    public bool CurrentGameState { get; private set; } = false;

    private float _cooldownTimer = 1f;
    private int _countNumber = 3;
    private bool _buttonPressed = false;

    public void LevelStart(bool buttonPressed)
    {
        _levelStartPanel.SetActive(true);
        _buttonPressed = buttonPressed;
    }

    private void Update()
    {
        if (_buttonPressed && _countNumber >= 0)
        {
            if (_countNumber == 0)
            {
                _counter.text = "GO!";
            }
            else
            {
                _counter.text = _countNumber.ToString();
            }
            if (_cooldownTimer <= 0)
            {
                --_countNumber;
                _cooldownTimer = 1f;
            }

            _cooldownTimer -= Time.deltaTime;
        }
        else
        {
            _buttonPressed = false;
            _countNumber = 3;
            _levelStartPanel.SetActive(false);

            CurrentGameState = true;
        }
    }
}