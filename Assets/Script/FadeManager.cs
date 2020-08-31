using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {
    //フェード用のCanvasとImage
    private static Canvas fadeCanvas;
    private static Image fadeImage;

    //フェード用imageの透明度
    private static float alpha = 0.0f;

    //フェードインフェードアウトのフラグ
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;

    //フェードしたい時間(秒)
    private static float fadeTime = 0.2f;

    //遷移先のシーン番号
    private static int nextScene = 1;

    static void Init()
    {
        //フェード用のCanvas生成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManager>();

        //最前面になるようにソートオーダーを設定する
        fadeCanvas.sortingOrder = 100;

        //fade用のImage生成
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;

        fadeImage.rectTransform.sizeDelta = new Vector2(1920, 1080);


    }


    public static void FadeIn()
    {
        if(fadeImage == null)
        {
            Init();
        }
        fadeImage.color = Color.black;
        isFadeIn = true;
    }

    public static void FadeOut(int n)
    {
        if (fadeImage == null)
        {
            Init();
        }
        nextScene = n;
        fadeImage.color = Color.clear;
        fadeCanvas.enabled = true;
        isFadeOut = true;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isFadeIn)
        {
            alpha -= Time.deltaTime / fadeTime;

            if(alpha <= 0.0f)
            {//終了
                isFadeIn = false;
                alpha = 0.0f;
                fadeCanvas.enabled = false;
            }

            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }else if (isFadeOut)
        {
            alpha += Time.deltaTime / fadeTime;

            if (alpha >= 1.0f){
                isFadeOut = false;
                alpha = 1.0f;

                SceneManager.LoadScene(nextScene);
            }

            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
	}
}
