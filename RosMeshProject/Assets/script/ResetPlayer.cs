using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour {
    private Vector3 setPosition;
    private Quaternion setRotation;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("PlayerCamera"))
        {
            GameObject camera = GameObject.Find("PlayerCamera");
            setPosition = camera.transform.localPosition;
            setRotation = camera.transform.rotation;
            //if (GameObject.Find("Main Camera"))
            //    GameObject.Find("Main Camera").SetActive(false);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            if (GameObject.Find("PlayerCamera"))
            {
                GameObject temp = GameObject.Find("PlayerCamera");
                temp.transform.position = setPosition;
                temp.transform.rotation = setRotation;
            }
            if (GameObject.FindGameObjectWithTag("bullet"))
            {
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("bullet");
                foreach(GameObject temp in gameObjects)
                {
                    GameObject.Destroy(temp);
                }
            }
        }
    }

}
