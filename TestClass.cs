﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    [CustomNameAttribute("CustomFieldName")]
    public class CustomNameAttribute : Attribute
    {
        public string CustomName;


        public CustomNameAttribute(string v)
        {
            CustomName = v;
        }
    }

    class TestClass
    {
        [CustomNameAttribute("CustomFieldName")]
        public int I { get; set; }
        public string? S { get; set; }
        public decimal D { get; set; }
        public char[]? C { get; set; }

        public TestClass()
        { }
        private TestClass(int i)
        {
            this.I = i;
        }
        public TestClass(int i, string s, decimal d, char[] c) : this(i)
        {
            this.S = s;
            this.D = d;
            this.C = c;
        }
    }
}
