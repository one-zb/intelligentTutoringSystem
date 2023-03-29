using GDI;
using ITS.DomainModule;
using KRLab.Core.SNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITS.MaterialModule
{
    class ImageGeneration 
    {

        

        // 拿到画图的指令
        public static List<string> GetCmd(ConclusionTopicModule topicModule, KRModuleSNet net)
        {

            // 拿到 图 这个节点
            SNNode graphNode = topicModule.GetGraphNode();
            
            // 拿到当前结论语义网
            ConclusionKRModuleSNet midLineConNet = new ConclusionKRModuleSNet(net.Net);
            // 拿到 与 图 这个节点 连着的DRAW 的节点 也就是要画的图形节点
            //SNNode imageNode = midLineConNet.Net.GetIncomingSource(graphNode, "ISA", "");
            SNNode imageNode = midLineConNet.Net.GetOutgoingDestination(graphNode, "ISA", "ISA");
            // 用来装命令集合
            List<string> res = new List<string>();

            // 测试：层序遍历
            Queue<SNNode> q = new Queue<SNNode>();
            // node 为trangleABC 节点
            q.Enqueue(imageNode);
            // 识别当前要画图的 图形 比如说 三角形
            //res.Add(midLineConNet.Net.GetOutgoingDestination(node, SNRational.ISA).ToString());

            // 把这个图形 的命令 加入结果
            res.Add(imageNode.Name.ToString());
            while (q.Count != 0)
            {
                int size = q.Count;
                for (int i = 0; i < size; i++)
                {
                    SNNode nodeFrom = q.Dequeue();
                    // 拿到第一层的节点
                    List<SNNode> nodes = net.Net.GetOutNeighbors(nodeFrom);

                    // 把这一层节点 加入队列 然后提取连接关系
                    foreach (SNNode nodeTo in nodes)
                    {
                        if (nodeTo.Name.Equals("三角形") || nodeTo.Name.Equals("图1"))
                        {
                            continue;
                        }

                        // 先加入队列
                        q.Enqueue(nodeTo);
                        //提取 节点间的关系
                        string label = net.Net.Rational(nodeFrom, nodeTo).Label;
                        // startRole 是拿到两个节点之间的链接上的前一个节点的补充
                        string s2 = net.Net.Rational(nodeFrom, nodeTo).StartRole;
                        string s3 = net.Net.Rational(nodeFrom, nodeTo).EndRole;
                        res.Add(s2 + label + s3 + nodeTo.Name);
                        //Console.WriteLine(s1 + nodeTo.Name);

                    }
                }

            }

            return res;

        }

        
    }
}
