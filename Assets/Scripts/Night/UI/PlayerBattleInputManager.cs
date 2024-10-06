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
    [SerializeField] private SpellCastTargetIndicator castTargetIndicatorPrefab;
    
    private BattleSpellsUIManager battleSpellsUIManager;

    private NightBattleContext battleContext;
    private UserEquippedSpell _selectedSpell;
    private SpellCastTargetIndicator castTargetIndicator;
    private float lastCastTime;

    private UserEquippedSpell SelectedSpell
    {
        get => _selectedSpell;
        set
        {
            _selectedSpell = value;
            castTargetIndicator.IsActive = _selectedSpell != null;
            if (_selectedSpell != null)
            {
                castTargetIndicator.SetSize(_selectedSpell.CastArea);
            }
        }
    }

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

    private void OnSelectSpell(UserEquippedSpell userEquippedSpell)
    {
        SelectedSpell = userEquippedSpell;
        castTargetIndicator.IsActive = true;
    }

    private void Update()
    {
        if (SelectedSpell == null)
        {
            return;
        }

        bool raycastValid = InputUtils.ScreenToWorld(Input.mousePosition, out Vector3 castTargetPos);
        if (raycastValid)
        {
            castTargetIndicator.Position = castTargetPos;
            bool shouldCast;
            if (SelectedSpell.Blueprint.IsRapidFire)
            {
                shouldCast = Input.GetMouseButton(0) && Time.time - lastCastTime > SelectedSpell.Blueprint.RapidFireCooldown;
            }
            else
            {
                shouldCast = Input.GetMouseButtonDown(0);
            }
            
            if (shouldCast)
            {
                battleContext.CastSpell(SelectedSpell, castTargetPos);
                lastCastTime = Time.time;
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            SelectedSpell = null;
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
