using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDailyQuestPopup : Popup
{
    [SerializeField] private AchievementPopup achievementPopup;
    [SerializeField] private DailyQuestPopup dailyQuestPopup;
    [SerializeField] private GameObject achievementActiveButton;
    [SerializeField] private GameObject dailyQuestActiveButton;

    protected override void BeforeShow()
    {
        base.BeforeShow();

        ResourcesController.DailyQuest.IncreaseByType(DailyQuestType.BuySkin);
        ResourcesController.Achievement.IncreaseByType(AchievementType.BuySkin);

        ResourcesController.DailyQuest.Reset();

        achievementPopup.Init(this);
        dailyQuestPopup.Init();

        achievementPopup.Show();
        dailyQuestPopup.Show();

        achievementPopup.UpdateProgress();


        if (data == null)
        {
            OnClickDailyQuestButton();
        }
        else
        {
            OnClickAchievementButton();
        }
    }

    public void OnClickDailyQuestButton()
    {
        achievementPopup.gameObject.SetActive(false);
        achievementActiveButton.SetActive(false);

        dailyQuestPopup.gameObject.SetActive(true);
        dailyQuestActiveButton.SetActive(true);
    }

    public void OnClickAchievementButton()
    {
        achievementPopup.gameObject.SetActive(true);
        achievementActiveButton.SetActive(true);

        dailyQuestPopup.gameObject.SetActive(false);
        dailyQuestActiveButton.SetActive(false);
    }
}
