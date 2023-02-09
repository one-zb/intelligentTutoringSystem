using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ITS.DomainModule;
using ITSText;
using KRLab.Core.SNet;

namespace ITS.MaterialModule
{
    public class PhenomenaPQAFactory : PQAFactory
    {
        public PhenomenaKRModule KRModule
        {
            get { return (PhenomenaKRModule)_krModule; }
        }
        public PhenomenaPQAFactory(string course) :
            base(new PhenomenaKRModule(course))
        {
        }

        public override PQA CreateSpecificPQA(string topic)
        {
            KRModuleSNet net = KRModule.GetKRModuleSNet(topic);
            if (net == null)
                return null;

            PhenomenaTopicModule topicModule = new PhenomenaTopicModule(KRModule.Course, net);
            PQA spa = new PQA(topic, new Problem());

            //（1）替换现象内容的关键词
            string content = topicModule.GetContent();
            List<string> assocs = topicModule.GetAssocConcepts();
            AddQAs(ref spa, new[] { 0.3, 0.3, 0.3 }, TextProcessor.ReplaceWithUnderLine(content, assocs), assocs.ToArray());

            //(2)获取topic的已建证明问题，这段逻辑为了复用可能放在别的位置较好
            SNNode questionNode = net.QuestionNode;
            if (questionNode != null)
            {
                SNNode typenode = net.Net.GetOutgoingDestination(questionNode, SNRational.IS);
                if (typenode != null && typenode.Name == "证明题")
                {
                    ATTParseInfo attParseInfo = new ATTParseInfo(questionNode, net.Net);
                    SNNode textDescribe = attParseInfo.ATTValueNodeDict["文字描述"][0];
                    SNNode pictureDescribe = attParseInfo.ATTValueNodeDict["图片描述"][0];
                    SNNode putQuestion = attParseInfo.ATTValueNodeDict["提问"][0];
                    SNNode answerNode = net.Net.FastGetNode("答案");
                    SNNode answerValuleNode = net.Net.GetOutgoingDestination(answerNode, SNRational.VAL);

                    StringBuilder stem = new StringBuilder(textDescribe.Name + putQuestion.Name);
                    stem.Append("\n");

                    CausalParseInfo causalParseInfo = new CausalParseInfo(net.Net.GetASSOCNode(textDescribe), net.Net);
                    string[] proof = causalParseInfo.ProofParse(answerValuleNode);
                    List<string> answer = new List<string>();
                    StringBuilder proce = new StringBuilder();
                    Random random = new Random();
                    foreach (var tuple in proof)
                    {
                        string[] sub = tuple.Split(' ');
                        
                        int index = random.Next(0, sub.Length);
                        string changedtuple=TextProcessor.ReplaceWithUnderLine(tuple, sub[index]);
                        answer.Add(sub[index]);
                        proce.Append(changedtuple+"\n");
                    }
                    proce.Remove(proce.Length - 1, 1);
                    AddQAs(ref spa, new[] { 0.4, 0.3, 0.3 }, stem.ToString()+proce.ToString(), answer.ToArray());
                }

                //看是否对题目类型分类
                //后面继续处理别的类型题目

            }
            return spa;
        }
    }
}
