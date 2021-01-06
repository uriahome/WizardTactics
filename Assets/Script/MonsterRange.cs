using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LookTarget;//見つけたターゲット
    public bool DetectTarget;//ターゲットを見つけているかどうか

    public GameObject Parent;//これを付けてるオブジェクト
    public Monster ParentM;
    void Start()
    {
        DetectTarget = false;
        Parent = transform.root.gameObject;//親を取得
        ParentM = Parent.GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LookTarget == null)
        {
            DetectTarget = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ParentM.IsEnemy)//これが敵キャラなら
        {
            if(collision.gameObject.tag == "PlayerMonster")
            {
                DetectTarget = true;
                LookTarget = collision.gameObject;
            }
        }
        else
        {
            if (collision.gameObject.tag == "EnemyMonster")
            {
                DetectTarget = true;
                LookTarget = collision.gameObject;
            }
        }
    }
}
