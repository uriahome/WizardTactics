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

    public int DefaultAttack;//初期攻撃力 
    public int Attack;//攻撃力

    public Animator PlayerAnim;//Animator用

    public GameObject BuildObj;//次に建築する建物
    public bool isBuild;//建物を召喚する予定があるかどうか
    public GameObject CursorObj;
    public Vector3 MousePos;

    // Start is called before the first frame update
    void Start()
    {
        DeltaTime = 0;
        Mana = 0;//初期化
        PlayerAnim = GetComponent<Animator>();//自身のAnimatorの取得
        CursorObj = GameObject.Find("cursor");
        CursorObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Battle)//戦闘中なら
        {
            //Summon();
            DeltaTime += AddMana * Time.deltaTime;
            if (DeltaTime >= SpanTime && Mana != MaxMana)//一定時間貯まったらマナが1つ増える
            {
                DeltaTime = 0;
                Mana++;
            }

            CostText.text = "マナ:" + Mana + "/" + MaxMana.ToString();
        }

        if (Input.GetMouseButtonDown(0) && isBuild)
        {//建築予定の建物がありクリックした時
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//マウスの座標を獲得
            SummonBuild();//召喚
            CursorObj.gameObject.SetActive(false);
        }

    }
    /*public void Summon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickPosition = Input.mousePosition;
            ClickPosition.z = 10f;
            Instantiate(Prefab, Camera.main.ScreenToWorldPoint(ClickPosition), Prefab.transform.rotation);

        }
    }*/
    public void PlayerAnimation(int Num)//Playerのアニメーション変更する
    {
        if (Num == 0)
        {
            PlayerAnim.SetTrigger("Magic");//魔法を唱えるアニメーションに移行させる
        }
    }

    public void SetUp()
    {
        Attack = DefaultAttack;//攻撃力をリセット
        Mana = DefaultMana;//マナもリセット
        AddMana = DefaultAddMana;
        MaxMana = DefaultMaxMana;//全部リセットしような...
    }

    public void MagicExpansion()
    {//魔力速度アップと最大マナを拡大
        MaxMana++;
        AddMana *= 1.2f;
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

    public void HealAll(){
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
}
