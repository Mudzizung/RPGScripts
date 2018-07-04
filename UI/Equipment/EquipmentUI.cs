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

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI _instance;
    private void Awake()
    {
        _instance = this;
    }
    private bool isShow = false;
    //装备信息面板
    public GameObject equipmentUI; 

    Inventory inv;  
    //部位的集合
    public GameObject[] parts;
    //将添加的item信息列表保存
    public List<Item> items = new List<Item>();
    //玩家的信息
    private PlayerInfo playerInfo;
    //需要实例化的
    public GameObject equipmentItem;
    //装备带来的攻击,防御,速度加成
    private int attackEquipment;
    private int defEquipment;
    private int speedEquipment;

    //根据装备信息来增加角色属性
    public void AddPlayerInfo()
    {
        attackEquipment = 0;
        defEquipment = 0;
        speedEquipment = 0;
        //初始化玩家信息
        playerInfo.attack = 10;
        playerInfo.speed = 5;
        playerInfo.def = 10;
        //遍历部位
        foreach (var v in parts)
        {
            //如果当前位置穿戴了装备
            if (v.GetComponent<EquipmentItem>().equipmentItemIndex != -1)
            {
                 //给相应的属性赋值
                 attackEquipment += ItemDataBase._instance.GetInfoById(v.GetComponent<EquipmentItem>().equipmentItemIndex).attack;
                 defEquipment += ItemDataBase._instance.GetInfoById(v.GetComponent<EquipmentItem>().equipmentItemIndex).def;
                 speedEquipment += ItemDataBase._instance.GetInfoById(v.GetComponent<EquipmentItem>().equipmentItemIndex).speed;
            }
        }
        //将获取到的属性加到角色身上
        playerInfo.attack += attackEquipment;//攻击
        playerInfo.def += defEquipment;//防御
        playerInfo.speed += speedEquipment;//速度
    }

    private void Start()
    {
        inv = GameObject.Find("GameSetting").GetComponent<Inventory>();
        equipmentUI.SetActive(false); //初始时将装备面板隐藏
        playerInfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
        //遍历身上的部位,添加空的item信息;
        for (int i = 0; i < parts.Length; i++)
        {
            items.Add(new Item());
        }
    }
    //是否可以穿戴
    public bool DressItem(int id)
    {
        Item item = ItemDataBase._instance.GetInfoById(id);
        //如果当前物品不是装备则穿戴不成功
        if (item.type != ObjectType.Equip)
        {
            return false;
        }
        //如果当前角色为法师
        if (playerInfo.playerType == PlayerType.Magician)
        {
            //而点击装备为战士类型
            if (item.applicationType == ApplicationType.Swordman)
            {
                //穿戴不成功
                return false;
            }
        }
        //相反的情况
        else if (playerInfo.playerType == PlayerType.Swordman)
        {
            if (item.applicationType == ApplicationType.Magician)
            {
                return false;
            }
        }
        return true;
    }
    //冗余,是否重构?
    //穿戴新装备
    public void DressNewItem(Item newItem)
    {
       
        for (int i = 0; i < parts.Length; i++) //遍历身上的部位
        {
            if (newItem.dressType.ToString() == parts[i].tag) //找到要穿的装备的位置
            {
                if (parts[i].GetComponent<EquipmentItem>().equipmentItemIndex == -1)//如果该部位没有穿装备,添加新的装备
                {
                    items.Add(newItem);
                    GameObject itemObj = Instantiate(equipmentItem);
                    //设置父节点
                    itemObj.transform.SetParent(parts[i].transform);
                    itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
                    itemObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    //改名字
                    itemObj.name = newItem.name;
                    //替换image
                    Sprite sprite = new Sprite();
                    sprite = Resources.Load(newItem.icon_name, sprite.GetType()) as Sprite;
                    itemObj.GetComponent<Image>().sprite = sprite;
                    //将当前格子下面的items信息的id=-1改为添加的item的id
                    parts[i].GetComponent<EquipmentItem>().equipmentItemIndex = newItem.id;
                    Debug.Log("equipmentIndex=" + parts[i].GetComponent<EquipmentItem>().equipmentItemIndex);
                    items[i].id = newItem.id;
                    break;
                }
                else //如果穿了,跟换装备
                {
                    Transform item = parts[i].transform.GetChild(0);//需要跟换的部位
                    inv.AddItem(parts[i].transform.GetComponent<EquipmentItem>().equipmentItemIndex);//先在物品栏添加该物品
                    Destroy(item.gameObject);//删除该物品
                    items.Add(newItem);
                    GameObject itemObj = Instantiate(equipmentItem);
                    //设置父节点
                    itemObj.transform.SetParent(parts[i].transform);
                    itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
                    itemObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    //改名字
                    itemObj.name = newItem.name;
                    //替换image
                    Sprite sprite = new Sprite();
                    sprite = Resources.Load(newItem.icon_name, sprite.GetType()) as Sprite;
                    itemObj.GetComponent<Image>().sprite = sprite;
                    //将当前格子下面的items信息的id=-1改为添加的item的id
                    parts[i].GetComponent<EquipmentItem>().equipmentItemIndex = newItem.id;
                    items[i].id = newItem.id;
                    break;
                }
            }
        }
    }
   
  //面板的显示以及隐藏的方法
    public void TransShow()
    {
        if (isShow)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }
    void ShowUI()
    {
        equipmentUI.SetActive(true);
        isShow = true;
    }
    void HideUI()
    {
        equipmentUI.SetActive(false);
        isShow = false;
    }
   
}

