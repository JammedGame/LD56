using System;
using System.Collections.Generic;
using Night.Town;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownController : MonoBehaviour
{
    [SerializeField] private BuildingUI buildingUI;
    [SerializeField] private Button nextWaveButton;

    public event Action NextWaveClick;

    private IEnumerable<TownBuilding> buildings;

    private TownBuilding selectedTownBuilding;

    private void Awake()
    {
        buildings = GetComponentsInChildren<TownBuilding>();
        buildingUI.Clear();
        nextWaveButton.SetListener(OnNextWaveClick);
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

    private void OnNextWaveClick()
    {
        NextWaveClick?.Invoke();
        SceneManager.LoadScene("StegaTest");
    }
}