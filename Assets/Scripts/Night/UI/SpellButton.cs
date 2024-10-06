using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Spells;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    private Button button;
    private KeyCode keyboardShortcut;
    private SpellBookItem spell;

    [SerializeField] private TMPro.TextMeshProUGUI keyboardShortcutText;
    [SerializeField] private TMPro.TextMeshProUGUI spellNameText;
    [SerializeField] private Image spellIconImage;

    public SpellBookItem Spell
    {
        get => spell;
        set
        {
            spell = value;
            spellNameText.text = value.DisplayName;
            spellIconImage.sprite = value.Icon;
        }
    }
    
    public KeyCode KeyboardShortcut
    {
        get => keyboardShortcut;
        set
        {
            keyboardShortcut = value;
            keyboardShortcutText.text = value.ToString();
        }
    }

    public Action<SpellButton> OnClick
    {
        set => button.onClick.AddListener(() => value?.Invoke(this));
    }

    public bool Interactable
    {
        set => button.interactable = value;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void DoClick()
    {
        ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }
}
