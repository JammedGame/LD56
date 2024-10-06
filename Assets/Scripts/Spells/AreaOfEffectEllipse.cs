using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectEllipse : MonoBehaviour
{
    private readonly Vector3 baseSize = new Vector3(2, 2, 1);
    private const float YOffset = 0.05f;

    [SerializeField] private Transform area;
    
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value + Vector3.up * YOffset;
    }

    public Color Color
    {
        set => area.GetComponent<SpriteRenderer>().color = value;
    }

    public bool IsActive
    {
        set => gameObject.SetActive(value);
    }
    
    private void Awake()
    {
        transform.forward = Vector3.up;
    }
    
    public void SetSize(Vector2 size)
    {
        if (size == Vector2.zero)
        {
            size = Vector2.one;
        }
        
        area.localScale = new Vector3(baseSize.x * size.x, baseSize.y * size.y, baseSize.z);
    }
}
