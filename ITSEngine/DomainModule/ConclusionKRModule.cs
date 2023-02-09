using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using KRLab.Core;
using KRLab.Core.SNet;
using Utilities;

namespace ITS.DomainModule
{
    /// <summary>
    /// 如下几种知识表达，都是文字陈述
    ///Principle = "原理";
    ///Theorem = "定理";
    ///Law = "定律";
    ///Axiom = "公理";
    /// </summary>
    public class ConclusionKRModule:KRModule
    { 
        public override object Project
        {
            get
            {
                if(_project==null)
                {
                    string path = FileManager.GetKRSNProjectPath(_course, ProjectType.consn);
                    if (path == null || !File.Exists(path))
                        return null; 
                    KRSNetProject<ConclusionKRModuleSNet> project = new KRSNetProject<ConclusionKRModuleSNet>();
                    project.LoadFromFile(path);
                    _project=project; 
                }
                return _project;
            }
        }

        public override List<SemanticNet> SNets
        {
            get
            {
                List<SemanticNet> nets = new List<SemanticNet>();
                List<ConclusionKRModuleSNet> conceptNets = ((KRSNetProject<ConclusionKRModuleSNet>)Project).NetList;
                foreach (var n in conceptNets)
                    nets.Add(n.Net);
                return nets;
            }
        } 

        public override List<string> KRAttributes => throw new NotImplementedException(); 

        public ConclusionKRModule(string course):base(course,KCNames.Conclusion)
        {

        } 

        public override KRModuleSNet GetKRModuleSNet(string netName)
        {
            if (Project == null)
                return null;
            SemanticNet net =( ( KRSNetProject<ConclusionKRModuleSNet>) Project).GetSNet(netName);
            if (net == null)
                return null;

            return new ConclusionKRModuleSNet(net);
        }

        public override TopicModule CreateTopicModule(string netName)
        {
            ConclusionTopicModule topicModule = new ConclusionTopicModule(this, netName);
            return topicModule;
        }

    }
}
