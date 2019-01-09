using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
/// <summary>
/// Game object that manages the ads of the game and give a basic functionality.
/// </summary>
public class UnityAds : MonoBehaviour
{

    /// <summary>
    /// Show a basic ad
    /// </summary>
    public void ShowBasicAd() {

        
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }

    }
    /// <summary>
    /// Show a rewarded ad and handle the result (finished, skyipped or failed)
    /// </summary>
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }
    /// <summary>
    /// Handle the result of an ad
    /// </summary>
    /// <param name="result">The result</param>
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
