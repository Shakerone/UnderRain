using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShowAd();
    }

    public void ShowAd()
    {
        Application.ExternalCall("ShowAd");
    }
}
