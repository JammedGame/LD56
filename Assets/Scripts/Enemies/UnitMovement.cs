using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _attackRange = 3;
    [SerializeField] private float _agroRange = 30;

    [Space(10)]
    public Transform Target;
    [SerializeField] private LayerMask _targetLayers;

    private void Update()
    {
        Tick();
    }

    public void Tick()
    {
        Target = SearchForTarget();
        if (Target == null) return;

        if (Vector3.Distance(transform.position, Target.position) > _attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Time.deltaTime * _moveSpeed);
        }
    }

    private Transform SearchForTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, _agroRange, _targetLayers);
        if (targets.Length == 0) return null;
        return targets[0].transform;
    }
}
