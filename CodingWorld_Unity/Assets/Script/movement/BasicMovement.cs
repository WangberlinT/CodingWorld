using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public abstract class BasicMovement : MonoBehaviour
    {

        public abstract void head(float distance, float maxspeed);
        public abstract void jump();


        public abstract void left(float distance, float maxspeed);


        public abstract void right(float distance, float maxspeed);


        public abstract void tail(float distance, float maxspeed);


        public abstract void turnLeft(float degree);


        public abstract void turnRight(float degree);

    }
}

