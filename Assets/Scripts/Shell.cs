using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : WarEntity
{
    private Vector3 _launchPoint;
    private Vector3 _targetPoint;
    private Vector3 _launchVelocity;

    private float _age;
    private float _blastRadius;
    private float _damage;

    public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity, float blastRadius, float damage)
    {
        _launchPoint = launchPoint;
        _targetPoint = targetPoint;
        _launchVelocity = launchVelocity;
        _blastRadius = blastRadius;
        _damage = damage;
    }

    public override bool GameUpdate()
    {
        _age += Time.deltaTime;
        Vector3 position = _launchPoint + _launchVelocity * _age;
        position.y -= 0.5f * 9.81f * _age * _age;

        if(position.y <=0)
        {
            Game.SpawnExplosion().Initialize(_targetPoint, _blastRadius, _damage);
            OriginFactory.Reclaim(this);
            return false;
        }

        transform.localPosition = position;

        Vector3 d = _launchVelocity;
        d.y -= 9.81f * _age;
        transform.localRotation = Quaternion.LookRotation(d);

        Game.SpawnExplosion().Initialize(position, 0.1f);

        return true;
    }
}
