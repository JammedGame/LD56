using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private SpellButton spellButtonPrefab;
    [SerializeField] private Transform spellButtonsParent;

    private readonly List<SpellButton> spellButtons = new List<SpellButton>();
    public Action<string> OnCastSpell;

    public void AddSpellButton(string spellId, string buttonText, KeyCode keyboardShortcut)
    {
        SpellButton newSpellButton = Instantiate(spellButtonPrefab, spellButtonsParent);
        newSpellButton.ButtonText = buttonText;
        newSpellButton.OnClick = () => OnCastSpell?.Invoke(spellId);
        newSpellButton.SpellId = spellId;
        newSpellButton.KeyboardShortcut = keyboardShortcut;
        spellButtons.Add(newSpellButton);
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
}
