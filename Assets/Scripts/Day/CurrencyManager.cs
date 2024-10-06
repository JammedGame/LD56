using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private TMP_Text amountText;
    [SerializeField] private GameObject currencyIcon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    public void UpdateCurrencyUI(int currentGold)
    {
        amountText.text = currentGold.ToString();
    }
}