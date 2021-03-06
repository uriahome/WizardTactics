﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rigid2d;
    void Start()
    {
        //上に跳ねるイメージ
        rigid2d = GetComponent<Rigidbody2D>();
        rigid2d.AddForce(new Vector3(0,300,0));
        StartCoroutine("DestroyObject");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyObject()//0.5秒後に消える
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
