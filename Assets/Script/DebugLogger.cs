using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
//https://qiita.com/toRisouP/items/d856d65dcc44916c487d
//上のコードを参考にUnityEditor上でのみDebug.Log()を実行する関数を作成した
public class DebugLogger{
    [Conditional("UNITY_EDITOR")]
    public static void Log(object o){
        UnityEngine.Debug.Log(o);
    }
}
