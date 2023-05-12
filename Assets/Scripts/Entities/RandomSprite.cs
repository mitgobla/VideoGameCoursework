using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Randomize();
    }

    public void Randomize()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}