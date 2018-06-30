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

public enum PlayerState
{
    Moving,
    Idle,
    Attacking,
}

public class PlayerMove : MonoBehaviour
{
  //  public float speed = 8f;
    PlayerInfo playerInfo;
    private PlayerDir dir;

    private PlayerAttack attack;
    private CharacterController controller;
    public PlayerState state = PlayerState.Idle;
    void Start () 
	{
        attack = GetComponent<PlayerAttack>();
        dir = GetComponent<PlayerDir>();
        controller = GetComponent<CharacterController>();
        playerInfo = GetComponent<PlayerInfo>();
    }

	void Update () 
	{
        if (attack.attackState != PlayerAttackState.ControlWalk)
            return;
        //目标位置与当前位置之间的差值
        float distance = Vector3.Distance(dir.targetPos, transform.position);
        //判断是否到达目标位置
        if (distance > 0.1f)
        {
            //更改player状态
            state = PlayerState.Moving;
            //移动
            controller.SimpleMove(transform.forward * playerInfo.speed);
            
        }
        else
        {
            state = PlayerState.Idle;
        }
	}

    public void SimpleMove(Vector3 target)
    {
        transform.LookAt(target);
        controller.SimpleMove(transform.forward * playerInfo.speed);
    }
}

