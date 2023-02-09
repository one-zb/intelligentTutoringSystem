using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using KRLab.Core;
using KRLab.Core.Algorithms.Graphs;
using KRLab.Core.SNet;
using ITS.StudentModule;
using ITSText;

using Utilities;
using ITS.DomainModule;

namespace Test
{
    class Program
    {
        public static void Main(string[] args)
        {
            DomainTopicKRModule domainTopicKRModule = new DomainTopicKRModule("八年级数学下");
            List<string> topics = domainTopicKRModule.GetTopics(18, 1);
            Console.WriteLine(topics);
            
            
        }
    }
}
