using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using I2.Loc;

public class CountdownBeforeNewDay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private string pattern;

    private void Start()
    {
        InvokeRepeating("Countdown", 0, 1);
    }

    private void Countdown()
    {
        //countdownText.text = pattern.Replace("{}", Util.SecondsToTimeFormatBeforeNewDay());
        countdownText.GetComponent<LocalizationParamsManager>().SetParameterValue("VALUE", Util.SecondsToTimeFormatBeforeNewDay().ToString(), true);
    }
}