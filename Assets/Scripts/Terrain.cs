using UnityEngine;

public class Terrain : MonoBehaviour
{
    public GameObject roundPutinPrefab;
    public Transform terrain;

    private float sizeX;
    private float sizeZ;


    private void Awake()
    {
        sizeX = GetComponent<Collider>().bounds.size.x;
        sizeZ = GetComponent<Collider>().bounds.size.z;
    }
    private void Start()
    {
        for (int i = 0 ; i < 500; i++)
        {
            Vector3 position = new Vector3(Random.Range(0, sizeX), 2, Random.Range(0, sizeZ));
            GameObject putin = Instantiate(roundPutinPrefab);
            putin.transform.SetParent(terrain);
            putin.transform.localPosition = position;
        }
    }
}
