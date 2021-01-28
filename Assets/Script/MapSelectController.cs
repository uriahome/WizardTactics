using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> MapList = new List<GameObject>();//全マップリスト的な
    public int RandomNum;
    public int MapListRange;//カードリストの長さ

    public GameObject MapPanel;//キャンバスの下のこのオブジェクトにこのスクリプトをつける。

    public List<int> RandomNumList;// = new List<int>();

    
    public List<GameObject> ResultDeck = new List<GameObject>();//最終デッキリスト
    public DeckController DCon;
    public GameObject Deck;

    public GameObject ShowDeckPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SelectMap()//SelectCardのようにマップを3種類表示する
    {//報酬のカードを3枚表示する
        foreach (Transform child in gameObject.transform)//子オブジェクト全削除
        {
            Destroy(child.gameObject);
        }
        Random.InitState(System.DateTime.Now.Millisecond);//現在の時間をシード値にする
        MapListRange = MapList.Count;
        RandomNumList = new List<int>();
        for(int j =0;j<MapListRange;j++){
            RandomNumList.Add(j);
        }
        for (int i = 0; i < 3; i++)
        {
            RandomNum = Random.Range(0, RandomNumList.Count);//ランダムに1枚選択
            int SelectNum =RandomNumList[RandomNum];
            GameObject Summon = Instantiate(MapList[SelectNum]) as GameObject;//カードリストの対応した番号から出す
            Summon.transform.SetParent(MapPanel.transform,false);//RewardPanelの子供にする
            RandomNumList.RemoveAt(RandomNum);
            DebugLogger.Log("選ばれたのは"+SelectNum);
        }
    }

    public void ShowDeck()//デッキの中身を表示する
    {
        foreach (Transform child in gameObject.transform)//子オブジェクト全削除
        {
            Destroy(child.gameObject);
        }
        Deck = GameObject.Find("Deck");
        DCon = Deck.gameObject.GetComponent<DeckController>();
        ResultDeck = new List<GameObject>(DCon.DeckList);
        DebugLogger.Log(ResultDeck.Count);
        for (int j = 0; j < ResultDeck.Count; j++)//全部引く
        {
            GameObject Summon = Instantiate(ResultDeck[j]) as GameObject;
            Summon.transform.SetParent(ShowDeckPanel.transform, false);//falseにすることでローカル座標での位置サイズに対応してくれる
        }//1枚ずつデッキを表示していく

        this.gameObject.SetActive(false);//自分を非アクティブにする
    }
}
