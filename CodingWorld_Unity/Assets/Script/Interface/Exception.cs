using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CodeWorldInterface
{
    public class AnimalWithoutEyeException : ApplicationException
    {
        public AnimalWithoutEyeException() { }
        public AnimalWithoutEyeException(string Message) : base(Message) { }
    }
}


