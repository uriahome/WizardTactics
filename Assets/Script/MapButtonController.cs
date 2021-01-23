using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtonController : MonoBehaviour
{
    // Start is called before the first frame update
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
    }
}
