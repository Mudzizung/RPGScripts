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

public class AnyIndex : MonoBehaviour
{
    [HideInInspector]
    public int id = -1;
    private Inventory inventory;
    private void Start()
    {
        inventory = GameObject.Find("GameSetting").GetComponent<Inventory>();
    }
    private void OnMouseEnter()
    {
        //Debug.Log("Over Me ?");
        //改变鼠标样式
        CursorManager._instance.SetLockTarget();
    }

    private void OnMouseDown()
    {
        //Debug.Log("Hit Me ?");
        inventory.AddItem(id);
        Destroy(this.gameObject,.5f);
    }
    private void OnMouseExit()
    {
        CursorManager._instance.SetNormal();
    }
}

