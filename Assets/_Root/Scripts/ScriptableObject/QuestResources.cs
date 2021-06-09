using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "QuestResources", menuName = "ScriptableObjects/QuestResources")]
public class QuestResources : ScriptableObject
{
    public List<QuestData> questDatas;

    public QuestData GetDataByCondition(ELevelCondition condition)
    {
        foreach (var data in questDatas)
        {
            if (data.condition == condition)
            {
                return data;
            }
        }

        return null;
    }
}

[Serializable]
public class QuestData
{
    public ELevelCondition condition;
    public string quest;
    public Sprite sprite;
}