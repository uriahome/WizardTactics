using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> CardList = new List<GameObject>();//全カードリスト的な
    //public List<GameObject> WishList = new List<GameObject>();//勝利時に出てくる3枚
    public int RandomNum;
    public int CardListRange;//カードリストの長さ

    public GameObject RewardPanel;//キャンバスの下のこのオブジェクトにこのスクリプトをつける。
    void Start()
    {
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Battle)//戦闘中ならば
        {
            this.gameObject.SetActive(false);
        }
        /*else
        {
            this.gameObject.SetActive(true);
        }*/
    }

    public void SelectCard()
    {//報酬のカードを3枚表示する
        foreach (Transform child in gameObject.transform)//子オブジェクト全削除
        {
            Destroy(child.gameObject);
        }
        Random.InitState(System.DateTime.Now.Millisecond);//現在の時間をシード値にする
        //CardListRange = 4;//CardList.Length;//カードリストの長さを代入//とりあえず5にするしかない
        CardListRange = CardList.Count;
        for (int i = 0; i < 3; i++)
        {
            RandomNum = Random.Range(0, CardListRange);//ランダムに1枚選択
            GameObject Summon = Instantiate(CardList[RandomNum]) as GameObject;//カードリストの対応した番号から出す
            Summon.transform.SetParent(RewardPanel.transform,false);//RewardPanelの子供にする
        }
    }
}
