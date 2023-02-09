using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRLab.Core.SNet
{
    public class CausalParseInfo : ParseInfo
    {
        /// <summary>
        /// 解析一个原因节点造成的一系列行为，参考图为证明题建模图
        /// </summary>
        SNNode _causeNode;//原因节点一张图可能有很多个，这里只针对具体的一个原因查看它的行为结果
        public CausalParseInfo(SNNode CauseNode, SemanticNet net) : base(net)
        {
            _causeNode = CauseNode;
            Parse();
        }
        protected void Parse()
        {

        }
        public override void ProduceQAs(out List<System.Tuple<string, string[]>> qas)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从答案值节点开始反向解析，最后把结果倒序得到正常的解题步骤
        /// </summary>
        /// <param name="sNet"></param>
        /// <param name="node">结果节点，可以是中间结果或最终结果</param>
        /// <returns>完整的解答流程，一个string为一个三元组（条件 原因 结果）</returns>
        public string[] ProofParse(SNNode node)
        {
            var que = new System.Collections.Generic.Queue<SNNode>();
            que.Enqueue(node);
            List<string> revResult = new List<string>();

            //BFS
            while (que.Count > 0)
            {
                SNNode curNode = que.Dequeue();
                List<SNNode> causeNodes = Net.GetOutgoingDestinations(curNode, SNRational.CAUSAL);//有问题，获得未访问过的原因节点
                SNNode confmNode = Net.GetOutgoingDestination(curNode, SNRational.CONFM);
                StringBuilder tmpCond = new StringBuilder();//curNode节点的原因
                if (causeNodes == null||causeNodes.Count==0)
                {
                    continue;
                }
                else if (causeNodes.Count == 1)
                {
                    tmpCond.Append(causeNodes[0].Name);
                    que.Enqueue(causeNodes[0]);
                }
                else //如果当前节点有多个原因节点
                {
                    foreach (SNNode causeNode in causeNodes)
                    {
                        tmpCond.Append(causeNode.Name);
                        tmpCond.Append(",");
                        que.Enqueue(causeNode);
                    }
                    tmpCond.Remove(tmpCond.Length - 1, 1);
                }
                string full = tmpCond.ToString();
                if (confmNode != null)
                {
                    full+=(" 根据：" + confmNode.Name);
                }
                revResult.Add("因为：" + full  + " 所以：" + curNode.Name);
            }
            string[] res = revResult.ToArray();
            Array.Reverse(res);
            return res;
        }
        //确保node不为空
        public List<SNNode> FindStartCauseNode(SNNode node)
        {
            List<SNNode> res = new List<SNNode>();
            List<SNNode> causeNodes = Net.GetAllDestinations(node, SNRational.CAUSAL);
            if (causeNodes == null)
            {
                res.Add(node);
                return res;
            }
            foreach (var causeNode in causeNodes)
            {
                res.AddRange(FindStartCauseNode(causeNode));
            }
            return res;
        }
    }
}
