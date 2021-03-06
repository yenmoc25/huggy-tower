using System;
using System.Collections.Generic;
using System.Text;
using Spine.Unity;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SkinResources", menuName = "ScriptableObjects/SkinResources")]
public class SkinResources : ScriptableObject, IHasSkeletonDataAsset
{
    [SerializeField] private SkeletonDataAsset skeletonDataAsset;
    public SkeletonDataAsset SkeletonDataAsset => skeletonDataAsset;

    public List<SkinData> SkinDatas;

    public List<SkinData> SkinsHaveFeature;
    [SerializeField, SpineSkin] private string skinNameDefault;

    public SkinData SkinDefault => SkinDatas.Find(item => item.SkinName == skinNameDefault);
    public List<SkinData> SkinsDailyReward => SkinDatas.FindAll(item => item.SkinType == SkinType.Daily);
    public List<SkinData> SkinsCoin => SkinDatas.FindAll(item => !item.IsUnlocked && item.SkinType == SkinType.Coin);
    public List<SkinData> SkinAchievements => SkinDatas.FindAll(item => item.SkinType == SkinType.Achievement);
    public SkinData SkinGiftcode => SkinDatas.Find(item => item.SkinType == SkinType.Giftcode);
    public List<SkinData> SkinsIsUnlocked => SkinDatas.FindAll(item => item.IsUnlocked && item.SkinName != skinNameDefault);
    public SkinData SkinLuckySpin => SkinDatas.Find(item => item.SkinType == SkinType.TGLuckySpin);

    public int TotalSkinUnlocked()
    {
        int count = 0;
        for (int i = 0; i < SkinDatas.Count; i++)
        {
            if (SkinDatas[i].IsUnlocked)
            {
                count++;
            }
        }

        return count;
    }
    public bool HasNoti
    {
        get
        {
            foreach (var skin in SkinsCoin)
            {
                if (Data.CoinTotal >= skin.Coin)
                {
                    return true;
                }
            }

            return false;
        }
    }
    public void Reset()
    {
        SkinDefault.IsUnlocked = true;
    }

    [ContextMenu("Convert")]
    public string ConvertData()
    {
        StringBuilder result = new StringBuilder("");
        for (int i = 0; i < SkinDatas.Count; i++)
        {
            result.Append($"{SkinDatas[i].IsUnlocked}@");
        }

        result.Remove(result.Length - 1, 1);
        Debug.Log(result.ToString());
        return result.ToString();
    }

    public void TransformTargetData(string raw)
    {
        var result = raw.Split('@');

        int count = result.Length;
        if (count > SkinDatas.Count) count = SkinDatas.Count;

        for (int i = 0; i < count; i++)
        {
            SkinDatas[i].IsUnlocked = bool.Parse(result[i].ToLower());
        }
    }
    public SkinData GetSkinDataById(string skinId)
    {
        for (int i = 0; i < SkinDatas.Count; i++)
        {
            var item = SkinDatas[i];
            if (item.Id == skinId)
                return item;
        }
        return null;
    }
}



[Serializable]
public class SkinData
{
    [SpineSkin] public string SkinName;

    [GUID] public string Id;

    public string Name;

    public SkinType SkinType;
    public RescuePartyType RescuePartyType;
    public TGType TGType;

    public int Coin;

    public string Giftcode;
    public int NumberMedalTarget;
    public int DayDaily;
    public int NumberAchievement;
    public int NumberTurkeyTarget;
    public bool HasNotiRescueParty
    {
        get
        {
            if (!IsUnlocked)
            {
                if (RescuePartyType == RescuePartyType.Top100)
                {
                    return false;
                }
                else
                {
                    return Data.TotalGoldMedal >= NumberMedalTarget;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public bool HasNotiTG
    {
        get
        {
            if (!IsUnlocked)
            {
                if (TGType == TGType.Top100)
                {
                    return false;
                }
                else
                {
                    return TGDatas.TotalTurkey >= NumberTurkeyTarget;
                }
            }
            else
            {
                return false;
            }
        }
    }

    public bool IsUnlocked
    {
        get
        {
            Data.IdCheckUnlocked = Id;
            return Data.IsUnlocked;
        }

        set
        {
            Data.IdCheckUnlocked = Id;
            Data.IsUnlocked = value;
        }
    }
    public string FeatureHead;
}