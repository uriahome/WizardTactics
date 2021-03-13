using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Prefab;

    private Vector3 ClickPosition;//クリックした場所

    public float AddMana;// = 1.0f;//増えるマナの速度
    public float DefaultAddMana;//試合開始時の増えるマナの速度

    public float SpanTime = 2.0f;//1マナ増えるのに必要なポイン

    public float DeltaTime;//経過時間

    public int DefaultMana;//初期マナ
    public int Mana;//マナコスト
    public int DefaultMaxMana;//試合開始時の最大マナコスト
    public int MaxMana;//最大マナコスト

    public Text CostText;//マナコスト表示用のテキスト
    public Text PowerText;//マナコスト表示用のテキスト

    public int DefaultAttack;//初期攻撃力 
    public int Attack;//攻撃力

    public Animator PlayerAnim;//Animator用

    public GameObject BuildObj;//次に建築する建物
    public bool isBuild;//建物を召喚する予定があるかどうか
    public GameObject CursorObj;
    public Vector3 MousePos;

    public Monster PlayerMon;//自分のMonster.cs

    public int DefaultThrowPotionCount;//初期に通常攻撃で投げるポーションの数
    public int ThrowPotionCount;//通常攻撃で投げるポーションの数

    // Start is called before the first frame update
    void Start()
    {
        DeltaTime = 0;
        Mana = 0;//初期化
        PlayerAnim = GetComponent<Animator>();//自身のAnimatorの取得
        CursorObj = GameObject.Find("cursor");
        CursorObj.gameObject.SetActive(false);
        PlayerMon = GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Battle)//戦闘中なら
        {
            DeltaTime += AddMana * Time.deltaTime;
            if (DeltaTime >= SpanTime && Mana != MaxMana)//一定時間貯まったらマナが1つ増える
            {
                DeltaTime = 0;
                Mana++;
            }

            CostText.text = "マナ:" + Mana + "/" + MaxMana.ToString();
            PowerText.text = "魔力:" + Attack.ToString();
        }

        if (Input.GetMouseButtonDown(0) && isBuild)
        {//建築予定の建物がありクリックした時
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//マウスの座標を獲得
            SummonBuild();//召喚
            CursorObj.gameObject.SetActive(false);
        }

    }
    public void PlayerAnimation(int Num)//Playerのアニメーション変更する
    {
        if (Num == 0)
        {
            PlayerAnim.SetTrigger("Magic");//魔法を唱えるアニメーションに移行させる
        }
    }

    public void SetUp()//戦闘準備でプレイヤーの設定されたパラメータを初期値に戻す
    {
        Attack = DefaultAttack;//攻撃力をリセット
        Mana = DefaultMana;//マナもリセット
        AddMana = DefaultAddMana;
        MaxMana = DefaultMaxMana;//全部リセットしような...
        ThrowPotionCount = DefaultThrowPotionCount;//ポーションを投げる数もリセットする
    }

    public void MagicExpansion()
    {//魔力速度アップと最大マナを拡大
        MaxMana++;
        ClockUp();
    }

    public void SummonBuild()//実際に建築予定の建物をInstantiate処理をする
    {
        GameObject Summon = Instantiate(BuildObj) as GameObject;//生成する
        Summon.transform.position = new Vector3(MousePos.x, 0.1f, this.transform.position.z);//クリックしたときのマウスのx座標で召喚する
        isBuild = false;
    }

    public void SetBuildObj(GameObject GObj)//建物カードからの召喚予定を登録する
    {
        BuildObj = GObj;
        isBuild = true;
        CursorObj.gameObject.SetActive(true);
    }

    public void HealAll(){//戦闘中のプレイヤー側のキャラクターを全て回復させる
        GameObject Ally;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))//Hierarchy上のオブジェクトをすべて取得
            {
                if (obj.gameObject.tag == "PlayerMonster")//tagがPlayerMonsterなら
                {
                    Ally = obj.gameObject;
                    Ally.GetComponent<Monster>().Heal();//回復を実行させていく
                    
                }
            }
    }

    public void AttackUpAll(){//戦闘中のプレイヤー側のキャラクターの攻撃力を上昇させる
        GameObject Ally;
        Monster AllyMonster;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))//Hierarchy上のオブジェクトをすべて取得
            {
                if (obj.gameObject.tag == "PlayerMonster")//tagがPlayerMonsterなら
                {
                    Ally = obj.gameObject;
                    AllyMonster = Ally.GetComponent<Monster>();
                    //if(!AllyMonster.PlayerMaster){//プレイヤーマスターの攻撃力は0にしたいのでここで上昇させない。そのためにBool型のPlayerMasterがfalseであるかチェックする      
                    AllyMonster.AttackUp();//攻撃力を上昇させる関数を実行する
                    //}
                    
                }
            }
    }

    public void Demonic(){//魔力上昇1回と2マナ回復とプレイヤーへのダメージ
        MagicEnhance();
        ManaEnhance();
        ManaEnhance();
        PlayerMon.SelfDestruct();
    }

    public void MagicEnhance(){//魔力を5上げる処理
        Attack+=5;
    }
    public void ManaEnhance(){//魔法の効果などで1マナ増加させる処理
        Mana++;
        if(Mana >= MaxMana){
            Mana = MaxMana;
        }
    }

    public void Snowstorm(){//「フブキ」の処理
        GameObject Enemy;
        Monster EnemyMonster;
        int Damage;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                if (obj.gameObject.tag == "EnemyMonster")//敵キャラクターの抽出
                {
                    Enemy = obj.gameObject;
                    EnemyMonster = Enemy.GetComponent<Monster>();
                    EnemyMonster.Freeze = true;
                    EnemyMonster.Freezing();
                    Damage = (int)(Attack * 0.5f);
                    EnemyMonster.AttackHit(Damage);//魔力の半分ダメージを与える
                    
                }
            }
    }

    public void MagicalAwakening(){//魔力を2倍にする
         Attack =Attack*2;
    }

    public void OverClock(){//マナ回復速度を2回上昇させる(1.44倍)
        ClockUp();//1.2上げる処理を2回
        ClockUp();//今度魔法呼び出しの段階でClockUp()を2回呼ぶように変更したい
    }
    public void ClockUp(){//汎用的にマナ回復速度を上げるようにこれで統一したい
        AddMana *=1.2f;
        DebugLogger.Log("ClockUp!!!");
    }

    public void ThrowPotionCountUp(){//通常攻撃で投げるポーションの数を増やす(戦闘時)
        ThrowPotionCount++;
    }

    public void DualSummon(GameObject Card){//2回同じモンスターを召喚する
       StartCoroutine(SummonDelay(Card));
    }

    IEnumerator SummonDelay(GameObject Card)//0.5秒の間隔を置いて召喚する
    {
        GameObject Summon = Instantiate(Card) as GameObject;//生成する
        Summon.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        Summon = Instantiate(Card) as GameObject;//生成する
        Summon.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);
    }

    public void DefaultAttackUp(){//基本攻撃力を上昇させるイベント
        DefaultAttack +=5;
    }

    public void DefaultMaxManaUp(){//最大マナを上昇させるイベント
        DefaultMaxMana++;
    }

    public void DefaultManaUp(){//戦闘開始時のマナを上昇させるイベント
        DefaultMana++;
        if(DefaultMana >= DefaultMaxMana){
            DefaultMana = DefaultMaxMana;
        }
    }

    public void DefaultThrowPotionCountUp(){//戦闘開始時の通常攻撃でポーションを投げる数を上昇させるイベント
        DefaultThrowPotionCount++;
    }
}
