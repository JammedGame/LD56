using System.Collections.Generic;
using Night.Town;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownController : MonoBehaviour
{
    [SerializeField] private BuildingUI buildingUI;
    [SerializeField] private UpgradeChoicesOverlay upgradeChoicesOverlay;
    [SerializeField] private Button nextWaveButton;

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
            building.SelectBuilding += SelectBuilding;
            building.OfferUpgradeChoices += OfferUpgradeChoices;
        }
    }

    private void OnDisable()
    {
        buildingUI.UpgradeClick -= OnBuildingUpgradeClick;
        foreach (var building in buildings)
        {
            building.SelectBuilding -= SelectBuilding;
            building.OfferUpgradeChoices -= OfferUpgradeChoices;
        }
    }

    private void OnBuildingUpgradeClick()
    {
        if (selectedTownBuilding == null) return;

        selectedTownBuilding.TryUpgrade();
    }

    private void SelectBuilding(TownBuilding buildingToSelect)
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

    private void OfferUpgradeChoices(TownBuilding buildingToUpgrade)
    {
        upgradeChoicesOverlay.Initialize(buildingToUpgrade,
            chosenUpgrade => { OnChosenUpgrade(buildingToUpgrade, chosenUpgrade); });
    }

    private void OnChosenUpgrade(TownBuilding originalBuilding, TownBuilding chosenUpgrade)
    {
        var originalBuildingTransform = originalBuilding.transform;
        var parent = originalBuildingTransform.parent;
        var position = originalBuildingTransform.localPosition;
        var slot = originalBuilding.Slot;
        Destroy(originalBuilding);
        var upgradedBuilding = Instantiate(chosenUpgrade, parent);
        upgradedBuilding.transform.localPosition = position;
        upgradedBuilding.Slot = slot;
        SelectBuilding(upgradedBuilding);
    }

    private static void OnNextWaveClick()
    {
        SceneManager.LoadScene("StegaTest");
    }
}