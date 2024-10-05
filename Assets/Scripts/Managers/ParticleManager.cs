using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : NonPersistentSingleton<ParticleManager>
{
    private void Awake()
    {
        Initialize();
    }
}
