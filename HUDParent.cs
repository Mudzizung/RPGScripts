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

public class HUDParent : MonoBehaviour
{
    public static HUDParent _instance;
    private void Awake()
    {
        _instance = this;
    }
}

