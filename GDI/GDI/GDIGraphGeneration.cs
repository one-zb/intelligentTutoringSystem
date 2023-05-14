using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GDI
{
     public class GDIGraphGeneration
    {
        ShapeFactory shapeFactory = new ShapeFactory();
        GDIGraphNode currentGDIGraph = new GDIGraphNode();
        DeviceGraph deviceGraph = new DeviceGraph();
        Dictionary<string, List<string>> nameTransformDic = new Dictionary<string, List<string>>();
        Dictionary<string, string> nameTransformDic2 = new Dictionary<string,string>();
        List<string> shapeList = new List<string>();
        public GDIGraphGeneration() 
        {
            //中文名称到相关类名称的转换
            nameTransformDic.Add("烧杯", new List<string>() { "Beaker", "0", "0" });//第一个系数 为图元名称 后面为 mode 具体的图元子类的选择
            nameTransformDic.Add("石棉网", new List<string>() { "AsbestosNet", "0", "0" });
            nameTransformDic.Add("酒精灯", new List<string>() { "AlcoholLamp", "0", "0" });
            nameTransformDic.Add("铁架台", new List<string>() { "IronSupport", "0", "0" });
            nameTransformDic.Add("反应瓶", new List<string>() { "Flask", "0", "0" });//第一个原始图元 mode必须为0
            nameTransformDic.Add("锥形瓶", new List<string>() { "Flask", "3", "3" });
            nameTransformDic.Add("圆底烧瓶", new List<string>() { "Flask", "1", "1" });
            nameTransformDic.Add("圆底蒸馏烧瓶", new List<string>() { "Flask", "2", "2" });
            nameTransformDic.Add("广口瓶", new List<string>() { "Bottle", "0", "0" });
            nameTransformDic.Add("漏斗", new List<string>() { "Funnel", "0", "0" });
            nameTransformDic.Add("分液漏斗", new List<string>() { "Funnel", "1", "1" });
            nameTransformDic.Add("长颈漏斗", new List<string>() { "Funnel", "2", "2" });
            nameTransformDic.Add("玻璃棒", new List<string>() { "GlassRod", "0", "0" });
            nameTransformDic.Add("玻璃管", new List<string>() { "GlassTube", "0", "0" });
            nameTransformDic.Add("短玻璃管", new List<string>() { "GlassTube", "0", "0" });
            nameTransformDic.Add("长玻璃管", new List<string>() { "GlassTube", "1", "1" });
            nameTransformDic.Add("试管", new List<string>() { "TestTube", "0", "0" });
            nameTransformDic.Add("三口烧瓶", new List<string>() { "ThreeNeckedFlask", "0", "0" });
            nameTransformDic.Add("u型管", new List<string>() { "U_Tube", "0", "0" });

            //英文到中文
            nameTransformDic2.Add("Beaker","烧杯");//第一个系数 为图元名称 后面为 mode 具体的图元子类的选择
            nameTransformDic2.Add("AsbestosNet","石棉网");
            nameTransformDic2.Add("AlcoholLamp","酒精灯");
            nameTransformDic2.Add("IronSupport","铁架台");
            nameTransformDic2.Add( "Flask","反应瓶");
            nameTransformDic2.Add( "Bottle","广口瓶");
            nameTransformDic2.Add( "Funnel","漏斗");
            nameTransformDic2.Add(  "GlassRod","玻璃棒");
            nameTransformDic2.Add( "GlassTube","玻璃管");
            nameTransformDic2.Add("TestTube", "试管"); //U_Tube
            nameTransformDic2.Add("ThreeNeckedFlask", "三口烧瓶");
            nameTransformDic2.Add("U_Tube", "u型管");

            //已有的组合图元
            shapeList.Add("IronSupport_Flask"); // 铁架台 反应瓶
            shapeList.Add("IronSupport_AlcoholLamp"); // 铁架台 酒精灯
            shapeList.Add("IronSupport_AlcoholLamp_AsbestosNet"); // 铁架台 酒精灯 石棉网
            shapeList.Add("IronSupport_Flask_Funnel"); // 铁架台 反应瓶 漏斗
            shapeList.Add("IronSupport_Flask_Funnel_AlcoholLamp_AsbestosNet"); // 铁架台 反应瓶 漏斗 酒精灯 石棉网
            shapeList.Add("IronSupport_Funnel_TestTube"); // 铁架台 漏斗 试管
            shapeList.Add("Flask_Funnel"); // 反应瓶 漏斗
            shapeList.Add("TestTube_Funnel"); // 试管 漏斗
            shapeList.Add("GlassTube_Flask_GlassTube"); // 玻璃管 反应瓶 玻璃管
            shapeList.Add("GlassTube_Bottle_GlassTube"); // 玻璃管 广口瓶 玻璃管
            shapeList.Add("Beaker_GlassRod"); // 烧杯 玻璃棒
            shapeList.Add("IronSupport_Flask_AlcoholLamp_AsbestosNet"); // 铁架台 反应瓶 酒精灯 石棉网
            shapeList.Add("IronSupport_Flask_Funnel_AlcoholLamp"); // 铁架台 反应瓶 漏斗 酒精灯
            shapeList.Add("IronSupport_Flask_AlcoholLamp"); // 铁架台 反应瓶 酒精灯
            //shapeList.Add("TestTube_GlassTube"); // 试管 玻璃管  这个暂时不要组合 直接按个独立生成即可 

        }
        //匹配备选队列最佳图元  选择出对列中包含用户要画的图形最多的组合图形 
        private List<string> getLongGraph(Queue<List<string>> myQueue,List<string> gdiGraph,int index) 
        {
            List<string> alternativeGraph = null;
            List<string> tempGraph;
            int max=int.MinValue;

            //当队列中包含组合图形 进入循环
            while (myQueue.Count!=0)
            {

                int indexTemp = index; // 用户传进来的的i 
                int myAux = 1;

                //开始出队 拿出队首元素 就是第一个入队的组合图形
                tempGraph = myQueue.Dequeue();

                /*  
                    队列中的对象格式为  { shapeList[j],equipInfo[1],equipInfo[2]});
                    [组合图形("IronSupport_Flask")  "0", "0" ]  对组合图形字符串进行分割 
                    
                */
                List<string> temp1 = tempGraph[0].Split('_').ToList();

                //判断组合图形英文 IronSupport_Flask 分割后的 单词（单个图元）数量 是否大于等于用书传入数组长度 
                if (temp1.Count >= (index+1)) continue; // index + 1 = 用户传入的数组长度
                for (int i = 0; i < temp1.Count; i++)
                {

                    //遍历分割后的组合图元 temp  里面分割后的是单个英文单词对象
                    if (temp1.Contains(nameTransformDic[gdiGraph[indexTemp]][0])) //这里括号里拿出来的就是用户刚传进来的图元数组里最后一个图元英文单词
                    {
                       
                        //indexTemp= index = i(用户传进来遍历那个图元数组下标)
                        //nameTransformDic<k,v> v还是list集合 ("铁架台", new List<string>() { "IronSupport", "0", "0" });
                        //[gdiGraph[indexTemp] 就是一开始遍历的数组最后那个图元
                        

                         //如果分割后这个单词等于用户传进来数组中后序遍历要画那个单个图元
                        //就把这个单词在 分割后单词集合temp1中记下 下标
                        int arrIndex = temp1.IndexOf(nameTransformDic[gdiGraph[indexTemp]][0]);
                        if (nameTransformDic[gdiGraph[indexTemp]][1] != "0")//负责组合图形里图元子类选择
                        {
                            // 这就是nameTransformDic 的 V 值数组 { "IronSupport", "0", "0" }
                           
                            if (arrIndex == 0)
                                arrIndex = 1;
                            else if (arrIndex > 2)
                            { 
                                arrIndex = myAux;
                                ++myAux;
                            }
                            tempGraph[arrIndex] = nameTransformDic[gdiGraph[indexTemp]][1];
                        }

                        //temp1 是分割后的单词集合 移除这个一判断的过的单词
                        temp1.Remove((nameTransformDic[gdiGraph[indexTemp]][0]));

                        //在这个单词集合插入这个判断过单词下标对应的单词
                        temp1.Insert(arrIndex, "");

                        //然后把用户传进来的数组最后一个图元下标自减  
                        //然后继续循环 拿分割后单词集合 跟用户传进来的数组倒数第二个开始判断 是否在这个单词集合中 
                        indexTemp--;
                    }
                    else
                        break;
                }

                //所以上面for循环遍历完所有里面的分割完一个组合图形的单词后  temp1 里面存着含有用户要画的图形英文单词 
                if (index - indexTemp == temp1.Count && (index - indexTemp) > max)
                {

                    /*
                        temp1.count 在遍历完单词集合后 里面就剩下了符合要画的的图形
                        英文单词个数  也就是组合图形里包含用户指定要画的图形的个数
                        这一步是选出队列中包含要用户画的图形个数最多的组合图形

                     */
               
                    //max 就是包含组合图形里符合要求的图形个数 也肯定是最多的
                     max = index - indexTemp;
                    //
                     alternativeGraph = tempGraph;
                }
            }
            return alternativeGraph;
        }

        /* 
         * 用户输入
         * { "二氧化硫制备实验反应装置", "广口瓶", "玻璃管", "广口瓶", "玻璃管", 
        "广口瓶", "玻璃管", "广口瓶", "玻璃管", "石棉网", "酒精灯", "分液漏斗", "锥形瓶", "铁架台" };
        */

        public void Draw(Graphics g, List<string> gdiGraph) //gdiGraph索引为0是图的名称
        {
            Queue<List<string>> gdiQueue = new Queue<List<string>>();
            List<string> alternativeGraph;

            //对于用户传入的图器件数组进行后序遍历 先拿出数组最后一个图元
            for (int i = gdiGraph.Count-1; i >= 0; --i)
            {
                
                //用户传进来的中文 这里转换成英文 从 MAP集合中取出 该图元对象信息
                List<string> equipInfo = nameTransformDic[gdiGraph[i]];  // new List<string>() { "IronSupport", "0", "0" })

                //选中可能的组合图形入队 从已有的组合图形中遍历是否含有这个拿出来的图元
                //这个for循环结束后 会拿到一个 包含这个图元的索引组合图形 队列
                for (int j = 0; j < shapeList.Count ; j++)
                {
                    if (shapeList[j].Contains(equipInfo[0]))
                    {
                        /*如果这个组合图形中包含这个图元  new List<string>() { "IronSupport", "0", "0" }))
                          就将这个组合图形 和 这个图元的 { "IronSupport", "0", "0" } 后面两个系数拿出来 当做一个对象入队
                         */
                        
                        gdiQueue.Enqueue(new List<string>(){ shapeList[j],equipInfo[1],equipInfo[2]});
                    }

                    
                    
                }

                 
                //从这个包含图元的所有组合图形的队列 选取最长的的组合图形 i是传入的图元数组最后一个下标
                alternativeGraph = getLongGraph(gdiQueue, gdiGraph, i);

                if (alternativeGraph==null)// Todo 画单个图像
                {
                    //如果当前画板中没有图形 也就是没有没有连接点
                    if (currentGDIGraph.connectPointsDic.Count == 0) 
                    {
                        //初始化第一个实例的器材  equipInfo集合数组： new List<string>() { "IronSupport", "0", "0" }
                        ChemistryGdi shape = shapeFactory.getShape(g, equipInfo[0], 100, 300, int.Parse(equipInfo[1]), int.Parse(equipInfo[2]));
                        List<PointF> connectPs = shape.connectPoints;

                        // Dictionary<string, List<PointF>> connectPointsDic
                        currentGDIGraph.connectPointsDic.Add(equipInfo[0], connectPs); 
                    }
                    else
                    {
                        //如果当前已经有图形了 那么就遍历已经画出来的图形的连接点
                        foreach (KeyValuePair<string, List<PointF>> res in currentGDIGraph.connectPointsDic)
                        {
                            // Dictionary<string, string> nameTransformDic2  这是键值对集合 图形英文名为key 中文名为value
                            if (deviceGraph.IsConnect(nameTransformDic2[equipInfo[0]], nameTransformDic2[res.Key]))
                            {
                                //这个 if 是判断当前拿到的图形与 图中已经画出来的图形 是否能链接
                                List<PointF> p = GDIAuxiliary.GetInstance().gdiDic[equipInfo[0]];
                                ChemistryGdi shape = shapeFactory.getShape(g, equipInfo[0], currentGDIGraph.connectPointsDic[res.Key][0].X - p[0].X, currentGDIGraph.connectPointsDic[res.Key][0].Y - p[0].Y, int.Parse(equipInfo[1]), int.Parse(equipInfo[2]));
                                currentGDIGraph.connectPointsDic.Remove(res.Key);
                                currentGDIGraph.connectPointsDic.Add(equipInfo[0], new List<PointF>() { shape.connectPoints[1] });
                                break;
                            }
                            else
                            {
                                //throw new ArgumentNullException(equipInfo[0] + "与" + res.Key + "不可组装");
                            }
                        }
                    }
                }
                else //todo 画组合图像
                {

                    // alternativeGraph 集合三个对象对象格式：[组合图形("IronSupport_Flask")  "0", "0" ]
                    //确认选择哪个组合图元 例如这里传进来三个元件 0 1 2  然后刚好后面两个是组合图形，那么下一个循环i 就要从 0 开始遍历绘制
                    i = i - alternativeGraph[0].Split('_').ToList().Count + 1;//如果i=0说明还剩 索引为0 这第一个元件需要绘制了 但是要记住 这个i 到下一轮循环会自动减1，这是在for循环里，所以要加1 千万要注意
                    if (currentGDIGraph.connectPointsDic.Count==0)//没有联接点说明，是第一个画的图元
                    {
                        ChemistryGdi shape = shapeFactory.getShape(g, alternativeGraph[0], 100, 300,int.Parse(alternativeGraph[1]),int.Parse( alternativeGraph[2]));//初始化第一个实例的器材
                        currentGDIGraph.connectPointsDic = shape.connectPointsDic; // Dictionary<string, List<PointF>> connectPointsDic
                    }
                    else
                    {
                        foreach (KeyValuePair<string, List<PointF>> res in currentGDIGraph.connectPointsDic)
                        {
                            if (deviceGraph.IsConnect(nameTransformDic2[equipInfo[0]], nameTransformDic2[res.Key]))
                            {
                                List<PointF> p = GDIAuxiliary.GetInstance().gdiDic[equipInfo[0]];
                                ChemistryGdi shape = shapeFactory.getShape(g, alternativeGraph[0], currentGDIGraph.connectPointsDic[res.Key][0].X - p[0].X, currentGDIGraph.connectPointsDic[res.Key][0].Y - p[0].Y, int.Parse(alternativeGraph[1]), int.Parse(alternativeGraph[2]));
                                currentGDIGraph.connectPointsDic.Remove(res.Key);
                                currentGDIGraph.connectPointsDic.Add(equipInfo[0], new List<PointF>() { shape.connectPointsDic[equipInfo[0]][1] });
                                break;
                            }
                            else
                            {
                                //throw new ArgumentNullException(equipInfo[0] + "与" + res.Key + "不可组装");
                            }
                        }
                    }
                }
            }
            // 在屏幕上显示传入进来的第一个仪器的名字
            //g.DrawString(gdiGraph[0], new Font("宋体", 20, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, 200, 20);
        }
        
    }

    //图形工厂  
    public class ShapeFactory 
    {
        public ChemistryGdi getShape(Graphics graphic,string shapeType,float x,float y,int mode1=0,int mode2=0) 
        {
            if (shapeType == null)
                return null;
            if (shapeType == "IronSupport")
                return new IronSupport(graphic, x, y);
            else if (shapeType == "Funnel")
                return new Funnel(graphic, x, y,mode1);
            else if (shapeType == "Beaker")
                return new Beaker(graphic, x, y);
            else if (shapeType == "Bottle")
                return new Bottle(graphic, x, y);
            else if (shapeType == "Flask")
                return new Flask(graphic, x, y,mode1);
            else if (shapeType == "TestTube")
                return new TestTube(graphic, x, y);
            else if (shapeType == "AlcoholLamp")
                return new AlcoholLamp(graphic, x, y);
            else if (shapeType == "GlassRod")
                return new GlassRod(graphic, x, y);
            else if (shapeType == "NarrowNeckedBottle")
                return new NarrowNeckedBottle(graphic, x, y);
            else if (shapeType == "GlassTube")
                return new GlassTube(graphic, x, y,mode1);
            else if (shapeType == "AsbestosNet")
                return new AsbestosNet(graphic, x, y);
            else if (shapeType == "ThreeNeckedFlask")
                return new ThreeNeckedFlask(graphic, x, y);
            else if (shapeType == "U_Tube")
                return new U_Tube(graphic, x, y);
            else if (shapeType == "IronSupport_Flask")
                return new IronSupport_Flask(graphic, x, y,mode1);
            else if (shapeType == "IronSupport_AlcoholLamp")
                return new IronSupport_AlcoholLamp(graphic, x, y, mode1);
            else if (shapeType == "IronSupport_AlcoholLamp_AsbestosNet")
                return new IronSupport_AlcoholLamp_AsbestosNet(graphic, x, y, mode1);
            else if (shapeType == "IronSupport_Flask_Funnel")
                return new IronSupport_Flask_Funnel(graphic, x, y,mode1,mode2);
            else if (shapeType == "IronSupport_Flask_AlcoholLamp")
                return new IronSupport_Flask_AlcoholLamp(graphic, x, y,mode1);
            else if (shapeType == "IronSupport_Flask_AlcoholLamp_AsbestosNet")
                return new IronSupport_Flask_AlcoholLamp_AsbestosNet(graphic, x, y,mode1);
            else if (shapeType == "IronSupport_Flask_Funnel_AlcoholLamp_AsbestosNet")
                return new IronSupport_Flask_Funnel_AlcoholLamp_AsbestosNet(graphic, x, y,mode1,mode2,true);
            else if (shapeType == "IronSupport_Flask_Funnel_AlcoholLamp")
                return new IronSupport_Flask_Funnel_AlcoholLamp(graphic, x, y, mode1, mode2);
            else if (shapeType == "IronSupport_Funnel_TestTube")
                return new IronSupport_Funnel_TestTube(graphic, x, y,mode1);
            else if (shapeType == "Flask_Funnel")
                return new Flask_Funnel(graphic, x, y,mode1,mode2);
            else if (shapeType == "GlassTube_Flask_GlassTube")
                return new GlassTube_Flask_GlassTube(graphic, x, y,mode1);
            else if (shapeType == "TestTube_Funnel")
                return new TestTube_Funnel(graphic, x, y,mode1);
            else if (shapeType == "TestTube_GlassTube")
                return new TestTube_GlassTube(graphic, x, y);
            else if (shapeType == "GlassTube_Bottle_GlassTube")
                return new GlassTube_Bottle_GlassTube(graphic, x, y);
            else if (shapeType == "Beaker_GlassRod")
                return new Beaker_GlassRod(graphic, x, y);
            return null;
        }
    }


    //图形节点
    public class GDIGraphNode 
    {
      public Dictionary<string, List<PointF>> connectPointsDic;//记录当前图形的可联接点
      public GDIGraphNode() 
      {
          connectPointsDic = new Dictionary<string, List<PointF>>();
      }

        //拿到图形连接点个数
      public int getPointCount()
      {
          int Count = 0;
          foreach (KeyValuePair<string,List<PointF>> res in connectPointsDic)
          {
              Count += res.Value.Count;
          }
          return Count;
      }
    }
}
