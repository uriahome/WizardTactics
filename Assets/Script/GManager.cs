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
    // public GameObject RewardCard;//実際に手に入れるカード
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

    }

    public void BattleReward(int Num)
    {
        DCon.DeckAdd(ButtonList[Num]);//デッキの中に新しいボタンを追加する
        BattleStart();
    }

    public void BattleStart()
    {//
        Battle = true;
        DCon.DeckSetting();
        DCon.DeckShuffle();
        EnemyMan.EnemyStart();
    }
    public void Win()
    {//ゲーム勝利時
        RewardCon.gameObject.SetActive(true);
        RCon.SelectCard();//3枚表示して選ぶ奴
    }
}