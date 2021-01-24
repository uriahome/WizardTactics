using System.Collections;
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
        BattleCount = GManager.instance.WinNum;
        GameObject Enemy = Instantiate(EnemyM[BattleCount]) as GameObject;
        Enemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    public void SelectEnemyStart(int num){
        BattleCount = GManager.instance.WinNum;
        GameObject Enemy = Instantiate(EnemyM[num]) as GameObject;
        Enemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
