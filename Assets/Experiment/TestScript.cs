using System;
using MarchingSquaresTool.PrototypeC.Core;
using UnityEngine;

namespace Experiment
{
    [Serializable]
    public class TestClass
    {
        public TestClass(float a, float b, float c, float d, float e)
        {
            this.a = a;
            this.b = b;
            this.c = c;

            test = new TestBClass(d, e);
        }
        
        public float a;
        public float b;
        public float c;
        
        public TestBClass test;
    }

    [Serializable]
    public class TestBClass
    {
        public TestBClass(float d, float e)
        {
            this.d = d;
            this.e = e;
        }
        
        public float d;
        public float e;
    }
    
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private TestClass test =  new TestClass(3,4,5,6,7);
        [SerializeField] private float value;
    }
}
