using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KRLab.Core.SNet;

namespace ITS.DomainModule
{
    public class ConclusionTopicModule:TopicModule
    {
        public ConclusionKRModuleSNet ConclusionSNet
        {
            get { return (ConclusionKRModuleSNet)_sNet; }
        }

        public ConclusionTopicModule(string course,KRModuleSNet net):base(course,net)
        {

        }

        public ConclusionTopicModule(ConclusionKRModule krModule,string topic):
            base(krModule, topic)
        {

        }

        public string Content
        {
            get 
            {
                if (ConclusionSNet.ConclusionNode == null)
                    return $"{Topic}相对应的语义网是空的";
                return ConclusionSNet.ConclusionNode.Name;
            }
        }

        public List<string> KeyWords
        {
            get
            {
                List<string> strs = new List<string>();
                List<SNNode> nodes = ConclusionSNet.KeyWordNodes;
                foreach (var node in nodes)
                {
                    strs.Add(node.Name);
                }
                return strs;
            }

        }

        public List<string> ContentCharacts 
        {
            get
            {
                List<string> strs = new List<string>();
                List<SNNode> nodes = ConclusionSNet.ContentAssocedNodes;
                foreach(var node in nodes)
                {
                    strs.Add(node.Name);
                }
                return strs;
            }

        }

        // 判断是否有DRAW链接的节点
        public Boolean NeedDraw()
        {
            return ConclusionSNet.ContentDrawNode != null ? true : false;
        }

        // 获取当前结论节点DRAW相连的节点 图
        public SNNode GetGraphNode()
        {
            return ConclusionSNet.ContentDrawNode;
        }

        public override string Parse()
        {
            return base.Parse();
        }

        public override string Parse(string name)
        {
            return base.Parse(name);
        }
    }
}
