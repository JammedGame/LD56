using System;
using UnityEngine;

namespace Night.Town
{
    public class UpgradeChoicesOverlay : MonoBehaviour
    {
        [SerializeField] private UpgradeChoice choicePrefab;

        public void Initialize(TownBuilding buildingToUpgrade, Action<TownBuilding> onChosenUpgrade)
        {
            gameObject.SetActive(true);
            var choiceHolder = transform;
            choiceHolder.ClearTransform();
            foreach (var choice in buildingToUpgrade.UpgradeChoices)
            {
                Instantiate(choicePrefab, choiceHolder)
                    .Initialize(choice, () => { OnClick(onChosenUpgrade, choice); });
            }
        }

        private void OnClick(Action<TownBuilding> onChosenUpgrade, TownBuilding choice)
        {
            onChosenUpgrade(choice);
            gameObject.SetActive(false);
        }
    }
}