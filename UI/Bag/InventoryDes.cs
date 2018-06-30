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

public class InventoryDes : MonoBehaviour
{
    public static InventoryDes _instance;
    //显示属性的Text组件
    private Text desText;
    private void Awake()
    {
        _instance = this;
        desText = GetComponentInChildren<Text>();//获取
    }

    public void ShowDes(int _id)
    {
        //显示本地坐标的位置
        Vector2 localPoint = Input.mousePosition - new Vector3(Screen.width * 0.2f, Screen.height * 0.5f);
        this.transform.localPosition = localPoint;
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //获取字典当中当前key的item
        Item item = ItemDataBase._instance.GetInfoById(_id);
        string des = "";
        switch (item.type)
        {
            case ObjectType.Drug:
                des = GetDrugDes(item);
                break;
            case ObjectType.Equip:
                des = GetEquipmentDes(item);
                break;
            default:
                break;
        }
        //显示
          desText.text = des;
    }
    //获取到药品的显示信息
    string GetDrugDes(Item item)
    {
        string str = "";
        str += "名称:" + item.name + "\n";
        str += "+HP:" + item.hp_count + "\n";
        str += "+MP:" + item.mp_count + "\n";
        str += "出售价:" + item.sale_price + "\n";
        str += "购买价:" + item.buy_price;
        return str;
    }
    //获取到装备的显示信息
    string GetEquipmentDes(Item item)
    {
        string str = "";
        str += "名称:" + item.name + "\n";
        str += "物理/魔法攻击+:" + item.attack + "\n";
        str += "防御力+:" + item.def + "\n";
        str += "移动速度+:" + item.speed + "\n";
        str += "出售价:" + item.sale_price + "\n";
        str += "购买价:" + item.buy_price;
        
        return str;
    }
}

