using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ILinqExtension
{
    public static void ForEach<T>(this IEnumerable<T> target, Action<T> invoker)
    {
        foreach (var t in target)
        {
            invoker?.Invoke(t);
        }
    }

    public static IEnumerable<(T value,int index)> SelectIndex<T>(this IEnumerable<T> target)
    {
        return target.Select((v, i) => (v, i));
    }
}
