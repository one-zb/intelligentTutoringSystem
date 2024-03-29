﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using KRLab.Core.SNet;
using KRLab.DiagramEditor.Properties;
using KRLab.Translations;
using System.Runtime.Remoting;
using System.IO;

namespace KRLab.DiagramEditor.NetworkDiagram.Dialogs
{
    public class RelationTypeNode:TreeNode
    {
        TreeView _view;
        public RelationTypeNode(TreeView view,string name)
        {
            _view = view;

            this.Text = name; 
            TreeNode objNode = new TreeNode("描述对象");
            objNode.ToolTipText = "IS,ISA,ISC,AREC,ISO,ISP,ISPC,ATT,VAL,VALR,SYMB,ROLE";
            objNode.Collapse();
            TreeNode nodeObj1 = new TreeNode(SNRational.AREC);
            nodeObj1.ToolTipText = "群体的特征";
            TreeNode nodeObj2 = new TreeNode(SNRational.ATT);
            nodeObj2.ToolTipText = "表示对象的属性";
            TreeNode nodeObj3 = new TreeNode(SNRational.NAME);
            nodeObj3.ToolTipText = "实体-实体的名称";
            TreeNode nodeObj15 = new TreeNode(SNRational.DEF);
            nodeObj15.ToolTipText = "对象被定义为";
            TreeNode nodeObj4 = new TreeNode(SNRational.IS);
            nodeObj4.ToolTipText = "子类-父类";
            TreeNode nodeObj5 = new TreeNode(SNRational.ISA);
            nodeObj5.ToolTipText = "实例-类属";
            TreeNode nodeObj6 = new TreeNode(SNRational.ISC);
            nodeObj6.ToolTipText = "个体的特征";
            TreeNode nodeObj7 = new TreeNode(SNRational.ISO);
            nodeObj7.ToolTipText = "个体-整体，表示其中之一";
            TreeNode nodeObj8= new TreeNode(SNRational.ISP);
            nodeObj8.ToolTipText = "个体-整体，表示组成";
            TreeNode nodeObj9 = new TreeNode(SNRational.ISPG);
            nodeObj9.ToolTipText = "广义抽象的组成关系";
            TreeNode nodeObj14 = new TreeNode(SNRational.KTYPE);
            nodeObj14.ToolTipText = "对象的知识类型";
            TreeNode nodeObj16 = new TreeNode(SNRational.PROP);
            nodeObj16.ToolTipText = "对象的特性";
            TreeNode nodeObj17 = new TreeNode(SNRational.PROPR);
            nodeObj17.ToolTipText = "多个对象之间的特性";
            TreeNode nodeObj13 = new TreeNode(SNRational.ROLE);
            nodeObj13.ToolTipText = "对象的作用或功能";
            TreeNode nodeObj10 = new TreeNode(SNRational.VAL);
            nodeObj10.ToolTipText = "属性或变量所赋予的值，与ASSGN比较接近";
            TreeNode nodeObj11 = new TreeNode(SNRational.VALR);
            nodeObj11.ToolTipText = "属性或变量所赋值的值在一个区间";
            TreeNode nodeObj12 = new TreeNode(SNRational.SYMB);
            nodeObj12.ToolTipText = "对象的符号表示";
            objNode.Nodes.Add(nodeObj1);
            objNode.Nodes.Add(nodeObj2);
            objNode.Nodes.Add(nodeObj3);
            objNode.Nodes.Add(nodeObj15);
            objNode.Nodes.Add(nodeObj4);
            objNode.Nodes.Add(nodeObj5);
            objNode.Nodes.Add(nodeObj6);
            objNode.Nodes.Add(nodeObj7);
            objNode.Nodes.Add(nodeObj8);
            objNode.Nodes.Add(nodeObj9);
            objNode.Nodes.Add(nodeObj14);
            objNode.Nodes.Add(nodeObj16);
            objNode.Nodes.Add(nodeObj17);
            objNode.Nodes.Add(nodeObj13);
            objNode.Nodes.Add(nodeObj10);
            objNode.Nodes.Add(nodeObj11);
            objNode.Nodes.Add(nodeObj12);

            TreeNode actNode = new TreeNode("行为相关类型");
            actNode.ToolTipText = "ACTR,MACTR,AFFED,ACTED,CSTR,EXECR,AVRT,CHPE,CIRCU,ORNT,SUPPL,\n" +
                "TOOL,METH,MANNR,MODAL,MODE";
            actNode.Collapse();
            TreeNode nodeAct1 = new TreeNode(SNRational.ACTR);
            nodeAct1.ToolTipText = "行为-行为主体，主体的属性不会改变";
            TreeNode nodeAct2 = new TreeNode(SNRational.ACTED);
            nodeAct2.ToolTipText = "行为-行为客体";
            TreeNode nodeAct3 = new TreeNode(SNRational.AFFED);
            nodeAct3.ToolTipText = "行为-行为的客体，客体会被影响";
            TreeNode nodeAct4 = new TreeNode(SNRational.AVRT);
            nodeAct4.ToolTipText = "行为-对象转移，表达'从'的语义";
            TreeNode nodeAct5 = new TreeNode(SNRational.CHPE);
            nodeAct5.ToolTipText = "性质-性质相应的行为";
            TreeNode nodeAct6 = new TreeNode(SNRational.CIRCU);
            nodeAct6.ToolTipText = "行为所处的环境";
            TreeNode nodeAct7 = new TreeNode(SNRational.CSTR);
            nodeAct7.ToolTipText = "行为触发者";
            TreeNode nodeAct8 = new TreeNode(SNRational.EXECR);
            nodeAct8.ToolTipText = "行为主体，主体的属性会被改变";
            TreeNode nodeAct9 = new TreeNode(SNRational.GOAL);
            nodeAct9.ToolTipText = "表示意愿或希望";
            TreeNode nodeAct10 = new TreeNode(SNRational.MACTR);
            nodeAct10.ToolTipText = "情感行为-行为主体";
            TreeNode nodeAct11 = new TreeNode(SNRational.MANNR);
            nodeAct11.ToolTipText = "行为-行为方式或行为修饰词";
            TreeNode nodeAct12 = new TreeNode(SNRational.MODAL);
            nodeAct12.ToolTipText = "行为-行为的执行意义，表达情态";
            TreeNode nodeAct13 = new TreeNode(SNRational.METH);
            nodeAct13.ToolTipText = "行为-方法";
            TreeNode nodeAct14 = new TreeNode(SNRational.MODE);
            nodeAct14.ToolTipText = "行为执行方式";
            TreeNode nodeAct15 = new TreeNode(SNRational.ORNT);
            nodeAct15.ToolTipText = "动作或状态的针对对象";
            TreeNode nodeAct16 = new TreeNode(SNRational.RESULT);
            nodeAct16.ToolTipText = "行为或事件导致的结果";
            TreeNode nodeAct17 = new TreeNode(SNRational.SUPPL);
            nodeAct17.ToolTipText = "动作的内在对象";
            TreeNode nodeAct18 = new TreeNode(SNRational.TOOL);
            nodeAct18.ToolTipText = "使用的工具";
            actNode.Nodes.Add(nodeAct1);
            actNode.Nodes.Add(nodeAct2);
            actNode.Nodes.Add(nodeAct3);
            actNode.Nodes.Add(nodeAct4);
            actNode.Nodes.Add(nodeAct5);
            actNode.Nodes.Add(nodeAct6);
            actNode.Nodes.Add(nodeAct7);
            actNode.Nodes.Add(nodeAct8);
            actNode.Nodes.Add(nodeAct9);
            actNode.Nodes.Add(nodeAct10);
            actNode.Nodes.Add(nodeAct11);
            actNode.Nodes.Add(nodeAct12);
            actNode.Nodes.Add(nodeAct13);
            actNode.Nodes.Add(nodeAct14);
            actNode.Nodes.Add(nodeAct15);
            actNode.Nodes.Add(nodeAct16);
            actNode.Nodes.Add(nodeAct17);
            actNode.Nodes.Add(nodeAct18);

            TreeNode mathNode = new TreeNode("数学类型");
            mathNode.ToolTipText = "ASSGN,EXPR,COMP,OPPS,MAJ,MAJE,MIN,MINE,OPRND,OPRED";
            mathNode.Collapse();
            TreeNode nodeMath1 = new TreeNode(SNRational.OPRND);
            nodeMath1.ToolTipText = "运算符的操作数";
            TreeNode nodeMath2 = new TreeNode(SNRational.OPRED);
            nodeMath2.ToolTipText = "运算符的被操作数";
            TreeNode nodeMath3 = new TreeNode(SNRational.ASSGN);
            nodeMath3.ToolTipText = "给变量或参数赋值";
            TreeNode nodeMath4 = new TreeNode(SNRational.COMP);
            nodeMath4.ToolTipText = "数学量之间的比较，加标签>,<,>=,<=,=,...";
            TreeNode nodeMath5 = new TreeNode(SNRational.EXPR);
            nodeMath5.ToolTipText = "数学表达式";
            TreeNode nodeMath6 = new TreeNode(SNRational.INVR);
            nodeMath6.ToolTipText = "互逆关系，倒数";
            TreeNode nodeMath7 = new TreeNode(SNRational.MAJ);
            nodeMath7.ToolTipText = "集合的下线，大于";
            TreeNode nodeMath8 = new TreeNode(SNRational.MAJE);
            nodeMath8.ToolTipText = "集合的下线，大于等于";
            TreeNode nodeMath9 = new TreeNode(SNRational.MIN);
            nodeMath9.ToolTipText = "集合的上线，小于";
            TreeNode nodeMath10 = new TreeNode(SNRational.MINE);
            nodeMath10.ToolTipText = "集合的上线，小于等于";
            TreeNode nodeMath11 = new TreeNode(SNRational.MRESULT);
            nodeMath11.ToolTipText = "运算、函数或算法的输出";
            TreeNode nodeMath12 = new TreeNode(SNRational.OPPS);
            nodeMath12.ToolTipText = "对象2是对象1的相反数";
            TreeNode nodeMath13 = new TreeNode(SNRational.SUM);
            nodeMath13.ToolTipText = "对象1是由对象2相加而得到";
            TreeNode nodeMath14 = new TreeNode(SNRational.MULTI);
            nodeMath14.ToolTipText = "对象1是由对象2相乘而得到";
            TreeNode nodeMath15 = new TreeNode(SNRational.SUBM);
            nodeMath15.ToolTipText = "子集关系";
            TreeNode nodeMath16 = new TreeNode(SNRational.SUBME);
            nodeMath16.ToolTipText = "集合相等或包含关系";
            mathNode.Nodes.Add(nodeMath1);
            mathNode.Nodes.Add(nodeMath2); 
            mathNode.Nodes.Add(nodeMath3);
            mathNode.Nodes.Add(nodeMath4);
            mathNode.Nodes.Add(nodeMath5);
            mathNode.Nodes.Add(nodeMath6);
            mathNode.Nodes.Add(nodeMath7);
            mathNode.Nodes.Add(nodeMath8);
            mathNode.Nodes.Add(nodeMath9);
            mathNode.Nodes.Add(nodeMath10); 
            mathNode.Nodes.Add(nodeMath11);
            mathNode.Nodes.Add(nodeMath12);
            mathNode.Nodes.Add(nodeMath13);
            mathNode.Nodes.Add(nodeMath14);
            mathNode.Nodes.Add(nodeMath15);
            mathNode.Nodes.Add(nodeMath16);

            TreeNode compNode = new TreeNode("比较相关类型");
            compNode.ToolTipText = "ANLG,ANLG2,ANLG3,ANTO,COMP,CORR,OPPS,CNVRS,CONTR,COMPL,CONC,\n" +
                ",DIFF2,DIFF3,EQUL,EQUL2,EQUL3,SYNO";
            compNode.Collapse();
            TreeNode nodeComp1 = new TreeNode(SNRational.ANLG);
            nodeComp1.ToolTipText = "两个对象类似";
            TreeNode nodeComp2 = new TreeNode(SNRational.ANLG2);
            nodeComp2.ToolTipText = "表示所有对象在属性a方面都相近";
            TreeNode nodeComp3 = new TreeNode(SNRational.ANLG3);
            nodeComp3.ToolTipText = "对象1与对象2在属性a方面相近";
            TreeNode nodeComp4 = new TreeNode(SNRational.ANTO);
            nodeComp4.ToolTipText = "对象1和对象2相反";
            TreeNode nodeComp5 = new TreeNode(SNRational.CNVRS);
            nodeComp5.ToolTipText = "对象1与对象2语义上相反";
            TreeNode nodeComp6 = new TreeNode(SNRational.COMPL);
            nodeComp6.ToolTipText = "两个互补并且相反的对象关系";
            TreeNode nodeComp7 = new TreeNode(SNRational.CONTR);
            nodeComp7.ToolTipText = "性质1-性质2，语义相反";
            TreeNode nodeComp8 = new TreeNode(SNRational.CONC);
            nodeComp8.ToolTipText = "表达语义上的让步";
            TreeNode nodeComp9 = new TreeNode(SNRational.CORR);
            nodeComp9.ToolTipText = "对象1相当于或对应于对象2";
            TreeNode nodeComp10 = new TreeNode(SNRational.DIFF2);
            nodeComp10.ToolTipText = "所有对象在属性a上都不同";
            TreeNode nodeComp11 = new TreeNode(SNRational.DIFF3);
            nodeComp11.ToolTipText = "对象1与对象2在属性a方面不同";
            TreeNode nodeComp12 = new TreeNode(SNRational.EQUL);
            nodeComp12.ToolTipText = "两个对象相等";
            TreeNode nodeComp13 = new TreeNode(SNRational.EQUL2);
            nodeComp13.ToolTipText = "对象在某个属性方面都相等";
            TreeNode nodeComp14 = new TreeNode(SNRational.EQUL3);
            nodeComp14.ToolTipText = "对象1和对象2在某个属性方面相等";
            TreeNode nodeComp16 = new TreeNode(SNRational.SYMM);
            nodeComp16.ToolTipText = "对象1与对象2对称";
            TreeNode nodeComp15 = new TreeNode(SNRational.SYNO);
            nodeComp15.ToolTipText = "两个对象相同或等价";
            compNode.Nodes.Add(nodeComp1);
            compNode.Nodes.Add(nodeComp2);
            compNode.Nodes.Add(nodeComp3);
            compNode.Nodes.Add(nodeComp4);
            compNode.Nodes.Add(nodeComp5);
            compNode.Nodes.Add(nodeComp6);
            compNode.Nodes.Add(nodeComp7);
            compNode.Nodes.Add(nodeComp8);
            compNode.Nodes.Add(nodeComp9);
            compNode.Nodes.Add(nodeComp10);
            compNode.Nodes.Add(nodeComp11);
            compNode.Nodes.Add(nodeComp12);
            compNode.Nodes.Add(nodeComp13);
            compNode.Nodes.Add(nodeComp14);
            compNode.Nodes.Add(nodeComp16);
            compNode.Nodes.Add(nodeComp15);

            TreeNode timeSpaceNode = new TreeNode("时间和空间相关类型");
            timeSpaceNode.ToolTipText = "SPACE,LRANG,LOC,ORIGL,DIR,TIME,STRT,DUR,ANTE,DEST,TDEST,PATH";
            timeSpaceNode.Collapse();
            TreeNode nodeTs1 = new TreeNode(SNRational.ANTE);
            nodeTs1.ToolTipText = "时间上，对象1早于对象2";
            TreeNode nodeTs2 = new TreeNode(SNRational.DEST);
            nodeTs2.ToolTipText = "目的地";
            TreeNode nodeTs3 = new TreeNode(SNRational.DIR);
            nodeTs3.ToolTipText = "事件或对象的空间方向";
            TreeNode nodeTs4 = new TreeNode(SNRational.DUR);
            nodeTs4.ToolTipText = "事件或状态的持续时间";
            TreeNode nodeTs5 = new TreeNode(SNRational.GEOM);
            nodeTs5.ToolTipText = "几何体-几何体的元素";
            TreeNode nodeTs6 = new TreeNode(SNRational.LRANG);
            nodeTs6.ToolTipText = "对象1的空间范围";
            TreeNode nodeTs7 = new TreeNode(SNRational.LOC);
            nodeTs7.ToolTipText = "对象1的空间位置";
            TreeNode nodeTs14 = new TreeNode(SNRational.LOCA);
            nodeTs14.ToolTipText = "对象的抽象地址";
            TreeNode nodeTs8 = new TreeNode(SNRational.ORIGL);
            nodeTs8.ToolTipText = "事件或对象的起始空间位置";
            TreeNode nodeTs9 = new TreeNode(SNRational.PATH);
            nodeTs9.ToolTipText = "表示一条路径或经过的某个地点";
            TreeNode nodeTs10 = new TreeNode(SNRational.STRT);
            nodeTs10.ToolTipText = "事件或行为的开始时刻";
            TreeNode nodeTs11 = new TreeNode(SNRational.SPACE);
            nodeTs11.ToolTipText = "对象之间的空间关系";
            TreeNode nodeTs12 = new TreeNode(SNRational.TDEST);
            nodeTs12.ToolTipText = "事件的结束时刻";
            TreeNode nodeTs13 = new TreeNode(SNRational.TIME);
            nodeTs13.ToolTipText = "事件发生的时间";
            timeSpaceNode.Nodes.Add(nodeTs1);
            timeSpaceNode.Nodes.Add(nodeTs2);
            timeSpaceNode.Nodes.Add(nodeTs3);
            timeSpaceNode.Nodes.Add(nodeTs4);
            timeSpaceNode.Nodes.Add(nodeTs5);
            timeSpaceNode.Nodes.Add(nodeTs6);
            timeSpaceNode.Nodes.Add(nodeTs7);
            timeSpaceNode.Nodes.Add(nodeTs14);
            timeSpaceNode.Nodes.Add(nodeTs8);
            timeSpaceNode.Nodes.Add(nodeTs9);
            timeSpaceNode.Nodes.Add(nodeTs10);
            timeSpaceNode.Nodes.Add(nodeTs11);
            timeSpaceNode.Nodes.Add(nodeTs12);
            timeSpaceNode.Nodes.Add(nodeTs13);

            TreeNode assocNode = new TreeNode("关联相关类型");
            assocNode.ToolTipText = "ARGU,ARGV,ARGV2,ARGV3,FARGV,ASSOC,CAUSAL,CONFM,DEPT,ATTCH,\n" +
                "IMPL,MCONT,ORIG";
            assocNode.Collapse();
            TreeNode nodeAss1 = new TreeNode(SNRational.ARGU);
            nodeAss1.ToolTipText = "节点文本中包含某个可变化的概念";
            TreeNode nodeAss2 = new TreeNode(SNRational.ARGV);
            nodeAss2.ToolTipText = "节点文本中包含变量值";
            TreeNode nodeAss3 = new TreeNode(SNRational.ARGV2);
            nodeAss3.ToolTipText = "节点文本中包含二维变量值";
            TreeNode nodeAss4 = new TreeNode(SNRational.ARGV3);
            nodeAss4.ToolTipText = "节点文本中包含三维变量值";
            TreeNode nodeAss5 = new TreeNode(SNRational.FARGV);
            nodeAss5.ToolTipText = "函数或算法的输入参数";
            TreeNode nodeAss6 = new TreeNode(SNRational.ASSOC);
            nodeAss6.ToolTipText = "对象1与对象2相关联";
            TreeNode nodeAss7 = new TreeNode(SNRational.ATTCH);
            nodeAss7.ToolTipText = "对象1依附对象2，对象2是被依附的关系";
            TreeNode nodeAss8 = new TreeNode(SNRational.CAUSAL);
            nodeAss8.ToolTipText = "因果关系";
            TreeNode nodeAss9 = new TreeNode(SNRational.CONFM);
            nodeAss9.ToolTipText = "事件遵循的某种规律或计划";
            TreeNode nodeAss10 = new TreeNode(SNRational.DEPT);
            nodeAss10.ToolTipText = "行为或对象1依赖于行为或对象2";
            TreeNode nodeAss11 = new TreeNode(SNRational.GRANU);
            nodeAss11.ToolTipText = "指向语义细颗粒";
            TreeNode nodeAss12 = new TreeNode(SNRational.IMPL);
            nodeAss12.ToolTipText = "隐含关系";
            TreeNode nodeAss13 = new TreeNode(SNRational.MCONT);
            nodeAss13.ToolTipText = "主观认知所涉及的内容";
            TreeNode nodeAss14 = new TreeNode(SNRational.ORIG);
            nodeAss14.ToolTipText = "信息或知识的出处、来源";
            TreeNode nodeAss15 = new TreeNode(SNRational.JUST);
            nodeAss15.ToolTipText = "对象被证实或确认";
            assocNode.Nodes.Add(nodeAss1);
            assocNode.Nodes.Add(nodeAss2);
            assocNode.Nodes.Add(nodeAss3);
            assocNode.Nodes.Add(nodeAss4);
            assocNode.Nodes.Add(nodeAss5);
            assocNode.Nodes.Add(nodeAss6);
            assocNode.Nodes.Add(nodeAss7);
            assocNode.Nodes.Add(nodeAss8);
            assocNode.Nodes.Add(nodeAss9);
            assocNode.Nodes.Add(nodeAss10);
            assocNode.Nodes.Add(nodeAss11);
            assocNode.Nodes.Add(nodeAss12);
            assocNode.Nodes.Add(nodeAss15);
            assocNode.Nodes.Add(nodeAss13);
            assocNode.Nodes.Add(nodeAss14);

            TreeNode restrNode = new TreeNode("限制相关类型");
            restrNode.ToolTipText = "AMONG,RANGE,REF,COND,CONTXT,INIT,MANNR,MODAL,ORIGM,QMOD,NUM,NON,UNIT";
            restrNode.Collapse();
            TreeNode node9 = new TreeNode(SNRational.AMONG);
            node9.ToolTipText = "集体或集合元素之间";
            TreeNode node19 = new TreeNode(SNRational.COND);
            node19.ToolTipText = "事件或行为发生的条件";
            TreeNode node42 = new TreeNode(SNRational.CONTXT);
            node42.ToolTipText = "事件或行为的限制条件或针对特定事物";
            TreeNode node50 = new TreeNode(SNRational.INIT);
            node50.ToolTipText = "事件或对象的初始状态或初始值";
            TreeNode node94 = new TreeNode(SNRational.FINAL);
            node94.ToolTipText = "事件或对象的终止状态或最终值";
            TreeNode node90 = new TreeNode(SNRational.NUM);
            node90.ToolTipText = "表示数量";
            TreeNode node98 = new TreeNode(SNRational.NON);
            node98.ToolTipText = "否定";
            TreeNode node68 = new TreeNode(SNRational.ORIGM);
            node68.ToolTipText = "表示物体的材料构成，由什么材料制造的";
            TreeNode node71 = new TreeNode(SNRational.QMOD);
            node71.ToolTipText = "表示多少或程度，起修饰作用";
            TreeNode node20 = new TreeNode(SNRational.RANGE);
            node20.ToolTipText = "表示范围";
            TreeNode node21 = new TreeNode(SNRational.REF);
            node21.ToolTipText = "参考对象";
            TreeNode node91 = new TreeNode(SNRational.UNIT);
            node91.ToolTipText = "表示数量的单位";
            restrNode.Nodes.Add(node9);
            restrNode.Nodes.Add(node19);
            restrNode.Nodes.Add(node42);
            restrNode.Nodes.Add(node50);
            restrNode.Nodes.Add(node94);
            restrNode.Nodes.Add(node90);
            restrNode.Nodes.Add(node98);
            restrNode.Nodes.Add(node68);
            restrNode.Nodes.Add(node71);
            restrNode.Nodes.Add(node20);
            restrNode.Nodes.Add(node21);
            restrNode.Nodes.Add(node91);

            TreeNode fileNode = new TreeNode("使用外部数据");
            fileNode.Collapse();
            TreeNode nodeData1 = new TreeNode(SNRational.ANNIM);
            nodeData1.ToolTipText = "生成指定的动画";
            TreeNode nodeData2 = new TreeNode(SNRational.DRAW);
            nodeData2.ToolTipText = "实时绘制指定的图形";
            TreeNode nodeData3 = new TreeNode(SNRational.IMAGE);
            nodeData3.ToolTipText = "调用图片文件";
            TreeNode nodeData4 = new TreeNode(SNRational.SOUND);
            nodeData4.ToolTipText = "播放指定的声音";
            fileNode.Nodes.Add(nodeData1);
            fileNode.Nodes.Add(nodeData2);
            fileNode.Nodes.Add(nodeData3);
            fileNode.Nodes.Add(nodeData4);

            TreeNode otherNode = new TreeNode("其他类型");
            otherNode.ToolTipText = "AND,OR,HAS,BENF,TOOL,NAME,FORM,\n" +
                "SUBST,CHPS,CHSP";
            otherNode.Collapse();
            TreeNode nodeOth1 = new TreeNode(SNRational.BENF);
            nodeOth1.ToolTipText = "行为或事件的益处或受益方，语义上可以表达'为了...'"; 
            TreeNode nodeOth2 = new TreeNode(SNRational.AND);
            nodeOth2.ToolTipText = "表示'和'";
            TreeNode nodeOth3 = new TreeNode(SNRational.CHPA);
            nodeOth3.ToolTipText = "表示性质对应的属性";
            TreeNode nodeOth4 = new TreeNode(SNRational.CHPS);
            nodeOth4.ToolTipText = "性质-性质决定的状态";
            TreeNode nodeOth5 = new TreeNode(SNRational.CHSP1);
            nodeOth5.ToolTipText = "状态或行为-性质，主动关系";
            TreeNode nodeOth6 = new TreeNode(SNRational.CHSP2);
            nodeOth6.ToolTipText = "状态或行为-性质，被动关系";
            TreeNode nodeOth7 = new TreeNode(SNRational.FORM);
            nodeOth7.ToolTipText = "对象的表现形式";
            TreeNode nodeOth8 = new TreeNode(SNRational.HAS);
            nodeOth8.ToolTipText = "对象1拥有对象2";
            TreeNode nodeOth9 = new TreeNode(SNRational.OR);
            nodeOth9.ToolTipText = "表示'或'";
            TreeNode nodeOth10 = new TreeNode(SNRational.SUBST);
            nodeOth10.ToolTipText = "对象1代替对象2，语义上表达'是..,而不是...'";
            TreeNode nodeOth11 = new TreeNode(SNRational.SSPE);
            nodeOth11.ToolTipText = "表征状态或行为所具有的对象";
            otherNode.Nodes.Add(nodeOth1); 
            otherNode.Nodes.Add(nodeOth2);
            otherNode.Nodes.Add(nodeOth3);
            otherNode.Nodes.Add(nodeOth4);
            otherNode.Nodes.Add(nodeOth5);
            otherNode.Nodes.Add(nodeOth6);
            otherNode.Nodes.Add(nodeOth7);
            otherNode.Nodes.Add(nodeOth8);
            otherNode.Nodes.Add(nodeOth9);
            otherNode.Nodes.Add(nodeOth10);
            otherNode.Nodes.Add(nodeOth11);

            Nodes.Add(objNode);
            Nodes.Add(actNode);
            Nodes.Add(mathNode);
            Nodes.Add(compNode);
            Nodes.Add(timeSpaceNode);
            Nodes.Add(assocNode);
            Nodes.Add(restrNode);
            Nodes.Add(fileNode);
            Nodes.Add(otherNode);
        }
    }
}
