using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private TMP_Text amountText;
    [SerializeField] private GameObject currencyIcon;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateCurrencyUI(int currentGold)
    {
        amountText.text = currentGold.ToString();
    }
}