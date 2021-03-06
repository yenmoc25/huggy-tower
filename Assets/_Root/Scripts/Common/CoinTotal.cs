using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTotal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTotal;

    private void Awake()
    {
        EventController.CoinTotalChanged += UpdateCoinText;
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinTotal.text = Data.CoinTotal.ToString();
    }

    public void ShowShopPopup()
    {
        PopupController.Instance.Show<ShopPopup>(null, ShowAction.DoNothing);
    }

    private void OnDestroy()
    {
        EventController.CoinTotalChanged -= UpdateCoinText;
    }
}
