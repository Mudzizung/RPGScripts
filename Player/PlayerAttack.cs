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
using UnityEngine.EventSystems;
/// <summary>
/// 角色状态
/// </summary>
public enum PlayerAttackState
{
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Deadth,
}

public enum AttackState
{
    Moving,
    Idle,
    Attack,
}
public class PlayerAttack : MonoBehaviour
{
    public PlayerAttackState attackState = PlayerAttackState.ControlWalk;
    public AttackState attack = AttackState.Idle;

    public string anima_idle;
    private string current_anima;
    public string anima_Death;
    public string anima_normalAttack;
    public float time_normalAttack;
    public float normalAttackSpeed = 2;
    private float timer = 0;
    public float min_attackDistance=20;
    [HideInInspector]
    public Animation anima;
    public Transform normal_target;
    //public float normalAttackNum = 20;
    private PlayerMove playerMove;
    private PlayerInfo playerInfo;

    public GameObject effect_SingleSkill;
    public GameObject effect_BigSkill;

    //.......
    //public string anima_Idle;
    private void Awake()
    {
        anima = GetComponent<Animation>();
        current_anima = anima_idle;
        playerMove = GetComponent<PlayerMove>();
        playerInfo = GetComponent<PlayerInfo>();
    }
    private void Update()
    {
        //角色没死
        if (Input.GetMouseButton(0)&& attackState != PlayerAttackState.Deadth&&!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isClick = Physics.Raycast(ray, out hit);
            //如果检测到点击目标为怪物
            if (isClick && hit.collider.tag == Tags.enemy)
            {
                //转向目标
                transform.LookAt(hit.collider.transform.position);
                normal_target = hit.collider.transform;
                attackState = PlayerAttackState.NormalAttack;
            }
            else
            {
                attackState = PlayerAttackState.ControlWalk;
                normal_target = null;
            }
            
        }
        if (attackState == PlayerAttackState.NormalAttack)
        {
            if (normal_target == null)
            {
                attackState = PlayerAttackState.ControlWalk;
                return;
            }
            float distance = Vector3.Distance(transform.position, normal_target.position);
            if (distance <= min_attackDistance)
            {
                attack = AttackState.Attack;
                transform.LookAt(normal_target.position);
                timer += Time.deltaTime;
                anima.Play(current_anima);
                if (timer >= time_normalAttack)
                {
                    current_anima = anima_idle;
                    //当前攻击目标收到伤害
                }
                if (timer >= (1 / normalAttackSpeed))
                {
                    current_anima = anima_normalAttack;
                    normal_target.GetComponent<SmallEnemy>().TakeDamage(playerInfo.attack);
                    //怪物死亡目标消失
                    if (normal_target.GetComponent<SmallEnemy>().hp <= 0)
                    {
                        normal_target = null;
                    }
                    timer = 0;
                }
                //Debug.Log ("Damage="+GetNormalAttackDamage());
            }
            //
            else
            {
                //向目标移动
                attack = AttackState.Moving;
                playerMove.SimpleMove(normal_target.position);
            }
        }
        else if(attackState==PlayerAttackState.Deadth)
        {
            //anima.CrossFade(anima_Death);
            anima.Play(current_anima);
        }
    }

    public int GetNormalAttackDamage()
    {
        return playerInfo.attack;
    }
   
    //受到伤害
    public void TakeDamage(int damage)
    {
        //获取到角色身上的防御总量
        float sumDef = playerInfo.def;
        //角色受到的真实伤害
        float realDamage = damage * ((200 - sumDef) / 200);
        //保护真实伤害大于零
        if (realDamage <= 0) realDamage = 5;
        //角色没死受到伤害
        if (playerInfo.current_hp > 0)
        {
            playerInfo.current_hp -= (int)realDamage;
        }
        else
        {
            //角色状态为死亡
            attackState = PlayerAttackState.Deadth;
            current_anima = anima_Death;
        }
        //声音播放?
    }


    //技能攻击
    public bool SkillAttack()
    {
        //实例化攻击特效,产生伤害等.......
        if (normal_target != null)
        {
            Instantiate(effect_SingleSkill, new Vector3(normal_target.transform.position.x, normal_target.transform.position.y + 1, normal_target.transform.position.z), Quaternion.identity);
            return true;
        }
        return false;
    }
    //大范围攻击
    public bool BigSkillAttack()
    {
        //实例化攻击特效,产生伤害等.......
        Instantiate(effect_BigSkill, new Vector3(transform.position.x,transform.position.y + 5, transform.position.z), Quaternion.identity);
        return true;
    }

}

