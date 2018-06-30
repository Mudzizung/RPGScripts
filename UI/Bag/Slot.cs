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

public class Slot : MonoBehaviour,IDropHandler
{
    public int slotID;//格子的编号,就是拖进去的格子顺序
    Inventory inv;
    //放下--松开鼠标时
    public void OnDrop(PointerEventData eventData)
    {
        ItemData dropedItem = eventData.pointerDrag.GetComponent<ItemData>();//拽住的物品
        if (inv.items[slotID].id == -1)//如果放下的位置没有物品
        {
            inv.items[dropedItem.slotIndex] = new Item();//将开始拽的位置设置为空
            dropedItem.slotIndex = slotID;//把拽住的物品的index改成放下的个子的编号
            inv.items[slotID] = dropedItem.item;//将item列表当中放下位子id的值赋值为拽住的物体
        }
        else if(dropedItem.slotIndex != slotID)//如果放下的位置不是原来的位置
        {
            Transform item = transform.GetChild(0);
            item.GetComponent<ItemData>().slotIndex = dropedItem.slotIndex;
            item.transform.SetParent(inv.slots[dropedItem.slotIndex].transform);
            item.transform.position = item.parent.position;

            inv.items[dropedItem.slotIndex] = item.GetComponent<ItemData>().item;
            dropedItem.slotIndex = slotID;
            inv.items[slotID] = dropedItem.item;
        }
    }
   
    void Start () 
	{
        inv = GameObject.Find("GameSetting").GetComponent<Inventory>();
    }
}

