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


public class PlayerInfo : MonoBehaviour
{
    public PlayerType playerType;//角色类型
    public string playerName="Unity Chan";
    public int level = 1;//等级  exp=100+level*30

    public float exp = 0;//当前角色的经验

    public int hp=100;//血量max
    public int mp=100;//mp量max

    public int current_hp;//当前的血量
    public int current_mp;//

    public int coin = 800;//金币数

    public int attack = 10;//攻击力
    public int attack_plus = 0;//攻击力增加点数

    public int def = 10;//防御力
    public int def_plus = 0;//防御力增加点数

    public int speed = 5;//移动速度
    public int speed_plus = 0;//移动速度增加点数

    private void Awake()
    {
       // playerName = "Unity Chan";
        current_hp = hp;
        current_mp = mp;
    }
    public int point_remain=5;//剩余的技能点数

    private void Start()
    {
        //AddExp(100);
        AddCoin(200);
    }
    //增加金币
    public void AddCoin(int num)
    {
        coin += num;
        //CoinManager._instance.ShowCoinNum();
    }
    //减少金币
    public void SubCoin(int num)
    {
        coin -= num;
    }
    //获得经验的方法
    public void AddExp(int exp)
    {
        this.exp += exp;
        int total_exp = 100 + level * 30;
        //获得经验值足够升级了
        while(this.exp >= total_exp)
        {
            level++;//等级提升
            this.exp -= total_exp;//减少升级索要经验
            total_exp = 100 + level * 30;
        }
        EXPManager._instance.SetValue(this.exp/total_exp);
    }

    //获得治疗效果
    public void AddHPOrMP(int hp,int costMp)
    {
        int temp = (current_hp += hp);
        if (current_hp < temp)
        {
            current_hp += 1;
        }

        current_mp -= costMp;
    }

    //获得属性加成 攻击力,速度
    public void AddAttackOrmoveSpeed(int attack, int moveSpeed, int costMp)
    {
        this.attack += attack;
        this.speed += (moveSpeed / 20);
        current_mp -= costMp;
    }
    //
    public void ReduceAttackOrmoveSpeed(int attack, int moveSpeed)
    {
        this.attack -= attack;
        this.speed -= (moveSpeed / 20);
    }
    //使用单个技能对目标造成伤害
    public int TakeDamage(int damage)
    {
        return damage;
    }
}
public enum PlayerType  //角色类型
{
    Swordman,
    Magician,
}
