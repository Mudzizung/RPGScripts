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

public class PlayerDir : MonoBehaviour
{
    public GameObject effectPhy;//特效
    bool isMove = false;//鼠标是否点击
    [SerializeField]
    public Vector3 targetPos;
  
	void Start () 
	{
        targetPos = transform.position;
    }

	void Update () 
	{
        //如果当前角色hp<=0;返回
        if (transform.GetComponent<PlayerInfo>().current_hp <= 0) return;
        //检测点击鼠标左键和是否点击在UI上面
        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            //检测鼠标点击位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //返回检测到的物体信息
            RaycastHit hitInfo;
            bool info =  Physics.Raycast(ray, out hitInfo);
            Vector3 newPos = new Vector3(hitInfo.point.x, hitInfo.point.y + 1f, hitInfo.point.z);
            if (info && hitInfo.collider.tag == Tags.ground)
            {
                isMove = true;
                //实例化点击效果
                ShowEffect(newPos);
                if (isMove)
                {
                    //得到要移动的目标位置
                    targetPos = hitInfo.point;

                    //角色朝向目标位置
                    targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
                    transform.LookAt(targetPos);

                }
            }
            
            isMove = false;
        }
       
	}
    void ShowEffect(Vector3 pos)
    {
        //实例化特效
        Instantiate(effectPhy, pos, Quaternion.identity);
    }
}

