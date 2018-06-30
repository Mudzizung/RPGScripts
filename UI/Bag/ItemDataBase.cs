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

public class ItemDataBase : MonoBehaviour
{
    //单例方便外界调用
    public static ItemDataBase _instance;
    //需要读取的文本
    public TextAsset objectListInfo;
    private void Awake()
    {
        _instance = this;
        ReadInfo();
    }

    //创建字典保存文本信息
    public static Dictionary<int, Item> infoDir = new Dictionary<int, Item>();
    void ReadInfo()
    {
        string text = objectListInfo.text;
        //根据换行拆分
        string[] strInfo = text.Split('\n');
        //Debug.Log("cd=" + strInfo.Length);
        foreach (string str in strInfo)
        {
            
            Item info = new Item();
            //根据逗号拆分
            string[] proArray = str.Split(',');
            if (int.Parse(proArray[0]) == 23)
                return;
            //Debug.Log("id=" + proArray[0]);
            //给相应的属性赋值
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string icon_name = proArray[2];
            string str_type = proArray[3];
         
            //将赋值的信息交给Item类
            info.id = id;
            info.name = name;
            info.icon_name = icon_name;
            //对相应的type赋值
            ObjectType type = ObjectType.Drug;
            switch (str_type)
            {
                case "Drug":
                    type = ObjectType.Drug;
                    break;
                case "Equip":
                    type = ObjectType.Equip;
                    break;
                case "Material":
                    type = ObjectType.Material;
                    break;
                default:
                    break;
            }
            //给物品类型赋值
            info.type = type;
            //如果类型为药品,给其相应的属性赋值
            if (type == ObjectType.Drug)
            {
                int hpCount = int.Parse(proArray[4]);
                int mpCount = int.Parse(proArray[5]);
                int saleCount = int.Parse(proArray[6]);
                int buyCount = int.Parse(proArray[7]);
                //是否可以叠加
                bool _stackable = proArray[8] == "true" ? true : false; //返回物品是否可以叠加的信息
                info.hp_count = hpCount;
                info.mp_count = mpCount;
                info.sale_price = saleCount;
                info.buy_price = buyCount;
                info.stackable = _stackable;
            }
            else if (type == ObjectType.Equip)
            {
                //如果类型为装备,给其相应的属性赋值
                info.attack = int.Parse(proArray[4]);
                info.def = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);
                info.sale_price = int.Parse(proArray[10]);
                info.buy_price = int.Parse(proArray[9]);
                info.stackable= proArray[11] == "true" ? true : false;
                string str_dressType = proArray[7];
                switch (str_dressType)
                {
                    case "Headgear":
                        info.dressType = DressType.Head;
                        break;
                    case "Armor":
                        info.dressType = DressType.Armor;
                        break;
                    case "Accessory":
                        info.dressType = DressType.Accessory;
                        break;
                    case "Weapon":
                        info.dressType = DressType.Weapon;
                        break;
                    case "Shoe":
                        info.dressType = DressType.Shoes;
                        break;
                    default:
                        break;
                }
                string str_Application = proArray[8];
                switch (str_Application)
                {
                    case "Magician":
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case "Swordman":
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case "Common":
                        info.applicationType = ApplicationType.Common;
                        break;
                    default:
                        break;
                }
            }
            //按照对应的id将信息加入字典当中
            infoDir.Add(id, info);
        }
    }
    //给外界提供一个查找方法
    public  Item GetInfoById(int id)
    {
        Item info = null;
        //通过键返回需要的值
        infoDir.TryGetValue(id, out info);
        return info;
    }
}
//物品类型
public enum ObjectType
{
    Drug,
    Equip,
    Material,
}
//穿戴类型
public enum DressType
{
    Head,
    Armor,
    Accessory,
    Weapon,
    Shoes,
}
//装备职业类型
public enum ApplicationType
{
    Magician,
    Swordman,
    Common,
}
//物品属性
public class Item
{
    //id
    public int id;
    //名字
    public string name;
    //图片名字
    public string icon_name;
    public ObjectType type;
    public int hp_count;
    public int mp_count;
    public int sale_price;
    public int buy_price;
    public bool stackable;

    public int attack;
    public int def;
    public int speed;
    public DressType dressType;
    public ApplicationType applicationType;
    //初始化列表,表示当前位置没有添加item信息
    public Item()
    {
        this.id = -1;
    }
}



