using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    private const float CooldownTime = 1f;
    private const int StartingCountNumber = 3;

    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private GameObject _levelStartPanel;

    private float _cooldownTimer;
    private int _countNumber;
    private bool _countIsStarted = false;

    private void Start()
    {
        _cooldownTimer = CooldownTime;
        _countNumber = StartingCountNumber;
    }

    public void LevelStart()
    {
        _levelStartPanel.SetActive(true);
        _countIsStarted = true;
    }

    private void Update()
    {
        if (_countIsStarted && _countNumber >= 0)
        {
            if (_countNumber == 0)
            {
                _counter.text = "GO!";
                Ambulance.Instance.StartRace();
            }
            else
            {
                _counter.text = _countNumber.ToString();
            }

            if (_cooldownTimer <= 0)
            {
                --_countNumber;
                _cooldownTimer = CooldownTime;
            }

            _cooldownTimer -= Time.deltaTime;
        }
        else
        {
            _countIsStarted = false;
            _countNumber = StartingCountNumber;
            _levelStartPanel.SetActive(false);
        }
    }
}