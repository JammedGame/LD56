using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextWaveUI : MonoBehaviour
{
    [SerializeField] private Button nextWaveButton;
    public event Action NextWaveClick;

    private void Awake()
    {
        if (nextWaveButton != null)
        {
            nextWaveButton.onClick.AddListener(OnNextWaveClick);
        }
        else
        {
            Debug.LogError("NextWaveButton is not assigned in the Inspector.");
        }
    }

    private void OnNextWaveClick()
    {
        NextWaveClick?.Invoke();
        SceneManager.LoadScene("StegaTest");
    }
}
