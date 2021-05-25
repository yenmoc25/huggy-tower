using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyRewardPopup : Popup
{
    [SerializeField] private GameObject x5Button;
    [SerializeField] private List<DailyRewardItem> dailyRewardItems;

    private int coinCurrent;
    private bool hasCoinCurrent;

    protected override void BeforeShow()
    {
        base.BeforeShow();

        Reset();
    }

    public void Reset()
    {
        int dayTotal = (int)(DateTime.Now - DateTime.Parse(Data.DateTimeStart)).TotalDays;
        int dayStart = 7 * (Data.DailyRewardCurrent / 7);
        bool isDayLoop = dayStart >= Config.Instance.DailyRewards.Count;
        int dayOffset = 0;

        hasCoinCurrent = false;
        dailyRewardItems.ForEach(item =>
        {
            int day = dayStart + dayOffset;
            int coin = isDayLoop ? Config.Instance.DailyRewardsLoop[day % 7] : Config.Instance.DailyRewards[day];

            item.Init(day, coin, dayTotal, isDayLoop, this);
            item.Reset();

            dayOffset++;
        });

        SetX5ButtonActive(hasCoinCurrent);
    }

    public void SetCoinCurrent(int coinCurrent)
    {
        hasCoinCurrent = Data.DailyRewardCurrent % 7 != 6;
        this.coinCurrent = coinCurrent;
    }

    public void OnClickClaim()
    {
        Claim(1);
    }

    public void OnClickClaimAds()
    {
        AdController.Instance.ShowRewardedAd(() =>
        {
            Claim(5);
        });
    }

    public void Claim(int multiplier)
    {
        Data.CoinTotal += coinCurrent * multiplier;
        Data.DailyRewardCurrent++;
        Reset();
    }

    public void SetX5ButtonActive(bool active)
    {
        x5Button.SetActive(active);
    }
}