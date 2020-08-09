using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{


    public string Name;
    public float Hp;
    public int Attack;
    public GameObject Body;
    public int ManaCost;
    public float Speed;
    public float MaxSpeed;
    public float SpeedX;//現在の速度
    public bool IsEnemy;//敵か自陣か
    public bool IsAttack;//攻撃中かどうか
    public float AttackSpan;
    public int MoveDirection;

    public GameObject MyRangePosition;
    [SerializeField] MonsterRange  MyRange= default;
    public GameObject AttackObj;//攻撃判定

    public Rigidbody2D rigid2d;

    public SpriteRenderer MySprite;//色変更用
    public bool Freeze;//氷状態


    public Monster(string Name, float Hp, int Attack ,float Speed,float MaxSpeed,GameObject Body,int ManaCost)
    {
        this.Name = Name;
        this.Hp = Hp;
        this.Attack = Attack;
        this.Speed = Speed;
        this.MaxSpeed = MaxSpeed;
        this.Body = Body;
        this.ManaCost = ManaCost;
    }

    // Start is called before the first frame update
    void Start()
    {
        //new Monster(Name, Hp, Attack, Body, ManaCost);
        this.rigid2d = GetComponent<Rigidbody2D>();
        MyRange = this.GetComponentInChildren<MonsterRange>();//子オブジェクトから取得
        IsAttack = false;
        MyRangePosition = transform.Find("Range").gameObject;//子オブジェクトの取得
        MySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SpeedX = Mathf.Abs(this.rigid2d.velocity.x);//現在の速度を代入
        if (!Freeze) {//凍っていないことが前提
            if (MyRange.DetectTarget)
            {
                //Debug.Log("nyaa");
                rigid2d.velocity = new Vector2(0, 0) * 0;//止める
                                                         //AttackMove();
                if (IsAttack == false)
                {
                    IsAttack = true;
                    //Debug.Log("Attack開始");
                    StartCoroutine("AttackMotion");
                }
            }
            else if (SpeedX < MaxSpeed)
            {
                IsAttack = false;
                Move();
            }
        }
        else
        {
            rigid2d.velocity = new Vector2(0, 0) * 0;
            StartCoroutine("FreezeDelete");
        }
    }

    public void Move()
    {
        /*if (IsEnemy)
        {
            this.rigid2d.AddForce(transform.right * -1*this.Speed);
        }
        else
        {
            this.rigid2d.AddForce(transform.right * this.Speed);
        }*/
        this.rigid2d.AddForce(transform.right * this.Speed*MoveDirection);
    }
    public void AttackMove()
    {
        //Debug.Log("AttackMove");
        IsAttack = true;
        StartCoroutine("AttackMotion");
    }

    IEnumerator AttackMotion()
    {
        StartCoroutine("UpDown");
        rigid2d.velocity = new Vector2(0, 0) * 0;//止める
        IsAttack = true;
        //Debug.Log("AttackMotion");
        //transform.Translate(0, 0.1f, 0);
        GameObject Obj = Instantiate(AttackObj) as GameObject;//攻撃範囲を生成する
        if (IsEnemy)
        {
            Obj.gameObject.tag = "EnemyAttack";
            //Obj.transform.position = MyRangePosition.transform.position;//射程の範囲に出す
            Obj.transform.position = new Vector3(MyRangePosition.transform.position.x-1, MyRangePosition.transform.position.y, MyRangePosition.transform.position.z);
        }
        else
        {
            Obj.gameObject.tag = "PlayerAttack";
            //Obj.transform.position = MyRangePosition.transform.position;//射程の範囲に出す
            Obj.transform.position = new Vector3(MyRangePosition.transform.position.x + 1, MyRangePosition.transform.position.y, MyRangePosition.transform.position.z);
        }
        //Obj.transform.position = MyRangePosition.transform.position;//射程の範囲に出す
        ShockWave SObj = Obj.GetComponent<ShockWave>();
        SObj.Power = this.Attack;//攻撃力の代入
        yield return new WaitForSeconds(AttackSpan);
        //transform.Translate(0, -0.1f, 0);
        Destroy(Obj.gameObject);//判定を消す
        //Debug.Log("オワオワリ");
        IsAttack = false;
        StopCoroutine("UpDown");

    }

    IEnumerator UpDown()
    {
        int UpCount = 0;
        float interval = 0.1f;
        while (true)
        {
            UpCount++;
            transform.Translate(0, 0.1f, 0);
            yield return new WaitForSeconds(interval);
            transform.Translate(0, -0.1f, 0);
            if(UpCount >= 2)
            {
                yield break;
            }
        }
    }

    IEnumerator FreezeDelete()//氷状態の解除
    {
        int Count = 0;
        float interval = 0.1f;
        while (true)
        {
            Count++;
            yield return new WaitForSeconds(interval);
            if (Count > 10)
            {
                MySprite.color = new Color(255f, 255f, 255f);
                Freeze = false;
                yield break;
            }
        }
    }

    public IEnumerator Blink()//点滅
    {
        //this.GetComponent<BoxCollider2D>().enabled = false;
        int BlinkCount = 0;
        float interval = 0.1f;
        while (true)
        {
            var renderComponent = GetComponent<Renderer>();
            BlinkCount++;
            if (BlinkCount >= 8)
            {
                renderComponent.enabled = true;
                //this.GetComponent<BoxCollider2D>().enabled = true;
                yield break;
            }
            renderComponent.enabled = !renderComponent.enabled;
            yield return new WaitForSeconds(interval);
        }
    }

    public void AttackHit(int Power)
    {
        //Debug.Log("Hit" + this.gameObject);
        StartCoroutine("Blink");//点滅処理そして無敵
        this.Hp -= Power;
        if (Hp < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEnemy)//敵かどうか
        {
            if(collision.gameObject.tag == "PlayerAttack")//Player側からの攻撃か
            {
                if (!collision.GetComponent<ShockWave>().IsHit)//まだその攻撃に誰も当たっていないか
                {
                    if (collision.GetComponent<ShockWave>().FreezeAttack)
                    {
                        Freeze = true;
                        MySprite.color = new Color(0, 255f, 255f, 255f);
                    }
                    collision.GetComponent<ShockWave>().IsHit = true;
                    AttackHit(collision.GetComponent<ShockWave>().Power);
                }
            }
        }
        else
        {
            if (collision.gameObject.tag == "EnemyAttack")
            {
                if (!collision.GetComponent<ShockWave>().IsHit)
                {
                    if (collision.GetComponent<ShockWave>().FreezeAttack)
                    {
                        Freeze = true;
                        MySprite.color = new Color(0, 255f, 255f, 255f);
                    }
                    collision.GetComponent<ShockWave>().IsHit = true;
                    AttackHit(collision.GetComponent<ShockWave>().Power);
                }
            }
        }
    }
}
