using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSuse.Test
{
    public class CircleAnimationTest : MonoBehaviour
    {

        [SerializeField]
        private Transform _center;

        [SerializeField]
        private float _period = 10.0f;

        private CircleAnimation _circleAnimation;

        // Start is called before the first frame update
        void Start()
        {
            _circleAnimation = new CircleAnimation(_center, _period);
        }

        // Update is called once per frame
        void Update()
        {
            _circleAnimation.Animate(this.transform, Time.deltaTime);
        }
    }
}
