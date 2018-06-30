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

public class SmallEnemyBirth : MonoBehaviour
{
    public int maxNum = 5;
    public int currentNum = 0;
    private float timer = 0;
    private float time = 3;
    public GameObject enemyPre;
    private void Update()
    {
        if (currentNum < maxNum)
        {
            timer += Time.deltaTime;
            Vector3 pos = transform.position;
            pos.x += Random.Range(-15, 15);
            pos.z+= Random.Range(-15, 15);
            if (timer >= time)
            {
                Instantiate(enemyPre, pos, Quaternion.identity);
                timer = 0;
                currentNum++;
            }
        }
    }
}

