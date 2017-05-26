using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAttack : MonoBehaviour {

    private Dictionary<string, PlayEffect> effectDict = new Dictionary<string, PlayEffect>();
    public float forwordAttackDistance = 50f;
    public float aroundAttackDistance = 50f;
    public int[] damage = new int[]{ 20, 30, 40, 50};
    public PlayEffect[] effectArray;
    public float hp = 300;

    private Animator animtor;
    private GameObject damagePrefab;
    private HUDText damageText;

    public enum AttackRange
    {
        Forword,
        Around
    }

    // Update is called once per frame
    void Start()
    {
        PlayEffect[] array = GetComponentsInChildren<PlayEffect>();
        foreach (PlayEffect effect in array)
        {
            effectDict.Add(effect.gameObject.name, effect);
        }
        foreach (PlayEffect effect in effectArray)
        {
            effectDict.Add(effect.gameObject.name, effect);
        }
        animtor = transform.GetComponent<Animator>();
        animtor = transform.GetComponent<Animator>();

        Transform hpBarPoint = transform.Find("hp_bar_point");
        damagePrefab = HpBarManage._instance.CreateDamageText(hpBarPoint.gameObject);
        damageText = damagePrefab.transform.GetComponent<HUDText>();
    }

    // 1. normal, skill1, skill2, skil3
    // 2. effect name
    // 3. sound name
    // 3. move forword
    // 3. jump hight
    public void Attack (string args)
    {
        string[] proArray = args.Split(',');
        string skillType = proArray[0];
        string effectName = proArray[1];
        string soundName = proArray[2];
        float moveForword = float.Parse(proArray[3]);
        float jumpHight = float.Parse(proArray[4]);

        if(skillType == "normal")
        {
            ArrayList array = GetCanAttackEnemy(AttackRange.Forword);
            foreach(GameObject go in array)
            {
                go.SendMessage("TakeDamage", damage[0]+","+moveForword+","+jumpHight);
            }
        }

        if (effectName != null)
        {
            ShowEffect(effectName);
        }
        if (soundName != "")
        {
            SoundManage._instance.PlaySound(soundName);
        }


        if (moveForword != null)
        {
            iTween.MoveBy(gameObject, -Vector3.forward * moveForword, 0.3f);
        }
    }


    public void SoundPlay(string soundName)
    {
        if (soundName != null)
        {
            SoundManage._instance.PlaySound(soundName);
        }
    }

    // 0. normal, skill1, skill2, skil3
    // 1. move forword
    // 2. jump hight
    public void SkillAttack(string args)
    {
        string[] proArray = args.Split(',');
        string skillType = proArray[0];
        float moveForword = float.Parse(proArray[1]);
        float jumpHight = float.Parse(proArray[2]);

        if (skillType == "skill1")
        {
            ArrayList array = GetCanAttackEnemy(AttackRange.Forword);
            foreach (GameObject go in array)
            {
                go.SendMessage("TakeDamage", damage[1] + "," + moveForword + "," + jumpHight);
            }
        }
        else if (skillType == "skill2_1" || skillType == "skill2_2" || skillType == "skill2_3")
        {
            ArrayList array = GetCanAttackEnemy(AttackRange.Forword);
            foreach (GameObject go in array)
            {
                go.SendMessage("TakeDamage", damage[2] + "," + moveForword + "," + jumpHight);
            }
        }
        else if (skillType == "skill3")
        {
            ArrayList array = GetCanAttackEnemy(AttackRange.Forword);
            foreach (GameObject go in array)
            {
                go.SendMessage("TakeDamage", damage[3] + "," + moveForword + "," + jumpHight);
            }
        }
    }

    public void ShowEffect(string effectName)
    {
        if (effectDict[effectName])
        {
            effectDict[effectName].ShowEffect();
        }
    }

    //检查攻击范围内的怪物
    ArrayList GetCanAttackEnemy(AttackRange attackARange)
    {
        ArrayList enemyArrayList = new ArrayList();
        if (attackARange == AttackRange.Forword)
        {
            foreach(GameObject go in TranscriptManager._instance.enemyList)
            {
                if(go != null)
                {
                    Vector3 goPosition = transform.InverseTransformPoint(go.transform.position);
                    if (goPosition.z <= 100f && goPosition.z >= 0f)
                    {
                        float distance = Vector3.Distance(Vector3.zero, goPosition);
                        if (distance < forwordAttackDistance)
                        {
                            enemyArrayList.Add(go);
                        }
                    }
                }
            }
        }
        else if (attackARange == AttackRange.Around)
        {
            foreach (GameObject go in TranscriptManager._instance.enemyList)
            {
                if (go != null)
                {
                    float distance = Vector3.Distance(transform.position, go.transform.position);
                    if (distance < aroundAttackDistance)
                    {
                        enemyArrayList.Add(go);
                    }
                }
            }
        }
        return enemyArrayList;
    }

    public void ShowDevilHand()
    {
        string effectName = "DevilHandMobile";

        ArrayList array = GetCanAttackEnemy(AttackRange.Forword);
        foreach (GameObject go in array)
        {
            if (effectDict[effectName] != null)
            {
                RaycastHit hit;
                bool colider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground"));
                if(colider)
                {
                    PlayEffect devilHand = GameObject.Instantiate(effectDict[effectName], hit.point, Quaternion.identity);
                    devilHand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
            }
        }
    }

    public void ShowEffectSelfToTarget(string effectName)
    {
        ArrayList array = GetCanAttackEnemy(AttackRange.Around);
        PlayEffect pe = effectDict[effectName];
        foreach (GameObject go in array)
        {
            if (effectDict[effectName] != null)
            {
                PlayEffect effect = GameObject.Instantiate(effectDict[effectName]);
                effect.transform.parent = transform;
                effect.transform.localPosition = Vector3.zero+Vector3.up*10;
                effect.GetComponent<EffectSettings>().Target = go;
            }
        }
    }

    public void ShowEffectTarget(string effectName)
    {
        ArrayList array = GetCanAttackEnemy(AttackRange.Around);
        PlayEffect pe = effectDict[effectName];
        foreach (GameObject go in array)
        {
            if (effectDict[effectName] != null)
            {
                RaycastHit hit;
                bool colider = Physics.Raycast(go.transform.position + Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground"));
                if (colider)
                {
                    PlayEffect effect = GameObject.Instantiate(effectDict[effectName], hit.point, Quaternion.identity);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            //transform.GetComponent<Animation>().Play("die");
        }
        else
        {
            int random = Random.Range(0, 100);
            if(random < damage)
            {
                animtor.SetTrigger("hit");
                PlayerBlood.Instance.ShowBloodScene();
            }
            damageText.Add(damage.ToString(), Color.yellow, 0.2f);

        }
    }

    public void PlayerDie()
    {
        Destroy(damagePrefab);
        Destroy(gameObject);
    }
}
