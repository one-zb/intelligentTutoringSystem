using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDI
{
    /// <summary>
    /// 定义用于反射的特性
    /// </summary>
    public class CMDAttribute : Attribute
    { 
        String[] cmd;
        public String[] Cmd
        {
            get { return cmd; }
        }

        //这个属性类的构造方法
        public CMDAttribute(params String[] cmd)
        {
            this.cmd = cmd;
        }
    }
}
