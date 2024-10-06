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

    private void OnEnable()
    {
        foreach (var building in buildings)
        {
            building.SelectBuilding += OnSelectBuilding;
        }
    }

    private void OnDisable()
    {
        foreach (var building in buildings)
        {
            building.SelectBuilding -= OnSelectBuilding;
        }
    }

    private void OnSelectBuilding(TownBuilding buildingToSelect)
    {
        Debug.Log($"{buildingToSelect.name} has been selected");
        SelectedTownBuilding = buildingToSelect;
        foreach (var building in buildings)
        {
            buildingToSelect.ToggleSelected(building == buildingToSelect);
        }
    }
}