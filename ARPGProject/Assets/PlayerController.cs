using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerInfoType
{
    Name,
    Head,
    Level,
    BattleForce,
    Exp,
    Token,
    Money,
    Enery,
    Toughen,
    All
}

public class PlayerController : MonoBehaviour {

    public static PlayerController _instance;

    #region property
    private string _name;
    private string _head;
    private int _level = 1;
    private int _battleForce = 0;
    private int _exp = 0;
    private int _token;
    private int _money;
    private int _enery;
    private int _toughen;
    #endregion

    private float eneryTimer = 0;
    private float toughenTimer = 0;

    public delegate void OnPlayerInfoChangeEvent(PlayerInfoType info);
    public event OnPlayerInfoChangeEvent playerInfochangeEvent;


    #region unity event
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (this.Enery < 100)
        {
            eneryTimer += Time.deltaTime;
            if (eneryTimer > 60)
            {
                this.Enery += 1;
                eneryTimer -= 60;
            }
        }
        else
        {
            eneryTimer = 0;
        }
        if (this.Toughen < 50)
        {
            toughenTimer += Time.deltaTime;
            if (toughenTimer > 60)
            {
                this.Toughen += 1;
                toughenTimer -= 60;
            }
        }
        else
        {
            toughenTimer = 0;
        }
    }

    #endregion

    #region get set methd
    public string Name
    {
        get { return _name; }
        set {
            _name = value;
            playerInfochangeEvent(PlayerInfoType.Name);
        }
    }
    public string Head
    {
        get { return _head; }
        set {
            _head = value;
            playerInfochangeEvent(PlayerInfoType.Head);
        }
    }
    public int Level
    {
        get { return _level; }
        set {
            _level = value;
            playerInfochangeEvent(PlayerInfoType.Level);
        }
    }
    public int BattleForce
    {
        get { return _battleForce; }
        set {
            _battleForce = value;
            playerInfochangeEvent(PlayerInfoType.BattleForce);
        }
    }
    public int Exp
    {
        get { return _exp; }
        set {
            _exp = value;
            playerInfochangeEvent(PlayerInfoType.Exp);
        }
    }
    public int Token
    {
        get { return _token; }
        set {
            _token = value;
            playerInfochangeEvent(PlayerInfoType.Token);
        }
    }
    public int Money
    {
        get { return _money; }
        set {
            _money = value;
            playerInfochangeEvent(PlayerInfoType.Money);
        }
    }
    public int Enery
    {
        get { return _enery; }
        set {
            _enery = value;
            playerInfochangeEvent(PlayerInfoType.Enery);
        }
    }
    public int Toughen
    {
        get { return _toughen; }
        set {
            _toughen = value;
            playerInfochangeEvent(PlayerInfoType.Toughen);
        }
    }
    #endregion


    void Init()
    {
        this.Money = 8987;
        this.Token = 345;
        this.Enery = 12;
        this.Exp = 34;
        this.Head = "头像底板女性";
        this.Level = 12;
        this.Name = "好好谢谢";
        this.BattleForce = 8987;
        this.Toughen = 23;
        playerInfochangeEvent(PlayerInfoType.All);
    }
}
