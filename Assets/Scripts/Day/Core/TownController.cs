using System.Collections.Generic;
using Night.Town;
using UnityEngine;

public class TownController : MonoBehaviour
{
    [SerializeField] private BuildingUI buildingUI;

    private IEnumerable<TownBuilding> buildings;

    public TownBuilding SelectedTownBuilding { get; private set; }

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
        if (SelectedTownBuilding == null) return;

        SelectedTownBuilding.TryUpgrade();
    }

    private void OnSelectBuilding(TownBuilding buildingToSelect)
    {
        SelectedTownBuilding = buildingToSelect;
        foreach (var building in buildings)
        {
            building.ToggleSelected(building == buildingToSelect);
        }

        RefreshBuildingUI();
    }

    private void RefreshBuildingUI()
    {
        if (SelectedTownBuilding == null)
        {
            buildingUI.Clear();
            return;
        }

        buildingUI.SetData(SelectedTownBuilding.name, SelectedTownBuilding.Level, SelectedTownBuilding.CurrentCost);
    }
}