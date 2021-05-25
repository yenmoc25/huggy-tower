using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ShopItemType shopItemType;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject doneIcon;

    private ShopPopup shopPopup;

    public void SetShopPopup(ShopPopup shopPopup)
    {
        this.shopPopup = shopPopup;
    }

    private void Start()
    {
        CheckNonConsume();
    }

    public void CheckNonConsume()
    {
        switch (shopItemType)
        {
            case ShopItemType.UnlockAllSkins:
                SetStateItems(Data.IsUnlockAllSkins);
                break;
            case ShopItemType.RemoveAds:
                SetStateItems(Data.IsRemovedAds);
                break;
            case ShopItemType.Vip:
                SetStateItems(Data.IsVip);
                break;
            default:
                SetStateItems(false);
                break;
        }
    }

    public void SetStateItems(bool done)
    {
        buyButton.SetActive(!done);
        doneIcon.SetActive(done);
    }

    public void OnPurchaseSuccess()
    {
        switch (shopItemType)
        {
            case ShopItemType.Gold1:
                Data.CoinTotal += 10000;
                break;
            case ShopItemType.Gold2:
                Data.CoinTotal += 10000;
                break;
            case ShopItemType.Gold3:
                Data.CoinTotal += 10000;
                break;
            case ShopItemType.UnlockAllSkins:
                Data.IsUnlockAllSkins = true;
                break;
            case ShopItemType.RemoveAds:
                Data.IsRemovedAds = true;
                break;
            case ShopItemType.Vip:
                Data.IsUnlockAllSkins = true;
                Data.IsRemovedAds = true;
                Data.IsVip = true;
                break;
        }

        shopPopup.CheckItems();
    }
}

enum ShopItemType
{
    Gold1,
    Gold2,
    Gold3,
    UnlockAllSkins,
    RemoveAds,
    Vip
}