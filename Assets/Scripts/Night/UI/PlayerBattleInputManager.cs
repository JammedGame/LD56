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
    private BattleSpellsUIManager battleSpellsUIManager;

    private NightBattleContext battleContext;
    private string selectedSpellId;
    
    public void Setup(NightBattleContext ctx, UserBattleData userBattleData)
    {
        battleContext = ctx;
        
        battleSpellsUIManager = Instantiate(battleSpellsUIManagerPrefab);
        battleSpellsUIManager.OnSelectSpell = OnSelectSpell;

        KeyCode[] keyCodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };
        for (var i = 0; i < userBattleData.EquippedSpells.Count; i++)
        {
            UserEquippedSpell spell = userBattleData.EquippedSpells[i];
            battleSpellsUIManager.AddSpellButton(spell.Blueprint.Id, keyCodes[i].ToString(), keyCodes[i]);
        }
    }

    private void OnSelectSpell(string spellId)
    {
        Debug.Log($"Select spell '{spellId}'");
        selectedSpellId = spellId;
    }

    private void Update()
    {
        if (selectedSpellId == null)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (InputUtils.ScreenToWorld(Input.mousePosition, out Vector3 castTargetPos))
            {
                battleContext.CastSpell(selectedSpellId, castTargetPos);
                selectedSpellId = null;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"Deselect spell '{selectedSpellId}'");
            selectedSpellId = null;
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
