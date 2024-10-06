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
    private string selectedSpellId;
    private SpellCastTargetIndicator castTargetIndicator;

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
            battleSpellsUIManager.AddSpellButton(spell.Blueprint, keyCodes[i]);
        }
    }

    private void OnSelectSpell(string spellId)
    {
        selectedSpellId = spellId;
        castTargetIndicator.IsActive = true;
    }

    private void Update()
    {
        if (selectedSpellId == null)
        {
            return;
        }

        bool raycastValid = InputUtils.ScreenToWorld(Input.mousePosition, out Vector3 castTargetPos);
        if (raycastValid)
        {
            castTargetIndicator.Position = castTargetPos;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (raycastValid)
            {
                battleContext.CastSpell(selectedSpellId, castTargetPos);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            selectedSpellId = null;
            battleSpellsUIManager.ResetButtonsInteractable();
            castTargetIndicator.IsActive = false;
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
