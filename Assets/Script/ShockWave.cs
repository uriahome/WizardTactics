using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    public int Power;//攻撃力
    public bool IsHit;
    public float Speed;
    public Rigidbody2D rigid2d;
    public bool Rotate;
    public float RotateZ;
    public bool FreezeAttack;//氷攻撃かどうか
 
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Delete");//勝手に消えるように
        IsHit = false;
        rigid2d = GetComponent<Rigidbody2D>();
        //Rotate = false;
        RotateZ = 0;//生き返れ
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotate)
        {
            RotateZ+= Time.deltaTime*30;
            RotateZ %= 360;
            this.transform.Rotate(0, 0, RotateZ);
        }
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit");
        if(this.gameObject.tag == "PlayerAttack")
        {
            //Debug.Log("Hit2");
            //Debug.Log(collision.gameObject.tag);
            GameObject ParentM = collision.gameObject.transform.root.gameObject;
            if (ParentM.gameObject.tag == "EnemyMonster")
            {
                //Debug.Log("Hit3");
                this.GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject);
            }
        }
        if(this.gameObject.tag == "EnemyAttack")
        {
            GameObject ParentM = collision.gameObject.transform.root.gameObject;
            if (ParentM.gameObject.tag == "PlayerMonster")
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                Destroy(this.gameObject);
            }
        }
    }

    public void Move()
    {
        this.rigid2d.AddForce(transform.right * this.Speed);
        Rotate = true;
    }
}
