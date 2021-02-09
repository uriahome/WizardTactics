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
    public GameObject SkipButton;//スキップボタン

    public GameObject MapCanvas;
    public GameObject MapPanel;
    public MapSelectController MapSelect;
    public GameObject MapText;
    public GameObject MapShowDeckPanel;

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

    public bool DeleteMagicCheck;//魔法の削除をしたかどうか/trueで削除を行った、falseでまだ削除を行っていない
    public bool CloneMagicCheck;//魔法の複製を行ったかどうか
    public bool MapBattleCheck;//trueのときは戦闘マップのみのリストを呼び出す

    public bool EventBattle;
    private void Awake()
    {

        if (instance == null)//1つだけ存在するようにする
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            WinNum = -1;
            MapBattleCheck = false;//最初はfalseにしておく
            EventBattle = false;
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
        SkipButton = GameObject.Find("RewardCanvas/SkipButton");
        SkipButton.gameObject.SetActive(false);

        MapCanvas = GameObject.Find("MapCanvas");
        MapPanel = GameObject.Find("MapCanvas/MapButtonPanel");
        MapText = GameObject.Find("MapCanvas/SubText");
        MapShowDeckPanel = GameObject.Find("MapCanvas/ShowDeck");
        MapSelect = MapPanel.GetComponent<MapSelectController>();
        MapCanvas.gameObject.SetActive(false);

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

    public void BattleReward(int Num)//デュアル魔法の場合似合い方を追加する処理、かなり無理やりなので調整したい
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
        }else if(Num ==18){
            DCon.DeckAdd(ButtonPairList[2]);//デッキの中に新しいボタンを追加する
            Debug.Log("カクセイを習得しました");
        }
        DCon.DeckAdd(ButtonList[Num]);//デッキの中に新しいボタンを追加する
        //BattleStart();
        NextMapSelect();//次に戦う相手を選択するために表示する
    }

    public void NextMapSelect()
    {
        //次に戦うキャラクターを選択できるボタンを表示する
        RewardCon.gameObject.SetActive(false);
        RewardText.gameObject.SetActive(false);
        SkipButton.gameObject.SetActive(false);
        MapCanvas.gameObject.SetActive(true);
        MapPanel.gameObject.SetActive(true);
        MapText.gameObject.SetActive(true);
        MapShowDeckPanel.gameObject.SetActive(false);
        if(MapBattleCheck){
            MapSelect.SelectMap_Battle();//戦闘のみのマップを呼び出す
        }else{
            MapSelect.SelectMap();//イベント込みのマップを呼び出す
        }
        MapBattleCheck = !MapBattleCheck;//trueとfalseを反転させる
    }
    public void SelectShowDeck()
    {
        MapText.gameObject.SetActive(false);
        MapShowDeckPanel.gameObject.SetActive(true);
    }
    public void SelectNextEnemy(int SelectNum)
    {
        RCon.Refresh();//報酬を呼び出していないとチェックする
        //選択された番号に応じた敵を出して戦闘を始める
        audio1.Stop();//今流れているのを止めてから流す
        audio1.PlayOneShot(BGM_battle1);
        RewardText.gameObject.SetActive(false);
        SkipButton.gameObject.SetActive(false);
        MapCanvas.gameObject.SetActive(false);


        PCon.SetUp();//プレイヤーマスターの攻撃力等をデフォルトに再設定する
        PMon.Refresh();//全回復させる
        Battle = true;
        StartCoroutine("WaitTime");
        WinNum++;
        //EnemyMan.EnemyStart();
        EnemyMan.SelectEnemyStart(SelectNum);
    }

    public void BattleStart()
    {
        DebugLogger.Log("戦闘開始");
        //RCon.Refresh();//報酬を呼び出していないとチェックする
        audio1.Stop();//今流れているのを止めてから流す
        audio1.PlayOneShot(BGM_battle1);
        RewardText.gameObject.SetActive(false);
        SkipButton.gameObject.SetActive(false);
        MapCanvas.gameObject.SetActive(false);
        PCon.SetUp();//プレイヤーマスターの攻撃力等をデフォルトに再設定する
        PMon.Refresh();//全回復させる
        Battle = true;
        StartCoroutine("WaitTime");
        WinNum++;
        EnemyMan.EnemyStart();
    }
    public void Win()
    {//ゲーム勝利時
        if(WinNum == 6){
            WinNum++;
            Lose();//実質7回目勝利でクリア表示
            return;
        }
        RewardCon.gameObject.SetActive(true);
        RewardText.gameObject.SetActive(true);
        SkipButton.gameObject.SetActive(true);
        if(!EventBattle){
            DebugLogger.Log("通常戦勝利！");
            RCon.SelectCard();//3枚表示して選ぶ奴
            //EventBattle = false;
        }else{
            DebugLogger.Log("イベント戦勝利！");
            RCon.EventSelectCard();
            EventBattle = false;
        }
    }

    public void Lose()
    {//ゲーム敗北時
        ResultCon.gameObject.SetActive(true);
        ReCon.Finish();
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

    public void Deathrattle(string Name){//やられたときに発動する効果一覧
        switch(Name){
            case "Gurimo":
            PCon.MagicEnhance();
            break;
            case "Spyder":
            DebugLogger.Log("増殖増殖!!");
            DCon.BattleDeckAdvance(ButtonList[16]);//Spyderのカードを戦闘中のデッキに追加する
            PCon.ManaEnhance();//マナを1つ追加
            break;
            case "Senpun":
            PCon.ClockUp();//マナ回復速度が1.2倍
            break;
            case "Green"://やられたらグリーンポーションの回復効果
            PCon.HealAll();
            break;
        } 
    }

    public void MaxManaUp(){//プレイヤーの最大マナを上げるコマンドを実行する
        PCon. DefaultMaxManaUp();
    }
}