/***
  *Title:""��Ŀ
  *Description:
  *		����:
  *Author:D
  *Data:2018.03.18
  *
  *
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager _instance;
    private void Awake()
    {
        _instance = this;
    }
    PlayerInfo info;
    public Text coinText;
    private void Start()
    {
        info = GetComponent<PlayerInfo>();
        coinText = GameObject.FindGameObjectWithTag("Coin").GetComponent<Text>();
        ShowCoinNum();
    }
    public void ShowCoinNum()
    {
        coinText.text = info.coin.ToString();
    }
   
}

