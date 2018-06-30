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
using UnityEngine.SceneManagement;

public class PlayerCreat : MonoBehaviour
{
    public GameObject[] PlayerPre;//需要p实例化的角色
    private GameObject[] Player;//创建出来之后的游戏物体
    private int IndexChoose = 0;//当前选择的角色索引
    private int length;
    public InputField inputName;//获取输入的角色名
    string playerName;

    void Start () 
	{
        length = PlayerPre.Length;
        Player = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            Player[i] = Instantiate(PlayerPre[i], transform.position, transform.rotation);
        }
        UpDataPlayerShow();

    }
   

    //更新所有角色的显示
    void UpDataPlayerShow()
    {
        Player[IndexChoose].SetActive(true);
        for (int i = 0; i < length; i++)
        {
            if(i!=IndexChoose)
                Player[i].SetActive(false);
        }
    }
    //点击下一个角色按钮
    public void ChooseRightBtn()
    {
        IndexChoose++;
        if (IndexChoose >= 1)
            IndexChoose = 1;
        UpDataPlayerShow();
    }
    //点击上一个选择角色按钮
    public void ChooseLeftBtn()
    {
        IndexChoose--;
        if (IndexChoose <= 0)
            IndexChoose = 0;
        UpDataPlayerShow();
    }
    //点击进入游戏
    public void OnEnterGame()
    {
        playerName = inputName.GetComponentInChildren<Text>().text.ToString();
        
        //存储选择的角色
        PlayerPrefs.SetInt("PlayerIndex", IndexChoose);
        //存储角色的名字
        PlayerPrefs.SetString("PlayerName", playerName);
        //Debug.Log("Name:" +playerName);
        //加载下个场景
        SceneManager.LoadScene(2);
    }
}

