using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();//デッキリスト
    public int EnemyCount;
    public float span;
    public float delta = 0;
    // Start is called before the first frame update
    void Start()
    {
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
        }
    }
}
