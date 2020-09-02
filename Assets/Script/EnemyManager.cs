using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject EnemyM;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyStart()
    {//EnemyMasterを設置する
        GameObject Enemy = Instantiate(EnemyM) as GameObject;
        Enemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
