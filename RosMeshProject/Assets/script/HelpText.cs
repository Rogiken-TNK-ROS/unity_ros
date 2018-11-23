using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpText : MonoBehaviour {

    //文字を書く場所を指定
    public Rect zahyou = new Rect(0, 0, 200, 50);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //GUI更新イベントが有ると勝手に呼ばれる
    void OnGUI()
    {
        GUI.color = Color.black;
        if (RosSharp.RosBridgeClient.RosConnector.rosConect)
        {
            GUI.Label(zahyou, "Connected to RosBridge");   //文字を書く
        }
        else
        {
            GUI.Label(zahyou, "Disconnected from RosBridge");   //文字を書く
        }
        GUI.Label(new Rect(5, 20, 400,100), "方向キーorWASDキー : プレイヤー移動\n右クリック＋ドラッグ : カメラ回転\nマウスホイール : カメラ前後移動\nマウスホイール＋ドラッグ : カメラ上下左右移動\nzキー : 弾を発射\nrキー : 初期位置に戻る");   //文字を書く
    }
}
