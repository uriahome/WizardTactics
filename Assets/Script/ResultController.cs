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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Finish()
    {
        DebugLogger.Log("Finish!!!");
        if (!isFinish)
        {
            isFinish = true;
            ShowDeck();
        }

        if (GManager.instance.WinNum != 7)//7回連続勝利以外なら勝利数を表示する
        {
            ResultText.text = (GManager.instance.WinNum) + "回勝利した！";
        }
        else
        {
            ResultText.text = "ゲームクリア!!!";
        }

    }

    public void ShowDeck()//デッキの中身を表示する
    {
        Deck = GameObject.Find("Deck");
        DCon = Deck.gameObject.GetComponent<DeckController>();
        ResultDeck = new List<GameObject>(DCon.DeckList);
        DebugLogger.Log(ResultDeck.Count);
        for (int j = 0; j < ResultDeck.Count; j++)//全部引く
        {
            DebugLogger.Log("負けてしまった");
            GameObject Summon = Instantiate(ResultDeck[j]) as GameObject;
            Summon.transform.SetParent(ResultPanel.transform, false);//falseにすることでローカル座標での位置サイズに対応してくれる
        }//1枚ずつデッキを表示していく
    }
}
