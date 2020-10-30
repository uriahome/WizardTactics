﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public int SelectDeckNum;//ゲーム開始時に選択されたデッキの番号
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeScene()
    {
        // SceneManager.LoadScene("BattleSceme",LoadSceneMode,Single);//メインの戦闘シーンを読み込む
        Debug.Log("SceneLoad");//シーンの読み込み(非同期)

        PlayerPrefs.SetInt("DeckNum", SelectDeckNum);//デッキを選択
        PlayerPrefs.Save();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BattleScene");
    }

    public void ChangeTitle()
    {//タイトルに戻ってくるとき
        Debug.Log("SceneLoad");//シーンの読み込み(非同期)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Title");
    }

    public void SelectDeck(int num)
    {//タイトル画面でデッキを選択するときに選択されているデッキの番号を保存する
        SelectDeckNum = num;
    }

}
