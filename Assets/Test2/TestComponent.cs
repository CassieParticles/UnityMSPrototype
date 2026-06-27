using System;
using UnityEngine;

namespace Test2
{

    [Serializable]
    public class TestObj
    {
        public float a;
        public float b;
        public float c;
    }
    
    public class TestComponent : MonoBehaviour
    {
        public TestObj testObj =  new TestObj();
    }
}