using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    private Button button;

    public string SpellId;
    public KeyCode KeyboardShortcut;

    public string ButtonText
    {
        set => button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = value;
    }

    public Action OnClick
    {
        set => button.onClick.AddListener(() => value?.Invoke());
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
