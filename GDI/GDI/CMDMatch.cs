using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace GDI
{
    /// <summary>
    /// 画图命令匹配画图方法
    /// </summary>
    public class CMDMatch
    {


        //public void GraphMethod()
        //{
        //    //创建InitilizeCmd类的对象，并初始化。
        //    InitializeCmd icmd = new InitializeCmd();


        //    //创建MethodInfo对象
        //    MethodInfo M;


        //    //以字符串数组的形式读取 textBox 用户输入的命令，命令之间用;分割
        //    //String[] inputCmd = Test.test.textBox1.Text.Trim().Split(';');

        //    // 测试从命令集合Hepler中取命令 实现画图 这个是GDI作为启动项目
        //    List<string> inputCmd = ImageSemanticNet.GetCmd();

           




        //    //遍历输入的命令数组,画图
        //    foreach (String cmd in inputCmd)
        //    {
                
        //        try
        //        {
        //            // segmentAB 截取 segmengt 表示画线段方法 就行了
        //            M = icmd.Cmd[Regex.Replace(cmd, @"[^a-z]+", "")];
        //            // cmd为segmentAB

        //            // 截取 segmentAB 截取 AB  用来表示线段两端的字母 传参
        //            string s = Regex.Replace(cmd, @"[^A-Z]+", "");
        //            char[] array = s.ToCharArray();
        //            if(array.Length == 1)
        //            {
        //                // 表示只有一个参数 也就是一个字母要显示的 比如说中点O
        //                M.Invoke(null, new Object[] { array[0].ToString()});
        //            } else if(array.Length == 2)
        //            {
        //                // 显示两个字母的
        //                M.Invoke(null, new Object[] { array[0].ToString(), array[1].ToString() });
        //            } else if(array.Length == 3)
        //            {
        //                // 需要传进去三个字母
        //                M.Invoke(null, new Object[] { array[0].ToString(), array[1].ToString(), array[2].ToString() });
        //            } else if(array.Length == 4)
        //            {
        //                // 需要穿进去四个字母 也就是矩形这种
        //                M.Invoke(null, new Object[] { array[0].ToString(), array[1].ToString(), array[2].ToString(), array[3].ToString() });
        //            } else if(array.Length == 5)
        //            {
        //                // 需要传进来5个字母 比如显示两条直线的交点
        //                M.Invoke(null, new Object[] { array[0].ToString(), array[1].ToString(), array[2].ToString(), array[3].ToString(), array[4].ToString() });
        //            } else
        //            {
        //                M.Invoke(null, null);
        //            }

                   
        //        }
        //        catch (Exception e1)
        //        {

        //            //把提示信息放到 MesageBox 弹出框中，它默认的是模态模式
        //            //点击确定后，重新让textbox清空，并获取焦点,就可以重新输入
        //            MessageBox.Show("请输入正确的指令！" + e1.Message);
        //        }

        //    }
            
        //}

       
            
    }
}
