using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Prefab;

    private Vector3 ClickPosition;//クリックした場所

    public float AddMana = 1.0f;//増えるマナの速度

    public float SpanTime = 2.0f;//1マナ増えるのに必要なポイン

    public float DeltaTime;//経過時間

    public int Mana;//マナコスト

    public int MaxMana;

    public Text CostText;//マナコスト表示用のテキスト

    public int Attack;

    // Start is called before the first frame update
    void Start()
    {
        DeltaTime = 0;
        Mana = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Summon();
        DeltaTime  += AddMana * Time.deltaTime;
        if(DeltaTime >= SpanTime && Mana != MaxMana)//一定時間貯まったらマナが1つ増える
        {
            DeltaTime = 0;
            Mana++;
        }

        CostText.text = "Cost:" + Mana + "/" + MaxMana.ToString();
        
    }
    /*public void Summon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickPosition = Input.mousePosition;
            ClickPosition.z = 10f;
            Instantiate(Prefab, Camera.main.ScreenToWorldPoint(ClickPosition), Prefab.transform.rotation);

        }
    }*/
}
