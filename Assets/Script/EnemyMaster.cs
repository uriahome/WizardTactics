using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();//デッキリスト

    public float span;
    public float delta = 0;
    // Start is called before the first frame update
    void Start()
    {
        delta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            this.delta = 0;
            int Num = Random.Range(1,100);
            int SummonNum = Num % 3;
            GameObject Enemy = Instantiate(EnemyList[SummonNum]) as GameObject;
            Enemy.transform.position = new Vector3(this.transform.position.x,0.1f,this.transform.position.z);
        }
    }
}
