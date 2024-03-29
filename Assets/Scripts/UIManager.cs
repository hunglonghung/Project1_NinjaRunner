using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager sceneInstance;

    public static UiManager Instance
    {
        get
        {
            if(sceneInstance == null)
            {
                sceneInstance = FindObjectOfType<UiManager>();

            }
            return sceneInstance;
        }
    }
    [SerializeField] TextMeshProUGUI coinText;
    public void SetCoin(int coinvalues)
    {
        coinText.text = coinvalues.ToString();
    }

}