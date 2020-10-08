using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeScene(){
       // SceneManager.LoadScene("BattleSceme",LoadSceneMode,Single);//メインの戦闘シーンを読み込む
       Debug.Log("SceneLoad");//シーンの読み込み(非同期)
       AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BattleScene");
    }

}
