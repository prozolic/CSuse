using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSuse.Test
{
    public class TestMethodAttribute : Attribute
    {
        public string SpecifiedName { get; private set; }

        public TestMethodAttribute(string specifiedName)
        {
            this.SpecifiedName = specifiedName;
        }

    }

}
