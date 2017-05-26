using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Animation animation;
    private Transform bloodPoint;
    private CharacterController cc;

    public GameObject damageEffect;
    public int fullHp = 200;
    private int hp = 200;
    public float speed = 0.5f;
    public float downSpeed = 0.5f;
    public float downDistance = 0f;
    public float attackRate = 2f;
    public float attackTimer = 2f;
    public float attackDistance = 2f;
    public float attack = 20f;

    private float distance = 2f;

    private GameObject hpbar;
    private UISlider hpBarFg;
    private GameObject damagePrefab;
    private HUDText damageText;

    // Use this for initialization
    void Start () {
        animation = GetComponent<Animation>();
        bloodPoint = transform.Find("bloodPoint");
        cc = GetComponent<CharacterController>();
        InvokeRepeating("CalcDistance", 0, 0.1f);

        Transform hpBarPoint = transform.Find("hp_bar_point");
        hpbar = HpBarManage._instance.CreateHpBar(hpBarPoint.gameObject);
        hpBarFg = hpbar.transform.Find("bg").GetComponent<UISlider>();
        hp = fullHp;

        damagePrefab = HpBarManage._instance.CreateDamageText(hpBarPoint.gameObject);
        damageText = damagePrefab.transform.GetComponent<HUDText>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(hp <= 0)
        {
            downDistance += downSpeed * Time.deltaTime;
            transform.Translate(-transform.up * downSpeed);
            if(downDistance > 0.05f)
            {
                Destroy(gameObject);
            }
            return;
        }
        if (distance < attackDistance)
        {
            attackTimer += Time.deltaTime;
            bool attact = false;
            if(attackTimer <= attackRate)
            {
                attact = animation.Play("attack01");
                attackTimer = 0;
            }
            if(attact == false)
            {
                attackTimer = 0;
                attact = animation.Play("idle");
            }
        }
        else
        {
            attackTimer = 0;
            animation.Play("walk");
            Move();
        }
    }

    void Move()
    {
        Transform player = TranscriptManager._instance.player.transform;
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        cc.SimpleMove(transform.forward*speed);
    }

    //param 1. 伤害 2.后退 3.浮空
    public void TakeDamage(string param)
    {
        if (hp <= 0) return;
        string[] proArray = param.Split(',');
        int damage = int.Parse(proArray[0]);
        float distance = float.Parse(proArray[1]);
        float hight = float.Parse(proArray[2]);
        Combo._instance.SubCombo();
        //受击动画
        animation.Play("takedamage");

        //浮空后退
        Vector3 playerDirection = transform.InverseTransformDirection(TranscriptManager._instance.player.transform.forward);
        iTween.MoveBy(gameObject, playerDirection * -distance + Vector3.up * hight, 0.3f);

        //出血特效
        GameObject.Instantiate(damageEffect, bloodPoint.transform.position, Quaternion.identity);

        hp -= damage;
        if(hp <= 0)
        {
            animation.Play("die");
            Destroy(hpbar);
            Destroy(damagePrefab);
            GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            hpBarFg.value = (float)hp / fullHp;
            damageText.Add("-"+ damage, Color.red, 0.2f);
        }
    }

    public void CalcDistance()
    {
        Transform player = TranscriptManager._instance.player.transform;
        distance = Vector3.Distance(player.position, transform.position);
    }

    public void Attack()
    {
        Transform player = TranscriptManager._instance.player.transform;
        CalcDistance();
        if (distance < attackDistance)
        {
            player.SendMessage("TakeDamage", attack, SendMessageOptions.DontRequireReceiver);
        }
    }
}
