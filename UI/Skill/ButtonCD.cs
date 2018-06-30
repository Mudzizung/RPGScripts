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

public class ButtonCD : MonoBehaviour
{
    public Image CDImage;//冷却时间的图片
    Button button;  //
    SkillSlot skillSlot;
    PlayerInfo playerInfo;
    PlayerAttack playerAttack;
    public GameObject effect_AddHP;
    public GameObject effect_Buff;
    //public GameObject effect_Single;
    private float cd = 5;
    float tempTime;

    bool startCd = false;
    private GameObject goEffect_AddHP;
    private GameObject goEffect_Buff;

    void Start () 
	{
        button = GetComponent<Button>();
        skillSlot = transform.GetComponentInParent<SkillSlot>();
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        playerAttack= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    void Update () 
	{
        if (startCd)
        {
            float amount = 1 - (Time.time - tempTime) / cd;
            if (amount >= 0)
            {
                CDImage.fillAmount = amount;
            }
        }
	}
    //鼠标点击技能事件
    public void SetCd()
    {
        //如果技能栏中没有技能 返回
        if (GetSkillInfo() == null) return;
        //在冷却时间中,就无法点击到,解除交互
        button.interactable = false;
        tempTime = Time.time;
        startCd = true;
        //获取到点击的技能属性
        SkillsInfo skillsInfo = GetSkillInfo();
        //判断点击到的技能
        switch (skillsInfo.applyType.ToString())
        {
            //恢复技能
            case "Passive":
                //effect_AddHP.SetActive(true);
                goEffect_AddHP = Instantiate(effect_AddHP, playerInfo.gameObject.transform.position, Quaternion.identity);
                goEffect_AddHP.transform.SetParent(playerInfo.gameObject.transform);
                goEffect_AddHP.SetActive(true);
                playerInfo.AddHPOrMP(skillsInfo.applyValue, skillsInfo.mp);
                break;
             //增益技能
            case "Buff":
                // effect_Buff.SetActive(true);
                goEffect_Buff = Instantiate(effect_Buff,new Vector3(playerInfo.transform.position.x, playerInfo.gameObject.transform.position.y+5, playerInfo.gameObject.transform.position.z), Quaternion.identity);
                //go.transform.SetParent(playerInfo.gameObject.transform);
                goEffect_Buff.SetActive(true);
                playerInfo.AddAttackOrmoveSpeed(skillsInfo.applyValue, skillsInfo.applyValue, skillsInfo.mp);
                break;
            //单体攻击技能
            case "SingleTarget":
                bool isSkillAttack=playerAttack.SkillAttack();
                if (isSkillAttack)
                {
                    //产生伤害
                    playerAttack.normal_target.GetComponent<SmallEnemy>().TakeDamage(skillsInfo.applyValue);
                }
                break;
            //大范围技能
            case "MultiTarget":
                playerAttack.BigSkillAttack();
                break;
        }
        // Debug.Log(skillsInfo.coldTime);
        StartCoroutine(StartCD(1.5f, skillsInfo.applyValue, skillsInfo.applyValue));
       
    }
    //......
    IEnumerator StartCD(float _cd,int value00,int value01)
    {
        yield return new WaitForSeconds(3.5f);
        //effect_AddHP.SetActive(false);
        if (goEffect_Buff!=null)
        {
            playerInfo.ReduceAttackOrmoveSpeed(value00, value01);
        }
        //effect_Buff.SetActive(false);

        yield return new WaitForSeconds(_cd);
        //不
        CDImage.fillAmount = 0;
        startCd = false;
        //冷却时间结束,现在可以点击到了button,恢复交互
        button.interactable = true;
    }
    
    
    //获取到当前点击到的技能信息
    public SkillsInfo GetSkillInfo()
    {
        return SkillInfo._instance.GetSkillInfoByID(skillSlot.skillSlotID);
    }

}

