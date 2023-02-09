using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using KRLab.Core.SNet;
using KRLab.Core;
using Utilities;

namespace ITS.DomainModule
{
     public class ConceptKRModule:KRModule
    { 

        public override object Project
        {
            get
            {
                if(_project==null)
                {
                    string path = FileManager.GetKRSNProjectPath(_course, ProjectType.conceptsn);
                    if (path==null || !File.Exists(path))
                        return null;
                    KRSNetProject<ConceptKRModuleSNet> project = new KRSNetProject<ConceptKRModuleSNet>();
                    project.LoadFromFile(path);

                    _project = project;
                }
                return _project;
            }
        } 

        public override List<string> KRAttributes
        {
            get { return new List<string>() {"内涵","外延","区别特征" }; }
        }

        public override List<SemanticNet> SNets
        {
            get 
            {
                List<SemanticNet> nets = new List<SemanticNet>();
                List<ConceptKRModuleSNet> conceptNets = ((KRSNetProject<ConceptKRModuleSNet>)Project).NetList;
                foreach (var n in conceptNets)
                    nets.Add(n.Net);
                return nets;
            }
        }

        public ConceptKRModule(string course):base(course,KCNames.Concept)
        {

        } 
         

        public override KRModuleSNet GetKRModuleSNet(string netName)
        {
            if(Project== null)
            {
                return null;
            } 
            SemanticNet net = ((KRSNetProject<ConceptKRModuleSNet>)Project).GetSNet(netName);
            if (net == null)
                return null;

            return new ConceptKRModuleSNet(net);            
        }

        public override TopicModule CreateTopicModule(string netName)
        { 
            ConceptTopicModule topicModule = new ConceptTopicModule(this, netName);
            return topicModule;
        }

        public string GetDefinition()
        {
            return string.Empty;
        }
        public string GetExtention()
        {
            return string.Empty;
        }
        public string GetDistinctive()
        {
            return string.Empty;
        }
    }
}
