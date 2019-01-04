using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour
{

    void Start()
    {
     

        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            ShowRewardedAd();
        }
    }


    public void ShowBasicAd() {

        
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }

    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");               
                GameManager.instance.RewardedForWatchingAd();                
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
