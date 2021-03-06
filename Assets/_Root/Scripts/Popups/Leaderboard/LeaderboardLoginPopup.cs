using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class LeaderboardLoginPopup : Popup
{
    // [SerializeField] private TMP_InputField inputName;
    [SerializeField] private InputField inputName;
    [SerializeField] private TextMeshProUGUI textWarning;
    [SerializeField] private Transform content;
    [SerializeField] private LeaderboardCountryItem countryItemCurrent;
    [SerializeField] private LeaderboardCountryItem countryItemPrefab;
    [SerializeField] private GameObject countryBox;
    [SerializeField] private GameObject btnDisable;
    [SerializeField] private GameObject connecting;

    private bool countryBoxStatus;
    private CountryData countryCurrent;

    protected override void AfterInstantiate()
    {
        base.AfterInstantiate();

        content.Clear();

        ResourcesController.Country.CountryDatas.ForEach(data =>
        {
            LeaderboardCountryItem countryItem = Instantiate(countryItemPrefab, content);
            countryItem.Init(data, this);
        });

        FetchCountryCurrent();
        countryItemCurrent.Init(countryCurrent);
    }

    private void FetchCountryCurrent()
    {
        string countryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName;
        CountryData countryData = ResourcesController.Country.GetDataByCode(countryCode);
        if (countryData == null)
        {
            countryCurrent = ResourcesController.Country.GetDataByCode("US");
        }
        else
        {
            countryCurrent = countryData;
        }
    }

    protected override void BeforeShow()
    {
        base.BeforeShow();

        countryBoxStatus = false;
        countryBox.SetActive(false);
        btnDisable.SetActive(false);
        connecting.SetActive(false);
    }

    public void ToggleCountryBox()
    {
        countryBoxStatus = !countryBoxStatus;
        countryBox.SetActive(countryBoxStatus);
    }

    public void OnClickOKButton()
    {
        if (inputName.text == "")
        {
            ShowWarning("Name can't be empty!");
        }
        else
        {
            btnDisable.SetActive(true);
            connecting.SetActive(true);
            if (data == null)
            {
                LeaderboardController.Instance.Login(
                    inputName.text,
                    countryCurrent.Code,
                    () => Close(),
                    () =>
                    {
                        ShowWarning("The name you choose already exists!");
                    },
                    () =>
                    {
                        ShowWarning("Check your network connection again!");
                    }
                );
            }
            else if ((int)data == 1)
            {
                LeaderboardRescuePartyController.Instance.Login(
                    inputName.text,
                    countryCurrent.Code,
                    () => Close(),
                    () =>
                    {
                        ShowWarning("The name you choose already exists!");
                    },
                    () =>
                    {
                        ShowWarning("Check your network connection again!");
                    }
                );
            }
            else if ((int)data == 2)
            {
                Debug.Log(1);
                TGRankController.Instance.Login(
                    inputName.text,
                    countryCurrent.Code,
                    () => Close(),
                    () =>
                    {
                        ShowWarning("The name you choose already exists!");
                    },
                    () =>
                    {
                        ShowWarning("Check your network connection again!");
                    }
                );
            }
        }
    }

    private void ShowWarning(string text)
    {
        btnDisable.SetActive(false);
        connecting.SetActive(false);
        textWarning.text = text;
        textWarning.gameObject.SetActive(true);
    }

    public void ChangeCurrent(CountryData countryData)
    {
        countryCurrent = countryData;
        countryItemCurrent.Init(countryData);
    }
}
