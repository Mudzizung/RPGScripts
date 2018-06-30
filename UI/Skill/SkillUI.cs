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

public class SkillUI : MonoBehaviour
{
    public static SkillUI _instance;
    private void Awake()
    {
        _instance = this;
    }
    private bool isShow = false;
    public GameObject skillPanel;  //当前技能UI界面
    private PlayerInfo playerInfo; //获取当前角色类型(法师或者战士)
    public GameObject[] skillImagePanel;//技能图标数组
    //用于存储当前角色技能列表的技能相关信息,由于技能列表当中技能之间不可相互拖拽,列表索引对应技能图标数组的索引
    private List<SkillsInfo> currentPlayerSkillsInfo;
    public Text desText;//技能说明信息文本
   
    private void Start()
    {
        currentPlayerSkillsInfo = new List<SkillsInfo>();//将当前列表清空
        skillPanel.SetActive(false);
        playerInfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
        InitSkillPanel();
    }
    //根据当前角色初始化技能栏
    void InitSkillPanel()
    {
        //获取到当前角色类型,根据类型来添加技能栏
        PlayerType currentPlayerType = playerInfo.playerType;
        switch (currentPlayerType)
        {
            case PlayerType.Swordman:
                for (int i = 0; i < skillImagePanel.Length; i++)
                {
                    skillImagePanel[i].GetComponent<SkillItem>().SkillItemID = i;//编号
                    currentPlayerSkillsInfo.Add(SkillInfo._instance.GetSkillInfoByID(i));//添加相互对应的列表信息,很好查找,添加的
                    Sprite sprite = new Sprite();
                    //获取到对应的sprite,并赋值
                    sprite = Resources.Load(SkillInfo._instance.GetSkillInfoByID(i + 6).iconName, sprite.GetType()) as Sprite;
                    skillImagePanel[i].GetComponent<Image>().sprite = sprite;
                }
                break;
            case PlayerType.Magician:
                for (int i = 0; i < skillImagePanel.Length; i++)
                {
                    skillImagePanel[i].GetComponent<SkillItem>().SkillItemID = i;
                    currentPlayerSkillsInfo.Add(SkillInfo._instance.GetSkillInfoByID(i+6));//添加相互对应的列表信息,很好查找,添加的
                    Sprite sprite = new Sprite();
                    //获取到对应的sprite,并赋值
                    sprite = Resources.Load(SkillInfo._instance.GetSkillInfoByID(i).iconName, sprite.GetType()) as Sprite;
                    skillImagePanel[i].GetComponent<Image>().sprite = sprite;
                }
                break;
            default:
                break;
        }
    }
    public string GetDesByID(int _id)
    {
        PlayerType currentPlayerType = playerInfo.playerType;
        string str_des = "";
        switch (currentPlayerType)
        {
            case PlayerType.Swordman:
                str_des += "名字:" + currentPlayerSkillsInfo[_id].name + '\n';
                str_des += "技能说明:" + currentPlayerSkillsInfo[_id].des + '\n';
                str_des += "技能伤害:" + currentPlayerSkillsInfo[_id].applyValue.ToString() + '\n';
                str_des += "技能范围:" + currentPlayerSkillsInfo[_id].distance.ToString() + '\n';
                break;
            case PlayerType.Magician:
                str_des += "名字:" + currentPlayerSkillsInfo[_id].name + '\n';
                str_des += "技能说明:" + currentPlayerSkillsInfo[_id].des + '\n';
                str_des += "技能伤害:" + currentPlayerSkillsInfo[_id].applyValue.ToString() + '\n';
                str_des += "技能范围:" + currentPlayerSkillsInfo[_id].distance.ToString() + '\n';
                break;
            default:
                break;
        }
        return str_des;

    }
    //
    public void TransformStatus()
    {
        if (isShow==false)
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }
    }
    void ShowUI()
    {
        skillPanel.SetActive(true);
        isShow = true;
    }
    void HideUI()
    {
        skillPanel.SetActive(false);
        isShow = false;
    }
}

