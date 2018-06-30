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

public class LoadGame : MonoBehaviour
{
    public GameObject majicPrefab;
    public GameObject swordPrefab;
    GameObject go = null;
    private void Awake()
    {
        //加载选择的角色
        int playerNumber= PlayerPrefs.GetInt("PlayerIndex");
        //加载角色的名字
        string playerName= PlayerPrefs.GetString("PlayerName");
        Debug.Log(playerNumber);
        if (playerNumber == 1)
        {
            go = Instantiate(majicPrefab);
        }
        else
        {
            go = Instantiate(swordPrefab);
        }
        go.SetActive(true);
        go.GetComponent<PlayerInfo>().playerName = playerName;
    }
}

