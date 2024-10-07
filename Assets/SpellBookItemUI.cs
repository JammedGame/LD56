using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Spells;
using Night;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookItemUI : MonoBehaviour
{
    public Button UpgradeButton;
    public Image SpellIcon;
    public TMP_Text Level;
    public TMP_Text SpellName;
    public TMP_Text SpellUpgradeCost;

    public SpellBookItem MySpell;

    public void Update()
    {
        SpellBookState state = UserState.Instance.SpellBookState;
        int currentLevel = state.GetSpellLevel(MySpell);
        int maxLevel = MySpell.UpgradeGoldCostPerLevel.Count;
        SpellIcon.sprite = MySpell.Icon;
        SpellName.text = MySpell.DisplayName;
        Level.text = currentLevel == 0
                     ? $"not equipped"
                     : $"lvl {currentLevel}";
        
        if (currentLevel >= maxLevel)
        {
            SpellUpgradeCost.text = "MAXED OUT";
        }
        else
        {
            int costForUpgrade = MySpell.UpgradeGoldCostPerLevel[currentLevel];
            SpellUpgradeCost.text = currentLevel == 0
                                    ? $"BUY {costForUpgrade} Gold"
                                    : $"Upgrade for {costForUpgrade} Gold";
        }
    }

    public void OnBuy()
    {
        SpellBookState state = UserState.Instance.SpellBookState;
        int currentLevel = state.GetSpellLevel(MySpell);
        int maxLevel = MySpell.UpgradeGoldCostPerLevel.Count;
        if (currentLevel >= maxLevel)
        {
            return;
        }
     
        int costForUpgrade = MySpell.UpgradeGoldCostPerLevel[currentLevel];
        if (UserState.Instance.Gold.TrySpend(costForUpgrade))
        {
            state.UpgradeSpell(MySpell);
        }
    }
}
