using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastTargetIndicator : MonoBehaviour
{
    private readonly Vector3 baseSize = new Vector3(2, 2, 1);
    private const float YOffset = 0.05f;

    [SerializeField] private Transform area;
    
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value + Vector3.up * YOffset;
    }

    public bool IsActive
    {
        set => gameObject.SetActive(value);
    }
    
    private void Awake()
    {
        transform.forward = Vector3.up;
    }

    public void SetSize(float xy)
    {
        SetSize(xy, xy);
    }
    
    public void SetSize(float x, float y)
    {
        area.localScale = new Vector3(baseSize.x * x, baseSize.y * y, baseSize.z);
    }
}
