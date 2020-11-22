using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIをいじるので宣言
public class TitleImage : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle MyToggle;//自分のトグル
    public GameObject Child;
    public Image MyImage;

    void Start()
    {
        MyToggle = GetComponent<Toggle>();
        MyImage = Child.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MyToggle.isOn){
            Child.transform.localScale = new Vector3(4,4,1);
            MyImage.color = new Color(1.0f,1.0f,1.0f,1.0f);

        }else{//選ばれていない間は小さく薄くして表示しておく
            Child.transform.localScale = new Vector3(3,3,1);
            MyImage.color = new Color(1.0f,1.0f,1.0f,0.6f);
        }
    }
}
