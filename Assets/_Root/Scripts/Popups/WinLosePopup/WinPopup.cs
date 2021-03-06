using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : Popup
{
    [SerializeField] private ProgressGift progressGift;
    [SerializeField] private GameObject tapToContinueButton;
    [SerializeField] private GameObject claimX5Button;
    [SerializeField] private CoinGeneration coinGeneration;
    [SerializeField] private Image iconCrossAds;
    [SerializeField] private Sprite[] imgIconCrossAds;

    private string[] UrlData = new string[2] { "market://details?id=com.gamee.huggytimepin", "market://details?id=com.GameeStudio.PoppyPin3D" };
    private int idCrossCurrent = 0;
    protected override void BeforeShow()
    {

        base.BeforeShow();

        claimX5Button.SetActive(true);
        tapToContinueButton.SetActive(false);
        progressGift.Reset();

        idCrossCurrent = idCrossCurrent == 0 ? 1 : 0;
        iconCrossAds.sprite = imgIconCrossAds[idCrossCurrent];
        checkCrossAds();
    }

    void checkCrossAds()
    {
        Debug.Log(RemoteConfigController.Instance.HasCrossAds);
        iconCrossAds.gameObject.SetActive(RemoteConfigController.Instance.HasCrossAds);
    }

    protected override void AfterShown()
    {
        base.AfterShown();

        Data.CoinTotal += ResourcesController.Config.CoinBonusPerLevel;

        progressGift.Move(() =>
        {
            tapToContinueButton.SetActive(true);
        });
    }

    public void OnClickX5()
    {
        AdController.Instance.ShowRewardedAd(() =>
        {
            claimX5Button.SetActive(false);

            int coinTotal = Data.CoinTotal + ResourcesController.Config.CoinBonusPerLevel * 5;
            coinGeneration.GenerateCoin(() =>
            {
                Data.CoinTotal++;
            }, () =>
            {
                Data.CoinTotal = coinTotal;
            });

            AnalyticController.AdjustLogEventClaimX5CoinWinLevel();
        });
    }

    public void OnClickContinue()
    {
        GameController.Instance.OnNextLevel();
    }

    public void OnClickHomeButton()
    {
        GameController.Instance.OnBackToHome();
    }

    public void OnClickCrossAdsFishPin()
    {
#if UNITY_ANDROID
        // Application.OpenURL("market://details?id=com.gamee.savethehero");
        Application.OpenURL(UrlData[idCrossCurrent]);
#else
        // Application.OpenURL("itms-apps://itunes.apple.com/app/id1562329957");
#endif
    }
}
