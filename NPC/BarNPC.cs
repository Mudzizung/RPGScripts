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

public class BarNPC : NPC
{
    public static BarNPC _instance;
    private void Awake()
    {
        _instance = this;
    }
    public GameObject UI_Question; //任务UI界面
    public bool isTasking = false; //标识 是否完成任务
    public int killCount = 0;   //击杀数目
    public Text taskText;   //任务信息
    public GameObject acceptBtn;//接受任务按钮
    public GameObject okBtn;//完成任务按钮
    PlayerInfo info;    //角色信息
    public AudioClip clip;  //按钮点击声音
    public AudioClip clip1;  //点击NPC发出的声音
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        info =GameObject.FindGameObjectWithTag(Tags.player). GetComponent<PlayerInfo>();
       
    }
    private void OnMouseDown()//该方法会检测鼠标点击该物体的事件,会在每帧执行
    {
      
        if (UI_Question.activeInHierarchy == false)//判断当前物体是否为激活状态
            ShowUI(); //如果没有显示则显示界面
        else
            ExitUI();
       
    }
    //显示
    public void ShowUI()
    {
        source.clip = clip1;  //获取声音clicp
        source.Play(); //播放
        UI_Question.SetActive(true);//显示任务界面
    }
    //影藏
    public void ExitUI()
    {
        UI_Question.SetActive(false);
    }
    //显示任务描述
    void ShowQuestion()
    {
        taskText.text = "任务:杀死10只怪物...\n\n报酬: 500g";  
        acceptBtn.SetActive(true);
        okBtn.SetActive(false);
    }
    //显示任务进度
    public  void ShowTasking()
    {
        taskText.text = "任务:\n当前任务进度" + killCount + "/10\n\n报酬:\n500g";
        acceptBtn.SetActive(false);
        okBtn.SetActive(true);
    }
    //接受任务,显示任务进度
    public void OnAcceptBtn()
    {
        source.clip = clip;
        source.Play();
        ShowTasking();
        isTasking = true;
    }
    //放弃任务,显示任务描述
    public void OnExitBtn()
    {
        isTasking = false;
        ShowQuestion();
    }
    //完成任务
    public void OnOkBtn()
    {
        source.clip = clip;
        source.Play();
        if (killCount >= 2) //达到任务要求
        {
            info.AddCoin(500); //角色获得任务奖励
            killCount = 0;
            Debug.Log("OK"+info.coin);
            CoinManager._instance.ShowCoinNum();
            ExitUI();
        }
        else {
            ExitUI();
        }
    }

    public void KillMonster()
    {
        if (isTasking)
        {
            killCount++;
        }
    }
}

