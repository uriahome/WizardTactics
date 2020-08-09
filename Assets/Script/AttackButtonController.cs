using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonController : MonoBehaviour
{
    public float AlphaColor;
    public bool UseAttack;//使用したかどうか
    public float Span;//再使用までの時間
    public Button AButton;
    public ColorBlock ButtonColors;

    public GameObject Player;
    public PlayerController PCon;
    public GameObject[] PlayerFire;

   
    // Start is called before the first frame update
    void Start()
    {
        UseAttack = false;
        AlphaColor = 1;
        AButton = GetComponent<Button>();
        ButtonColors = AButton.colors;
        Player = GameObject.Find("PlayerMaster");
        PCon = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UseAttack && UseAttack)
        {
            ButtonColors.disabledColor = new Color(255f, 255f, 255f, AlphaColor);
            AlphaColor += Time.deltaTime * Span;
            if(AlphaColor >= 1)
            {
                AlphaColor = 255f;
                UseAttack = false;
                AButton.interactable = true;
            }
            //Debug.Log(AlphaColor);
            AButton.colors = ButtonColors;

        }
    }

    public void OnClick() //ボタンが押されたとき
    {
        UseAttack = true;
        AlphaColor = 0;
        AButton.interactable = false;//使用不可に

        /*GameObject Fire = Instantiate(PlayerFire) as GameObject;//炎の生成
        Fire.gameObject.tag = "PlayerAttack";
        Fire.transform.position = Player.transform.position;//プレイヤーの場所に出す
        ShockWave SObj = Fire.GetComponent<ShockWave>();
        SObj.Power = PCon.Attack;//攻撃力の代入
        SObj.Move();*/
        ThrowPotion(0);
    }

    public void ThrowPotion(int Num)
    {
        GameObject Fire = Instantiate(PlayerFire[Num]) as GameObject;//炎の生成
        Fire.gameObject.tag = "PlayerAttack";
        Fire.transform.position = new Vector3(Player.transform.position.x,0, Player.transform.position.z);//プレイヤーの場所に出す
        ShockWave SObj = Fire.GetComponent<ShockWave>();
        SObj.Power = PCon.Attack;//攻撃力の代入
        SObj.Move();
    }

    public void ThreePotion(int Num)
    {
        StartCoroutine("ThreeThrow",Num);
    }
    public IEnumerator ThreeThrow(int Num)
    {
        int Count = 0;
        float interval = 0.1f;
        while (true)
        {
            //Debug.Log("nyaa");
            Count++;
            if (Count > 3)
            {
                Debug.Log("oaaa");
                yield break;
            }
           ThrowPotion(Num);
            yield return new WaitForSeconds(interval);
        }
    }
}
