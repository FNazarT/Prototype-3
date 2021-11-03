using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefab;
    [SerializeField] private PlayerController playerController;
    private Vector3 spawnPosition = new Vector3(25f, 0f, 0f);
    private float startDelay = 1f, repeatRate = 2f;
    private int index;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacles), startDelay, repeatRate);
    }

    private void SpawnObstacles()
    {        
        if (playerController.gameOver == false)
        {
            index = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[index], spawnPosition, obstaclePrefab[index].transform.rotation);
        }
    }
}
