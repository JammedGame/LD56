using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleInputManager : MonoBehaviour
{
    [SerializeField] private BattleUIManager battleUIManagerPrefab;
    private BattleUIManager battleUIManager;

    private void Start()
    {
        battleUIManager = Instantiate(battleUIManagerPrefab);
        battleUIManager.AddSpellButton("spell_frost", "Q", KeyCode.Q);
        battleUIManager.AddSpellButton("spell_fireball","W", KeyCode.W);
        battleUIManager.AddSpellButton("spell_zap","E", KeyCode.E);
        battleUIManager.AddSpellButton("spell_heal","R", KeyCode.R);

        battleUIManager.OnCastSpell = OnCastSpell;
    }

    private void OnCastSpell(string spellId)
    {
        Debug.Log($"Cast spell '{spellId}'");
    }
}
