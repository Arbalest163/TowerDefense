using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField, Range(1f, 100f)]
    private float _damagePerSecond = 10f;

    [SerializeField]
    private Transform _turrel;

    [SerializeField]
    private Transform _lasserBeam;

    [SerializeField]
    private Vector3 _laserBeamScale;

    private TargetPoint _target;

    public override TowerType Type => TowerType.Laser;

    private void Awake()
    {
        _laserBeamScale = _lasserBeam.localScale;
    }
    public override void GameUpdate()
    {
        if (IsTargetTracked(ref _target) || IsAcquireTarget(out _target))
        {
            Shoot();
        }
        else
        {
            _lasserBeam.localScale = Vector3.zero;
        }
    }

    private void Shoot()
    {
        var point = _target.Position;
        _turrel.LookAt(point);
        _lasserBeam.localRotation = _turrel.localRotation;

        var distance = Vector3.Distance(_turrel.position, point);
        _laserBeamScale.z = distance;
        _lasserBeam.localScale = _laserBeamScale;
        _lasserBeam.localPosition = _turrel.localPosition + 0.5f * distance * _lasserBeam.forward;

        _target.Enemy.TakeDamage(_damagePerSecond * Time.deltaTime);
    }
}
