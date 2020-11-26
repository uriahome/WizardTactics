using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public bool Battle;//戦闘中かどうか
    public int WinNum;//勝利回数

    public GameObject DeckCon;//デッキコントローラーを参照できるように
    public DeckController DCon;
    public List<GameObject> ButtonList = new List<GameObject>();//全カードリスト的な(実際に使えるボタンの方)
                                                                // public List<GameObject> WishList = new List<GameObject>();//勝利時に出てくる3枚
    public List<GameObject> ButtonPairList = new List<GameObject>();//ペアカードの相方用のリスト

    public GameObject RewardCon;//RewardCanvas入れる
    public RewardController RCon;//RewardController参照用
    public GameObject RewardText;

    public GameObject ResultCon;//ResultCanvas入れる
    public ResultController ReCon;//ResultController参照用

    public GameObject EnemyM;//敵の生成をしてくれるやつ
    public EnemyManager EnemyMan;

    public GameObject PlayerM;//プレイヤーマスター
    public PlayerController PCon;//プレイヤーコントローラー
    public Monster PMon;//プレイヤーのモンスター
    // public GameObject RewardCard;//実際に手に入れるカード

    public GameObject SceneCon;
    public SceneController SCon;

    public AudioClip BGM_battle1;//戦闘シーンのBGMの管理もここで行う
    public AudioClip BGM_battle2;//戦闘シーンのBGMの管理もここで行う
    public AudioClip BGM_battle_change;//戦闘シーンのBGMの管理もここで行う
    public AudioClip BGM_gameover;//戦闘シーンのBGMの管理もここで行う

    public AudioSource audio1;


    private void Awake()
    {

        if (instance == null)//1つだけ存在するようにする
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            //WinNum = 0;//勝利回数を０に設定
            WinNum = -1;
        }
        else
        {
            Destroy(this.gameObject);//被っていたら消える
        }

        DeckCon = GameObject.Find("DeckCanvas/Deck");//探索
        DCon = DeckCon.GetComponent<DeckController>();//参照できるように

        RewardCon = GameObject.Find("RewardCanvas/RewardPanel");
        RCon = RewardCon.GetComponent<RewardController>();
        RewardText = GameObject.Find("RewardCanvas/SubText");
        RewardText.gameObject.SetActive(false);

        ResultCon = GameObject.Find("ResultCanvas");
        ReCon = ResultCon.GetComponent<ResultController>();
        ResultCon.gameObject.SetActive(false);


        EnemyM = GameObject.Find("EnemyManager");
        EnemyMan = EnemyM.GetComponent<EnemyManager>();

        PlayerM = GameObject.Find("PlayerMaster");
        PCon = PlayerM.GetComponent<PlayerController>();
        PMon = PlayerM.GetComponent<Monster>();

        SceneCon = GameObject.Find("SceneController");
        SCon = SceneCon.GetComponent<SceneController>();

        audio1 = GetComponent<AudioSource>();
        audio1.volume = 0.5f;
        BattleStart();

    }

    public void BattleReward(int Num)
    {
        if (Num == 9)
        {//フレイムのLの場合Rを追加
            DCon.DeckAdd(ButtonPairList[0]);//デッキの中に新しいボタンを追加する
            Debug.Log("フレイムを習得しました");
        }
        else if (Num == 11)
        {
            DCon.DeckAdd(ButtonPairList[1]);//デッキの中に新しいボタンを追加する
            Debug.Log("ポーションフィーバーを習得しました");
        }
        DCon.DeckAdd(ButtonList[Num]);//デッキの中に新しいボタンを追加する
        BattleStart();
    }

    public void BattleStart()
    {
        audio1.Stop();//今流れているのを止めてから流す
        audio1.PlayOneShot(BGM_battle1);
        RewardText.gameObject.SetActive(false);


        Debug.Log("戦闘開始!!");
        //GameObject Master = Instantiate(PlayerM) as GameObject;
        //Master.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        PCon.SetUp();//プレイヤーマスターの攻撃力等をデフォルトに再設定する
        PMon.Refresh();//全回復させる
        Battle = true;
        //DCon.DeckSetting();
        //DCon.DeckShuffle();
        StartCoroutine("WaitTime");
        //DCon.DeckPreparation();//なぜかここでエラーが出るようになってしまった
        //DCon.DeckPreparation_Act();
        WinNum++;
        EnemyMan.EnemyStart();
        //WinNum++;
    }
    public void Win()
    {//ゲーム勝利時
        //WinNum++;//ここでWinNumをカウントすると二重に呼び出されていることがあったので変更
        if(WinNum == 6){
            WinNum++;
            Lose();//実質7回目勝利でクリア表示
            return;
        }
        RewardCon.gameObject.SetActive(true);
        RewardText.gameObject.SetActive(true);
        RCon.SelectCard();//3枚表示して選ぶ奴
    }

    public void Lose()
    {//ゲーム敗北時
        ResultCon.gameObject.SetActive(true);
        ReCon.Finish();

        //SCon.ChangeTitle();//タイトルに戻る
        //Destroy(this.gameObject);//ここで消さないとタイトルからリスタートした時に試合開始できなかったため負けたら削除するように変更
    }


    public void TitleBack(){
        SCon.ChangeTitle();//タイトルに戻る
        Destroy(this.gameObject);//ここで消さないとタイトルからリスタートした時に試合開始できなかったため負けたら削除するように変更
    }
    public IEnumerator WaitTime()
    {//0.1秒待機
        yield return new WaitForSeconds(0.1f);//時間を置くことでDCon.DeckPreparation()のエラーを回避無理やりだがとりあえずこれで
        DCon.DeckPreparation_Act();
    }

    public void BattleChange()
    {
        audio1.Stop();//今流れているのを止めてから流す
        audio1.PlayOneShot(BGM_battle_change);
    }
}