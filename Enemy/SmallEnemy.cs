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

public enum EnemyState
{
    attack1,
    death,
    hit,
    walk,
    idle,
}
public class SmallEnemy : MonoBehaviour
{
    Animation anima;
    CharacterController cc;
   // public ParticleSystem particle;
    public AudioClip hit_click;
    public int hp = 100;
    public EnemyState state = EnemyState.idle;
    private float misValue = .2f;
    public string ani_death;
    public string ani_idle;
    public string ani_walk;
    public string ani_attack1;
    public string ani_hit;

    public float time = 1;
    public float timer = 0;
    public float speed = 6.0f;
    private string ani_now;


    public string anima_normal;
    public float normalAttack_time;

    public string anima_creazy;
    public float creazyAttack_time;
    public float creazyAttack_range;
    public string current_attack;

    public int attack_speed = 1;
    private float attack_timer;
    public int attackDamage = 10;
    public Transform attackTarget;
    public float minDietance = 8;
    public float maxDietance =20;


    public ParticleSystem particle;
    private PlayerInfo playerInfo;
    public int exp = 20;
    //掉落物品模型
    public GameObject anyPrefabs;
    private GameObject go;
    //public Text anyName;
    /*private GameObject HUDTextGo;//实例化出来的
      public GameObject HUDTextPrefab;//
      public GameObject HUDTextFolllow;//实例化出来在角色身上的
      private HUDText HUDText;
      private UIFollowTarget UIFollowTarget;*/
    private void Awake()
    {
        ani_now = ani_idle;
    }
    private void Start()
    {
        playerInfo= GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfo>();
        anima = GetComponent<Animation>();
        cc = GetComponent<CharacterController>();
        //particle.Play();
       /* HUDTextGo = Instantiate(HUDTextPrefab, Vector3.zero, Quaternion.identity);
        HUDTextGo.transform.parent = HUDParent._instance.gameObject.transform;
         HUDTextGo = NGUITools.AddChild(HUDParent._instance.gameObject, HUDTextPrefab);
         HUDTextGo.transform.localScale = new Vector3(.05f,.05f, .05f);
         HUDText = HUDTextGo.GetComponent<HUDText>();
         UIFollowTarget = HUDTextGo.GetComponent<UIFollowTarget>();
         UIFollowTarget.target = HUDTextFolllow.transform;
         UIFollowTarget.gameCamera = Camera.main;
         //UIFollowTarget.uiCamera = Camera.main.GetComponent<Camera>();*/

    }
    private void Update()
    {
        //死亡状态
        if (state == EnemyState.death)
        {
            anima.CrossFade(ani_death);
        }
        //攻击01状态
        else if (state == EnemyState.attack1)
        {
            AutoAttack();
        }
        //被打
        else if(state==EnemyState.hit)
        {
            //particle.Play();
            anima.CrossFade(ani_hit);

        }

        else
        {
            anima.CrossFade(ani_now);
            if (ani_now == ani_walk)
            {
                cc.SimpleMove(transform.forward * speed);
            }
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                RandomState();
            }
        }
       
    }

    void AutoAttack()
    {
        if (attackTarget != null)
        {
          
            float distance = Vector3.Distance(transform.position, attackTarget.position);
           // Debug.Log("dis=" + distance);
            //停止攻击
            if (distance > maxDietance)
            {
                attackTarget = null;
                state = EnemyState.idle;
                //RandomState();
            }
            else if (distance <= minDietance)
            {
                
                attack_timer += Time.deltaTime;
                anima.CrossFade(current_attack);
                if (current_attack == anima_normal)
                {
                    if (attack_timer > normalAttack_time)
                    {
                        //产生伤害
                        attackTarget.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
                        current_attack = ani_idle;
                    }
                }
                else if (current_attack == anima_creazy)
                {
                    if (attack_timer > creazyAttack_time)
                    {
                        ////产生伤害
                        attackTarget.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
                        current_attack = ani_idle;
                    }

                }
                if (attack_timer >= (1f / attack_speed))
                {
                    RandomAttack();
                    attack_timer = 0;
                }
            }
            else
            {
                transform.LookAt(attackTarget);
                cc.SimpleMove(transform.forward * 2);
                anima.CrossFade(ani_walk);
            }
        }
        else
        {
            state = EnemyState.idle;
        }
    }
    void RandomAttack()
    {
        float r = Random.Range(0, 1f);
        if (r <= creazyAttack_range)
        {
            current_attack = anima_creazy;
        }
        else
        {
            current_attack = anima_normal;
        }
    }
    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        attackTarget = GameObject.FindGameObjectWithTag(Tags.player).transform;
        //如果角色血没了,目标设为空;
        if (attackTarget.GetComponent<PlayerInfo>().current_hp <= 0)
            attackTarget = null;

        // Debug.Log("tar=" + attackTarget.name);
        state = EnemyState.attack1;
        float v = Random.Range(0, 1.0f);
        if (v < misValue)
        {

           // HUDText.Add("Miss", Color.red, 1);
        }
        else
        {
            //state = EnemyState.hit;
            this.hp -= damage;
            AudioSource.PlayClipAtPoint(hit_click, transform.position);
            // particle.Play();
            if (hp <= 0)
            {
                //掉落装备
                //Random.RandomRange(0, 20);
                int id=Random.Range(0, 50);
                //如果随机的数字可以获取到装备
                if (ItemDataBase._instance.GetInfoById(id) != null)
                {
                    go = Instantiate(anyPrefabs, new Vector3(transform.position.x, transform.position.y + 2,transform.position.z), Quaternion.identity);
                    go.GetComponent<AnyIndex>().id = ItemDataBase._instance.GetInfoById(id).id;
                    go.GetComponentInChildren<Text>() .text = ItemDataBase._instance.GetInfoById(id).name;
                }
                playerInfo.AddExp(exp);
                state = EnemyState.death;
                GameObject.Find("EnemyBirthPlace").GetComponent<SmallEnemyBirth>().currentNum--;
                BarNPC._instance.KillMonster();
                BarNPC._instance.ShowTasking();
                StartCoroutine(Timer());
            }
        }
    }
   
    void RandomState()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            ani_now = ani_idle;
        }
        else {
            ani_now = ani_walk;
            transform.Rotate(transform.up * Random.Range(-90, 90));
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2.75f);
        Destroy(this.gameObject);
    }
    private void OnMouseEnter()
    {
        CursorManager._instance.SetAttack();
    }
    private void OnMouseExit()
    {
        CursorManager._instance.SetNormal();
    }
}

