using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUpdatePopup : Popup
{
    [SerializeField] private GameObject checkIcon;
    [SerializeField] private Text description;

    protected override void BeforeShow()
    {
        base.BeforeShow();

        CheckIcon();
        description.text = RemoteConfigController.Instance.UpdateDescription.Replace("<br>", "\n");
    }

    public void OnClickCheckButton()
    {
        Data.DontShowUpdateAgain = !Data.DontShowUpdateAgain;
        CheckIcon();
    }

    public void CheckIcon()
    {
        checkIcon.SetActive(!Data.DontShowUpdateAgain);
    }

    public void OnClickUpdateButton()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.gamee.herotowerwar");
#else
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1570840391");
#endif
    }
}
