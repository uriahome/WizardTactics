using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    //建物系のカードにつける
    //体力とかはMonsterの方もつけるのでそっちにまかせる
    public GameObject ShotObject;//発射するオブジェクト
    public float span;
    public float delta;

    public GameObject Player;
    public PlayerController PCon;
    public bool MakeFire;//モンスター以外を召喚するかどうか(ポーション系の場合はtrue)
    // Start is called before the first frame update
    void Start()
    {
        delta = 0;
        Player = GameObject.Find("PlayerMaster");
        PCon = Player.GetComponent<PlayerController>();
        this.transform.position = new Vector3(-5.70f, this.transform.position.y, this.transform.position.z);//ちょっと前に出す
    }

    // Update is called once per frame
    void Update()
    {
        if (span < delta)
        {
            delta = 0;
            MakeObj();//オブジェクトを作る
        }
        else
        {
            this.delta += Time.deltaTime;//経過時間の加算(いつものやつ)
        }
    }

    void MakeObj()
    {//ポーションとかを出す
        GameObject Fire = Instantiate(ShotObject) as GameObject;//炎の生成
        Fire.gameObject.tag = "PlayerAttack";
        //Fire.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);//自分の場所に出す
        Fire.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);//自分の場所に出す
        if (MakeFire)//ポーションとかの場合は攻撃力を代入
        {
            ShockWave SObj = Fire.GetComponent<ShockWave>();
            SObj.Power = PCon.Attack;//攻撃力の代入
            SObj.Move();
        }
    }
}
