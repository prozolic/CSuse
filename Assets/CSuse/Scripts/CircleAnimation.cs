using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAnimation
{
    private Transform _center;

    private float _period;

    public CircleAnimation(Transform center, float period)
    {
        _center = center;
        _period = period;
    }

    public void Animate(Transform transform, float offset)
    {
        if (_period <= 0) return;
        transform.RotateAround(_center.position, Vector3.up, 360 / _period * offset);
    }
}
