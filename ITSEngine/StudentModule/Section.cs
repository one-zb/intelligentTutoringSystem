using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ITS.DomainModule;

namespace ITS.StudentModule
{
    ///小节下所有的学习记录
    using ResultDcit = Dictionary<string, LearningResult>;
    public class Section
    {
        protected ResultDcit _topicDict;
        //小节的全名，如，第1节-时间的测量
        protected string _sectName;
        public ResultDcit SectDict
        {
            get { return _topicDict; }
        }

        public List<string> Topics
        {
            get { return _topicDict.Keys.ToList(); }
        }

        public Section(string name)
        {
            _sectName = name;
            _topicDict = new ResultDcit(); 
        }

        public LearningResult GetLearningResult(string topic)
        {
            if (!_topicDict.Keys.Contains(topic))
                return null;
            return _topicDict[topic];
        }

        public void AddLearningResult(string topic,LearningResult result)
        {
            _topicDict[topic] = result;
        }
    }
}
