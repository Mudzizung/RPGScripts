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

public class DrugShopNPC : NPC
{
    public GameObject UI_DrugShop; //商店面板
    public AudioClip clip; //音效片段
    private AudioSource source;
    public static DrugShopNPC _instance; //写作单例方便菜单管理
    public InputField input; //数量输入框
    public GameObject ReNum; //确认购物面板
    private int buyID;  
    private PlayerInfo playerInfo;
    private Inventory inventory;
    private CoinManager coinManager;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        //获取到相应的引用信息
        source = GetComponent<AudioSource>();
        UI_DrugShop.gameObject.SetActive(false);
        ReNum.SetActive(false);
        playerInfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
        inventory = GameObject.Find("GameSetting").GetComponent<Inventory>();
        coinManager= GameObject.FindGameObjectWithTag(Tags.player).GetComponent<CoinManager>();
    }
    private void OnMouseDown()
    {
        //source.clip = clip;
        //source.Play();
        if (UI_DrugShop.activeInHierarchy == false)//判断当前物体是否为激活状态
            ShowUI(); //没有激活就显示
        else
            ExitUI();//激活了就影藏
    }
    //显示
    public void ShowUI() 
    {
        source.clip = clip;
        source.Play();//播放音乐
        UI_DrugShop.SetActive(true);
    }
    //影藏
    public void ExitUI()
    {
        UI_DrugShop.SetActive(false);
    }

    public void OnBuySmallHPBtn()
    {
        ShowNumber();
        BuyNum(0);
    }
    public void OnBuyBigHPBtn()
    {
        ShowNumber();
        BuyNum(1);
    }
    public void OnBuyMPBtn()
    {
        ShowNumber();
        BuyNum(2);
    }
    void BuyNum(int id)
    {
        buyID = id;
    }
    void ShowNumber()
    {
        ReNum.SetActive(true);
        input.text = "0";
    }
    public void BuyBtn()
    {
        //Debug.Log("输入:"+input.text.ToString());
        int count = int.Parse(input.text);//获取输入的购物数量
        Item item = ItemDataBase._instance.GetInfoById(buyID);//获取物品
        int total_price = count * item.sale_price;//计算总价
        if (total_price <= playerInfo.coin) //是否可以购买
        {
            for (int i = 0; i < count; i++) //count为购买数量,循环一次购买一个
            {
                inventory.AddItem(buyID);  //在物品栏中添加购买物品
            }
            playerInfo.SubCoin(total_price);//角色扣除相应金币
            coinManager.ShowCoinNum();//刷新金币菜单
            ReNum.SetActive(false);//隐藏确认购物列表
        }
        else
        {
           //处理购买失败相关内容
        }
    }
   
}

