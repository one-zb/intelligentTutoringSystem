using KRLab.Core.SNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDI;
using System.Drawing;

namespace ITS.DomainModule
{
    class ExperimentsGraph
    {

        // 这里设为600 400 image.width设为400 是清晰图片，
        static readonly Bitmap bmp = new Bitmap(600, 500);
        static readonly Graphics g = Graphics.FromImage(bmp);
        static string directoryPath = @"C:\Users\10114\Desktop\大论文\20221204老师项目\老师项目\image\"; // 指定目录路径


        public string[] drawGraph(ConclusionTopicModule topicModule, KRModuleSNet net)
        {



            // 拿到 图 这个节点
            SNNode graphNode = topicModule.GetGraphNode();
            List<string> imagesPath = new List<string>();


            // 拿到当前结论语义网
            ConclusionKRModuleSNet midLineConNet = new ConclusionKRModuleSNet(net.Net);
            SNNode chemicalImageNode = midLineConNet.Net.GetOutgoingDestination(graphNode, "ISA", "ISA");
            // 拿到 与 图 这个节点 连着的DRAW 的节点 也就是要画的图形节点
            //SNNode imageNode = midLineConNet.Net.GetIncomingSource(graphNode, "ISA", "");
            SNNode imageNode = midLineConNet.Net.GetOutgoingDestination(chemicalImageNode, "GRANU", "GRANU"); // 细化语义颗粒
            // 先把这第一个图形绘制 单个 组合 两种都可以绘制
            List<string> gdiGraph = new List<string>(imageNode.Name.ToString().Split('，'));
            GDIGraphGeneration gdiGeneration = new GDIGraphGeneration();
            gdiGeneration.Draw(g, gdiGraph);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // 获取当前时间的时间戳，格式为yyyyMMddHHmmssfff
            string imagePath = string.Format("{0}image_{1}.png", directoryPath, timestamp); // 将目录路径和时间戳嵌入到文件名中，生成新的文件路径
            imagesPath.Add(imagePath);
            bmp.Save(imagePath);
            
            // 再判断后面需不要再画下一个

            List<SNNode> res = midLineConNet.Net.Neighbours(imageNode, "ASSOC");
            while(res.Count != 0)
            {
                g.Clear(System.Drawing.Color.Beige); // 刷新画布 重新绘制
                // 表示后面还有图形 并排 出现 不是组合 一个图 左右两个图形
                List<string> gdiGraph2 = new List<string>(res[0].Name.ToString().Split('，'));
                GDIGraphGeneration gdiGeneration2 = new GDIGraphGeneration();
                gdiGeneration2.Draw(g, gdiGraph2);
                timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                imagePath = string.Format("{0}image_{1}.png", directoryPath, timestamp);
                imagesPath.Add(imagePath);
                bmp.Save(imagePath);
                res = midLineConNet.Net.Neighbours(res[0], "ASSOC"); // 再看下这个节点后面是否还跟着图形节点
            }

            return imagesPath.ToArray();


        }
    }
}
