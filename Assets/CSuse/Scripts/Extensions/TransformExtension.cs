using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{

    /// <summary>
    /// 指定した<see cref="Transform"/>のすべて子オブジェクトを取得する。
    /// </summary>
    /// <param name="target">gameobject.transform</param>
    /// <returns></returns>
    public static IEnumerable<GameObject> GetChildrenObject(this Transform target)
    {
        foreach (Transform obj in target)
        {
            var child = obj.gameObject;
            foreach (var c in GetChildrenObject(obj))
            {
                yield return c;
            }
            yield return child;
        }
    }

    /// <summary>
    /// 指定した<see cref="Transform"/>のすべて子オブジェクトを取得する。
    /// </summary>
    /// <param name="target">gameobject.transform</param>
    /// <returns></returns>
    public static IEnumerable<GameObject> GetChildrenObject(this Transform target, bool includeSelf)
    {
        if (includeSelf)
            yield return target.gameObject;

        foreach(var c in GetChildrenObject(target))
        {
            yield return c;
        }
    }



}
