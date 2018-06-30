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

public class SkillSlot : MonoBehaviour,IDropHandler
{
    public int skillSlotID = -1;//表示当前快捷栏没有放入技能
    public GameObject[] skillItemPanel;
    public void OnDrop(PointerEventData eventData)
    {
        SkillItem skill= eventData.pointerDrag.GetComponent<SkillItem>();
        //Debug.Log("name:" +eventData.pointerCurrentRaycast.gameObject.name+":"+this.skillSlotID);
        for (int i = 0; i < skillItemPanel.Length; i++)
        {
            //如果当前快捷栏有这个技能了,那就将原来的设置为空
            if (skillItemPanel[i].gameObject.GetComponent<SkillSlot>().skillSlotID == skill.SkillItemID)
            {
                Sprite sprite = new Sprite();
                sprite = Resources.Load("LabelTag", sprite.GetType()) as Sprite;
                //将原来的设置为默认的,并且将他的skillSlotID=-1,为放入技能的状态
                skillItemPanel[i].transform.GetChild(0). gameObject.GetComponent<Image>().sprite = sprite;
                skillItemPanel[i].gameObject.GetComponent<SkillSlot>().skillSlotID = -1;
            }
            if (this.skillSlotID == -1)
            {
                //将其图案设置为拖拽的技能的图标
                Sprite sprite = new Sprite();
                sprite = Resources.Load(SkillInfo._instance.GetSkillInfoByID(skill.SkillItemID).iconName, sprite.GetType()) as Sprite;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Image>().sprite = sprite;
                //将其id设置为技能id,使用时查找
                this.skillSlotID = skill.SkillItemID;
            }
            else//交换
            {

            }
        }
       
    }
}

