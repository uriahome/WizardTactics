using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RewardController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> CardList = new List<GameObject>();//全カードリスト的な

    public List<GameObject> PairCardList = new List<GameObject>();//ペア魔法のみのリスト
    //public List<GameObject> WishList = new List<GameObject>();//勝利時に出てくる3枚
    public int RandomNum;
    public int CardListRange;//カードリストの長さ

    public GameObject RewardPanel;//キャンバスの下のこのオブジェクトにこのスクリプトをつける。

    public List<int> RandomNumList;// = new List<int>();

    public bool RewardCheck;//報酬を呼び出したかどうか判定する
    
    void Start()
    {
        //this.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Battle)//戦闘中ならば
        {
            this.gameObject.SetActive(false);//隠す
        }
    }

    public void SelectCard()
    {//報酬のカードを3枚表示する
        if(RewardCheck){
            DebugLogger.Log("お返ししますー");
            return;
        }else{
            RewardCheck = true;
        }
        foreach (Transform child in gameObject.transform)//子オブジェクト全削除
        {
            Destroy(child.gameObject);
        }
        Random.InitState(System.DateTime.Now.Millisecond);//現在の時間をシード値にする
        CardListRange = CardList.Count;//カードリストの長さを代入
        RandomNumList = new List<int>();//番号リストを作る
        for(int j =0;j<CardListRange;j++){
            RandomNumList.Add(j);//0空数字を割り振っていく
        }
        for (int i = 0; i < 3; i++)
        {
            RandomNum = Random.Range(0, RandomNumList.Count);//ランダムに1枚選択
            int SelectNum =RandomNumList[RandomNum];
            GameObject Summon = Instantiate(CardList[SelectNum]) as GameObject;//カードリストの対応した番号から出す
            Summon.transform.SetParent(RewardPanel.transform,false);//RewardPanelの子供にする
            RandomNumList.RemoveAt(RandomNum);//既に選ばれた番号を削除する
            DebugLogger.Log("選ばれたのは"+SelectNum);
        }
    }

    public void EventSelectCard(){//イベント戦勝利時のペアカードのみのリスト//番号の抽選などは報酬カードと同じ処理
        if(RewardCheck){
            DebugLogger.Log("お返ししますー");
            return;
        }else{
            RewardCheck = true;
        }
        //DebugLogger.Log("イベント戦勝利！");
        foreach(Transform child in gameObject.transform){
            Destroy(child.gameObject);
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        CardListRange = PairCardList.Count;
        RandomNumList = new List<int>();
        for(int j = 0; j < CardListRange; j++){
            RandomNumList.Add(j);
        }
        for(int i = 0; i < 3 ; i++){
            RandomNum = Random.Range(0,RandomNumList.Count);
            int SelectNum = RandomNumList[RandomNum];
            GameObject Summon = Instantiate(PairCardList[SelectNum]) as GameObject;
            Summon.transform.SetParent(RewardPanel.transform,false);
            RandomNumList.RemoveAt(RandomNum);
        }

    }

    public void Refresh(){
        DebugLogger.Log("Refresh");
        RewardCheck = false;
    }
}
