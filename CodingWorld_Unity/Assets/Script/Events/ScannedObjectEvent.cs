using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Events
{
    /*
     * 所有事件类实现
     * Tiki 2020-03-18 update
     */



    /*
     * ScannedObjectEvent
     * 存储物体被扫描事件可获取基本信息
     * (List)Visual Message
     * 1. 物体类别
     * 2. 相对位置：Vector3
     * 3. 物体属性
     *      1. 动物属性(包含生命值，攻击力...)
     *      2. 环境属性(包含方块种类...)
     * 
     * 不可获取
     * (List)Collider
     * 
     */
    public class ScannedObjectEvent
    {
        //todo 整个的逻辑和内容都需要设计
        private List<Collider> colliders;

        public ScannedObjectEvent(List<Collider> c_list)
        {
            colliders = c_list;
            //set Visual Message
            for(int i = 0;i < c_list.Count;i ++)
            {
                Collider temp = c_list[i];
                //获取碰撞体的GameObject
                GameObject g = temp.gameObject;
                GameObject root = g.transform.parent.gameObject;
            }
        }



    }
}

