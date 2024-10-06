using Night;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text amountText;
    [SerializeField] private GameObject currencyIcon;

    private int lastGold = -1;

    public void UpdateCurrencyUI(int currentGold)
    {
        if (lastGold != currentGold)
        {
            amountText.text = currentGold.ToString();
            lastGold = currentGold;
        }
    }

    public void Update()
    {
        UpdateCurrencyUI(UserState.Instance.Gold.Currency);        
    }
}