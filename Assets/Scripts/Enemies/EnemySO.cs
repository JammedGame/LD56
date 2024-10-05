using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy SO", menuName = "Enemy/New Enemy SO", order = 0)]
public class EnemySO : ScriptableObject
{
    [Header("Attack Settings")]
    [SerializeField] private float _agroRange = 10;
    public float AgroRange => _agroRange;

    [SerializeField] private float _attackRange = 3;
    public float AttackRange => _attackRange;

    [SerializeField] private int _damage = 1;
    public int Damage => _damage;

    [Space(10)]
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5;
    public float MoveSpeed  => _moveSpeed;

    [Space(10)]
    [Header("Health Settings:")]
    [SerializeField] private float _maxHealth = 10;
    public float MaxHealth => _maxHealth;
}
