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

public class Status : MonoBehaviour
{
    private PlayerInfo playerInfo;
    public static Status _instance;
    private void Awake()
    {
        _instance = this;
    }

    public Text attacktext;
    public Button addAttackBtn;

    public Text deftext;
    public Button addDefBtn;

    public Text speedtext;
    public Button addSpeedBtn;

    public Text lastText;

    private void Start()
    {
        playerInfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
        UpDataShow();
        myObj.SetActive(false);
    }
    bool isShow = false;
    public GameObject myObj;
    public void TransformStatus()
    {
        if (isShow)
        {
            ShowInventory();
        }
        else
        {
            HideInventory();
        }
    }
    //显示界面
    void ShowInventory()
    {
        isShow = false;
        myObj.SetActive(true);
    }
    //隐藏界面
    void HideInventory()
    {
        isShow = true;
        myObj.SetActive(false);
    }
    //更新显示,根据playerInfo的属性去显示
    public void UpDataShow()
    {
        if (playerInfo.point_remain > 0)
        {
            addAttackBtn.gameObject.SetActive(true);
            addDefBtn.gameObject.SetActive(true);
            addSpeedBtn.gameObject.SetActive(true);
        }
        else
        {
            addAttackBtn.gameObject.SetActive(false);
            addDefBtn.gameObject.SetActive(false);
            addSpeedBtn.gameObject.SetActive(false);
        }
        //string addAttackText= playerInfo.attack_plus
      
        attacktext.text = "<color=#00FF00> " +playerInfo.attack + "+" +  "</color><color=#FFFF00>" + playerInfo.attack_plus + "</color>";
        deftext.text = "<color=#00FF00> " + playerInfo.def + "+" + "</color><color=#FFFF00>" + playerInfo.def_plus + "</color>";
        speedtext.text = "<color=#00FF00> " + playerInfo.speed + "+" + "</color><color=#FFFF00>" + playerInfo.speed_plus + "</color>";
        lastText.text = playerInfo.point_remain.ToString();
    }
    public void AddAttackPoint()
    {
        playerInfo.attack_plus++;
        playerInfo.attack++;
        playerInfo.point_remain--;
        UpDataShow();
    }
    public void AddDefPoint()
    {
        playerInfo.def_plus++;
        playerInfo.def++;
        playerInfo.point_remain--;
        UpDataShow();
    }
    public void AddSpeedPoint()
    {
        playerInfo.speed_plus++;
        playerInfo.speed++;
        playerInfo.point_remain--;
        UpDataShow();
    }
}

