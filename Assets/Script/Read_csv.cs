using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Read_csv : MonoBehaviour
{
    // Start is called before the first frame update
    //public Monster Knight;
    public string Name;
    public float Hp;
    public int Attack;
    public float Speed;
    public float MaxSpeed;
    public float AttackSpan;

    public int MonsterNumber;

    public TextAsset csvFile;//元データ
    List<string[]> csvData = new List<string[]>();//読み込んだ全てのデータ
    public List<string> Parameters =  new List<string>();//読み込んだ1キャラのデータ
    void Start()
    {
        csvFile = Resources.Load("Data/Data") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek() != -1){
            string line = reader.ReadLine();
            csvData.Add(line.Split(','));
            //Debug.Log(line);
        }


        //Debug.Log(csvFile.text);
        //Debug.Log(csvData.Count);
        //Debug.Log(csvData[0][0]);//Name
        //Debug.Log(csvData[0][1]);//Hp
        //Debug.Log(csvData[1]);
        Parameters = MonsterStatus(MonsterNumber);//1番のデータのキャラステータスを読み込む
        SetStatus();//ステータスをセットする
        //MonsterStatus(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string>  MonsterStatus(int num){//番号に対応したモンスターのステータスを1キャラ分まとめて返してくれる関数
        List<string> MonsterData = new List<string>();
        for(int y=0;y<csvData[num].Length;y++){
            Debug.Log(csvData[0][y]+"="+csvData[num][y]);
            MonsterData.Add(csvData[num][y]);
        }

        return MonsterData;
    }

    public void SetStatus(){//ステータスをセットする
        Name = Parameters[0];
        Hp =  float.Parse(Parameters[1]);
        Attack = int.Parse(Parameters[2]);
        Speed = float.Parse(Parameters[3]);
        MaxSpeed = float.Parse(Parameters[4]);
        AttackSpan = float.Parse(Parameters[5]);
    }
}
