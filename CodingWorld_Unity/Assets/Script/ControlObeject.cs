using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Control
{
    public enum ObjectType {Animal,Enviorment};
    public abstract class ControlObject
    {
        protected bool running = false;

        /*
         * 行为调用接口，在对象运行脚本中调用
         */
        public abstract void Run();
        /*
         * Task()
         * 子类重写，实现具体的任务
         */
        public abstract void Task();

        

        public void Begin()
        {
            running = true;
        }

        public void End()
        {
            running = false;
        }

        public bool IsRunning()
        {
            return running;
        }

        public abstract ObjectType GetObjectType();

    }
}
