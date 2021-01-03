using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public int SelectDeckNum;//ゲーム開始時に選択されたデッキの番号

    //public AudioClip BGM_title;//戦闘シーンのBGMの管理もここで行う
    public AudioClip sound;
    public AudioSource audio;

    public GameObject FadeObject;
    public Image FadeImage;//フェードアウト用のイメージ
    public bool IsFadeOut;
    public float red,green,blue,alpha;//画像の赤、緑、青、透明度
    public float FadeSpeed;
    public string CurrentSceneName;//現在のシーンの名前
    void Start()
    {
        audio = GetComponent<AudioSource>();

        CurrentSceneName = SceneManager.GetActiveScene().name;
        if(CurrentSceneName =="Title"){
            FadeObject = GameObject.Find("DeckViewCanvas/FadeImage");
        }else{
            FadeObject = GameObject.Find("DeckCanvas/FadeImage");
        }
        FadeImage = FadeObject.GetComponent<Image>();
        red = FadeImage.color.r;
        green = FadeImage.color.g;
        blue = FadeImage.color.b;
        FadeImage.color = new Color(red,green,blue,0);
        alpha = FadeImage.color.a;
        FadeImage.enabled = false;

        /*audio1 = GetComponent<AudioSource>();
        audio1.volume = 0.5f;
        audio1.Stop();//今流れているのを止めてから流す
        audio1.PlayOneShot(BGM_title);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFadeOut){
            StartFadeOut();
        }
    }

    void StartFadeOut(){
        FadeImage.enabled = true;
        alpha += FadeSpeed;
        FadeImage.color = new Color(red,green,blue,alpha);
        if(alpha >= 1){
            IsFadeOut = false;
        }
    }

    /*public void ChangeFade(){
        IsFadeOut = true;
    }*/

    public void ChangeScene()
    {
        IsFadeOut = true;
        audio.PlayOneShot(sound);//効果音を再生する
        // SceneManager.LoadScene("BattleSceme",LoadSceneMode,Single);//メインの戦闘シーンを読み込む
        Debug.Log("SceneLoad");//シーンの読み込み(非同期)

        PlayerPrefs.SetInt("DeckNum", SelectDeckNum);//デッキを選択してそれをPlayerPrefsに保存
        PlayerPrefs.Save();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BattleScene");
    }

    public void ChangeTitle()
    {//タイトルに戻ってくるとき
        IsFadeOut = true;
        audio.PlayOneShot(sound);//効果音を再生する
        Debug.Log("SceneLoad");//シーンの読み込み(非同期)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Title");
    }

    public void SelectDeck(int num)
    {//タイトル画面でデッキを選択するときに選択されているデッキの番号を保存する
        SelectDeckNum = num;
    }

}
