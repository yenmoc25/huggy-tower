using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FacebookPopup : Popup
{
    [SerializeField] private List<GameObject> backgrounds;
    [SerializeField] private GameObject buttonClaim;
    [SerializeField] private CoinGeneration coinGeneration;
    [SerializeField] private GameObject coinTotal;

    private bool isClaming;

    protected override void BeforeShow()
    {
        base.BeforeShow();

        Reset();
    }

    private void Reset()
    {
        HideAll();
        CheckBackground();
    }

    private void HideAll()
    {
        backgrounds.ForEach(item => item.SetActive(false));
    }

    private void CheckBackground()
    {
        if (Data.JoinFbProgress > 2)
        {
            Data.JoinFbProgress = 0;
        }
        backgrounds[Data.JoinFbProgress].SetActive(true);
        coinTotal.SetActive(Data.JoinFbProgress < 2);
    }

    public void OnClickFacebookButton()
    {
        if (!isClaming)
        {
            isClaming = true;

            Data.JoinFbProgress = 1;

            ResourcesController.DailyQuest.IncreaseByType(DailyQuestType.LoginFacebook);

            DOTween.Sequence().AppendInterval(1).AppendCallback(() =>
            {
                Reset();
                isClaming = false;
            });

            Application.OpenURL("https://www.facebook.com/groups/hero.tower.wars");
        }
    }

    public void OnClickClaimButton()
    {
        if (!isClaming)
        {
            isClaming = true;

            buttonClaim.SetActive(false);

            int coinTotal = Data.CoinTotal + 500;
            coinGeneration.GenerateCoin(() =>
            {
                Data.CoinTotal++;
            }, () =>
            {
                Data.JoinFbProgress = 2;
                Data.CoinTotal = coinTotal;
                ResourcesController.Achievement.IncreaseByType(AchievementType.JoinGroupFacebookSuccessfully);
                Close();
                isClaming = false;
            });
        }
    }
}
