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

    //効果音用の設定
    public AudioClip sound;
    public AudioSource audio1;
    // Start is called before the first frame update
    public bool potion;
    void Start()
    {
        //StartCoroutine("Delete");//勝手に消えるように
        IsHit = false;
        rigid2d = GetComponent<Rigidbody2D>();
        //Rotate = false;
        RotateZ = 0;//生き返れ

        //音を鳴らす
        if(potion){
        audio1 = GetComponent<AudioSource>();
        audio1.PlayOneShot(sound);
        }
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
