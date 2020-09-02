using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour
{
    public List<GameObject> DeckList = new List<GameObject>();//デッキリスト
    public List<GameObject> BattleDeckList = new List<GameObject>();//戦闘で使うデッキリスト
    public List<GameObject> NowDeckList = new List<GameObject>();//今の戦闘で使用しているデッキリスト


    public GameObject DeckCanvas;
    // Start is called before the first frame update
    void Start()
    {

        DeckSetting();//この戦闘で使用するデッキをセットする
        DeckShuffle();//デッキを混ぜる
        /* for(int i =0; i < 3; i++)
         {
             Button Draw = DeckDraw();
             Draw.transform.SetParent(DeckCanvas.transform);
         }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeckSetting()
    {
        BattleDeckList = new List<GameObject>(DeckList);//デッキリストの設定
    }

    public void DeckShuffle()
    {
        for (int i = 0; i < BattleDeckList.Count; i++)
        {
            GameObject temp = BattleDeckList[i];
            int RandomIndex = Random.Range(0, BattleDeckList.Count);
            BattleDeckList[i] = BattleDeckList[RandomIndex];
            BattleDeckList[RandomIndex] = temp;
        }
        NowDeckList = new List<GameObject>(BattleDeckList);//更新したデッキをセットする
    }

    public GameObject DeckDraw()//デッキから1枚引く
    {
        GameObject Draw;
        Draw = NowDeckList[NowDeckList.Count - 1];
        NowDeckList.RemoveAt(NowDeckList.Count - 1);
        Debug.Log(Draw + "を引きました");
        if (NowDeckList.Count == 0)
        {
            DeckShuffle();
        }
        return Draw;
    }

    public void DestroyCard(GameObject DButton)
    {//使用したカードは破棄されて新たに1枚引く
        Destroy(DButton);
        GameObject Draw = DeckDraw();
        GameObject Summon = Instantiate(Draw) as GameObject;
        Summon.transform.SetParent(DeckCanvas.transform);
    }

    public void DeckAdd(GameObject AddCard)
    {//デッキ内にカードを追加する
        DeckList.Add(AddCard);//デッキに追加する
    }
}
