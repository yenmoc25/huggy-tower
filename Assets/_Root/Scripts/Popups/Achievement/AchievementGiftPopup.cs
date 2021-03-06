using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class AchievementGiftPopup : Popup
{
    [SerializeField] private SkeletonGraphic hero;

    private SkinData skinData;
    private int indexSkin;
    private AchievementTargetData achievementTargetData;

    protected override void BeforeShow()
    {
        base.BeforeShow();

        indexSkin = (int)data;
        skinData = ResourcesController.Hero.SkinAchievements[indexSkin];
        hero.ChangeSkin(skinData.SkinName);
        achievementTargetData = ResourcesController.Achievement.AchievementTargetDatas[indexSkin];

        skinData.IsUnlocked = true;
        achievementTargetData.IsClaimed = true;
        EventController.SkinPopupReseted?.Invoke();
    }

    public void OnClickClaimButton()
    {
        Data.currentSkinHeroId = skinData.Id;
        Data.CurrentSkinHero = skinData.SkinName;
        Close();
    }
}
