using System.Collections;
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
