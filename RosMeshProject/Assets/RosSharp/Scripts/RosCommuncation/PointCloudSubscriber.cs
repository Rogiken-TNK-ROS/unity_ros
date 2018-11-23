using System;
using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient.Messages.Sensor;
using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class PointCloudSubscriber : Subscriber<Messages.Sensor.PointCloud2>
    {
        private byte[] byteArray;
        private bool isMessageReceived = false;
        private int size;
        private Mesh mesh;

        public Material material;
        public bool pclCollision;

        public GameObject floorObject;
        public GameObject player;
        static public Vector3 floor_posi;

        private Vector3[] pcl;

        int width;
        int height;
        int row_step;
        int point_step;

        int num = 0;

        protected override void Start()
        {
            base.Start();

        }

        public void Update()
        {
            
            if (isMessageReceived)
            {
                //Mesh生成
                CreateMesh();
                    
                //生成したMeshを点群に変換
                MeshFilter meshFilter = GetComponent<MeshFilter>();
                meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0), MeshTopology.Points, 0);
                if (pclCollision)
                {
                    MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
                    if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();
                    meshCollider.sharedMesh = mesh;
                }
                isMessageReceived = false;
            }

            
        }

        protected override void ReceiveMessage(PointCloud2 message)
        {
            //throw new NotImplementedException();
            size = message.data.GetLength(0);
            int i=0;
            //Debug.Log("first"+ size);
            byteArray = new byte[size];
            foreach (byte temp in message.data)
            {
                byteArray[i] = temp;
                i++;
            }

            width = message.width;
            height = message.height;
            row_step = message.row_step;
            point_step = message.point_step;

            size = size/point_step;

            isMessageReceived = true;
         }

        void CreateMesh()
        {
            //Debug.Log("message");
            int amari = size % 3;
            size = size - amari;
            //Debug.Log("CreateMesh" + pcl.GetLength(0));
            mesh = new Mesh();
            //Vector3[] points = pcl;
            int[] indecies = new int[size];
            Color[] colors = new Color[size];
            //Vector3[] points = new Vector3[size];
            /*
            for (int i = 0; i < size; ++i)
            {
                //points[i] = new Vector3(byteArray[3*i], byteArray[3 * i + 1], byteArray[3 * i + 2]);
                indecies[i] = i;
                //colors[i] = new Color(i/size, i / size, i / size, 1.0f);
                colors[i] = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                Debug.Log("i" + i);
                Debug.Log("x" + pcl[i].x);
                Debug.Log("y" + pcl[i].y);
                Debug.Log("z" + pcl[i].z);
            }*/

            /**/

            pcl = new Vector3[size];
            float min_x = 0.0f;
            float min_y = 0.0f;
            float min_z = 0.0f;
            float max_x = 0.0f;
            float max_y = 0.0f;
            float max_z = 0.0f;

            for (int n = 0; n < size; n++)
            {
                int x_posi = n * point_step + 0;
                int y_posi = n * point_step + 4;
                int z_posi = n * point_step + 8;

                float x = BitConverter.ToSingle(byteArray, x_posi);
                float y = BitConverter.ToSingle(byteArray, y_posi);
                float z = BitConverter.ToSingle(byteArray, z_posi);

                //Debug.Log(x);
                //Debug.Log(y);
                //Debug.Log(z);

                min_x = ComFloat(x, min_x, "min");
                min_y = ComFloat(y, min_y, "min");
                min_z = ComFloat(z, min_z, "min");
                max_x = ComFloat(x, max_x, "max");
                max_y = ComFloat(y, max_y, "max");
                max_z = ComFloat(z, max_z, "max");

                //pcl[n] = new Vector3(1.5f*x, 1.5f*z, 1.5f*y);
                pcl[n] = new Vector3(y, z, x);
                indecies[n] = n;
                //colors[n] = new Color(1.0f/size, 1.0f/size, 1.0f/size, 1.0f);
                //Instantiate(floorObject, pcl[n], Quaternion.identity);// as GameObject;
            }

            Debug.Log("pcl_Finished" + pcl[0]);
            //床生成
            if (!GameObject.Find("Floor(Clone)"))
            {
                //floor_posi = new Vector3(max_x - min_x, min_z - 2.0f, max_y - min_y);
                Instantiate(player, new Vector3(0.0f, 0.8f, 0.0f), Quaternion.identity);
                GameObject obj = Instantiate(floorObject, new Vector3((max_y - min_y) / 2.0f + min_y, min_z - 2.5f, ((max_x - min_x)/2.0f)), Quaternion.identity) as GameObject;
                obj.transform.localScale = new Vector3(1.2f*(max_y - min_y), 5.0f, 1.2f*(max_x - min_x));
            }
            else
            {
                GameObject floor = GameObject.Find("Floor(Clone)");
                floor.transform.localPosition = new Vector3((max_y - min_y) / 2.0f + min_y, min_z - 2.5f, ((max_x - min_x) / 2.0f));
                floor.transform.localScale = new Vector3(1.2f * (max_y - min_y), 5.0f, 1.2f * (max_x - min_x));
            }


            mesh.vertices = pcl;
            mesh.triangles = indecies;

            mesh.RecalculateNormals();

            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();

            //if (pclCollision)
            //{
            //    MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
            //    if (!meshCollider) meshCollider = gameObject.AddComponent<MeshCollider>();
            //    meshCollider.sharedMesh = mesh;
            //}

            meshFilter.mesh = mesh;

            /*色付けテスト*/
            //meshRenderer.sharedMaterial.mainTexture = CreateTexture(vertices);

            meshRenderer.sharedMaterial = material;
            
           // meshCollider.sharedMaterial = physicMaterial;

            /*
            mesh.vertices = pcl;
            mesh.colors = colors;
            mesh.SetIndices(indecies, MeshTopology.Points, 0);
            */
        }

        float ComFloat(float x, float last_x, string type)
        {
            if (type == "min")
            {
                if (x < last_x)
                {
                    return x;
                }
                else
                {
                    return last_x;
                }
            }else if(type == "max")
            {
                if (x > last_x)
                {
                    return x;
                }
                else
                {
                    return last_x;
                }
            }
            else
            {
                return 0.0f;
            }
        }
    }
}

