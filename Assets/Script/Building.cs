﻿using System.Collections;
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
    public Monster BuildMon;//耐久値を削れるように自分のMonsterにアクセスできるようにする
    // Start is called before the first frame update
    void Start()
    {
        BuildMon = this.GetComponent<Monster>();
        delta = 0;
        Player = GameObject.Find("PlayerMaster");
        PCon = Player.GetComponent<PlayerController>();
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
        BuildMon.Hp -= Time.deltaTime*10;//少しずつ耐久を削る
    }

    void MakeObj()
    {//ポーションとかを出す
        GameObject Fire = Instantiate(ShotObject) as GameObject;//炎の生成
        //Fire.gameObject.tag = "PlayerAttack";
        Fire.transform.position = new Vector3(this.transform.position.x, 0.1f, this.transform.position.z);//自分の場所に出す
        if (MakeFire)//ポーションとかの場合は攻撃力を代入
        {
            Fire.gameObject.tag = "PlayerAttack";//ポーションなどを作るときのみtagを変更する
            ShockWave SObj = Fire.GetComponent<ShockWave>();
            SObj.Power = PCon.Attack;//攻撃力の代入
            SObj.Move();
        }
    }
}
