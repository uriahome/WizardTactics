using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MagicDeck;
    public GameObject MonsterDeck;
    public string SelectText;

    public AudioClip sound;
    public AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeShow(int num)//タイトル画面でのデッキ表示を切り替える
    {
        audio.PlayOneShot(sound);//効果音を再生(カチッ)
        switch (num)
        {
            case 0:
                MagicDeck.SetActive(false);
                MonsterDeck.SetActive(true);
                break;
            case 1:
                MonsterDeck.SetActive(false);
                MagicDeck.SetActive(true);
                break;

        }
    }
}
