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
/// <summary>
/// 角色头像框相关,根据角色当前状态(血量,等级,魔法值,名字)更新UI界面显示
/// </summary>
public class HeadPicManager : MonoBehaviour
{
    
    private PlayerInfo playerInfo;
    public Text palyerName;
    public Text playerLevel;

    public GameObject hp_control;
    public GameObject mp_control;

    void Start () 
	{
        playerInfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
    }

	void Update () 
	{
        ShowHead();
    }
    //ui血量,魔法值更新
    void ShowHead()
    {
        palyerName.text = playerInfo.playerName;
        playerLevel.text = playerInfo.level.ToString();
        //Debug.Log("count=" + (1 - (playerInfo.hp - playerInfo.current_hp) / 100));
        hp_control.GetComponent<Image>().fillAmount = (1 - (playerInfo.hp - playerInfo.current_hp * 1.0f) / playerInfo.hp);
        mp_control.GetComponent<Image>().fillAmount = (1 - (playerInfo.mp - playerInfo.current_mp * 1.0f) / playerInfo.mp);
    }
}

