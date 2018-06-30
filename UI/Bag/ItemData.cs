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
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 鼠标拖拽物品,鼠标停留等,处理鼠标交互的一些内容
/// </summary>
public class ItemData : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    public Item item;
    public int slotIndex;
    public int amount = 1; //物品数量
    Inventory inv;
    //检测鼠标连续点击的
    UnityEvent doubleClick = new UnityEvent();
    private float interval = 0.5f;
    private float firstClick = 0;
    private float secondClick = 0;
    public void OnPointerDown(PointerEventData eventData)
    {
        secondClick = Time.realtimeSinceStartup;
        if (secondClick - firstClick < interval)
        {
            doubleClick.Invoke();
            //Debug.Log("连续点击");
            //如果当前点击处没有物品就返回不执行
            if (eventData.pointerCurrentRaycast.gameObject == null)
                return;
            if (EquipmentUI._instance.DressItem(this.gameObject.GetComponent<ItemData>().item.id))
            {
                ItemData dropedItem = eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemData>();//拽住的物品
                //将其对应的list表中的元素置为空
                inv.items[dropedItem.slotIndex] = new Item();
                Destroy(this.gameObject);//删除子物体
                //将其父节点中的sprite修改为默认的精灵
                Sprite sprite = new Sprite();
                sprite = Resources.Load("shortcutskill", sprite.GetType()) as Sprite;
                eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Image>().sprite = sprite;
                //穿上
                EquipmentUI._instance.DressNewItem(this.gameObject.GetComponent<ItemData>().item);
                EquipmentUI._instance. AddPlayerInfo();//更新角色属性
                Status._instance.UpDataShow();//更新界面显示
            }

        }
        else
        {
            //Debug.Log("没有连续点击");
            firstClick = secondClick;
        }
    }
    //监听鼠标移入事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        //当鼠标下面没有物品则影藏信息面板
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            InventoryDes._instance.gameObject.transform.localPosition = new Vector2(400, 400);
            return;
        }
        //当鼠标停在物品上时显示物品的信息面板
        InventoryDes._instance.ShowDes(this.gameObject.GetComponent<ItemData>().item.id);
    }
    //监听鼠标移出事件
    public void OnPointerExit(PointerEventData eventData)
    {
        //当鼠标离开物品上时隐藏(将其位置移开)物品的信息面板
        InventoryDes._instance.gameObject.transform.localPosition = new Vector2(400, 400);
    }
    //开始拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            transform.SetParent(transform.parent.parent.parent);
            transform.position = eventData.position;
            //关闭拽住的物品的blocksRaycasts可以检测到鼠标下面的东西
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
    //正在拽
    public void OnDrag(PointerEventData eventData)
    {
        //如果拽住的不是空,让他跟着鼠标的位置
        if (item != null)
        {
            transform.position = eventData.position;
        }
    }
    //松开
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(inv.slots[slotIndex].transform);
        transform.position = transform.parent.position;
        //拖拽结束后恢复它的blocksRaycasts
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    void Start () 
	{
        inv = GameObject.Find("GameSetting").GetComponent<Inventory>();
    }
}

