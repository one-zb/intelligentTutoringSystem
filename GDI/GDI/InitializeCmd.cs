using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GDI
{
    /// <summary>
    /// 让这个类的对象被创建的时候，
    /// 自动加载保存 命令 -> 方法 的字典
    /// </summary>
    public class InitializeCmd
    {
        Dictionary<String, MethodInfo> cmd = new Dictionary<string, MethodInfo>();

        public Dictionary<String, MethodInfo> Cmd
        {
            get { return cmd; }
        }

        public InitializeCmd()
        {
            CMDAttribute[] attribute1;
            CMDAttribute attribute;


            //抽象化命令类和特性类，方便利用反射调用这个类的方法，属性...
            Type tpc = typeof(Commander);
            Type tpa = typeof(CMDAttribute);

            MethodInfo[] meths = tpc.GetMethods(); //获取Commadner类的所有方法，并且放入一个数组

            //遍历方法数组，获取每个方法的特性，然后将方法的特性与用户输入的命令比较。
            //如果用户输入的命令与特性一致，就执行这个特性所表示的方法，如果不一致，就告诉用户命令有错
            foreach (MethodInfo M in meths)
            {
                //判断方法是否应用了TCmdAttribute所抽象的特性。并且不向上查找继承的类。  
               //这一句非常重要。因为Type的GetMethods()方法会获取类的所有方法，包括他所继承的基类的方法。  
               //而基类的方法是没有使用我们自定义的特性，那么在进行下一句，对attr赋值是，attr会是一个空。
               
               //所有我们使用MethodInfo对象的IsDefined()方法来确定某个方法使用了某个自定义的特性。
                if (M.IsDefined(tpa, false))
                {
                    //获取方法的特性
                    attribute1 = (CMDAttribute[])M.GetCustomAttributes(tpa, false);
                    attribute = attribute1[0];

                    //然后从上面拿到的这个方法所有的特性后
                    //再从这个特性里遍历拿出来作为键，放入字典里。
                    // [CMD("s", "sh", "h", "hi")] 就是从这里遍历特性，也就是命令作为键，命令对应的方法作为值
                    //字典允许多个键 对应 一个值 
                    foreach (String command in attribute.Cmd)
                    {
                        cmd.Add(command, M);
                    }

                }
            }
        }
    }
}
