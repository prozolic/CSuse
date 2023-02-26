using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace CSuse.Test
{
    public class TransformExtension : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            typeof(CSuse.Test.TransformExtension).GetMethods().Where(m => m.GetCustomAttributes(false).Any(a => a.GetType() == typeof(TestMethodAttribute)))
                .ForEach(m =>
                {
                    m.Invoke(this, null);
                });
        }

        [TestMethod("GetChildrenObject")]
        public void GetChildrenObjectTest()
        {
            Assert.IsTrue(6 == this.transform.GetChildrenObject().Count());
        }

        [TestMethod("GetChildrenObject")]
        public void GetChildrenObject2Test()
        {
            Assert.IsTrue(7 == this.transform.GetChildrenObject(true).Count());
        }

    }
}

