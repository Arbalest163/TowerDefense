using System;
using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [Serializable]
    class EnemyConfig
    {
        public Enemy Prefab;
        [FloatRangeSlider(0.5f, 2f)]
        public FloatRange Scale = new FloatRange(1f);
        [FloatRangeSlider(-0.4f, 0.4f)]
        public FloatRange PathOffset = new FloatRange(0f);
        [FloatRangeSlider(0.2f, 5f)]
        public FloatRange Speed = new FloatRange(1f);
        [FloatRangeSlider(10f, 1000f)]
        public FloatRange Health = new FloatRange(100f);
    }

    [SerializeField]
    private EnemyConfig _small, _medium, _golem, _zombie;

    public Enemy Get(EnemyType type)
    {
        var config = GetConfig(type);
        var instance = CreateGameObjectInstance(config.Prefab);
        instance.OriginFactory = this;
        instance.Initialize(config.Scale.RandomValueRange, config.PathOffset.RandomValueRange, config.Speed.RandomValueRange, config.Health.RandomValueRange);
        return instance;
    }

    private EnemyConfig GetConfig(EnemyType type)
    {
        return type switch
        {
            EnemyType.Golem => _golem,
            EnemyType.Medium => _medium,
            EnemyType.Small => _small,
            EnemyType.Zombie => _zombie,
            _ => _medium
        };
    }

    public void Reclaim(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}

