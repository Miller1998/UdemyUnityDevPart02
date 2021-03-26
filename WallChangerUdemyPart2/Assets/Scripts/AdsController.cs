using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdsController : MonoBehaviour
{
    private static string storeId = "4064422";
    private static string videoAd = "video";



    // Start is called before the first frame update
    private void Start()
    {
        Monetization.Initialize(storeId, false); //if we was publication our games set it to true but if we're not just set into false;
    }

    private void Update()
    {
        
    }

    public static void VideoAd()
    {
        
        if (Monetization.IsReady(videoAd))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(videoAd) as ShowAdPlacementContent;
            if (ad != null)
            {
                ad.Show();
            }
        }

    }

}
