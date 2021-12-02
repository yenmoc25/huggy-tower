using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class DailyRewardPopupEvent : Popup
{
    [SerializeField] private PanalItemWeek panalItemWeek;
    [SerializeField] private GameObject containerGird;
    [SerializeField] private CoinGeneration coinGeneration;
    [SerializeField] private GameObject coinTotal;
    [SerializeField] private GameObject tapGift;
    [SerializeField] private GameObject tapEvent;
    [SerializeField] private ToggleButton btnGift;
    [SerializeField] private ToggleButton btnEvent;

    protected override void BeforeShow()
    {
        base.BeforeShow();
        InitData();
    }

    void InitData()
    {
        var dataConfig = ResourcesController.DailyEventReward.DailyRewards;
        var numRows = Math.Ceiling(dataConfig.Count / 7f);
        for (int i = 0; i < numRows; i++)
        {

            Transform row;
            int maxCount = 7;
            if ((i + 1) * 7 > dataConfig.Count) maxCount = dataConfig.Count - i * 7;
            var dataWeek = dataConfig.GetRange(i * 7, maxCount);
            if (i < containerGird.transform.childCount)
                row = containerGird.transform.GetChild(i);
            else
            {
                row = Instantiate(panalItemWeek.gameObject, containerGird.transform).transform;
                row.SetParent(containerGird.transform);
            }
            var _comp = row.GetComponent<PanalItemWeek>();
            _comp.InitDataWeek(i, dataWeek, (day, cfg, item) =>
            {
                OnClickClaimCallBack(day, cfg, item, coinTotal);
            });
        }
    }
    void OnClickClaimCallBack(int day, ItemConfigEvent cfg, GameObject from, GameObject to)
    {
        Data.DailyRewardEventCurrent = day + 1;
        Data.lastTimeClaimDailyEvent = DateTime.Now.ToString();
        if (cfg.SkinId > 0)
        {
            var dataSkin = ResourcesController.Hero.SkinDatas[cfg.SkinId];
            dataSkin.IsUnlocked = true;
        }
        if (cfg.Coin > 0)
        {
            int coinTotal = Data.CoinTotal + cfg.Coin;
            TGDatas.TotalTurkeyText += cfg.CandyXmas;
            TGDatas.TotalTurkey = TGDatas.TotalTurkeyText;
            coinGeneration.GenerateCoin(() =>
            {
                Data.CoinTotal++;
            }, () =>
            {
                Data.CoinTotal = coinTotal;
            }, from, to);
        }
    }

    public void onClickGift()
    {
        this.btnGift.isCheck = true;
        this.btnEvent.isCheck = false;
        tapGift.SetActive(true);
        tapEvent.SetActive(false);
    }

    public void onClickEvent()
    {
        this.btnGift.isCheck = false;
        this.btnEvent.isCheck = true;
        tapGift.SetActive(false);
        tapEvent.SetActive(true);
    }


}
