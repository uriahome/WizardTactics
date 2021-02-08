﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonController : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Onclick(int SelectNum)
    {//押したボタンに対応する
        //Debug.Log(SelectNum + "を押しました");
        DebugLogger.Log(SelectNum + "を押しました");
        GManager.instance.SelectNextEnemy(SelectNum);
    }

    public void CheckDeck()
    {//デッキの中身を表示するボタンを押されたとき
        GManager.instance.DeleteMagicCheck = false;
        GManager.instance.CloneMagicCheck = true;
        GManager.instance.SelectShowDeck();
        GameObject MapButtonPanel = transform.parent.gameObject;
        MapSelectController MapCon = MapButtonPanel.GetComponent<MapSelectController>();
        MapCon.ShowDeck();
    }

    public void CheckDeckClone()
    {//デッキの中の魔法を1つ複製出来る準備をする
        GManager.instance.DeleteMagicCheck = true;
        GManager.instance.CloneMagicCheck = false;
        GManager.instance.SelectShowDeck();
        GameObject MapButtonPanel = transform.parent.gameObject;
        MapSelectController MapCon = MapButtonPanel.GetComponent<MapSelectController>();
        MapCon.ShowDeck();
    }

    public void EventBattleCheck()
    {
        GManager.instance.EventBattle = true;
    }
}
