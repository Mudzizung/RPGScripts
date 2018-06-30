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

public class SkillInfo : MonoBehaviour
{
    public static SkillInfo _instance;
    public TextAsset skillsText;
    private void Awake()
    {
        _instance = this;
        InitSkillInfoDict();
        //Debug.Log("名字:"+GetSkillInfoByID(11).name);
    }
    //保存技能
    private Dictionary<int, SkillsInfo> dict = new Dictionary<int, SkillsInfo>();

    public SkillsInfo GetSkillInfoByID(int _id)
    {
        SkillsInfo skillsInfo = null;
        dict.TryGetValue(_id, out skillsInfo);
        return skillsInfo;
    }
    //初始化技能信息
    void InitSkillInfoDict()
    {
        string skillText= skillsText.text;
        string[] skillTextArray = skillText.Split('\n');
        foreach (var skillTextEveryRol in skillTextArray)
        {
            string[] skillEveryID = skillTextEveryRol.Split(',');
            SkillsInfo skillEveryInfo = new SkillsInfo();
            skillEveryInfo.id = int.Parse(skillEveryID[0]);
            skillEveryInfo.name = skillEveryID[1];

            skillEveryInfo.iconName= skillEveryID[2];
            skillEveryInfo.des = skillEveryID[3];
            string str_applytype = skillEveryID[4];
            switch (str_applytype)
            {
                case "SingleTarget":
                    skillEveryInfo.applyType = ApplyType.SingleTarget;
                    break;
                case "Buff":
                    skillEveryInfo.applyType = ApplyType.Buff;
                    break;
                case "MultiTarget":
                    skillEveryInfo.applyType = ApplyType.MultiTarget;
                    break;
                case "Passive":
                    skillEveryInfo.applyType = ApplyType.Passive;
                    break;
                default:
                    break;
            }
            string str_ApplyProprety = skillEveryID[5];
            switch (str_ApplyProprety)
            {
                case "Attack":
                    skillEveryInfo.applyProprety = ApplyProprety.Attack;
                    break;
                case "Def":
                    skillEveryInfo.applyProprety = ApplyProprety.Def;
                    break;
                case "Speed":
                    skillEveryInfo.applyProprety = ApplyProprety.Speed;
                    break;
                case "AttackSpeed":
                    skillEveryInfo.applyProprety = ApplyProprety.AttackSpeed;
                    break;
                case "HP":
                    skillEveryInfo.applyProprety = ApplyProprety.HP;
                    break;
                case "MP":
                    skillEveryInfo.applyProprety = ApplyProprety.MP;
                    break;
                default:
                    break;
            }

            skillEveryInfo.applyValue = int.Parse(skillEveryID[6]);
            skillEveryInfo.applyTime = int.Parse(skillEveryID[7]);
            skillEveryInfo.mp = int.Parse(skillEveryID[8]);
            skillEveryInfo.coldTime = int.Parse(skillEveryID[9]);

            string str_applicableRole = skillEveryID[10];
            switch (str_applicableRole)
            {
                case "Swordman":
                    skillEveryInfo.applicableRole = ApplicableRole.Swordman;
                    break;
                case "Majician":
                    skillEveryInfo.applicableRole = ApplicableRole.Majician;
                    break;
                default:
                    break;
            }
            skillEveryInfo.level = int.Parse(skillEveryID[11]);
            string str_releaseType = skillEveryID[12];
            switch (str_releaseType)
            {
                case "Self":
                    skillEveryInfo.releaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    skillEveryInfo.releaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    skillEveryInfo.releaseType = ReleaseType.Position;
                    break;
                default:
                    break;
            }
            skillEveryInfo.distance = float.Parse(skillEveryID[13]);
           // skillEveryInfo.effect_name = skillEveryID[14];
            dict.Add(skillEveryInfo.id, skillEveryInfo);
        }
    }
}
//使用角色类型
public enum ApplicableRole
{
    Swordman,
    Majician,
}
//技能类型
public enum ApplyType
{
    SingleTarget,
    Buff,
    MultiTarget,
    Passive,
}
//技能作用类型
public enum ApplyProprety
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP,
}
//释放类型
public enum ReleaseType
{
    Self,
    Enemy,
    Position,
}
public class SkillsInfo
{
    public int id;
    public string name;
    public string iconName;
    public string des;
    public ApplyType applyType;
    public ApplyProprety applyProprety;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicableRole applicableRole;
    public int level;
    public ReleaseType releaseType;
    public float distance;
    //public string effect_name;
}


