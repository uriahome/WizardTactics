using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{


    public string Name;

    public float MaxHp;//最大体力
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
    public int MoveDirection;//進行方向
    public GameObject MyRangePosition;
    [SerializeField] MonsterRange MyRange = default;
    public GameObject AttackObj;//攻撃判定

    public Rigidbody2D rigid2d;

    public SpriteRenderer MySprite;//色変更用
    public bool Freeze;//氷状態

    public bool Master;//マスターかどうか(敵)
    public bool ChangeBGM;

    public bool FreezeAttackM;//氷属性の攻撃かどうか

    public bool PlayerMaster;//プレイヤーのマスターかどうか

    //効果音用の設定
    public AudioClip sound1;
    public AudioClip sound2;//被弾の効果音
    public AudioSource audio1;

    //public bool Death;
    public bool Deathrattle;//やられたときの効果を持っているかどうか

    public bool AttackAnime;//攻撃用の画像を持っているかどうか//試験的にここで導入する将来的には攻撃アニメーションにして全キャラにつけたい
    public Sprite[] ChangeSprite;//攻撃用画像と待機画像


    /*public Monster(string Name, float Hp, int Attack, float Speed, float MaxSpeed, GameObject Body, int ManaCost)//生かせてない
    {
        this.Name = Name;
        this.Hp = Hp;
        this.Attack = Attack;
        this.Speed = Speed;
        this.MaxSpeed = MaxSpeed;
        this.Body = Body;
        this.ManaCost = ManaCost;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        //new Monster(Name, Hp, Attack, Body, ManaCost);
        this.rigid2d = GetComponent<Rigidbody2D>();
        MyRange = this.GetComponentInChildren<MonsterRange>();//子オブジェクトから取得
        IsAttack = false;
        MyRangePosition = transform.Find("Range").gameObject;//子オブジェクトの取得
        MySprite = GetComponent<SpriteRenderer>();
        audio1 = GetComponent<AudioSource>();
        if (!Master && !PlayerMaster)
        {
            audio1.PlayOneShot(sound1);//召喚の効果音を再生
        }
        ChangeBGM = false;
        //Death = false;
    }

    // Update is called once per frame
    void Update()
    {


        SpeedX = Mathf.Abs(this.rigid2d.velocity.x);//現在の速度を代入
        if (!Freeze)
        {//凍っていないことが前提
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
        /*if (Master)
        {
            this.transform.position = new Vector3(7.56f, 0.5f, 0.0f);//敵のマスターならこの座標にいる
        }*/
        if (!GManager.instance.Battle)
        {
            if (!PlayerMaster)
            {
                Destroy(this.gameObject);//非戦闘時は消える
            }
        }

        if (Hp <= 0)//未満なら破壊される
        {
            Debug.Log("やられた" + this.gameObject);
            if (Master)
            {//マスターがやられたなら
                //GManager.instance.Battle = false;//戦闘終了
                //GManager.instance.Win();//勝ち
                StartCoroutine("MasterDeath");
            }
            else if (PlayerMaster)
            {//自分がやられたならば
             //Death = true;
                StartCoroutine("PlayerMasterDeath");
            }
            else
            {
                if(Deathrattle){//やられたときの効果を処理する
                    //string Name = this.gameObject.name;
                    //Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す
                    GManager.instance.Deathrattle(Name);
                }
                Destroy(this.gameObject);
            }
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
        this.rigid2d.AddForce(transform.right * this.Speed * MoveDirection);
    }
    public void AttackMove()
    {
        //Debug.Log("AttackMove");
        IsAttack = true;
        StartCoroutine("AttackMotion");//攻撃モーションの開始
    }

    IEnumerator AttackMotion()
    {
        StartCoroutine("UpDown");//なんとなく上下に動かす
        rigid2d.velocity = new Vector2(0, 0) * 0;//止める
        IsAttack = true;//攻撃中の設定にする

        if(AttackAnime){
            MySprite.sprite = ChangeSprite[1];//攻撃用画像に変更
        }

        //Debug.Log("AttackMotion");
        //transform.Translate(0, 0.1f, 0);
        GameObject Obj = Instantiate(AttackObj) as GameObject;//攻撃範囲を生成する
        if (IsEnemy)//敵キャラの時の処理
        {
            Obj.gameObject.tag = "EnemyAttack";
            //Obj.transform.position = MyRangePosition.transform.position;//射程の範囲に出す
            Obj.transform.position = new Vector3(MyRangePosition.transform.position.x - 1, MyRangePosition.transform.position.y, MyRangePosition.transform.position.z);
        }
        else//自キャラの時の処理
        {
            Obj.gameObject.tag = "PlayerAttack";
            //Obj.transform.position = MyRangePosition.transform.position;//射程の範囲に出す
            Obj.transform.position = new Vector3(MyRangePosition.transform.position.x + 1, MyRangePosition.transform.position.y, MyRangePosition.transform.position.z);
        }
        //Obj.transform.position = MyRangePosition.transform.position;//射程の範囲に出す
        ShockWave SObj = Obj.GetComponent<ShockWave>();
        SObj.Power = this.Attack;//攻撃力の代入
        if (FreezeAttackM)
        {//氷属性攻撃なら氷攻撃にちゃんとする
            SObj.FreezeAttack = true;
            Destroy(this.gameObject);//氷属性攻撃は一度使用したらやられるようにする（バランス調整のため)
        }
        yield return new WaitForSeconds(AttackSpan);
        //transform.Translate(0, -0.1f, 0);
        Destroy(Obj.gameObject);//判定を消す
        //Debug.Log("オワオワリ");
        IsAttack = false;
        
        if(AttackAnime){
            MySprite.sprite = ChangeSprite[0];//待機画像に変更
        }

        StopCoroutine("UpDown");

    }

    IEnumerator UpDown()//攻撃の動作
    {
        int UpCount = 0;
        float interval = 0.1f;
        while (true)
        {
            UpCount++;
            transform.Translate(0, 0.1f, 0);
            yield return new WaitForSeconds(interval);
            transform.Translate(0, -0.1f, 0);
            if (UpCount >= 2)
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
            if (Count > 15)
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

    public void AttackHit(int Power)//攻撃がヒットしたときの処理
    {
        //Debug.Log("Hit" + this.gameObject);
        StartCoroutine("Blink");//点滅処理そして無敵
                                //if (!Master)
                                //{
                                //audio1.PlayOneShot(sound2);//被弾の効果音を再生
        audio1.volume = 0.25f;
        audio1.clip = sound2;
        audio1.Play();
        //}
        this.Hp -= Power;//ダメージを受ける
        /*if (Hp <= 0)//未満なら破壊される
        {
            Debug.Log("やられた" + this.gameObject);
            if (Master)
            {//マスターがやられたなら
                //GManager.instance.Battle = false;//戦闘終了
                //GManager.instance.Win();//勝ち
                StartCoroutine("MasterDeath");
            }
            else if (PlayerMaster)
            {
                //Death = true;
                StartCoroutine("PlayerMasterDeath");
            }
            else
            {

                Destroy(this.gameObject);
            }
        }*/
        //バグの温床

        if ((Hp <= MaxHp / 2) && Master && !ChangeBGM)
        {
            ChangeBGM = true;
            GManager.instance.BattleChange();//敵を半分まで削ったら曲を変える
        }
    }

    public IEnumerator MasterDeath()//敵マスターの撃破時
    {
        yield return new WaitForSeconds(0.1f);
        //Destroy(this.gameObject);
        GManager.instance.Battle = false;//戦闘終了
        GManager.instance.Win();//勝ち
        Destroy(this.gameObject);
        //yield break;
    }

    public IEnumerator PlayerMasterDeath()
    {//自分が敗北した時
        yield return new WaitForSeconds(0.1f);
        //Destroy(this.gameObject);
        GManager.instance.Battle = false;//戦闘終了
        GManager.instance.Lose();//負け
        Destroy(this.gameObject);
    }

    public void Refresh()
    {//全回復
        this.Hp = MaxHp;
    }

    public void Heal()
    {//30%回復
        this.Hp += MaxHp * 0.3f;
        if (this.Hp >= MaxHp)
        {
            this.Hp = MaxHp;
        }
    }

    public void SelfDestruct()
    {//自爆
        int Damage;
        Damage = (int)(MaxHp * 0.1f);
        AttackHit(Damage);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEnemy)//敵かどうか
        {
            if (collision.gameObject.tag == "PlayerAttack")//Player側からの攻撃か
            {
                if (!collision.GetComponent<ShockWave>().IsHit)//まだその攻撃に誰も当たっていないか
                {
                    //Debug.Log(collision.GetComponent<ShockWave>().IsHit);
                    if (collision.GetComponent<ShockWave>().FreezeAttack && (!Freeze))
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

    public void Freezing(){
        MySprite.color = new Color(0, 255f, 255f, 255f);
    }
}
