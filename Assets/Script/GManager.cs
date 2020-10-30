using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public bool Battle;//戦闘中かどうか

    public GameObject DeckCon;//デッキコントローラーを参照できるように
    public DeckController DCon;
    public List<GameObject> ButtonList = new List<GameObject>();//全カードリスト的な(実際に使えるボタンの方)
                                                                // public List<GameObject> WishList = new List<GameObject>();//勝利時に出てくる3枚

    public GameObject RewardCon;//RewardCanvas入れる
    public RewardController RCon;//RewardController参照用

    public GameObject EnemyM;//敵の生成をしてくれるやつ
    public EnemyManager EnemyMan;

    public GameObject PlayerM;//プレイヤーマスター
    public PlayerController PCon;//プレイヤーコントローラー
    public Monster PMon;//プレイヤーのモンスター
    // public GameObject RewardCard;//実際に手に入れるカード

    public GameObject SceneCon;
    public SceneController SCon;

    private void Awake()
    {
        
        if (instance == null)//1つだけ存在するようにする
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);//被っていたら消える
        }

        DeckCon = GameObject.Find("DeckCanvas/Deck");//探索
        DCon = DeckCon.GetComponent<DeckController>();//参照できるように

        RewardCon = GameObject.Find("RewardCanvas/RewardPanel");
        RCon = RewardCon.GetComponent<RewardController>();

        EnemyM = GameObject.Find("EnemyManager");
        EnemyMan = EnemyM.GetComponent<EnemyManager>();

        PlayerM = GameObject.Find("PlayerMaster");
        PCon = PlayerM.GetComponent<PlayerController>();
        PMon = PlayerM.GetComponent<Monster>();

        SceneCon = GameObject.Find("SceneController");
        SCon = SceneCon.GetComponent<SceneController>();

        BattleStart();

    }

    public void BattleReward(int Num)
    {
        DCon.DeckAdd(ButtonList[Num]);//デッキの中に新しいボタンを追加する
        BattleStart();
    }

    public void BattleStart()
    {
        Debug.Log("戦闘開始!!");
        //GameObject Master = Instantiate(PlayerM) as GameObject;
        //Master.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        PCon.SetUp();//プレイヤーマスターの攻撃力等をデフォルトに再設定する
        PMon.Refresh();//全回復させる
        Battle = true;
        //DCon.DeckSetting();
        //DCon.DeckShuffle();
        DCon.DeckPreparation();
        EnemyMan.EnemyStart();
    }
    public void Win()
    {//ゲーム勝利時
        RewardCon.gameObject.SetActive(true);
        RCon.SelectCard();//3枚表示して選ぶ奴
    }

    public void Lose()
    {//ゲーム敗北時
        SCon.ChangeTitle();//タイトルに戻る
        Destroy(this.gameObject);//ここで消さないとタイトルからリスタートした時に試合開始できなかったため負けたら削除するように変更
    }
}