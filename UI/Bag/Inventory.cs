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

public class Inventory : MonoBehaviour //大篮子
{
    //背包界面
    public GameObject myObj;
    //实际的所有的小格子,拖动附加
    public List<GameObject> slots = new List<GameObject>();
    //item的数据库
    ItemDataBase ItemDataBase;
    //保存添加的item信息在列表当中
    public List<Item> items = new List<Item>();
    private int coinCount = 500;
    public Text coinText;
    //需要实例化的item对象
    public GameObject inventoryItem;
    private void Start()
    {
        ItemDataBase = GetComponent<ItemDataBase>();
        for (int i = 0; i < slots.Count; i++)
        {
            //遍历每个小格子
            items.Add(new Item());//添加一Item的空信息,id=-1;
            //给每个小格子添加一个id信息,同时给它赋值
            slots[i].GetComponent<Slot>().slotID = i;
            //Debug.Log(items[i].id);
        }
        AddItem(0);
        AddItem(2);
        AddItem(1);
        AddItem(22);
        AddItem(18);
        AddItem(11);
        AddItem(14);
        AddItem(7);
        AddItem(15);
        AddItem(5);
        myObj.SetActive(false);
    }
   //添加
    public void AddItem(int _id)
    {
        Item itemToAdd = ItemDataBase.GetInfoById(_id);//需要添加的item
        //检查是否可叠加,背包中是否存在该物体
        if (itemToAdd.stackable == true && CheckItenExist(_id))
        {
            //遍历每个格子当中的Item的空信息
            for (int i = 0; i < items.Count; i++)
            {
                //如果存在
                if (items[i].id == _id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    //给其数量加一
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        //如果不存在
        else
        {
            CreatNewItem(itemToAdd);//创建一个新的
        }
    }
    //检擦是否存在该item
    bool CheckItenExist(int _id)
    {
        // //遍历每个格子当中的Item的空信息,空的item.id=-1;
        for (int i = 0; i <items.Count; i++)
        {
            if (items[i].id == _id)
            {
                return true;
            }
        }
        return false;
    }
    //创建新的item
    void CreatNewItem(Item itemToAdd)
    {
        //遍历格子
        for (int i = 0; i < slots.Count; i++)
        {
            //如果当前格子下面的items信息的id=-1;则表示没有物品在哪里
            if (items[i].id == -1)
            {
                items.Add(itemToAdd);
                GameObject itemObj = Instantiate(inventoryItem);
                //设置父节点
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
                itemObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                //改名字
                itemObj.name = itemToAdd.name;
                //替换image
                Sprite sprite = new Sprite();
                sprite = Resources.Load(itemToAdd.icon_name, sprite.GetType()) as Sprite;
                itemObj.GetComponent<Image>().sprite = sprite;
                //将当前格子下面的items信息的id=-1改为添加的item的id
                items[i].id = itemToAdd.id;
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slotIndex = i;
                break;
            }
        }
    }
    public bool isShow = false;
    //显示界面
    void ShowInventory()
    {
        isShow = true;
        myObj.SetActive(true);
    }
    //隐藏界面
    void HideInventory()
    {
        isShow = false;
        myObj.SetActive(false);
    }
    //设置显示或者隐藏
    public void TransformState()
    {
        if (isShow)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }
}

