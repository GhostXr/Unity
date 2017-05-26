using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManage : MonoBehaviour {

    public static HpBarManage _instance;
    public GameObject hpBarPrefab;
    public GameObject DamagePrefab;

    // Use this for initialization
    void Awake () {
        _instance = this;

    }
	
    public GameObject CreateHpBar(GameObject target)
    {
        GameObject hpbar = NGUITools.AddChild(gameObject, hpBarPrefab);
        hpbar.GetComponent<UIFollowTarget>().target = target.transform;
        return hpbar;
    }

    public GameObject CreateDamageText(GameObject target)
    {
        GameObject damage = NGUITools.AddChild(gameObject, DamagePrefab);
        damage.GetComponent<UIFollowTarget>().target = target.transform;
        return damage;
    }
}
