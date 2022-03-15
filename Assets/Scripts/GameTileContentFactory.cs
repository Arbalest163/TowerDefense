using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileContentFactory : GameObjectFactory
{
    [SerializeField]
    private GameTileContent _destinationPrefab;

    [SerializeField]
    private GameTileContent _emptyPrefab;

    [SerializeField]
    private GameTileContent _wallPrefab;

    [SerializeField]
    private GameTileContent _spawnPointPrefab;

    public void Reclaim(GameTileContent content)
    {
        Destroy(content.gameObject);
    }

    public GameTileContent Get(GameTileContentType type)
    {
        return type switch
        {
            GameTileContentType.Destination => Get(_destinationPrefab),
            GameTileContentType.Empty => Get(_emptyPrefab),
            GameTileContentType.Wall => Get(_wallPrefab),
            GameTileContentType.SpawnPoint => Get(_spawnPointPrefab),
            _ => null
        };
    }
    private GameTileContent Get(GameTileContent prefab)
    {
        var instance = CreateGameObjectInstance(prefab);
        instance.OriginFactory = this;
        return instance;
    }
}
