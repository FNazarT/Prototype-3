using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private float repeatOffset;
    private Vector3 backgroundInitialPos;

    void Start()
    {
        repeatOffset = GetComponent<SpriteRenderer>().bounds.extents.x;      // Get half of the background's horizontal size
        backgroundInitialPos = transform.position;                           // Save the initial position of the background
    }

    void Update()
    {
        if( transform.position.x < (backgroundInitialPos.x - repeatOffset))
        {
            transform.position = backgroundInitialPos;
        }
    }
}
