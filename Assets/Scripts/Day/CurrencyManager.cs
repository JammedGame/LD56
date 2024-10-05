using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Night;
using TMPro;

public class CurrencyManager : MonoBehaviour
{

    public TMP_Text amountText;
    public GameObject currencyIcon;

    public void UpdateCurrencyUI(int currentGold)
    {
        amountText.text = currentGold.ToString();
    }
}
