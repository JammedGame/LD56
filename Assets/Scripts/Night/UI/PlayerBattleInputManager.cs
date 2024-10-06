using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Night;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBattleInputManager : MonoBehaviour
{
    [SerializeField] private BattleSpellsUIManager battleSpellsUIManagerPrefab;
    [SerializeField] private AreaOfEffectEllipse castTargetIndicatorPrefab;
    
    private BattleSpellsUIManager battleSpellsUIManager;

    private NightBattleContext battleContext;
    private SpellButton _selectedSpellButton;
    private AreaOfEffectEllipse castTargetIndicator;

    private SpellButton SelectedSpellButton
    {
        get => _selectedSpellButton;
        set
        {
            _selectedSpellButton = value;
            castTargetIndicator.IsActive = _selectedSpellButton != null;
            if (_selectedSpellButton != null)
            {
                castTargetIndicator.SetSize(_selectedSpellButton.Spell.CastArea);
            }
        }
    }

    private UserEquippedSpell SelectedSpell => SelectedSpellButton?.Spell;

    private void Awake()
    {
        castTargetIndicator = Instantiate(castTargetIndicatorPrefab, transform);
        castTargetIndicator.IsActive = false;
    }

    public void Setup(NightBattleContext ctx, UserBattleData userBattleData)
    {
        battleContext = ctx;
        
        battleSpellsUIManager = Instantiate(battleSpellsUIManagerPrefab);
        battleSpellsUIManager.OnSelectSpell = OnSelectSpell;

        KeyCode[] keyCodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };
        for (var i = 0; i < userBattleData.EquippedSpells.Count; i++)
        {
            UserEquippedSpell spell = userBattleData.EquippedSpells[i];
            battleSpellsUIManager.AddSpellButton(spell, keyCodes[i]);
        }
    }

    private void OnSelectSpell(SpellButton spellButton)
    {
        SelectedSpellButton = spellButton;
        castTargetIndicator.IsActive = true;
    }

    private void Update()
    {
        if (SelectedSpell == null)
        {
            return;
        }

        if (!SelectedSpell.IsOnCooldown)
        {
            bool raycastValid = InputUtils.ScreenToWorld(Input.mousePosition, out Vector3 castTargetPos);
            if (raycastValid)
            {
                castTargetIndicator.Position = castTargetPos;
                bool shouldCast = SelectedSpell.Blueprint.IsRapidFire ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
                if (shouldCast)
                {
                    battleContext.CastSpell(SelectedSpell, castTargetPos);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            SelectedSpellButton = null;
            battleSpellsUIManager.ResetButtonsInteractable();
        }
    }

    private void OnDestroy()
    {
        if (battleSpellsUIManager != null)
        {
            Destroy(battleSpellsUIManager.gameObject);
        }
    }
}
