using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.CodeDom;
using System.CodeDom.Compiler;                             

using System.Reflection;                                          

using UnityEngine;
using Microsoft.CSharp;

namespace InGameCompiler
{
    class GameCompiler
    {
        CSharpCodeProvider provider;

        CompilerParameters parameters;

        public GameCompiler()
        {

            //定义c#代码容器

            provider = new CSharpCodeProvider();

            parameters = new CompilerParameters();

        }

        public string TestCompiler(string code,string name)        //参数code是unity传来的脚本代码，返回值是编译后的结果

        {

            StringBuilder sb = new StringBuilder();

            try

            {

                //动态添加dll库

                parameters.ReferencedAssemblies.Add("UnityEngine.dll");

                parameters.ReferencedAssemblies.Add("System.dll");

                parameters.ReferencedAssemblies.Add("System.Data.dll");

                parameters.ReferencedAssemblies.Add("System.Xml.dll");



                //True - 生成在内存中, false - 生成在外部文件中

                parameters.GenerateInMemory = false;

                //True - 生成 exe, false - 生成 dll

                parameters.GenerateExecutable = false;

                parameters.OutputAssembly = name+".dll";         //编译后的dll库输出的名称，会在bin/Debug下生成Test.dll库





                //取得编译结果

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)

                {

                    foreach (CompilerError error in results.Errors)

                    {

                        sb.AppendLine(String.Format("Error({0}): {1}", error.Line, error.ErrorText));



                    }

                    return sb.ToString();                                     //编译不通过，返回错误信息

                }

                else

                {

                    return "Ok";                                             //编译通过，返回Ok

                }



            }

            catch (Exception)

            {

                Console.WriteLine("抛异常了");

                return sb.ToString();

            }

        }

    }

}


