﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;//テキストの書き込みを行うために

public class DeckController : MonoBehaviour
{

    public List<GameObject> StructureList_monster = new List<GameObject>();//デッキリスト 
    public List<GameObject> StructureList_magic = new List<GameObject>();//デッキリスト 
    public List<GameObject> StructureList_Debug = new List<GameObject>();//デッキリスト(デバッグ用)
    public List<GameObject> DeckList = new List<GameObject>();//デッキリスト
    public List<GameObject> BattleDeckList = new List<GameObject>();//戦闘で使うデッキリスト
    public List<GameObject> NowDeckList = new List<GameObject>();//今の戦闘で使用しているデッキリスト


    public List<GameObject> HandList = new List<GameObject>();//現在の手札
    public List<string> HandNameList = new List<string>();//現在の手札の名前一覧


    public GameObject DeckCanvas;

    public GameObject MissButton;//戦闘中に追加するミスボタンを宣言
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("DeckNum", 100);//デバッグ用のデッキを選択(最終的にはコメントアウトする必要あり)
        DeckSelect();
        DeckPreparation();
        //SortHandst();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DeckSelect()//ゲーム開始時のデッキを設定する
    {
        int num = PlayerPrefs.GetInt("DeckNum");
        switch (num)
        {
            case 0:
                DeckList = new List<GameObject>(StructureList_monster);
                DebugLogger.Log("StructureList_monsterで戦います");
                break;
            case 1:
                DeckList = new List<GameObject>(StructureList_magic);
                DebugLogger.Log("StructureList_magicで戦います");
                break;
            default:
                DeckList = new List<GameObject>(StructureList_Debug);
                DebugLogger.Log("StructureList_Debugで戦います");
                break;
        }
        //DeckList = new List<GameObject>(StructureList[0]);
    }

    public void DeckSetting()
    {
        BattleDeckList = new List<GameObject>(DeckList);//デッキリストの設定
    }

    public void DeckShuffle()//デッキの中身をシャッフルする
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
        //Debug.Log(Draw + "を引きました");
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
        Summon.transform.SetParent(DeckCanvas.transform, false);//falseにすることでローカル座標での位置サイズに対応してくれる
        StartCoroutine("WaitTime");
        //SortHands();
    }

    public void DeckAdd(GameObject AddCard)
    {//デッキ内にカードを追加する
        DeckList.Add(AddCard);//デッキに追加する
    }

    public void DeckPreparation()
    {//デッキの全体的な準備
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        NowDeckList.Clear();//今のデッキの中身を空にする
        DeckSetting();//この戦闘で使用するデッキをセットする
        DeckShuffle();//デッキを混ぜる
        for (int j = 0; j < 5; j++)//5枚引く
        {
            GameObject Draw = DeckDraw();
            GameObject Summon = Instantiate(Draw) as GameObject;
            Summon.transform.SetParent(DeckCanvas.transform, false);//falseにすることでローカル座標での位置サイズに対応してくれる
        }
        StartCoroutine("WaitTime");//手札のソート
    }

    public void SortHands()
    {//手札の並び替え
        int i = 0;
        foreach (Transform child in DeckCanvas.transform)
        {//子オブジェクトをすべて取得
            HandList[i] = child.gameObject;
            HandNameList[i] = child.gameObject.name;
            i++;
        }
        HandNameList.Sort();//名前のリストをソート
        for (int j = 0; j < HandNameList.Count; j++)//名前リストを1つずつ取得
        {
            foreach (Transform child in DeckCanvas.transform)//子オブジェクト取得
            {
                if (child.gameObject.name.Equals(HandNameList[j])){//取得したオブジェクトと名前リストの名前が同じなら場所変更
                    child.transform.SetSiblingIndex(j);
                }
            }
        }
    }

    public IEnumerator WaitTime()
    {//0.1秒待機してからソートする
        yield return new WaitForSeconds(0.1f);
        SortHands();
    }

    public void DeckPreparation_Act()//GManagerから読み込むときにエラーが出たためこの方式、いつか調整したい
    {
        DeckPreparation();
    }

    public void BattleDeckAdvance(GameObject AddCard){//戦闘中のみのカードを追加する
        BattleDeckList.Add(AddCard);
    }

    public void AddMiss(){
        NowDeckList.Add(MissButton);//一度だけ使用可能なミスボタンを追加する
    }

    public void WriteText(){//テキストファイルにデッキの内容を書き込む
        string path = Application.dataPath +"/Log/LogText.txt";//保存先のパス
        int i = 0;
        List<string> DeckNameList = new List<string>();//現在のデッキの名前一覧
        DebugLogger.Log("LogTextに書き込みます");
        File.AppendAllText(path,"ここから\n");
        File.AppendAllText(path,"ここまで\n");
    }
}
