using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    public List<GameObject> ResultDeck = new List<GameObject>();//最終デッキリスト

    public DeckController DCon;
    public GameObject Deck;

    public GameObject ResultPanel;

    public GameObject TextObj;
    public Text ResultText;

    public bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        Deck = GameObject.Find("Deck");
        DCon = Deck.gameObject.GetComponent<DeckController>();
        isFinish = false;
        //ResultDeck = DCon.DeckList;//デッキリストの中身をコピーする

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Finish()
    {
        /*Deck = GameObject.Find("Deck");
        DCon = Deck.gameObject.GetComponent<DeckController>();
        ResultDeck = new List<GameObject>(DCon.DeckList);
        Debug.Log(ResultDeck.Count);
        for (int j = 0; j < ResultDeck.Count; j++)//全部引く
        {
            Debug.Log("負けてしまった");
            GameObject Summon = Instantiate(ResultDeck[j]) as GameObject;
            Summon.transform.SetParent(ResultPanel.transform, false);//falseにすることでローカル座標での位置サイズに対応してくれる
        }//1枚ずつデッキを表示していく*/
        Debug.Log("Finish!!!");
        if (!isFinish)
        {
            isFinish = true;
            ShowDeck();
        }

        ResultText.text = (GManager.instance.WinNum/2) + "回勝利した！";
        //WinNumが常に2回カウントされているので半分の数が正しいUpdateとヒット時でDeath判定を取っているのがおそらくダメ

    }

    public void ShowDeck()
    {
        Deck = GameObject.Find("Deck");
        DCon = Deck.gameObject.GetComponent<DeckController>();
        ResultDeck = new List<GameObject>(DCon.DeckList);
        Debug.Log(ResultDeck.Count);
        for (int j = 0; j < ResultDeck.Count; j++)//全部引く
        {
            Debug.Log("負けてしまった");
            GameObject Summon = Instantiate(ResultDeck[j]) as GameObject;
            Summon.transform.SetParent(ResultPanel.transform, false);//falseにすることでローカル座標での位置サイズに対応してくれる
        }//1枚ずつデッキを表示していく
    }
}
