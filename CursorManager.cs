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

public class CursorManager : MonoBehaviour
{
    //鼠标的各种样子
    public Texture2D cursor_normal;
    public Texture2D cursor_npc_talk;
    public Texture2D cursor_attack;
    public Texture2D cursor_lockTarget;
    public Texture2D cursor_pick;

    public static CursorManager _instance;
    private void Start()
    {
        _instance = this;
    }
    private Vector2 hotspot = Vector2.zero;
    /// <summary>
    /// 鼠标样式
    /// </summary>
    public void SetNormal()
    {
        Cursor.SetCursor(cursor_normal, hotspot, CursorMode.Auto);
    }
    public void SetNPCTalk()
    {
        Cursor.SetCursor(cursor_npc_talk, hotspot, CursorMode.Auto);
    }
    public void SetAttack()
    {
        Cursor.SetCursor(cursor_attack, hotspot, CursorMode.Auto);
    }
    public void SetLockTarget()
    {
        Cursor.SetCursor(cursor_pick, hotspot, CursorMode.Auto);
    }
}

