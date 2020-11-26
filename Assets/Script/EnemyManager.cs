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
        BattleCount = GManager.instance.WinNum / 2;
        GameObject Enemy = Instantiate(EnemyM[BattleCount]) as GameObject;
        Enemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
