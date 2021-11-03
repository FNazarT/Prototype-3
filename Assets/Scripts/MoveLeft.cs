using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float moveSpeed = 30f, leftBound = -5;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(playerController.gameOver == false)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
