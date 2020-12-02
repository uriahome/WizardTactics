using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();//デッキリスト
    public int EnemyCount;
    public float span;
    public float delta = 0;
    public int BattleCount;
    // Start is called before the first frame update
    void Start()
    {
        BattleCount = GManager.instance.WinNum;
        delta = 0;
        EnemyCount = EnemyList.Count;//自分の召喚できる種類を数える
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.Battle)//戦闘中ならば
        {
            this.delta += Time.deltaTime;
            if (this.delta > this.span)
            {
                this.delta = 0;
                int Num = Random.Range(1, 100);
                int SummonNum = Num % EnemyCount;
                GameObject Enemy = Instantiate(EnemyList[SummonNum]) as GameObject;
                Enemy.transform.position = new Vector3(this.transform.position.x-1.0f, 0.1f, this.transform.position.z);//場所微調整
            }

            switch(BattleCount){
                case 0:case 1:
                this.transform.position = new Vector3(7.56f, 0.5f, 0.0f);//敵のマスターならこの座標にいる
                break;
                case 2:
                this.transform.position = new Vector3(7.56f, 1.0f, 0.0f);//敵のマスターならこの座標にいる
                break;
                case 6:
                this.transform.position = new Vector3(7.56f, 1.9f, 0.0f);//敵のマスターならこの座標にいる
                break;
                default:
                this.transform.position = new Vector3(7.56f, 1.0f, 0.0f);//敵のマスターならこの座標にいる
                break;

            }
        }
    }
}
