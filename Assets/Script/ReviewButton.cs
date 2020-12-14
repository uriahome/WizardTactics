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
    // Start is called before the first frame update
    void Start()
    {
        ViewFlag = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(ViewFlag){
            ButtonListCanvas.SetActive(true);
            DeckCancas.SetActive(false);
            TitleCanvas.SetActive(false);
        }else{
            ButtonListCanvas.SetActive(false);
            DeckCancas.SetActive(true);
            TitleCanvas.SetActive(true);
        }
    }

    public void OnClick(){
        ViewFlag = !ViewFlag;
    }
}
