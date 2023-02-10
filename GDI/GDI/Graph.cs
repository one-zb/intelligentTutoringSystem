using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDI
{
    class Graph
    {
        private int UNM_VERTICES = 6;
        public Vertex[] vertices;//存放图中所有顶点
        private int[,] adjMatrix;
        int numVerts;
        public Graph(int numvertices)
        {
            UNM_VERTICES = numvertices;
            vertices = new Vertex[UNM_VERTICES];
            adjMatrix = new int[UNM_VERTICES, UNM_VERTICES];
            numVerts = 0;
            for (int j = 0; j < UNM_VERTICES; j++)
            {
                for (int k = 0; k < UNM_VERTICES; k++)
                {
                    adjMatrix[j, k] = 0;
                }
            }
        }
        public void AddVertex(string label)
        {//添加顶点
            vertices[numVerts] = new Vertex(label);
            numVerts++;
        }
        public void AddEdge(int start, int eend)
        {//添加边
            adjMatrix[start, eend] = 1;
        }
        public bool IsConnectBase(int equip1, int equip2)
        {
            if (adjMatrix[equip1, equip2] == 1 || adjMatrix[equip2, equip1] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class Vertex
    {
        public bool wasVisited;
        public string label;//进行查找和排序算法用到，没有调用算法就没用
        public Vertex(string label)
        {
            this.label = label;
            wasVisited = false;
        }

    }
    public class DeviceGraph
    {
        Dictionary<string, int> equipmentToNumber = new Dictionary<string, int>();
        private Graph equipmentConGraph = new Graph(9);
        public DeviceGraph()
        {   //构造器材之间的连接关系
            equipmentConGraph.AddVertex("烧杯");      //0
            equipmentConGraph.AddVertex("反应瓶");    //1
            equipmentConGraph.AddVertex("漏斗");    //2
            equipmentConGraph.AddVertex("广口瓶");     //3
            equipmentConGraph.AddVertex("试管");         //4
            equipmentConGraph.AddVertex("酒精灯");//5
            equipmentConGraph.AddVertex("铁架台");  //6
            equipmentConGraph.AddVertex("石棉网");      //7
            equipmentConGraph.AddVertex("玻璃管");    //8
            equipmentConGraph.AddEdge(0, 8);
            equipmentConGraph.AddEdge(0, 6);
            equipmentConGraph.AddEdge(1, 2);
            equipmentConGraph.AddEdge(1, 6);
            equipmentConGraph.AddEdge(1, 8);
            equipmentConGraph.AddEdge(2, 3);
            equipmentConGraph.AddEdge(2, 6);
            equipmentConGraph.AddEdge(3, 8);
            equipmentConGraph.AddEdge(4, 6);
            equipmentConGraph.AddEdge(4, 8);
            equipmentConGraph.AddEdge(5, 6);
            equipmentConGraph.AddEdge(6, 7);
            equipmentToNumber.Add("烧杯", 0);
            equipmentToNumber.Add("锥形瓶", 1);
            equipmentToNumber.Add("圆底烧瓶", 1);
            equipmentToNumber.Add("反应瓶", 1);
            equipmentToNumber.Add("漏斗", 2);
            equipmentToNumber.Add("广口瓶", 3);
            equipmentToNumber.Add("试管", 4);
            equipmentToNumber.Add("分液漏斗", 2);
            equipmentToNumber.Add("酒精灯", 5);
            equipmentToNumber.Add("铁架台", 6);
            equipmentToNumber.Add("石棉网", 7);
            equipmentToNumber.Add("大玻璃管", 8);
            equipmentToNumber.Add("小玻璃管", 8);
            equipmentToNumber.Add("玻璃管", 8);
        }

        // 判断拿到的图形 与已经画出来的图形 是否能链接
        public bool IsConnect(string equip1, string equip2)
        {

            //拿到图元到的编号 这个编号是提前定义好的数字 代表图元
            int i = equipmentToNumber[equip1];
            int j = equipmentToNumber[equip2];
            bool canConnected = equipmentConGraph.IsConnectBase(i, j);
            return canConnected;
        }
    }
}
