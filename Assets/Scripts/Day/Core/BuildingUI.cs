using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text buildingTitle;
    [SerializeField] private TMP_Text buildingLevel;
    [SerializeField] private TMP_Text buildingCost;
    [SerializeField] private Button upgradeButton;


    public event Action UpgradeClick;
    private void Awake()
    {
        upgradeButton.SetListener(OnUpgradeClick);
    }

    private void OnUpgradeClick()
    {
        UpgradeClick?.Invoke();
    }

    public void SetData(string title, int level, int cost)
    {
        gameObject.SetActive(true);
        buildingTitle.text = title;
        buildingLevel.text = $"Level {level + 1}";

        if (cost is >= 0 and < int.MaxValue)
        {
            buildingCost.text = cost.ToString();
            upgradeButton.gameObject.SetActive(true);
        }
        else
        {
            upgradeButton.gameObject.SetActive(false);
        }
    }


    public void Clear()
    {
        gameObject.SetActive(false);
    }
}
