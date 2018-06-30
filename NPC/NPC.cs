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

public class NPC : MonoBehaviour
{
    public void OnMouseEnter()
    {
        CursorManager._instance.SetNPCTalk();
    }
    public void OnMouseExit()
    {
        CursorManager._instance.SetNormal();

    }
}

