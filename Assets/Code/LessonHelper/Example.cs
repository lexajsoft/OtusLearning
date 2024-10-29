using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public sealed class Example
    {
        private void Main()
        {
            List<A> t = new List<A>();
            // List<B> t2 = new List<A>();
            // List<A> t3 = new List<B>();
            IEnumerable<A> y = new List<B>();
            //IComparer<B> u =

            A a = new A();

            // a.Action = null;
            // a.Action();
        }

        public class A
        {
            public event Action Action;
        }
        
        public class B : A
        {
            
        }
    }
}
