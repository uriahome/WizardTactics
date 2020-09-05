﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject AttackButton;
    public AttackButtonController AttackCon;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerMaster");
        PCon = Player.gameObject.GetComponent<PlayerController>();
        Deck = GameObject.Find("Deck");
        DCon = Deck.gameObject.GetComponent<DeckController>();

        AttackButton = GameObject.Find("DeckCanvas/Mana/AttackButton");
        AttackCon = AttackButton.GetComponent<AttackButtonController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick() //ボタンが押されたとき
    {
        int NowMana = PCon.Mana;
        if (NowMana >= MyCost)
        {
            PCon.PlayerAnimation(0);//魔法を唱えるアニメーションに移行させる

            if (!Magic)//モンスターなら
            {
                GameObject Summon = Instantiate(Card) as GameObject;//生成する
                                                                    //   Summon.transform.position = Player.transform.position;//Playerの場所に出す
                Summon.transform.position = new Vector3(Player.transform.position.x, 0.1f, Player.transform.position.z);
                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;
                Debug.Log("召喚しました！");
            }
            else
            {
                string Name = this.gameObject.name;
                Name = Name.Replace("(Clone)", "");//名前を参照するためcloneの部分を消す
                Debug.Log(Name);
                OnMagic(Name);
                DCon.DestroyCard(this.gameObject);
                PCon.Mana -= MyCost;
                Debug.Log(Name);
                Debug.Log("唱えました!");
            }
        }
        else
        {
            Debug.Log("コストを払えませんでした");
        }
    }
    public void OnMagic(string Name)//名前によって処理を変える
    {
        Debug.Log("Magic!!");
        Debug.Log(Name);
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
