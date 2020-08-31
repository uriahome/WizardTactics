using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public bool Battle ;//戦闘中かどうか

    public GameObject DeckCon;//デッキコントローラーを参照できるように
    public DeckController DCon;

    public List<GameObject> CardList = new List<GameObject>();//全カードリスト的な
    public List<GameObject> WishList = new List<GameObject>();//勝利時に出てくる3枚

    public GameObject RewardCard;//実際に手に入れるカード
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

        DeckCon = GameObject.Find("Canvas/Deck");//探索
        DCon = DeckCon.GetComponent<DeckController>();//参照できるように
    }

    public void BattleReward(){
        DCon.DeckAdd(RewardCard);//デッキの中にRewardCardを追加する
    }
}