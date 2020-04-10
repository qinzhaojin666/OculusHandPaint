using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hp
{
    /// <summary>
    /// Save、Load機能の実装
    /// </summary>
    public class HpJsonDataManager
    {
        /// <summary>
        /// パスを取得 & セーブファイル名記録
        /// </summary>
        private static string getFilePath () { return Application.persistentDataPath + "/PaintData" + ".json"; } 
        
        
        /// <summary>
        /// 書き込み機能
        /// </summary>
        /// <param name="hpPaintDataWrapper">シリアライズするデータ</param>
        public static void Save(HpPaintDataWrapper hpPaintDataWrapper)
        {
            //シリアライズ実行
            string jsonSerializedData = JsonUtility.ToJson(hpPaintDataWrapper);
            Debug.Log(jsonSerializedData);
            
            //実際にファイル作って書き込む
            using (var sw = new StreamWriter (getFilePath(), false)) 
            {
                try
                {
                    //ファイルに書き込む
                    sw.Write (jsonSerializedData);
                }
                //  失敗した時の処理
                catch (Exception e) 
                {
                    Debug.Log (e);
                }
            }
        }

        /// <summary>
        /// 読み込み機能
        /// </summary>
        /// <returns>デシリアライズした構造体</returns>
        public static HpPaintDataWrapper Load()
        {
            HpPaintDataWrapper jsonDeserializedData = new HpPaintDataWrapper();
            
            try 
            {
                //ファイルを読み込む
                using (FileStream fs = new FileStream (getFilePath(), FileMode.Open))
                using (StreamReader sr = new StreamReader (fs)) 
                {
                    string result = sr.ReadToEnd ();
                    Debug.Log(result);
                    
                    //読み込んだJsonを文字列化
                    jsonDeserializedData = JsonUtility.FromJson<HpPaintDataWrapper>(result);
                }
            }
            catch (Exception e) //失敗した時の処理
            {
                Debug.Log (e);
            }

            //デシリアライズした構造体を返す
            return jsonDeserializedData;
        }
    }
}
