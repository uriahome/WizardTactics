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

    public bool Dual;//2枚合体のカードであるかどうか
    // Start is called before the first frame update
    void Start()
    {
        //this.GameObject.transform.localscale = new Vector3(1.0f,1.0f,1.0f);
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
        if (delta >= span)
        {
            MyButton.interactable = true;
        }
    }
    public void OnClick() //ボタンが押されたとき
    {
        int NowMana = PCon.Mana;
        if (NowMana >= MyCost)
        {
            if (Dual)
            {//Dualカードであるならば
                string Name = this.gameObject.name;
                Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す
                string SearchName = Name.Substring(0, Name.Length - 1);//この行でButton_LのL部分を削除
                SearchName += "R(Clone)";//Rを文字列に追加(相方を探すため)
                Debug.Log(Name);
                Debug.Log(SearchName);
                GameObject DualObj = GameObject.Find("DeckCanvas/Deck/" + SearchName);
                if (DualObj)
                {
                    Debug.Log(DualObj + "発見!!!");
                    DCon.DestroyCard(DualObj);
                }
                else
                {
                    Debug.Log("発見できませんでした...");
                    return;//発見できない場合はここで終了する
                }
            }
            PCon.PlayerAnimation(0);//魔法を唱えるアニメーションに移行させる

            if (Build)//建物なら
            {
                // GameObject Summon = Instantiate(Card) as GameObject;//生成する
                //   Summon.transform.position = Player.transform.position;//Playerの場所に出す
                //Summon.transform.position = new Vector3(Player.transform.position.x, 0.1f, Player.transform.position.z);
                PCon.SetBuildObj(Card);//召喚予定のカードに追加する

                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;
                //Debug.Log("召喚しました！");
            }
            else if (Magic)
            {
                string Name = this.gameObject.name;
                Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す
                //Debug.Log(Name);
                OnMagic(Name);
                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;
                //Debug.Log(Name);
                //Debug.Log("唱えました!");
            }
            else
            {//モンスターなら
                GameObject Summon = Instantiate(Card) as GameObject;//生成する
                                                                    //   Summon.transform.position = Player.transform.position;//Playerの場所に出す
                Summon.transform.position = new Vector3(Player.transform.position.x, 0.1f, Player.transform.position.z);
                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;
                //Debug.Log("召喚しました！");
            }
        }
        else
        {
            //Debug.Log("コストを払えませんでした");
        }
    }
    public void OnMagic(string Name)//名前によって処理を変える
    {
        //Debug.Log("Magic!!");
        //Debug.Log(Name);
        switch (Name)
        {
            case "ThreeRedPotionButton":
                //Debug.Log("aaa");
                //StartCoroutine("ThreePotion");
                AttackCon.ThreePotion(0);
                break;
            case "ThreePotionButton":
                AttackCon.ThreePotion(1);
                break;
            case "MagicEnhanceButton":
                Debug.Log("魔力アップ！！");
                PCon.Attack += 10;//プレイヤー自身の攻撃力を上げる(ポーションとかの威力が上がる)
                break;
            case "MagicExpansionButton":
                PCon.MagicExpansion();
                break;
            case "GreenPotionButton":
                PCon.HealAll();
                break;
            case "PotionFeverButton_L":
                AttackCon.FeverPotion();
                break;
            case "DemonicPactButton":
                PCon.Demonic();
                break;

        }
    }

    /*public IEnumerator ThreePotion()
    {
        int Count = 0;
        float interval = 0.05f;
        while (true)
        {
            //Debug.Log("nyaa");
            Count++;
            if (Count >= 3)
            {
                Debug.Log("oaaa");
                yield break;
            }
            AttackCon.ThrowPotion();
            yield return new WaitForSeconds(interval);
        }
    }*/

    public void RewardOnClick(int Num)
    {//報酬画面でのクリック時
        GManager.instance.BattleReward(Num);//自分の番号を送る
    }
}
