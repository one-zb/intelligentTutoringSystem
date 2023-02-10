
using KRLab.Core.SNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GDI
{
    public class ImageSemanticNet
    {

        //public static List<string> GetCmd()
        //{
        //    // 1. 结论语义项目中 寻找 八年级数学下的结论类型项目
        //    ConclusionKRModule conclusionKRModule = new ConclusionKRModule("八年级数学下");


        //    // 2. 在结论类型中 寻找 中位线 语义网
        //    KRModuleSNet midLineNet = conclusionKRModule.GetKRModuleSNet("中位线定理");

        //    // 3. ConclusionKRModuleSNet 这个类是继承了 KRModuleSNet
        //    ConclusionKRModuleSNet midLineConNet = new ConclusionKRModuleSNet(midLineNet.Net);

        //    // 存放命令集合
        //    //List<string> cmdLib = new List<string>();


        //    // 在语义网中找到节点类型 为 结论、概念、原理这些类型的节点 也是就KCname
        //    //List<SNNode> nodes = midLine.GetKCNodes();


        //    // 用来装命令集合
        //    List<string> res = new List<string>();
        //    foreach (SNNode node in midLineNet.Nodes)
        //    {
        //        // 在语义网里寻找找到要画图的节点 
        //        if (node.Name.Contains("图"))
        //        {
        //            // 找到这个画图的节点 然后解析这个要画的图是什么 
        //            SNNode graphNode = midLineConNet.Net.GetIncomingSource(node, "DRAW", "");


                
        //        // 获取结论中位线这个语义网里所有的节点
        //        //if (node.Name.Equals("△ABC"))
        //        //{
        //            // 打印这个节点名字
        //            //Console.WriteLine(node.Name);
        //            //Console.WriteLine("---------------------------");

        //            // 获取从这个△ABC节点出去的相邻节点 也可以理解从△ABC的第一层遍历
        //            // 拿到的从△ABC节点出发的下一个节点  AB AC BC
        //            //List<SNNode> nodes =  midLineNet.Net.GetOutNeighbors(node);


        //            // 测试：层序遍历
        //            Queue<SNNode> q = new Queue<SNNode>();
        //            // node 为trangleABC 节点
        //            q.Enqueue(graphNode);
        //            // 识别当前要画图的 图形 比如说 三角形
        //            //res.Add(midLineConNet.Net.GetOutgoingDestination(node, SNRational.ISA).ToString());

        //            // 把这个图形 的命令 加入结果
        //            res.Add(graphNode.Name.ToString());
        //            while (q.Count != 0)
        //            {
        //                int size = q.Count;
        //                for (int i = 0; i < size; i++)
        //                {
        //                    SNNode nodeFrom = q.Dequeue();
        //                    // 拿到第一层的节点
        //                    List<SNNode> nodes = midLineNet.Net.GetOutNeighbors(nodeFrom);

        //                    // 把这一层节点 加入队列 然后提取连接关系
        //                    foreach (SNNode nodeTo in nodes)
        //                    {
        //                        if (nodeTo.Name.Equals("三角形") || nodeTo.Name.Equals("图1"))
        //                        {
        //                            continue;
        //                        }
        //                        // 先加入队列
        //                        q.Enqueue(nodeTo);
        //                        //提取 节点间的关系
        //                        string label = midLineNet.Net.Rational(nodeFrom, nodeTo).Label;
        //                        // startRole 是拿到两个节点之间的链接上的前一个节点的补充
        //                        string s2 = midLineNet.Net.Rational(nodeFrom, nodeTo).StartRole;
        //                        string s3 = midLineNet.Net.Rational(nodeFrom, nodeTo).EndRole;
        //                        res.Add(s2 + label + s3 + nodeTo.Name);
        //                        //Console.WriteLine(s1 + nodeTo.Name);

        //                    }
        //                }

        //            }

                   
        //        }

            

        //    }

        //    return res;
        //}


       


    }
    
}
