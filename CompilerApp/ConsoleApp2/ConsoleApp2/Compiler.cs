using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;

using Microsoft.CSharp;                                             //需要自己添加

using System.CodeDom.Compiler;                             //需要自己添加

using System.Reflection;                                          //反射命名空间

using UnityEngine;                                                   //需要自己添加，再强调一下，得添加UnityEngine.dll引用


namespace CompilerServer

{
    public class Compiler
    {
        //单例Compiler
        //TODO: Console输出编译结果, Write编译结果
        //TODO: 提供调用接口
        private CSharpCodeProvider provider;
        private CompilerParameters parameters;
        private string code;
        private string scrpit_name;
        private static Compiler instance = new Compiler();

        public static Compiler GetInstance()
        {
            return instance;
        }

        private Compiler()
        {
            //定义c#代码容器
            provider = new CSharpCodeProvider();
            parameters = new CompilerParameters();
        }

        public void SetCode(string code)
        {
            this.code = code;
        }

        public void SetName(string name)
        {
            this.scrpit_name = name;
        }

        public bool ConditionCheck()
        {
            return code != null;
        }

        public void Compile()
        {
            //TODO: Exception handle
            string wrongmessage = null;
            wrongmessage = TestCompiler();
            Console.WriteLine(wrongmessage);
            File.WriteAllText("./Log.txt", wrongmessage);

            finished();
        }

        private void finished()
        {
            code = null;
            scrpit_name = null;
        }
        

        public string TestCompiler()
        {
            if (scrpit_name == null)
            {
                return TestCompiler(code);
            }
            return TestCompiler(code, scrpit_name);
        }

        public string TestCompiler(string code, string name)        //参数code是unity传来的脚本代码，返回值是编译后的结果
        {
            StringBuilder sb = new StringBuilder();
            string current_dir = Directory.GetCurrentDirectory();
            string output_dir = Directory.GetParent(current_dir).FullName;
            output_dir += "\\UserDll\\";
            Console.WriteLine(output_dir);
            try
            {
                //动态添加dll库

                //parameters.ReferencedAssemblies.Add("UnityEngine.dll");
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Data.dll");
                parameters.ReferencedAssemblies.Add("System.Xml.dll");
                parameters.ReferencedAssemblies.Add("Assembly-CSharp-firstpass.dll");
                parameters.ReferencedAssemblies.Add("Assembly-CSharp.dll");
                parameters.ReferencedAssemblies.Add("UnityEngine.CoreModule.dll");

                //True - 生成在内存中, false  生成在外部文件中
                parameters.GenerateInMemory = false;
                //True - 生成 exe, false - 生成 dll
                parameters.GenerateExecutable = false;

                parameters.OutputAssembly = output_dir + name + ".dll";         //编译后的dll库输出的名称，会在bin/Debug下生成Test.dll库
                //取得编译结果
                //TODO: 更改输出目录
                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)
                {

                    foreach (CompilerError error in results.Errors)

                    {
                        sb.AppendLine(String.Format("Error({0}): {1}", error.Line, error.ErrorText));
                    }

                    return sb.ToString();//编译不通过，返回错误信息
                }
                else
                {
                    return "Ok"+output_dir;//编译通过，返回Ok
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("抛异常了");
                Console.WriteLine(e.Message);
                return sb.ToString();
            }
            finally
            {
                finished();
            }

        }
        public string TestCompiler(string code)
        {
            return TestCompiler(code, "Test");
        }
    }

}
