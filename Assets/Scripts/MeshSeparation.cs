using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshSeparation : MonoBehaviour
{
    public float slashWidth = 0.01f;
    private GameObject scrollCylinderParent;
    [Header("Please order meshes and corresponding prefabs in the same way")]
    public List<Mesh> possibleMeshes;
    public List<GameObject> correspondingUpperTrees;
    public List<GameObject> correspondingLowerTrees;
    GameObject upperObjectInstance;
    GameObject lowerObjectInstance;
    Mesh upperMesh;
    Mesh lowerMesh;
    List<Vector3> verticesToMove;
    List<int> verticesIndexes = new List<int>();


    void Start()
    {
        InitializeVariables();
        OnCut(45f, 0.1f);
    }

    void InitializeVariables()
    {
        scrollCylinderParent = GameManager.instance.scroll_cylinder_parent;
        return;
    }

    public void OnCut(float angle, float height)
    {
        for (int j = 0; j < possibleMeshes.Count(); j++)
        {
            if (gameObject.GetComponent<MeshFilter>().mesh.name.Contains(possibleMeshes[j].name))
            {
                upperObjectInstance = Instantiate(correspondingUpperTrees[j], transform.position, transform.rotation, scrollCylinderParent.transform);
                upperObjectInstance.transform.localScale = transform.localScale;
                upperMesh = upperObjectInstance.GetComponent<MeshFilter>().mesh;
                verticesToMove = upperMesh.vertices.ToList<Vector3>();

                for (int i = 0; i < upperMesh.vertices.Count(); i++)
                {
                    if (upperMesh.vertices[i].z < height + Mathf.Tan(angle * (Mathf.PI / 180)) * verticesToMove[i].y + slashWidth)
                    {
                        verticesIndexes.Add(i);
                    }
                }

                for (int i = 0; i < verticesIndexes.Count(); i++)
                {
                    verticesToMove[verticesIndexes[i]] = new Vector3(verticesToMove[verticesIndexes[i]].x, verticesToMove[verticesIndexes[i]].y, height + Mathf.Tan(angle * (Mathf.PI / 180)) * verticesToMove[verticesIndexes[i]].y + slashWidth);
                }
                upperMesh.vertices = verticesToMove.ToArray<Vector3>();
                upperObjectInstance.AddComponent<MeshCollider>();
                upperObjectInstance.GetComponent<MeshCollider>().convex = true;


                verticesToMove.Clear();
                verticesIndexes.Clear();


                lowerObjectInstance = Instantiate(correspondingLowerTrees[j], transform.position, transform.rotation, scrollCylinderParent.transform);
                lowerObjectInstance.transform.localScale = transform.localScale;
                lowerMesh = lowerObjectInstance.GetComponent<MeshFilter>().mesh;
                verticesToMove = lowerMesh.vertices.ToList<Vector3>();

                for (int i = 0; i < lowerMesh.vertices.Count(); i++)
                {
                    if (lowerMesh.vertices[i].z > height + Mathf.Tan(angle * (Mathf.PI / 180)) * verticesToMove[i].y)
                    {
                        verticesIndexes.Add(i);
                    }
                }

                for (int i = 0; i < verticesIndexes.Count(); i++)
                {
                    verticesToMove[verticesIndexes[i]] = new Vector3(verticesToMove[verticesIndexes[i]].x, verticesToMove[verticesIndexes[i]].y, height + Mathf.Tan(angle * (Mathf.PI / 180)) * verticesToMove[verticesIndexes[i]].y);
                }
                lowerMesh.vertices = verticesToMove.ToArray<Vector3>();
                lowerObjectInstance.AddComponent<MeshCollider>();
                lowerObjectInstance.GetComponent<MeshCollider>().convex = true;
            }
        }

        Destroy(gameObject);
    }
}
