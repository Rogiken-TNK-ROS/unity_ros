using System;
using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient.Messages.Sensor;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class PointCloudSubscriber : Subscriber<Messages.Sensor.PointCloud2>
    {
        private byte[] byteArray;
        private bool isMessageReceived = false;
        private int size;
        private Mesh mesh;

        public Transform test_point;

        private Vector3[] pcl;

        protected override void Start()
        {
            base.Start();

        }

        public void Update()
        {
            
            if (isMessageReceived)
            {
                CreateMesh();
                test_point.position = new Vector3(pcl[0].x, pcl[1].y, pcl[2].z);
                isMessageReceived = false;
            }
        }

        protected override void ReceiveMessage(PointCloud2 message)
        {
            //throw new NotImplementedException();
            //size = message.data.GetLength(0);
            //int i=0;

            /*
            foreach (byte temp in message.data)
            {
                byteArray[i] = temp;
                i++;
            }
            Debug.Log(size);
            isMessageReceived = true;*/
            //byte width = (byte)message.width;
            //byte height = (byte)message.height;
            //byte row_step = (byte)message.row_step;
            //byte point_step = (byte)message.point_step;

            int width = message.width;
            int height = message.height;
            int row_step = message.row_step;
            int point_step = message.point_step;

            Debug.Log("width" + message.width);
            Debug.Log("height" + message.height);
            Debug.Log("row_step" + message.row_step);
            Debug.Log("point_step" + message.point_step);
            
            size = width;
            Debug.Log(size);

            pcl = new Vector3[size];

            for(int n = 0; n < size; n++)
            {
                //float x = float32(0x0001 *[0]);
                pcl[n] = new Vector3(message.data[n * message.point_step + message.fields[0].offset], message.data[n * message.point_step + message.fields[1].offset], message.data[n * message.point_step + message.fields[2].offset]);
            }

            //0x0001*[0] 0x0010*

            //for (byte column = 0; column < width; column++)
            //{
            //    for(byte row = 0; row < height; row++)
            //    {
            //        int arrayPosition = column * point_step + row * row_step;

            //        //Debug.Log("x" + message.fields[0].offset);
            //        //Debug.Log("y" + message.fields[1].offset);
            //        //Debug.Log("z" + message.fields[2].offset);
            //        int x = arrayPosition + message.fields[0].offset;
            //        int y = arrayPosition + message.fields[1].offset;
            //        int z = arrayPosition + message.fields[2].offset;
            //        pcl[column * message.width + row] = new Vector3(message.data[column * message.point_step + row * message.row_step + message.fields[0].offset], message.data[y], message.data[z]);
            //        //Debug.Log(column * message.width + row);
            //        //Debug.Log("x" + pcl[column * width + row].x);
            //        //Debug.Log("y" + pcl[column * width + row].y);
            //        //Debug.Log("z" + pcl[column * width + row].z);
            //    }
            //}
            
            //int max = message.fields.GetLength(0);

            //for (int n = 0; n < max; n++)
            //{
            // Debug.Log("[" + n+"]"+ message.fields[n]);

            //}


        }

        void CreateMesh()
        {
            mesh = new Mesh();
            //Vector3[] points = pcl;
            int[] indecies = new int[size];
            Color[] colors = new Color[size];
            for (int i = 0; i < size; ++i)
            {
                //points[i] = new Vector3(byteArray[3*i], byteArray[3 * i + 1], byteArray[3 * i + 2]);
                indecies[i] = i;
                colors[i] = new Color(i/size, i / size, i / size, 1.0f);
                Debug.Log("i" + i);
                Debug.Log("x" + pcl[i].x);
                Debug.Log("y" + pcl[i].y);
                Debug.Log("z" + pcl[i].z);
            }

            mesh.vertices = pcl;
            mesh.colors = colors;
            mesh.SetIndices(indecies, MeshTopology.Points, 0);

        }
    }
}

