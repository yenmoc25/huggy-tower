﻿using System;
using GoogleMobileAds.Api;
using UnityEngine;


public class AdMobController : MonoBehaviour, IAd
{
#if UNITY_EDITOR
    private string bannerId = "ca-app-pub-3940256099942544/6300978111";
    private string interstitialId = "ca-app-pub-3940256099942544/1033173712";
    private string rewardedId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_ANDROID
    private string bannerId = "ca-app-pub-8566745611252640/9406501328";
    private string interstitialId = "ca-app-pub-8566745611252640/8093419659";
    private string rewardedId = "ca-app-pub-8566745611252640/6780337982";
#elif UNITY_IOS
    private string bannerId = "ca-app-pub-8566745611252640/1287914758";
    private string interstitialId = "ca-app-pub-8566745611252640/9268871770";
    private string rewardedId = "ca-app-pub-8566745611252640/5329626764";
#endif

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    public bool IsBannerLoaded => bannerView != null;
    public bool IsInterLoaded => interstitial != null && interstitial.IsLoaded();
    public bool IsRewardLoaded => rewardedAd != null && rewardedAd.IsLoaded();

    public Action OnInterClosed { get; set; }
    public Action OnInterLoaded { get; set; }
    public Action OnRewardLoaded { get; set; }
    public Action OnRewardClosed { get; set; }
    public Action OnRewardEarned { get; set; }

    public void Init(Action OnInterClosed, Action OnInterLoaded, Action OnRewardLoaded, Action OnRewardClosed, Action OnRewardEarned)
    {
        MobileAds.Initialize(initStatus => { });

        this.OnInterClosed = OnInterClosed;
        this.OnInterLoaded = OnInterLoaded;
        this.OnRewardLoaded = OnRewardLoaded;
        this.OnRewardClosed = OnRewardClosed;
        this.OnRewardEarned = OnRewardEarned;
    }

    public AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    public void RequestBanner()
    {
        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        bannerView.LoadAd(GetAdRequest());
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    public void RequestInterstitial()
    {
        interstitial = new InterstitialAd(interstitialId);
        interstitial.OnAdClosed += (sender, args) => OnInterClosed();
        interstitial.OnAdLoaded += (sender, args) => OnInterLoaded();
        interstitial.LoadAd(GetAdRequest());
    }

    public void ShowInterstitial()
    {
        interstitial.Show();
    }

    public void RequestRewarded()
    {
        rewardedAd = new RewardedAd(rewardedId);
        rewardedAd.OnAdClosed += (sender, args) => OnRewardClosed();
        rewardedAd.OnAdLoaded += (sender, args) => OnRewardLoaded();
        rewardedAd.OnUserEarnedReward += (sender, args) => OnRewardEarned();
        rewardedAd.LoadAd(GetAdRequest());
    }

    public void ShowRewardedAd()
    {
        rewardedAd.Show();
    }
}