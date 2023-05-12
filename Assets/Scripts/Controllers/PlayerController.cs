using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float minX = 0.5f;
    [SerializeField] private float maxX = 19.5f;
    [SerializeField] private float minY = 0.5f;
    [SerializeField] private float maxY = 18.5f;

    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite deathSprite;

    [SerializeField] public float respawnDelay { get; private set; } = 1f;

    private SpriteRenderer _spriteRenderer;
    private CommandManager _commandManager;
    private GameManager _gameManager;

    private Vector3 _spawnPosition;
    public Color _color;

    private ContactFilter2D _contactFilter2D;

    private float furthestRow;

    private IPlayerDataService _playerDataService;

    private void Awake()
    {
        _spawnPosition = transform.position;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;

        _commandManager = CommandManager.Instance;
        _gameManager = GameManager.Instance;

        _contactFilter2D = new ContactFilter2D();
        _contactFilter2D.NoFilter();
    }

    private void Start()
    {
        _playerDataService = ServiceLocator.Instance.Get<IPlayerDataService>();
        ColorData colorData = _playerDataService.GetCurrentPlayerData().playerColor;
        _color = new Color(colorData.r, colorData.g, colorData.b);
        SetColor(_color);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            _commandManager.UndoCommand();
        }
    }

    private void Move(Vector3 direction)
    {
        Vector3 target = transform.position + direction;
        if (!IsInBounds(target))
        {
            return;
        }

        Transform originalParent = transform.parent;

        Collider2D[] results = new Collider2D[2];
        int numberOfResults = Physics2D.OverlapBox(target, Vector2.zero, 0f, _contactFilter2D, results);

        if (numberOfResults > 0)
        {
            ProcessColliders(results, target);
        } else
        {
            transform.SetParent(null);
        }

        if (enabled) 
        { 
            if (target.y > furthestRow)
            {
                furthestRow = target.y;
                _gameManager.RowIncremented();
            }
            MoveCommand command = new MoveCommand(transform, originalParent, direction);
            _commandManager.ExecuteCommand(command);
        }
    }

    private void ProcessColliders(Collider2D[] colliders, Vector3 target)
    {
        Collider2D platformCollider = GetColliderWithTag(colliders, "Platform");
        Collider2D obstacleCollider = GetColliderWithTag(colliders, "Obstacle");

        if (platformCollider != null)
        {
            transform.SetParent(platformCollider.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if (obstacleCollider != null && platformCollider == null)
        {
            transform.position = target;
            Death();
        }
    }

    private bool IsInBounds(Vector3 target)
    {
        return (target.x >= minX && target.x <= maxX && target.y >= minY && target.y <= maxY);
    }

    private bool IsTagAtCollision(Collider2D collider, String tag)
    {
        return (collider != null && collider.CompareTag(tag));
    }

    private Collider2D GetColliderWithTag(Collider2D[] colliders, String tag)
    {
        foreach (Collider2D collider in colliders)
        {
            if (IsTagAtCollision(collider, tag))
            {
                return collider;
            }
        }
        return null;
    }

    private void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    private void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void Death()
    {
        transform.rotation = Quaternion.identity;
        SetSprite(deathSprite);
        SetColor(Color.white);
        enabled = false;
        _gameManager.PlayerDeath();
    }

    public void Respawn()
    {
        transform.rotation = Quaternion.identity;
        transform.position = _spawnPosition;
        SetSprite(idleSprite);
        SetColor(_color);
        gameObject.SetActive(true);
        furthestRow = transform.position.y;
        enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enabled && IsTagAtCollision(other, "Obstacle") && transform.parent == null)
        {
            Death();
        }
    }

}