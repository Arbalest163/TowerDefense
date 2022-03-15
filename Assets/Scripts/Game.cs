using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _boardSize;

    [SerializeField]
    private GameBoard _board;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameTileContentFactory _contentFactory;

    [SerializeField]
    private EnemyFactory _enemyFactory;

    [SerializeField, Range(0.1f,10f)]
    private float _spawnSpeed;

    private float _spawnProgress;

    private EnemyCollection _enemies = new EnemyCollection();

    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    void Start()
    {
        _board.Initilize(_boardSize, _contentFactory);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HandleAlternativeTouch();
        }

        _spawnProgress +=_spawnSpeed * Time.deltaTime;
        while (_spawnProgress >= 1f)
        {
            _spawnProgress -= 1f;
            SpawnEnemy();
        }

        _enemies.GameUpdate();
    }

    private void SpawnEnemy()
    {
        var spawnPoint = _board.GetSpawnPoint(Random.Range(0, _board.SpawnPointCount));
        var enemy = _enemyFactory.Get();
        enemy.SpawnOn(spawnPoint);
        _enemies.Add(enemy);
    }

    private void HandleTouch()
    {
        var tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
           _board.ToggleWall(tile);
        }
    }
    private void HandleAlternativeTouch()
    {
        var tile = _board.GetTile(TouchRay);
        if (tile != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _board.ToggleDestination(tile);
            }
            else
            {
                _board.ToggleSpawnPoint(tile);
            }
        }
    }
}

