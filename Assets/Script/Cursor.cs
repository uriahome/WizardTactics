using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 MousePos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//マウスの座標を獲得
      MousePos.z = 0;
      this.transform.position = MousePos;
    }
}
