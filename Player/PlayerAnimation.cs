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

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private PlayerAttack playerAttack;
    Animation anima;
	void Start () 
	{
        move = GetComponent<PlayerMove>();
        anima = GetComponent<Animation>();
        playerAttack = GetComponent<PlayerAttack>();
    }

	void LateUpdate () 
	{
        if (playerAttack.attackState == PlayerAttackState.ControlWalk)
        {
            if (move.state == PlayerState.Moving)
            {
                PlayAnima("move_forward_fast");

            }
            else if (move.state == PlayerState.Idle)
            {
                PlayAnima("idle_combat");
            }
        }
        else if (playerAttack.attackState == PlayerAttackState.NormalAttack)
        {
            if (playerAttack.attack == AttackState.Moving)
            {
                PlayAnima("move_forward_fast");
            }
        }
       
	}
    void PlayAnima(string animaName)
    {
        anima.Play(animaName);
    }
}

