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
                test_point.position = new Vector3(byteArray[0], byteArray[1], byteArray[2]);
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
            int width = message.width;
            int height = message.height;
            int row_step = message.row_step;
            int point_step = message.point_step;

            pcl = new Vector3[width*height];

            for (int column = 0; column < width; column++)
            {
                for(int row = 0; row < height; row++)
                {
                    int arrayPosition = column * point_step + row * row_step;
                    pcl[column * width + row] = new Vector3(arrayPosition+message.fields[0].offset, arrayPosition+ message.fields[1].offset, arrayPosition+ message.fields[2].offset);
                }
            }


        }

        void CreateMesh()
        {
            mesh = new Mesh();
            Vector3[] points = pcl;
            int[] indecies = new int[size/3];
            Color[] colors = new Color[size/3];
            for (int i = 0; i < size/3; ++i)
            {
                points[i] = new Vector3(byteArray[3*i], byteArray[3 * i + 1], byteArray[3 * i + 2]);
                indecies[i] = i;
                colors[i] = new Color(3*i/size, 3 * i / size, 3 * i / size, 1.0f);
            }

            mesh.vertices = points;
            mesh.colors = colors;
            mesh.SetIndices(indecies, MeshTopology.Points, 0);

        }
    }
}

