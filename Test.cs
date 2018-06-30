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

public class Test : MonoBehaviour
{
    public GameObject myGO;
    float timerr = 0;
    bool isShow = false;
	void Update () 
	{
        if (Input.GetKey(KeyCode.X))
        {
            myGO.SetActive(false);
            //StartCoroutine(Timer());
            isShow = true;
        }
        if (isShow)
        {
            timerr += Time.deltaTime;
            if (timerr >= 2 && timerr <= 3)
            {
                myGO.SetActive(true);
                isShow = false;
            }
        }
      
	}
    //IEnumerator Timer()
    //{
    //    yield return new WaitForSeconds(2f);
    //    myGO.SetActive(true);
    //}
}

