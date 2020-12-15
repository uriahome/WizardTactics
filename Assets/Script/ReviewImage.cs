using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReviewImage : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject BookButton;
    public Image BookImage;
    public Sprite[] BookSprite;
    public bool ViewFlag;
    void Start()
    {
        BookImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(){
        ViewFlag = !ViewFlag;
        if(ViewFlag){
            BookImage.sprite = BookSprite[1];
        }else{
            BookImage.sprite = BookSprite[0];
        }
    }
}
