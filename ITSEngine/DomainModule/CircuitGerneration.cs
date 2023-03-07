using GDI;
using KRLab.Core.SNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITS.DomainModule
{
    class CircuitGerneration
    {

        PhysicalElectricityShapeFactory circuitShapeFacotory = new PhysicalElectricityShapeFactory();

        // 定义四行分别的第一个位置坐标
        PointF position1 = new PointF(200, 100);
        PointF position2 = new PointF(200, 200);
        PointF position3 = new PointF(200, 300);
        PointF position4 = new PointF(200, 400);

        private Dictionary<string, PointF> circuitDiagram = new Dictionary<string, PointF>();  // 记录电路中的元件和它的中心点坐标位置
        private Dictionary<PointF, List<PointF>> circutiConnections = new Dictionary<PointF, List<PointF>>(); // 记录元件连接点的信息
        private Dictionary<string, string> nameTransformDic = new Dictionary<string, string>();
        private Dictionary<string, PhysicalElectricityGdi> nameToComponent= new Dictionary<string, PhysicalElectricityGdi>();
        private List<PointF> rowLeftConnections = new List<PointF>(); // 记录每一行的第一个元件的左连接点

        // 这里设为600 400 image.width设为400 是清晰图片，
        static readonly Bitmap bmp = new Bitmap(600, 500);
        static readonly Graphics g = Graphics.FromImage(bmp);
        const string imagePath = @"C:\Users\10114\Desktop\大论文\20221204老师项目\老师项目\image\test.png";





        public CircuitGerneration()
        {


            nameTransformDic.Add("电压表", "Voltmeter");
            nameTransformDic.Add("电流表", "Ammeter");
            nameTransformDic.Add("电阻", "Resistance");
            nameTransformDic.Add("滑动变阻器", "SlidingRheostat");
            nameTransformDic.Add("开关", "Switch");
            nameTransformDic.Add("电源", "Power");
        }

        

        // 解析语义网
        public void circuitDraw(ConclusionTopicModule topicModule, KRModuleSNet net)
        {
            // 拿到 图 这个节点
            SNNode graphNode = topicModule.GetGraphNode();
            // 拿到当前结论语义网
            ConclusionKRModuleSNet currentSNet = new ConclusionKRModuleSNet(net.Net);
            SNNode imageNode = currentSNet.Net.GetOutgoingDestination(graphNode, "ISA", "ISA");


            if (imageNode.Name.Equals("物理图"))
            {

                try 
                {
                    // 测试：层序遍历
                    Queue<SNNode> q = new Queue<SNNode>();
                    q.Enqueue(imageNode);
                    int currentLayer = 1; // 因为只有第一层是连接上面是数字，其他层包含中文
                    while (q.Count != 0)
                    {
                        int size = q.Count;
                        for (int i = 0; i < size; i++)
                        {
                            SNNode nodeFrom = q.Dequeue();
                            // 拿到从物理图走出去的节点
                            List<SNNode> outNeighborNodes = net.Net.GetOutNeighbors(nodeFrom);


                            // 并且把这些节点的语义连接上的数字提取出来
                            foreach (SNNode nodeTo in outNeighborNodes)
                            {
                                // 先加入队列
                                q.Enqueue(nodeTo);
                                //提取 节点间的关系
                                string label = net.Net.Rational(nodeFrom, nodeTo).Label;


                                // 先碰到电阻，不能画，因为下面数字要用来分行的，这里电阻是并联，所以不能确定是在哪一行
                                if (label.Equals("") && net.Net.Rational(nodeFrom, nodeTo).StartMulti.Equals("") && net.Net.Rational(nodeFrom, nodeTo).EndMulti.Equals(""))
                                {
                                    // 碰到空标签，说明这里不用确定在哪一行位置
                                    continue;
                                }
                                else if (Regex.IsMatch(label, @"[\u4e00-\u9fbb]+"))
                                {


                                    // 如果该连接上写到是中文 比如并联 那第一个节点肯定是之前已经画过的，所以这里我们直接跳过
                                    // 但是要绘制并联的元件 这里我们约定画在元件上方  因为我们是首先绘制串联电路 
                                    PointF position = new PointF(circuitDiagram[nameTransformDic[nodeFrom.Name]].X, circuitDiagram[nameTransformDic[nodeFrom.Name]].Y - 50);
                                    string currentShapeEnglishName = nameTransformDic[nodeTo.Name];
                                    PhysicalElectricityGdi currentComponent = circuitShapeFacotory.getPhysicalShape(g, currentShapeEnglishName, position.X, position.Y); // 绘制并联元件
                                    circuitDiagram.Add(currentShapeEnglishName, position); // 把元件名字 和中心点坐标添加进去
                                    string leftConnectedPointIndex = net.Net.Rational(nodeFrom, nodeTo).StartMulti;
                                    string rightConnectedPointIndex = net.Net.Rational(nodeFrom, nodeTo).EndMulti;

                                    // 判断空字符 记录连接点信息
                                    if (!string.IsNullOrEmpty(leftConnectedPointIndex))
                                    {
                                        int leftIndex = Convert.ToInt32(leftConnectedPointIndex);
                                        // 记录链接点信息
                                        // 把当前连接对信息添加进去 这里因为是处理并联关系，所以我们约定这里并联的时候并不是直接连接在并联元件两边两个连接点
                                        // 我们这里把连接点往里面减少一点
                                        PointF parallelPoint = new PointF(nameToComponent[nodeFrom.Name].connectPoints[0].X, nameToComponent[nodeFrom.Name].connectPoints[0].Y);
                                        if (circutiConnections.ContainsKey(parallelPoint))
                                        {
                                            // 如果已经存在当前key
                                            circutiConnections[parallelPoint].Add(currentComponent.connectPoints[leftIndex]);
                                        }
                                        else
                                        {
                                            // 如果没有 则新建key 
                                            circutiConnections.Add(parallelPoint, new List<PointF>() { currentComponent.connectPoints[leftIndex] });
                                        }


                                    }
                                    if (!string.IsNullOrEmpty(rightConnectedPointIndex))
                                    {
                                        // 表示右边连接的是下一个节点的 rightIndex 索引上的连接点
                                        int rightIndex = Convert.ToInt32(rightConnectedPointIndex);
                                        // 把当前连接对信息添加进去
                                        PointF parallelPoint = new PointF(nameToComponent[nodeFrom.Name].connectPoints[1].X, nameToComponent[nodeFrom.Name].connectPoints[1].Y);
                                        if (circutiConnections.ContainsKey(parallelPoint))
                                        {
                                            // 如果已经存在当前key
                                            circutiConnections[parallelPoint].Add(currentComponent.connectPoints[rightIndex]);
                                        }
                                        else
                                        {
                                            // 如果没有 则新建key 
                                            circutiConnections.Add(parallelPoint, new List<PointF>() { currentComponent.connectPoints[rightIndex] });
                                        }


                                    }

                                }
                                else if (currentLayer <= 1)
                                {
                                    // 在第一层 拿到数字
                                    int number = Convert.ToInt32(label);
                                    if (number == 1)
                                    {

                                        // 表示是在第一层的元件，从上往下,初始化坐标
                                        PointF position = position1;
                                        string currentShapeEnglishName = nameTransformDic[nodeTo.Name];
                                        circuitDiagram.Add(currentShapeEnglishName, position); // 把元件名字 和中心点坐标添加进去
                                        if (!nameToComponent.ContainsKey(currentShapeEnglishName))
                                        {
                                            // 如果该元件没有绘制，就绘制
                                            PhysicalElectricityGdi component = circuitShapeFacotory.getPhysicalShape(g, currentShapeEnglishName, position.X, position.Y);
                                            nameToComponent.Add(nodeTo.Name, component);
                                        }

                                        // 这一行的左边连接点
                                        float x = nameToComponent[nodeTo.Name].connectPoints[0].X;
                                        float y = nameToComponent[nodeTo.Name].connectPoints[0].Y;
                                        PointF currentRowInitialPosition = new PointF(x, y);
                                        rowLeftConnections.Add(currentRowInitialPosition); // 记录当前行的左连接点坐标 用来最后绘制
                                    }
                                    else if (number == 2)
                                    {
                                        // 第二行元件起始坐标
                                        PointF position = position2;
                                        string currentShapeEnglishName = nameTransformDic[nodeTo.Name];
                                        circuitDiagram.Add(currentShapeEnglishName, position); // 把元件名字 和中心点坐标添加进去
                                        if (!nameToComponent.ContainsKey(currentShapeEnglishName))
                                        {
                                            // 如果该元件没有绘制，就绘制
                                            PhysicalElectricityGdi component = circuitShapeFacotory.getPhysicalShape(g, currentShapeEnglishName, position.X, position.Y);
                                            nameToComponent.Add(nodeTo.Name, component);
                                        }

                                        // 这一行的左边连接点
                                        float x = nameToComponent[nodeTo.Name].connectPoints[0].X;
                                        float y = nameToComponent[nodeTo.Name].connectPoints[0].Y;
                                        PointF currentRowInitialPosition = new PointF(x, y);
                                        rowLeftConnections.Add(currentRowInitialPosition); // 记录当前行的左连接点坐标 用来最后绘制
                                    }
                                    else if (number == 3)
                                    {
                                        // 第三行元件起始坐标
                                        PointF position = position3;
                                        string currentShapeEnglishName = nameTransformDic[nodeTo.Name];
                                        circuitDiagram.Add(currentShapeEnglishName, position); // 把元件名字 和中心点坐标添加进去
                                        if (!nameToComponent.ContainsKey(currentShapeEnglishName))
                                        {
                                            // 如果该元件没有绘制，就绘制
                                            PhysicalElectricityGdi component = circuitShapeFacotory.getPhysicalShape(g, currentShapeEnglishName, position.X, position.Y);
                                            nameToComponent.Add(nodeTo.Name, component);
                                        }

                                        // 这一行的左边连接点
                                        float x = nameToComponent[nodeTo.Name].connectPoints[0].X;
                                        float y = nameToComponent[nodeTo.Name].connectPoints[0].Y;
                                        PointF currentRowInitialPosition = new PointF(x, y);
                                        rowLeftConnections.Add(currentRowInitialPosition); // 记录当前行的左连接点坐标 用来最后绘制
                                    }
                                    // 直接用第一个链接点 连接到最后一个链接点  rowConnections
                                    // 前提是保证每一行第一个元件的左连接点是竖直对齐 这里我们在GID库中定义元件类的时候可以直接控制
                                    //circutiConnections.Add(rowLeftConnections[0], new List<PointF>() { rowLeftConnections[rowLeftConnections.Count - 1] });
                                }
                                else if (currentLayer > 1)
                                {
                                    // 当currentLayer 来到第二层 先计算这个元件中心坐标
                                    // 先拿到前一个元件中心点坐标 这个400是前一个元件中心点坐标往右移400的距离
                                    PointF position = new PointF(circuitDiagram[nameTransformDic[nodeFrom.Name]].X + 100, circuitDiagram[nameTransformDic[nodeFrom.Name]].Y);
                                    // 绘制当前图形
                                    string currentShapeEnglishName = nameTransformDic[nodeTo.Name];
                                    if (!nameToComponent.ContainsKey(nodeTo.Name))
                                    {
                                        // 如果该元件没有绘制，就绘制
                                        PhysicalElectricityGdi component = circuitShapeFacotory.getPhysicalShape(g, currentShapeEnglishName, position.X, position.Y);
                                        nameToComponent.Add(nodeTo.Name, component);
                                        circuitDiagram.Add(currentShapeEnglishName, position); // 把元件名字 和中心点坐标添加进去
                                    }
                                    // 记录链接点
                                    // startRole 是拿到两个节点之间的链接上的前一个节点的补充 StartRole表示当前元件左连接点  EndRole表示右
                                    // startRole上的值 是连接的对方元件索引  值为0 表示对方的左 1表示对方的右
                                    string leftConnectedPointIndex = net.Net.Rational(nodeFrom, nodeTo).StartMulti;
                                    string rightConnectedPointIndex = net.Net.Rational(nodeFrom, nodeTo).EndMulti;

                                    // 判断空字符
                                    if (!string.IsNullOrEmpty(leftConnectedPointIndex))
                                    {
                                        int leftIndex = Convert.ToInt32(leftConnectedPointIndex);
                                        // 记录链接点信息
                                        // 把当前连接对信息添加进去
                                        // 看下判断当前连接点是否已经有其他连接点，如果有直接添加 因为只能包含一个key 不能有相同的key
                                        if (circutiConnections.ContainsKey(nameToComponent[nodeFrom.Name].connectPoints[0]))
                                        {
                                            PointF key = nameToComponent[nodeFrom.Name].connectPoints[0];
                                            circutiConnections[key].Add(nameToComponent[nodeTo.Name].connectPoints[leftIndex]);
                                        }
                                        else
                                        {
                                            // 如果这个key 还没有存在，那么就新建
                                            circutiConnections.Add(nameToComponent[nodeFrom.Name].connectPoints[0], new List<PointF>() { nameToComponent[nodeTo.Name].connectPoints[leftIndex] });

                                        }

                                    }
                                    if (!string.IsNullOrEmpty(rightConnectedPointIndex))
                                    {
                                        // 表示右边连接的是下一个节点的 rightIndex 索引上的连接点
                                        int rightIndex = Convert.ToInt32(rightConnectedPointIndex);
                                        // 看下判断当前连接点是否已经有其他连接点，如果有直接添加 因为只能包含一个key 不能有相同的key
                                        if (circutiConnections.ContainsKey(nameToComponent[nodeFrom.Name].connectPoints[1]))
                                        {
                                            PointF key = nameToComponent[nodeFrom.Name].connectPoints[1];
                                            circutiConnections[key].Add(nameToComponent[nodeTo.Name].connectPoints[rightIndex]);
                                        }
                                        else
                                        {
                                            // 如果不包含 则新建 把当前连接对信息添加进去
                                            circutiConnections.Add(nameToComponent[nodeFrom.Name].connectPoints[1], new List<PointF>() { nameToComponent[nodeTo.Name].connectPoints[rightIndex] });

                                        }

                                    }
                                }



                            }
                            // 说明这一层走完，下一层就是拿到链接点信息
                            currentLayer++;
                        }
                    }
                    if(circutiConnections.ContainsKey(rowLeftConnections[0]))
                    {
                        circutiConnections[rowLeftConnections[0]].Add(rowLeftConnections[rowLeftConnections.Count - 1]);
                    } else
                    {
                        circutiConnections.Add(rowLeftConnections[0], new List<PointF>() { rowLeftConnections[rowLeftConnections.Count - 1] });
                    }
                   

                    // 绘制连接点
                    DrawConnectionPoint(g);

                   
                } catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
              
               

            }
   
        }

        // 绘制连接点
        public void  DrawConnectionPoint(Graphics g)
        {

            // 创建画笔
            Pen pen = new Pen(Color.Black);
            foreach(KeyValuePair<PointF, List<PointF>> connectionPair in circutiConnections)
            {
                foreach(PointF point in connectionPair.Value)
                {
                    if (connectionPair.Key.Y == point.Y)
                    {
                        // 如果两个点是同一水平，那么直接相连
                        g.DrawLine(pen, connectionPair.Key, point); 
                    } else if(connectionPair.Key.X == point.X)
                    {
                        // 如果是同一竖直方法，直接相连
                        g.DrawLine(pen, connectionPair.Key, point);
                    } else
                    {
                        // 确定路径 拐角
                        PointF cornerPoint;
                        if (connectionPair.Key.X < point.X)
                            cornerPoint = new PointF(point.X, connectionPair.Key.Y);
                        else
                            cornerPoint = new PointF(connectionPair.Key.X, point.Y);

                        //// 如果连接点 也就是 key 对应的 连接点 Y 相比 key 在 要连接的点下边
                        //if (connectionPair.Key.Y > point.Y)
                        //    // 这里处理并联的时候，我们直接把并联在下面的元件两边的向外延线长度 高于 上面的元件，定义好了在构造连接点的时候
                        //    cornerPoint = new PointF(connectionPair.Key.X, point.Y);
                        //else if(connectionPair.Key.X < point.X)
                        //{
                        //    // 这里处理每行最后一个元件的连接，连接起来形成一个环
                        //    cornerPoint = new PointF(point.X, connectionPair.Key.Y);
                        //}
                            

                        // 连接路径
                        g.DrawLine(pen, connectionPair.Key, cornerPoint);
                        g.DrawLine(pen, cornerPoint, point);

                    }
                    
                }
            }

            bmp.Save(imagePath);

        }


        public class PhysicalElectricityShapeFactory
        {
            public PhysicalElectricityGdi getPhysicalShape(Graphics graphic, string shapeType, float x, float y)
            {
                if (shapeType == null)
                    return null;
                else if (shapeType == "Ammeter")
                    return new Ammeter(graphic, x, y);
                else if (shapeType == "Voltmeter")
                    return new Voltmeter(graphic, x, y);
                else if (shapeType == "Resistance")
                    return new Resistance(graphic, x, y);
                else if (shapeType == "Switch")
                    return new Switch(graphic, x, y);
                else if (shapeType == "Power")
                    return new Power(graphic, x, y);
                else if (shapeType == "SlidingRheostat")
                    return new SlidingRheostat(graphic, x, y);
                return null;


            }
        }

    }



}
