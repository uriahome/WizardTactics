using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewButton : MonoBehaviour
{
    public GameObject ButtonListCanvas;
    public GameObject DeckCancas;
    public GameObject TitleCanvas;
    public bool ViewFlag;

    /*public GameObject BookButton;
    public Image BookImage;
    public Sprite[] BookSprite;*/
    // Start is called before the first frame update
    void Start()
    {
        ViewFlag = false;

        /*BookButton = transform.GetChild(0).gameObject.
        BookImage = BookButton.GetComponent<Image>();*/

    }

    // Update is called once per frame
    void Update()
    {
        if(ViewFlag){
            ButtonListCanvas.SetActive(true);
            DeckCancas.SetActive(false);
            TitleCanvas.SetActive(false);
            //BookImage.image = BookSprite[0];
        }else{
            ButtonListCanvas.SetActive(false);
            DeckCancas.SetActive(true);
            TitleCanvas.SetActive(true);
            //BookImage.image = BookSprite[1];
        }
    }

    public void OnClick(){
        ViewFlag = !ViewFlag;
    }
}
