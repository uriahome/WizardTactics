﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] EnemyM;
    public int BattleCount;//戦闘回数
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyStart()
    {//EnemyMasterを設置する
    //現状は勝利数＝配列の番号の順番に呼び出すしかない
    //今後、敵のボスの種類を増やして戦う敵キャラを分岐させたい
    //最初の戦闘はまだこちらを使用している
        BattleCount = GManager.instance.WinNum;
        GameObject Enemy = Instantiate(EnemyM[BattleCount]) as GameObject;
        Enemy.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
    }

    public void SelectEnemyStart(int num){//選択された敵と戦う
        //BattleCount = GManager.instance.WinNum;
        DebugLogger.Log("Select");
        GameObject Enemy = Instantiate(EnemyM[num]) as GameObject;
        switch(num){
            case 0:case 1:
                Enemy.transform.position = new Vector3(7.56f, 0.5f, 0.0f);//敵のマスターならこの座標にいる
                break;
            case 2:
                Enemy.transform.position = new Vector3(7.56f, 1.0f, 0.0f);//敵のマスターならこの座標にいる
                break;
            case 6:
                Enemy.transform.position = new Vector3(7.56f, 1.9f, 0.0f);//敵のマスターならこの座標にいる
                break;
            default:
                Enemy.transform.position = new Vector3(7.56f, 1.0f, 0.0f);//敵のマスターならこの座標にいる
                break;
        }
        //Enemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
