using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Blog.Core.AOP
{
    public class BlogLogAop:IInterceptor
    {
        /// <summary>
        /// 实例化IInterceptor唯一方法
        /// </summary>
        /// <param name="invocation">包含拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            //事件前处理
            var dataIntercept = "" +
                                $"【当前执行方法】:{invocation.Method}\r\n" +
                                $"【携带的参数有】;{string.Join(",", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())}\r\n";
            //执行当前访问的服务方法（注意 如果下面还有其他的AOP拦截器，会跳转到其他AOP里）
            invocation.Proceed();
            //事后处理 在services被执行了之后，做相应的处理，这里是输出到日志文件
            dataIntercept += ($"【执行结果】:{invocation.ReturnValue}");

            //输出到日志文件
            //Parallel.For(0, 1, e =>
            //{
            //    LogLock.OutSql2Log("AOPLog", new string[] { dataIntercept });
            //});


            #region  测试输出日志

            var path = Directory.GetCurrentDirectory() + @"\Log";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = path + $@"\InterceptLog-{DateTime.Now.ToString("yyyyMMddHHmmss")}.log";

            StreamWriter sw = File.AppendText(fileName);
            sw.WriteLine(dataIntercept);
            sw.Close();


            #endregion
        }
    }
}
