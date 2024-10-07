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

    private IEnumerable<TownBuilding> Buildings => GetComponentsInChildren<TownBuilding>();

    private TownBuilding selectedTownBuilding;

    private void Awake()
    {
        buildingUI.Clear();
        nextWaveButton.SetListener(OnNextWaveClick);
    }

    private void OnEnable()
    {
        buildingUI.UpgradeClick += OnBuildingUpgradeClick;
        foreach (var building in Buildings)
        {
            building.UpdateState();
            building.SelectBuilding += SelectBuilding;
            building.OfferUpgradeChoices += OfferUpgradeChoices;
        }
    }

    private void OnDisable()
    {
        buildingUI.UpgradeClick -= OnBuildingUpgradeClick;
        foreach (var building in Buildings)
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
        foreach (var building in Buildings)
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
        Destroy(originalBuilding.gameObject);
        var upgradedBuilding = Instantiate(chosenUpgrade, parent);
        upgradedBuilding.name = chosenUpgrade.name;
        upgradedBuilding.transform.localPosition = position;
        upgradedBuilding.Slot = slot;
        upgradedBuilding.Level++;
        upgradedBuilding.UpdateState();
        SelectBuilding(upgradedBuilding);
        upgradedBuilding.SelectBuilding += SelectBuilding;
        upgradedBuilding.OfferUpgradeChoices += OfferUpgradeChoices;
    }

    private static void OnNextWaveClick()
    {
        SceneManager.LoadScene("StegaTest");
    }
}