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

public class FunctionBar : MonoBehaviour
{
    Inventory inv;
    private void Start()
    {
        inv = GameObject.Find("GameSetting").GetComponent<Inventory>();
    }
    //背包
    public void OnBagBtn()
    {
        inv.TransformState();
    }
    //设置
    public void OnSettingBtn()
    {

    }
    //装备
    public void OnEquipBtn()
    {
        EquipmentUI._instance.TransShow();
    }
    //技能
    public void OnSkillBtn()
    {
        SkillUI._instance.TransformStatus();
    }
    //状态
    public void OnStatusBtn()
    {
        Status._instance.TransformStatus();
    }
}

