using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private int _lapCounter = 0;
    public int LapCounter => _lapCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ambulance>())
        {
            ++_lapCounter;
        }
    }
}
