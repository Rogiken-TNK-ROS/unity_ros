using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatChangeTest : MonoBehaviour {

    private byte[] test = BitConverter.GetBytes(9000f);

    // Use this for initialization
    void Start () {
        //test[0] = 3;
        //test[1] = 17;
        //test[2] = 7;
        //test[3] = 85;

        float z = (float)(0x0000000000001111 & test[0] + 0x0000000011110000 & test[1] + 0x0000111100000000 & test[2] + 0x1111000000000000 & test[3]);
        float bit = BitConverter.ToSingle(test, 0);

        for (int i = 0; i < 4; i++)
        {
            Debug.Log("[" + i + "]" + test[i]);
        }

        //Debug.Log("[z]" + z);
        string text2 = bit.ToString("F5"); // 小数点以下を0桁表示する指示
        //Debug.Log("[bit]"+bit);
        //Debug.Log("[bit]"+text2);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
