
using System.Linq;
using UnityEngine;

public class MeshReverse : MonoBehaviour
{
    private void Awake()
    {
        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        var mesh = meshFilter.mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

}
