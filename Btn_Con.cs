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
using UnityEngine.SceneManagement;

public class Btn_Con : MonoBehaviour
{
    /// <summary>
    /// 游戏数据的保存和场景之间游戏数据的传递使用playerPrefs
    /// </summary>
    /// 场景: 开始场景..角色选择场景 ..play场景
    //开始新游戏
    public void OnNewGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 0);
        PlayerPrefs.DeleteAll();
        //加载选择角色场景
        SceneManager.LoadScene(1);
    }
    //加载游戏
    public void OnLoadGame()
    {
        PlayerPrefs.SetInt("DataFromSave",1);//DataFromSave表示数据来自保存
        //加载play场景
    }
}

