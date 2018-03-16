using UnityEngine;
using System.Collections;
using UnityEditor;

public class CombineMeshes : Editor
{

    [MenuItem("GameObject/CombineSelectedObjects")]
    static void CombineSelected()
    {
        if (EditorUtility.DisplayDialog("Do you want to combine these objects?", "This can't be undone! Make sure you have a backup if you don't know what you're doing.", "Heck Yeah!", "No, I'm scared"))
        {
            int amountSelected = Selection.gameObjects.Length;

            MeshFilter[] meshFilters = new MeshFilter[amountSelected];
            CombineInstance[] combineInstances = new CombineInstance[amountSelected];


            for (var i = 0; i < amountSelected; i++)
            {
                meshFilters[i] = Selection.gameObjects[i].GetComponent<MeshFilter>();

                combineInstances[i].mesh = meshFilters[i].sharedMesh;
                combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }

            GameObject obj = new GameObject("CombinededMeshes", typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider));
            obj.GetComponent<MeshFilter>().mesh = new Mesh();
            obj.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combineInstances);
            obj.GetComponent<MeshRenderer>().sharedMaterial = new Material(meshFilters[0].gameObject.GetComponent<MeshRenderer>().sharedMaterial);
            obj.GetComponent<MeshCollider>().sharedMesh = obj.GetComponent<MeshFilter>().sharedMesh;


            foreach (MeshFilter m in meshFilters)
            {
                DestroyImmediate(m.gameObject);
            }
        }
    }
}
