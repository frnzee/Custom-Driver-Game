using UnityEngine;

public class Terrain : MonoBehaviour
{
    public GameObject roundPutinPrefab;
    public Transform terrain;

    private Bounds _bounds;

    private void Awake()
    {
        _bounds = GetComponent<Collider>().bounds;
    }
    private void Start()
    {
        for (int i = 0 ; i < 500; i++)
        {
            Vector3 position = new Vector3(Random.Range(0, _bounds.size.x), 2, Random.Range(0, _bounds.size.y));
            GameObject putin = Instantiate(roundPutinPrefab, terrain);
            putin.transform.localPosition = position;
        }
    }
}
