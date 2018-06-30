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
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseQuickBag : MonoBehaviour,IPointerEnterHandler
{
    PlayerInfo playerInfo;
    Slot slot;
    Inventory inv;

    void Start () 
	{
        //获取相关
        playerInfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
        slot = GetComponent<Slot>();
        inv = GameObject.Find("GameSetting").GetComponent<Inventory>();
    }

   
    //检测鼠标进入快捷栏
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.gameObject.GetComponentInChildren<ItemData>()==null)
            return;
        //Debug.Log("name=" + eventData.pointerCurrentRaycast.gameObject.name);
        //如果当前快捷栏中有物品,
        //Debug.Log("null=" + eventData.pointerCurrentRaycast.gameObject.transform.childCount.ToString());
        if (eventData.pointerCurrentRaycast.gameObject.transform.childCount == 1)
        {
            //Debug.Log("物品id=" + eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).GetComponent<ItemData>().item.id);
            Item currentItem = ItemDataBase._instance.GetInfoById(eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<ItemData>().item.id);
            //如果当前角色血量少于hp最大值
            if (playerInfo.current_hp < playerInfo.hp )
            {
                if (currentItem.id == 0) //使用小瓶血药
                {
                   
                    int count = int.Parse(eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInChildren<Text>().text);
                    //如果数量为零
                    if (count > 0)
                    {
                        count--;
                        playerInfo.current_hp += currentItem.hp_count;//当前角色的血量加上商品提供的血量
                        //更新数量显示
                        eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).GetComponentInChildren<Text>().text = count.ToString();
                        if (playerInfo.current_hp >= playerInfo.hp)
                        {
                            playerInfo.current_hp = playerInfo.hp;
                        }
                    }
                    else
                    {
                        ////请空格子
                        inv.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<ItemData>().slotIndex] = new Item();
                        Destroy(eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).gameObject);
                    }
                    //血量不得超过满血
                  
                }
                else if (currentItem.id == 1)//使用大瓶血药
                {
                    int count = int.Parse(eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInChildren<Text>().text);
                    if (count > 0)
                    {
                        count--;
                        playerInfo.current_hp += currentItem.hp_count;
                        eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInChildren<Text>().text = count.ToString();
                        if (playerInfo.current_hp >= playerInfo.hp)
                        {
                            playerInfo.current_hp = playerInfo.hp;
                        }
                    }
                    else
                    {
                        inv.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<ItemData>().slotIndex] = new Item();
                        Destroy(eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).gameObject);
                    }
                }
            }
            //如果当前角色魔法量少于最大值
            if (playerInfo.current_mp < playerInfo.mp)
            {
                if (currentItem.id == 2) //使用魔法药
                {
                    int count = int.Parse(eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInChildren<Text>().text);
                    if (count > 0)
                    {
                        count--;
                        playerInfo.current_mp += currentItem.mp_count;
                        eventData.pointerCurrentRaycast.gameObject.transform.GetComponentInChildren<Text>().text = count.ToString();
                        if (playerInfo.current_mp >= playerInfo.mp)
                        {
                            playerInfo.current_mp = playerInfo.mp;
                        }
                    }
                    else
                    {
                        inv.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<ItemData>().slotIndex] = new Item();
                        Destroy(eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).gameObject);
                    }
                }
            }
        }
        else
        {
            //不做处理
        }
    }

}

