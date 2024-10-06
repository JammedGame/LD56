using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Spells;
using Night;
using UnityEngine;

public class BattleSpellsUIManager : MonoBehaviour
{
    [SerializeField] private SpellButton spellButtonPrefab;
    [SerializeField] private Transform spellButtonsParent;

    private readonly List<SpellButton> spellButtons = new List<SpellButton>();
    public Action<UserEquippedSpell> OnSelectSpell;

    public void AddSpellButton(UserEquippedSpell equippedSpell, KeyCode keyboardShortcut)
    {
        SpellButton newSpellButton = Instantiate(spellButtonPrefab, spellButtonsParent);
        newSpellButton.OnClick = SelectSpell;
        newSpellButton.Spell = equippedSpell;
        newSpellButton.KeyboardShortcut = keyboardShortcut;
        spellButtons.Add(newSpellButton);
    }

    private void SelectSpell(SpellButton selectedButton)
    {
        foreach (SpellButton button in spellButtons)
        {
            button.Interactable = button != selectedButton;
        }
        
        OnSelectSpell?.Invoke(selectedButton.Spell);
    }

    public void ClearSpellButtons()
    {
        spellButtons.Clear();
        foreach (Transform child in spellButtonsParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void Update()
    {
        foreach (SpellButton spellButton in spellButtons)
        {
            if (Input.GetKeyDown(spellButton.KeyboardShortcut))
            {
                spellButton.DoClick();
                break;
            }
        }
    }

    public void ResetButtonsInteractable()
    {
        foreach (SpellButton button in spellButtons)
        {
            button.Interactable = true;
        }
    }
}
