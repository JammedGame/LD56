using System.Collections.Generic;
using Night.Town;
using UnityEngine;

public class TownController : MonoBehaviour
{
    private IEnumerable<TownBuilding> buildings;

    public TownBuilding SelectedTownBuilding { get; private set; }

    private void Awake()
    {
        buildings = GetComponentsInChildren<TownBuilding>();
    }
}