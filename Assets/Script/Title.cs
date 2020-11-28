using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip BGM_title;
    public AudioSource audio1;
    void Start()
    {
        audio1 = GetComponent<AudioSource>();
        audio1.volume = 0.5f;
        audio1.Stop();//今流れているのを止めてから流す
        audio1.PlayOneShot(BGM_title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
