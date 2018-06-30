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

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 offsetDis;//位置偏移
    private bool isRotate = false;
	void Start () 
	{
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        transform.LookAt(player);
        offsetDis = transform.position - player.position;
    }

	void Update () 
	{
        transform.position = offsetDis + player.position;
        //RotateView();

    }
    //旋转视角
    void RotateView() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotate = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotate = false;
        }

        if (isRotate)
        {
            transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X") * 5f);
            transform.position = offsetDis + player.position;
        }
       
    }
}

