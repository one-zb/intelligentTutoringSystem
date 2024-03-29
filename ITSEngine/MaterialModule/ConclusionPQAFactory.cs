﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITS.DomainModule;
using ITSText;
using KRLab.Core.SNet;
using Utilities;



namespace ITS.MaterialModule
{
    public class ConclusionPQAFactory : PQAFactory
    {
        public ConclusionKRModule KRModule
        {
            get { return (ConclusionKRModule)_krModule; }
        }
        public ConclusionPQAFactory(string course) :
            base(new ConclusionKRModule(course))
        {
        }

        public override PQA CreateSpecificPQA(string topic)
        {
            KRModuleSNet net = KRModule.GetKRModuleSNet(topic);
            if (net == null)
                return null;

            // 拿到这个课程结论语义网
            ConclusionTopicModule topicModule = new ConclusionTopicModule(KRModule.Course, net);
            PQA pqa = new PQA(topic, new Problem());

            ///(1) 测试结论的内容，填空题////////////////////////////////////////
            List<string> charas = topicModule.ContentCharacts;
            string content = TextProcessor.ReplaceWithUnderLine(topicModule.Content, charas);

            // 判断一下当前结论节点相连的有没有DRAW连接的节点，如果有这个链接，表示要画图
            if (topicModule.NeedDraw())
            {
               

                // 拿到 图 这个节点
                SNNode graphNode = topicModule.GetGraphNode();
                // 拿到当前结论语义网
                ConclusionKRModuleSNet currentSNet = new ConclusionKRModuleSNet(net.Net);

                // 判断是否有并排绘制 化学图元或者是 化学实验装置组合图绘制需求 即出现 GRANU 连接
                List<SNNode> res = currentSNet.Net.Neighbours(graphNode, "GRANU");
                string[] imagePath = GDI.Commander.GetImagePath();
                SNNode imageNode = currentSNet.Net.GetOutgoingDestination(graphNode, "ISA", "ISA"); // 拿到图节点出发的这个节点，判断是数学还是物理、化学
                if (imageNode.Name.Equals("化学图"))
                {
                    ExperimentsGraph experimentsGraph = new ExperimentsGraph();
                    imagePath = experimentsGraph.drawGraph(topicModule, net); // 这里会直接返回生成图片的路径数组
                }
                else if (imageNode.Name.Equals("物理图"))
                {
                    // 否则就调用命令库绘制图元
                    //SNNode imageNode = currentSNet.Net.GetOutgoingDestination(graphNode, "ISA", "ISA");


                    // 测试物理画图
                    CircuitGerneration circuitGeneration = new CircuitGerneration();
                    circuitGeneration.circuitDraw(topicModule, net);
                }
                else if (imageNode.Name.Equals("数学图"))
                {
                    // 如果当前节点需要画图,再进行画图操作 
                    // 初始化画图命令 这里主要是命令画图 目前数学和化学 部分支持
                    CMDMatch match = new CMDMatch();
                    match.GraphMethod(topicModule, net);
                }
                

                
               
                
                AddQAs(ref pqa, new[] { 0.5, 0.1, 0.1 }, imagePath, "请按顺序填写下面的空缺：\n" + content + "。",
               charas.ToArray());

            } else
            {
                AddQAs(ref pqa, new[] { 0.5, 0.1, 0.1 }, "请按顺序填写下面的空缺：\n" + content + "。",
              charas.ToArray());
            }


            // 下面这一步就是形成问题了，所以我们要在这一步之前，把图片生成，
            // 并且保存一个路径，然后把这个图片路径传到AddQAs这个方法里的参数里
            // AddQAs里没有带有图片参数的方法，所以我们要在里面再重载一个带有图片的方法
            //string[] imagePath = null;
           



            //AddQAs(ref pqa, new[] { 0.5, 0.1, 0.1 }, "请按顺序填写下面的空缺：\n" + content + "。",
            //    charas.ToArray());




            return pqa;
        }
        
    }
}
