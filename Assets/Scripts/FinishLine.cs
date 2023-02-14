using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private int _lapCounter = 0;
    public int LapCounter => _lapCounter;
    private bool _skipFirst = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!_skipFirst && other.gameObject.GetComponent<Ambulance>())
        {
            ++_lapCounter;
        }
        else if (other.gameObject.GetComponent<Ambulance>())
        {
            _skipFirst = false;
        }
    }
}
