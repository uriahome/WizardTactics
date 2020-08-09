using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public float MaxHp;
    public float CurrentHp;
    public float HpScale;
    public GameObject Body;
    public Monster BodyMonster;

    // Start is called before the first frame update
    void Start()
    {
        Body = transform.root.gameObject;//親を取得
        BodyMonster = Body.GetComponent<Monster>();
        MaxHp = BodyMonster.Hp;//HPの最大値を取得
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHp = BodyMonster.Hp;
        HpScale = CurrentHp / MaxHp;
        transform.localScale = new Vector3(HpScale, 1, 1);
    }
}
