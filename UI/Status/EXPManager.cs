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

public class EXPManager : MonoBehaviour
{
    public static EXPManager _instance;
    private Slider slider;
    private void Awake()
    {
        _instance = this;
        slider = GetComponent<Slider>();
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}

