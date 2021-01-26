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
}
