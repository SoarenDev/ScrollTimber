using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeshSeparation : MonoBehaviour
{
    public float slashWidth = 0.01f;
    private GameObject scrollCylinderParent;
    public GameObject upperObjectToSpawn;
    public GameObject lowerObjectToSpawn;
    GameObject upperObjectInstance;
    GameObject lowerObjectInstance;
    Mesh upperMesh;
    Mesh lowerMesh;
    List<Vector3> verticesToMove;
    List<int> verticesIndexes = new List<int>();


    void Start()
    {
        InitializeVariables();
        // OnCut(45f, 0.1f);
    }

    void InitializeVariables()
    {
        scrollCylinderParent = GameManager.instance.scroll_cylinder_parent;
        return;
    }

    public void OnCut(float angle, float height)
    {
        upperObjectInstance = Instantiate(upperObjectToSpawn, transform.position, transform.rotation, scrollCylinderParent.transform);
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


        lowerObjectInstance = Instantiate(lowerObjectToSpawn, transform.position, transform.rotation, scrollCylinderParent.transform);
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

        Destroy(gameObject);
    }
}
