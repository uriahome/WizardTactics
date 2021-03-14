using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour
{
    public PlayerController PCon;//PlayerController

    public GameObject Player;

    public int MyCost;//このカードのコスト
    public GameObject Card;//このカードで召喚する（発動する)モンスター(魔法)

    public DeckController DCon;
    public GameObject Deck;


    public bool Magic;//魔法かどうか
    public GameObject Spell;//魔法で出す奴

    public bool Build;//建物かどうか

    public GameObject AttackButton;
    public AttackButtonController AttackCon;

    public Button MyButton;
    public float span;
    public float delta;

    public bool Dual;//2枚合体のカードであるかどうか[ペア]
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerMaster");
        PCon = Player.gameObject.GetComponent<PlayerController>();
        Deck = GameObject.Find("Deck");
        DCon = Deck.gameObject.GetComponent<DeckController>();

        AttackButton = GameObject.Find("DeckCanvas/Mana/AttackButton");
        AttackCon = AttackButton.GetComponent<AttackButtonController>();
        MyButton = GetComponent<Button>();//自分のボタンを制御できるようにする
        MyButton.interactable = false;//一時的に使用不能にする
        span = 1.5f;
        delta = 0;

    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta >= span)//登場後すぐには押せないように時間を計っている
        {
            MyButton.interactable = true;
        }
    }
    public void OnClick() //ボタンが押されたとき
    {
        if (!GManager.instance.Battle)//戦闘中以外は削除か複製の処理に移行する
        {
            if(!GManager.instance.DeleteMagicCheck){
                DeleteMagic();
            }else if(!GManager.instance.CloneMagicCheck){
                CloneMagic();
            }
            return;
        }
        int NowMana = PCon.Mana;
        if (NowMana >= MyCost)
        {
            if (Dual)
            {//Dualカードであるならば
                string Name = this.gameObject.name;
                Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す
               
                string SearchName = Name.Substring(0, Name.Length - 1);//この行でButton_LのL部分を削除
                string PairName = Name.Substring(Name.Length-1,1);//LまたはRの部分を取得
                if(PairName[0] =='L'){//Lの時ときはRをRのときはLを探す
                    SearchName += "R(Clone)";//Rを文字列に追加(相方を探すため)
                }else{
                    SearchName += "L(Clone)";//Rを文字列に追加(相方を探すため)
                }
               

                GameObject DualObj = GameObject.Find("DeckCanvas/Deck/" + SearchName);
                if (DualObj)//相方がある場合のみ使用できる
                {
                    DebugLogger.Log(DualObj + "発見!!!");
                    DCon.DestroyCard(DualObj);
                }
                else
                {
                    DebugLogger.Log("発見できませんでした...");
                    return;//発見できない場合はここで終了する
                }
            }
            PCon.PlayerAnimation(0);//魔法を唱えるアニメーションに移行させる

            if (Build)//建物なら
            {
                PCon.SetBuildObj(Card);//召喚予定のカードに追加する

                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;
            }
            else if (Magic)//魔法なら
            {
                string Name = this.gameObject.name;
                Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す

                OnMagic(Name);
                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;

            }
            else
            {//モンスターなら
                GameObject Summon = Instantiate(Card) as GameObject;//生成する
                                                                    //   Summon.transform.position = Player.transform.position;//Playerの場所に出す
                Summon.transform.position = new Vector3(Player.transform.position.x, 0.1f, Player.transform.position.z);
                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;
            }
        }
        else//コストを払えなかったときの処理を今後ついかするかも？
        {
            //Debug.Log("コストを払えませんでした");
        }
    }

    public void OnMagic(string Name)//名前によって処理を変える
    {

        switch (Name)
        {
            case "ThreeRedPotionButton"://レッドポーション
                AttackCon.ThreePotion(0);
                break;
            case "ThreePotionButton"://ブルーポーション
                AttackCon.ThreePotion(1);
                break;
            case "MagicEnhanceButton"://ヒラメキ
                Debug.Log("魔力アップ！！");
                PCon.Attack += 10;//プレイヤー自身の攻撃力を上げる(ポーションとかの威力が上がる)
                break;
            case "MagicExpansionButton"://キュウケイ
                PCon.MagicExpansion();
                break;
            case "GreenPotionButton"://グリーンポーション
                PCon.HealAll();
                break;
            case "PotionFeverButton_L":case "PotionFeverButton_R"://LでもRでも同じ効果//ポーションフィーバー
                AttackCon.FeverPotion();
                break;
            case "DemonicPactButton"://ケイヤク
                PCon.Demonic();
                break;
            case "SnowstormButton"://フブキ
                PCon.Snowstorm();
                break;
            case "MagicalAwakeningButton_L":case "MagicalAwakeningButton_R"://LでもRでも同じ効果//カクセイ
                //Debug.Log("Awakening!!");
                PCon.MagicalAwakening();
                DCon.AddMiss();
                break;
            case "MissButton"://ミスボタン
                Debug.Log("Miss!");
                break;
            case "OverClockButton"://カソク
                PCon.OverClock();
                DCon.AddMiss();
                break;
            case "PotionExpansionButton"://ゾウリョウ
                PCon.ThrowPotionCountUp();
                break;
            case "KnightDualButton"://ソードを2体召喚するがミスを3枚追加する//ツインソード
                //PCon.DualSummon(Card);
                PCon.MultipleSummon(Card,2);
                DCon.AddMiss();
                DCon.AddMiss();
                DCon.AddMiss();
                break;
            case "IceSlimeButton"://アイススライム4体召喚するので2*2で出したい
                PCon.MultipleSummon(Card,4);
                break;
        }
    }
    public void RewardOnClick(int Num)
    {//報酬画面でのクリック時
        GManager.instance.BattleReward(Num);//自分の番号を送る
    }

    public void SkipReward(){//報酬をスキップする
        DebugLogger.Log("Skip!!");
        //GManager.instance.BattleStart();
        GManager.instance.NextMapSelect();//マップ選択が追加されたのでそっちに移行する
    }

    public void DeleteMagic(){//デッキリストから選択されたカードを削除する処理
        if(GManager.instance.DeleteMagicCheck){
            return;//削除をすでに行っていたら処理をここで抜ける
        }
        DebugLogger.Log("ここでデッキから削除する処理をしたい");
        string Name = this.gameObject.name;
        Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す
        //DebugLogger.Log(Name);
        //DebugLogger.Log(DCon.DeckList[0].name);
        for (int j = 0; j < DCon.DeckList.Count; j++)//デッキリストを1枚ずつ参照
        {
            DebugLogger.Log(DCon.DeckList[j]);
            if(DCon.DeckList[j].name == Name){
                DCon.DeckList.Remove(DCon.DeckList[j]);
                //同名カードを複数枚削除しないように一度やったら抜ける
                GManager.instance.DeleteMagicCheck = true;//削除を行ったことを記録する
                PCon.DefaultAttackUp();//プレイヤーの基本攻撃力を上げる
                break;
            }          
        }

        if(Dual){//ペア魔法ならどちらも消したい
            string SearchName = Name.Substring(0, Name.Length - 1);//この行でButton_LのL部分を削除
            string PairName = Name.Substring(Name.Length-1,1);//LまたはRの部分を取得
            if(PairName[0] =='L'){//Lの時ときはRをRのときはLを探す
                SearchName += "R";//Rを文字列に追加(相方を探すため)
            }else{
                SearchName += "L";//Lを文字列に追加(相方を探すため)
            }
            DebugLogger.Log(Name);
            DebugLogger.Log(SearchName);
             for (int j = 0; j < DCon.DeckList.Count; j++)//デッキリストを1枚ずつ参照
             {
                 DebugLogger.Log(DCon.DeckList[j]);
                 if(DCon.DeckList[j].name == SearchName){
                     DebugLogger.Log("削除しました");
                     DCon.DeckList.Remove(DCon.DeckList[j]);
                     //同名カードを複数枚削除しないように一度やったら抜ける
                     GManager.instance.DeleteMagicCheck = true;//削除を行ったことを記録する
                     //PCon.DefaultAttackUp();//プレイヤーの基本攻撃力を上げる
                     break;
                     }
             }
            //表示の上でのペア魔法のもう片方を削除する処理
            SearchName += "(Clone)";
            GameObject ShowDeckPanel;
            ShowDeckPanel = transform.parent.gameObject;//親オブジェクトを取得
            foreach (Transform child in ShowDeckPanel.transform)//子オブジェクト取得
            {
                if (child.gameObject.name.Equals(SearchName)){//取得したオブジェクトのペアになるオブジェクトなら削除する
                    Destroy(child.gameObject);
                    break;
                }
            }


        }

        Destroy(this.gameObject);//一覧表示からも削除

    }

    public void CloneMagic(){//デッキリストから選択されたカードを複製する処理
        DebugLogger.Log("ここで魔法を複製する処理を実行する");
        string Name = this.gameObject.name;
        Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す

        for (int j = 0; j < DCon.DeckList.Count; j++)//デッキリストを1枚ずつ参照
        {
            if(DCon.DeckList[j].name == Name){
                DCon.DeckAdd(DCon.DeckList[j]);
                GManager.instance.CloneMagicCheck = true;//複製を行ったことを記録する
                break;
            }          
        }

        if(Dual){//ペア魔法ならどちらも複製する
            string SearchName = Name.Substring(0, Name.Length - 1);//この行でButton_LのL部分を削除
            string PairName = Name.Substring(Name.Length-1,1);//LまたはRの部分を取得
            if(PairName[0] =='L'){//Lの時ときはRをRのときはLを探す
                SearchName += "R";//Rを文字列に追加(相方を探すため)
            }else{
                SearchName += "L";//Lを文字列に追加(相方を探すため)
            }
             for (int j = 0; j < DCon.DeckList.Count; j++)//デッキリストを1枚ずつ参照
             {
                 DebugLogger.Log(DCon.DeckList[j]);
                 if(DCon.DeckList[j].name == SearchName){
                     DebugLogger.Log("削除しました");
                     DCon.DeckAdd(DCon.DeckList[j]);
                     GManager.instance.CloneMagicCheck = true;//複製を行ったことを記録する
                     break;
                     }
             }
        }

        //複製後の一覧を表示する
        GManager.instance.SelectShowDeck();
        GameObject MapButtonPanel = GameObject.Find("MapCanvas/MapButtonPanel");
        MapSelectController MapCon = MapButtonPanel.GetComponent<MapSelectController>();
        MapCon.ShowDeck();
        //Destroy(this.gameObject);//一覧表示からも削除
    }
}
