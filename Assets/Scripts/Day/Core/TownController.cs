using System.Collections.Generic;
using Night.Town;
using UnityEngine;

public class TownController : MonoBehaviour
{
    [SerializeField] private BuildingUI buildingUI;

    private IEnumerable<TownBuilding> buildings;

    private TownBuilding selectedTownBuilding;

    private void Awake()
    {
        buildings = GetComponentsInChildren<TownBuilding>();
        buildingUI.Clear();
    }

    private void OnEnable()
    {
        buildingUI.UpgradeClick += OnBuildingUpgradeClick;
        foreach (var building in buildings)
        {
            building.SelectBuilding += OnSelectBuilding;
        }
    }

    private void OnDisable()
    {
        buildingUI.UpgradeClick -= OnBuildingUpgradeClick;
        foreach (var building in buildings)
        {
            building.SelectBuilding -= OnSelectBuilding;
        }
    }

    private void OnBuildingUpgradeClick()
    {
        if (selectedTownBuilding == null) return;

        selectedTownBuilding.TryUpgrade();
    }

    private void OnSelectBuilding(TownBuilding buildingToSelect)
    {
        selectedTownBuilding = buildingToSelect;
        foreach (var building in buildings)
        {
            building.ToggleSelected(building == buildingToSelect);
        }

        RefreshBuildingUI();
    }

    private void RefreshBuildingUI()
    {
        if (selectedTownBuilding == null)
        {
            buildingUI.Clear();
            return;
        }

        buildingUI.SetData(selectedTownBuilding.name, selectedTownBuilding.Level, selectedTownBuilding.CurrentCost);
    }
}