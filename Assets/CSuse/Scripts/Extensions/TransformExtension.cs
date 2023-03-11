using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{

    /// <summary>
    /// �w�肵��<see cref="Transform"/>�̂��ׂĎq�I�u�W�F�N�g���擾����B
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
    /// �w�肵��<see cref="Transform"/>�̂��ׂĎq�I�u�W�F�N�g���擾����B
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
