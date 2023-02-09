using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WpfMath; 
using KRLab.Core.FuzzyEngine;
using KRLab.Core.SNet;
using KRLab.Core;

using ITS.DomainModule; 

using Utilities;
using ITSText;
 
using System.Reflection;
using ITS.MathSolvers;
using ITS.MaterialModule;


namespace KRLabConsole
{
    class Program 
    {
        //public static void callback(string name)
        //{
        // Console.WriteLine(name);
        // }
        static void Main(string[] args)
        {


          
            //Console.WriteLine(Regex.Replace("KHacbAB", @"[^A-Z]+", ""));
            //SemanticNet net = new SemanticNet("八年级数学下");

            // 传入目录
            //DomainTopicKRModule SN = new DomainTopicKRModule("八年级数学下");
            // 在八年级数学下的目录中找到18节的对应的语义网 平行四边形
            //SemanticNet chaptNode = SN.GetSNet("18");
            // 在 平行四边形语义网中拿到 结论的节点
            //SNNode node = chaptNode.FastGetNode("结论");

            // 1. 结论语义项目中 寻找 八年级数学下的结论类型项目
            ConclusionKRModule conclusionKRModule = new ConclusionKRModule("八年级数学下");
           
            
            // 2. 在结论类型中 寻找 中位线 语义网
            KRModuleSNet midLineNet = conclusionKRModule.GetKRModuleSNet("中位线定理");

            // 3. ConclusionKRModuleSNet 这个类是继承了 KRModuleSNet
            ConclusionKRModuleSNet midLineConNet = new ConclusionKRModuleSNet(midLineNet.Net);

            // 存放命令集合
            //List<string> cmdLib = new List<string>();


            // 在语义网中找到节点类型 为 结论、概念、原理这些类型的节点 也是就KCname
            //List<SNNode> nodes = midLine.GetKCNodes();

            foreach (SNNode node in midLineNet.Nodes) {
                // 获取结论中位线这个语义网里所有的节点
                if(node.Name.Equals("△ABC"))
                {
                    // 打印这个节点名字
                    Console.WriteLine(node.Name);
                    Console.WriteLine("---------------------------");
                    // 获取从这个△ABC节点出去的相邻节点 也可以理解从△ABC的第一层遍历
                    // 拿到的从△ABC节点出发的下一个节点  AB AC BC
                    //List<SNNode> nodes =  midLineNet.Net.GetOutNeighbors(node);


                    // 测试：层序遍历
                    Queue<SNNode> q = new Queue<SNNode>();
                    // node 为△ABC 节点
                    q.Enqueue(node);
                    while(q.Count != 0)
                    {
                        int size = q.Count;
                        for(int i = 0; i < size; i++)
                        {
                            SNNode node1 = q.Dequeue();
                            // 拿到第一层的节点
                            List<SNNode> nodes = midLineNet.Net.GetOutNeighbors(node1);

                            // 把这一层节点 加入队列 然后提取连接关系
                            foreach (SNNode node2 in nodes)
                            {
                                if (node2.Name.Equals("三角形") || node2.Name.Equals("图1"))
                                {
                                    continue;
                                }
                                // 先加入队列
                                q.Enqueue(node2);
                                //提取 节点间的关系
                                string s1 = midLineNet.Net.Rational(node1, node2).Label;
                                Console.WriteLine(s1 + node2.Name);

                            }
                        }
                       
                    }


                    //// 遍历 节点 AB AC BC
                    //foreach (SNNode neigbor in nodes)
                    //{
                    //    if(neigbor.Name.Equals("三角形") || neigbor.Name.Equals("图1"))
                    //    {
                    //        continue;
                    //    }
                    //    //提取 节点间的关系
                    //    string s1 = midLineNet.Net.Rational(node, neigbor).Label;
                    //    // 结果为 边AB
                    //    Console.WriteLine(s1 + neigbor.Name);
                    //    // 加入到命令集合中
                    //    //cmdLib.Add(s1);
                       
                    //    // 然后以这个节点去去下一层遍历 找到该节点的下一个节点
                    //    // 这里拿到了 AB的下一个节点 D AC的下一个节点E
                    //    List<SNNode> neiborSubNodes = midLineNet.Net.GetOutNeighbors(neigbor);

                    //    // 这里就是在 节点 D E 遍历这两个节点
                    //    foreach (SNNode subNode in neiborSubNodes)
                    //    {
                    //        //提取AB - D 节点间的关系 提取 中点
                    //        string s2 = midLineNet.Net.Rational(neigbor, subNode).Label;
                    //        // 结果为 中点 D
                    //        Console.WriteLine(s2 + subNode.Name);
                    //        //cmdLib.Add(s2);
                    //        // 然后以这个节点去去下一层遍历 找到该节点的下一个节点 
                    //        // 就是 D的下一个节点 E  E 的下一个节点 D
                    //        List<SNNode> subNodesNextNodes = midLineNet.Net.GetOutNeighbors(subNode);
                    //        //Console.WriteLine(subNodesNextNodes.Count);

                    //        // 寻找 D 的下一个节点 E
                    //        foreach(SNNode subNodeSub in subNodesNextNodes)
                    //        {

                    //            //提取D - E 节点间的关系 提取 连接
                    //            string s3 = midLineNet.Net.Rational(subNode, subNodeSub).Label;
                    //            // 结果为 D连接E
                    //            Console.WriteLine(subNode.Name + s3 + subNodeSub.Name);
                    //            //cmdLib.Add(s3);
                    //            Console.WriteLine("---------------------------");
                    //            //Console.WriteLine(subNodeSub.Name);
                    //        }
                    //    }
                        // 拿到 指向这个节点的连接
                        //List<SNEdge> edges1 = midLineNet.Net.GetIncomingEdges(neigbor);
                        //foreach(SNEdge edge in edges1)
                        //{
                        //    // label 就是连接上标注的字  Rational  这条edge连接关系
                        //    Console.WriteLine(edge.Rational.Label);
                        //    Console.WriteLine(neigbor.Name);
                        //    Console.WriteLine("---------------------------");
                            
                        //}

                        
                    //}
                    //IEnumerator<SNEdge> edges = node.OutEdges.GetEnumerator();
                    //while(edges.MoveNext())
                    //{
                    //    Console.WriteLine(edges.Current.Weight);
                    //}

                    // 从当前节点BFS搜索 节点
                    //IEnumerable<SNNode> list = midLineConNet.Net.BreadthFirstWalk(node);
                    //IEnumerator<SNNode> enumerator = list.GetEnumerator();
                    //while(enumerator.MoveNext())
                    //{
                    //    Console.WriteLine(enumerator.Current.Name);
                    //}
                     

                    // 拿到从△ABC这个节点出去的连接线
                    //Console.WriteLine(midLineConNet.Net.GetOutgoingEdges(node).Count);

                    // 拿到与这个节点想连的节点 不管是进还是出
                    //List<SNNode> neibors = midLineConNet.Net.GetOutNeighbors(node);
                    //foreach(SNNode neiborNode in neibors)
                    //{
                    //    Console.WriteLine(neiborNode.Name);
                    //}

                    //// 拿到这个节点出发的叶子节点
                    //List<SNNode> leafNodes = midLineConNet.Net.GetLeafNodes(node);
                    //foreach(SNNode leafNode in leafNodes)
                    //{
                    //    Console.WriteLine(leafNode.Name);
                    //}


                }
                
            }
            
            Console.Read();



            // SNNode 是一个单独节点 不是一个语义网图 SemanticNet 是一个语义网图 
            // SNNode secNode = chaptNode.GetFirstNode("平行四边形");


            //Console.WriteLine(secNode.Name);

            //string str = "X\xB2";
            //Console.WriteLine(str);


            // Console.WriteLine(equ.ToString());

            //Expr c = Expr.Parse("2");
            //c = c.Sqrt();
            //Expr b = Expr.Parse("2^(1/2)");
            //if (b.ToInternalString() == c.ToInternalString())
            //    Console.WriteLine(b.ToInternalString());

            //YYECEqu equ = new YYECEqu();
            //equ.CreateEquModelOne(new Tuple<int, int, int>(2, 3, 0), new Tuple<int, int, int>(0, 2, 3));
            //Console.WriteLine(equ.ToString());
        }
        //static void Main(string[] args)
        //{
        //    Symbol x = new Symbol("x");
        //    MathObject y = new DoubleFloat(0);

        //    Equation obj = new Equation(x, y, Equation.Operators.NotEqual);
        //    obj.StandardForm();
        //    Console.WriteLine(obj.StandardForm());


        //    //string path = FileManager.KnowledgePath + @"\物理\结论.consn";
        //    //SemanticNet net = KRModuleSNet.CreateASNet("运动和静止的相对性", path);
        //    //SNNode node = net.FuzzyGetNode("运动和静止的相对性");

        //    ////ACTParseInfo actInfo = new ACTParseInfo(node,net);
        //    ////List<SNNode> actors = actInfo.GetActorNodes();
        //    ////List<SNNode> results = actInfo.GetAssocResultNodes(actors[0]);

        //    //ATTParseInfo info = new ATTParseInfo(node, net);
        //    //string str=info.ParseAttValue("内容");

        //    //SNNode node1 = net.FastGetNode("内容");
        //    //ATTParseInfo info1 = new ATTParseInfo(node1, net);
        //    //string str1 = info1.ParseAttValue("特征");

        //}
        //class Fuzzy
        /*{
            bool selected = false;
            IFuzzyEngine engine;
            public float Speedmult = 0.3f;
            LinguisticVariable distance, direction;

            public void Init()
            {
                // Here we need to setup the Fuzzy Inference System
                distance = new LinguisticVariable("distance");
                var farRight = distance.MembershipFunctions.AddTrapezoid("farRight", -100, -100, -60, -45);
                var right = distance.MembershipFunctions.AddTrapezoid("right", -50, -50, -7, -0.05f);
                var none = distance.MembershipFunctions.AddTrapezoid("none", -7, -0.5, 0.5, 7);
                var left = distance.MembershipFunctions.AddTrapezoid("left", 0.05f, 7, 50, 50);
                var farLeft = distance.MembershipFunctions.AddTrapezoid("farLeft", 45, 60, 100, 100);

                direction = new LinguisticVariable("direction");
                var farRightD = direction.MembershipFunctions.AddTrapezoid("farRight", -100, -100, -60, -45);
                var rightD = direction.MembershipFunctions.AddTrapezoid("right", -50, -50, -7, -0.05f);
                var noneD = direction.MembershipFunctions.AddTrapezoid("none", -7, -0.5, 0.5, 7);
                var leftD = direction.MembershipFunctions.AddTrapezoid("left", 0.05f, 7, 50, 50);
                var farLeftD = direction.MembershipFunctions.AddTrapezoid("farLeft", 45, 60, 100, 100);

                engine = new FuzzyEngineFactory().Default();
                var rule0 = Rule.If(distance.Is(farRight)).Then(direction.Is(farLeftD));
                var rule1 = Rule.If(distance.Is(right)).Then(direction.Is(leftD));
                var rule2 = Rule.If(distance.Is(left)).Then(direction.Is(rightD));
                var rule3 = Rule.If(distance.Is(none)).Then(distance.Is(noneD));
                var rule4 = Rule.If(distance.Is(farLeft)).Then(direction.Is(farRightD));

                engine.Rules.Add(rule0, rule1, rule2, rule3, rule4);
                Console.WriteLine(engine.Defuzzify(new { distance = 10 }));
            }

            private LinguisticVariable _performance;
            private LinguisticVariable _difficulty;
            private IFuzzyEngine _engine;

            public void Init1()
            {
                _performance = new LinguisticVariable("Performance");
                IMembershipFunction excellent = _performance.MembershipFunctions.AddRectangle("Excellent", 9, 10);
                IMembershipFunction poor = _performance.MembershipFunctions.AddRectangle("Poor", 0, 9);

                _difficulty = new LinguisticVariable("Difficulty");
                IMembershipFunction difficulty = _difficulty.MembershipFunctions.AddRectangle("Difficulty", 5, 10);
                IMembershipFunction easy = _difficulty.MembershipFunctions.AddRectangle("Easy", 0, 5);

                FuzzyRule rule0 = Rule.If(_performance.Is(excellent)).Then(_difficulty.Is(difficulty));
                FuzzyRule rule1 = Rule.If(_performance.Is(poor)).Then(_difficulty.Is(easy));

                _engine = new FuzzyEngineFactory().Default();
                _engine.Rules.Add(rule0, rule1);

                Console.WriteLine(_engine.Defuzzify(new { Performance = 9.5 }));

            }
        }
        //static void Main(string[] args)
        //{

        //    //Fuzzy f = new Fuzzy();
        //    //f.Init1();
        //    //BaiChenEvaluationMethod method = new BaiChenEvaluationMethod();
        //    //method.Run();

        //    //Paper20200322 paper = new Paper20200322();
        //    //paper.Run();

        //    //double x = Symbolics.Calculate("2*4/x", "x", 5.0);

        //}

        //static void Main(string[] args)
        //{

        //    BDIExample bdi0 = new BDIExample("first_bdi");
        //    bdi0.Config();
        //    Agent agent0 = new Agent("first", 0, bdi0);
        //    agent0.Go();

        //}
        //static void Main(string[] args)
        //{
        //    try
        //    {
        //        QAEngine qa = new QAEngine();
        //        qa.Config("初中物理计算.fvc", "机械能", "机械能");
        //        //Console.WriteLine(qa.EquationMaker.Equations[0].RightFormula.ExprString);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //}
    }
}

    } */
    }
}