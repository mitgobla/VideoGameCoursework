using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private RandomSprite randomSprite;

    private void Awake()
    {
        randomSprite = GetComponent<RandomSprite>();
    }

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            Vector3 position = transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;
            RandomizeSprite();
        }
        else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
        {
            Vector3 position = transform.position;
            position.x = rightEdge.x + size;
            transform.position = position;
            RandomizeSprite();
        } else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void RandomizeSprite()
    {
        if (randomSprite != null)
        {
            randomSprite.Randomize();
        }
    }
}
