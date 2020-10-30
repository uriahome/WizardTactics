using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MagicDeck;
    public GameObject MonsterDeck;
    public string SelectText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeShow(int num)
    {
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
