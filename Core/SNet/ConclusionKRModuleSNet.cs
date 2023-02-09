using KRLab.Core.BDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
 

namespace KRLab.Core.SNet
{
    public class ConclusionKRModuleSNet:KRModuleSNet
    {
        public override string KRType => ProjectType.consn;

        protected SNNode _contentNode;
        protected SNNode _defNode;//表示结论的具体内容的结点

        public SNNode ConclusionNode
        {
            get { return _defNode;}
        }

        public ConclusionKRModuleSNet(SemanticNet net):base(net,KCNames.Conclusion)
        {

        }

        public override void CheckAndInit()
        {
            base.CheckAndInit();
            _contentNode = Net.GetOutgoingDestination(_krNode, SNRational.ATT);

            List<SNNode> nodes = Net.GetOutgoingDestinations(_contentNode, SNRational.VAL);
            if (nodes.Count > 0)
                _defNode = nodes[0];
            else
                _defNode = null;
        }

        /// <summary>
        /// 如果发现与内容关联的结点是行为或行为的结果，则列为关键结点，进行分析
        /// </summary>
        /// <returns></returns>
        public List<SNNode> KeyWordNodes
        {
            get
            {
                List<SNEdge> edges = Net.GetOutgoingEdges(_defNode, SNRational.ASSOC);
                List<SNNode> nodes = new List<SNNode>();
                foreach (var edge in edges)
                {
                    SNNode node = _net.GetIncomingSource(edge.Destination, SNRational.RESULT, SNRational.ACT);
                    nodes.Add(node);
                }

                return nodes;

            }

        }

        public List<SNNode> ContentAssocedNodes
        {
            get
            {
                return Net.GetOutgoingDestinations(_defNode, SNRational.ASSOC);
            }
        }


        // 获取与结论具体内容节点DRAW相连的节点
        public SNNode ContentDrawNode
        {
            get
            {
                return Net.GetOutgoingDestination(_defNode, SNRational.DRAW);
            }
        }

        /// <summary>
        /// 用于检查ACT的语法，在KRLab编辑器的代码中调用
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="relations"></param>
        /// <param name="callback"></param>
        public new static void Check(List<IEntity> entities, List<Relationship> relations, Action<bool, string> callback)
        {
            IEntity topNode = null;
            IEntity contentNode = null;
            foreach (var node in entities)
            {
                if (node.Name == KCNames.Conclusion)
                    topNode = node;
                else if (node.Name == "内容")
                    contentNode = node;
            }

            if (topNode == null)
            {
                callback(false, "没有<" + KCNames.Conclusion + ">结点");
                return;
            } 
            if(contentNode==null)
            {
                callback(false, "没有<内容>结点");
                return;
            }

            IEntity topicNode = null;
            List<Relationship> rels = new List<Relationship>();
            bool isOk1 = false;
            foreach (var rl in relations)
            {
                SNRelationship snr = (SNRelationship)rl;
                if ((snr.SNRelationshipType.ToString() == SNRational.KTYPE || snr.SNRelationshipType.ToString()==SNRational.ISA)
                    && snr.Second == topNode)
                      topicNode = snr.First;
                if (snr.SNRelationshipType.ToString() == SNRational.VAL && snr.First == contentNode)
                {
                    rels.Add(rl);
                }
                if (snr.SNRelationshipType.ToString() == SNRational.ATT && snr.First == topNode &&
                    snr.Second == contentNode)
                    isOk1 = true;

            }
            if (topicNode == null)
            {
                callback(false, "必须有子节点与<" + topNode.Name + ">结点以KTYPE相连！");
                return;
            }
            if(!isOk1)
            {
                callback(false, "<内容>结点必须以ATT关系与<" + topNode.Name + ">相连！");
                return;
            }
            if(rels.Count==0)
            {
                callback(false, "必须给出<" + contentNode.Name + ">的VAL子节点，用以给出结论的具体内容！");
                return;
            }

            IEntity concNode = null;
            if(rels.Count==1)
            {
                concNode = rels[0].Second;
            }
            else if(rels.Count>1)
            { 
                foreach(var rel in rels)
                {
                    if (rel.Label.Contains("文字描述"))
                    { 
                        concNode = rel.Second;
                        break;
                    }
                }
            }
            if(concNode==null)
            {
                callback(false, "有多个<" + contentNode.Name + ">的VAL子节点，必须在VAL连接上用'文字描述’指明内容的文字描述节点！");
                return;
            }

            List<IEntity> contKeyNodes = new List<IEntity>();
            foreach (var rl in relations)
            {
                SNRelationship snr = (SNRelationship)rl;
                if (snr.SNRelationshipType.ToString() == SNRational.ASSOC &&
                    snr.First == concNode)
                    contKeyNodes.Add(snr.Second); 
            } 
            if(contKeyNodes.Count==0)
            {
                callback(false, "必须以ASSOC关系指出结论内容的关键词!");
                return;
            } 

            ACTParseInfo.Check(entities, relations, callback);

        }
    }
}
