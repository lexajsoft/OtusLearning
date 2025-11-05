using System;
using ShootEmUp;
using UnityEngine;
using Bullet = Zenject.SpaceFighter.Bullet;

public class Npc : MonoBehaviour
{
    [SerializeField] private MoveComponent _moveComponent;
    [SerializeField] private WeaponComponent _weaponComponent;
    [SerializeField] private HitPointsComponent _hitPointsComponent;
    [SerializeField] private TeamComponent _teamComponent;

    public MoveComponent MoveComponent => _moveComponent;
    public WeaponComponent WeaponComponent => _weaponComponent;
    public HitPointsComponent HitPointsComponent => _hitPointsComponent;
    public TeamComponent TeamComponent => _teamComponent;
    
}
