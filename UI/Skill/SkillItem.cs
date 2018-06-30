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


public class SkillItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public int SkillItemID = -1;
    public GameObject ownGo;
    private GameObject go;
    public Image coverImage;
    public Text level;
    PlayerInfo playerInfo;

    void Start()
    {
        
        coverImage.gameObject.SetActive(true);//表示该技能无法使用
        playerInfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
    }
    void Update()
    {
        
        //如果当前等级大于技能等级,红色图案消失
        if (playerInfo.level >= int.Parse(level.text.ToString()))
        {
            coverImage.gameObject.SetActive(false);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //如果当前等级小于技能等级,则无法拖动该技能
        if (playerInfo.level < int.Parse(level.text.ToString()))
            return;
        //Debug.Log("id=" + this.SkillItemID);
        if (this.gameObject != null)
        {
            go = Instantiate(ownGo, transform.position, Quaternion.identity);
            go.transform.SetParent(transform.parent.parent.parent);
            //transform.SetParent(transform.parent.parent.parent.parent);
            go.transform.position = eventData.position;
            //关闭拽住的物品的blocksRaycasts可以检测到鼠标下面的东西
            go.transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (playerInfo.level < int.Parse(level.text.ToString()))
            return;
        if (eventData != null)
        {
            go.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (playerInfo.level < int.Parse(level.text.ToString()))
            return;
        go.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(go);
    }
}

