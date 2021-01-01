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
    public AudioClip sound;
    public AudioSource audio;
    void Start()
    {
        BookImage = GetComponent<Image>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(){
        audio.PlayOneShot(sound);//効果音を再生(カチッ)
        ViewFlag = !ViewFlag;
        if(ViewFlag){
            BookImage.sprite = BookSprite[1];
        }else{
            BookImage.sprite = BookSprite[0];
        }
    }
}
