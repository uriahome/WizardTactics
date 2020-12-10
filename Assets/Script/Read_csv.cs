using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Read_csv : MonoBehaviour
{
    // Start is called before the first frame update
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


        Debug.Log(csvFile.text);
        Debug.Log(csvData.Count);
        Debug.Log(csvData[0][0]);//Name
        Debug.Log(csvData[0][1]);//Hp
        //Debug.Log(csvData[1]);

        /*for(int x = 0; x < csvData.Count;x++){
            //string CharacterData ="";
            for(int y = 0; y <csvData[x].Length;y++){
                //CharacterData+=csvData[x][y];
                //Debug.Log(csvData[x][y]);
            }
            //Debug.Log(CharacterData);
        }*/
        Parameters = MonsterStatus(1);
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
}
