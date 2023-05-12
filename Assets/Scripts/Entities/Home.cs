using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Home : MonoBehaviour
{
    [SerializeField] private GameObject emptyHome;
    [SerializeField] private GameObject filledHome;
    [SerializeField] private SpriteRenderer filledHomeSpriteRenderer;

    [SerializeField] private TMP_Text homeName;

    [SerializeField] private string[] names;
    [SerializeField] private string claimedName;

    private BoxCollider2D _boxCollider;
    private GameManager _gameManager;

    private string PickRandomName()
    {
        return names[Random.Range(0, names.Length)];
    }

    private void SetName(string name)
    {
        homeName.SetText(name);
    }

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _boxCollider = GetComponent<BoxCollider2D>();
        SetName(PickRandomName());
    }

    private void OnEnable()
    {
        SetName(claimedName);
        filledHome.SetActive(true);
        emptyHome.SetActive(false);
        _boxCollider.enabled = false;
    }

    private void OnDisable()
    {
        SetName(PickRandomName());
        filledHome.SetActive(false);
        emptyHome.SetActive(true);
        _boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enabled = true;
            filledHomeSpriteRenderer.color = other.GetComponent<SpriteRenderer>().color;
            _gameManager.HomeOccupied();
        }
    }
}
