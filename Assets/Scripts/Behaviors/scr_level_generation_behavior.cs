using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class scr_level_generation_behavior : MonoBehaviour
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    [SerializeField]    private int                 actual_generation_index             ;
                        private Coroutine           generation_coroutine                ;  
                        public List<GameObject>     generated_trees                     ; 

[Space(10)][Header("References")]
                        public  GameObject          generation_spawn_point_ref          ;
                        public  GameObject          generated_objects_container_ref     ;
       
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Resets the generation behavior.
    /// </summary>
    public void ResetLevelGeneration()
    {
        Debug.Log("Level generation reset");
        StopAllCoroutines();
        ClearLevelObjects();
        actual_generation_index = 0;

        return;
    }

    /// <summary>
    /// Starts the generation behavior with the Level Manager actual level.
    /// </summary>
    public void StartLevelGeneration()
    {
        print("bidule");
        ResetLevelGeneration();
        print("bidule2");
        generation_coroutine = StartCoroutine("GenerationCoroutine");

        Debug.Log("inGeneration");

        // set gamemanager remaining tree amount
        GameManager.instance.remaining_trees = LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list.Count;

        return;
    }

    /// <summary>
    /// Instantiates a new object as part of level generation.
    /// </summary>
    public void GenerateLevelObject(GameObject object_to_generate, Mesh mesh_to_generate)
    {
        GameObject instance;

        instance = Instantiate(object_to_generate, generation_spawn_point_ref.transform.position, Quaternion.Euler(-50,0,0) );
        instance.transform.SetParent(generated_objects_container_ref.transform, true);

        //modifies the mesh of the newly-created GameObject
        instance.GetComponent<MeshFilter>().mesh = mesh_to_generate;

        // add generated tree to generated list
        generated_trees.Add(instance);

        Debug.Log("Level item generated");

        return;
    }

    /// <summary>
    /// Clears every generated level object.
    /// </summary>
    public void ClearLevelObjects()
    {
        List<GameObject> buffer_list = new List<GameObject>(generated_trees);

        foreach (GameObject item in buffer_list)
        {
            generated_trees.Remove(item);
            Destroy(item);
        }

        return;
    }

// = = =

// = = = [ COROUTINES ] = = =

    /// <summary>
    /// Coroutine used to control the level objects' generation over time.
    /// </summary>
    private IEnumerator GenerationCoroutine()
    {
        Debug.Log("Couroutine started");

        // kill coroutine if there is no more objects to generate
        if (actual_generation_index > LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list.Count-1)
        { Debug.LogWarning("All level item generated"); StopAllCoroutines(); yield return null;}
        
        // wait for actual object delay time
        Debug.Log("Generation delay launched:" + LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list[actual_generation_index].delay_before_generation);
        yield return new WaitForSeconds(LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list[actual_generation_index].delay_before_generation);

        // actually generate the object with a random mesh
        GenerateLevelObject(LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].prefab_tree_type, LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].meshes_to_spawn_list[Random.Range(0, LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].meshes_to_spawn_list.Count())]);

        // increments actual generated object index
        actual_generation_index += 1;

        // restart coroutine
        generation_coroutine = StartCoroutine("GenerationCoroutine");
    }


// = = =

}
