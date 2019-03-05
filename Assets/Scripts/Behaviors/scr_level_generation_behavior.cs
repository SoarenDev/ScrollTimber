using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_level_generation_behavior : MonoBehaviour
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    [SerializeField]    private int             actual_generation_index             ;
                        private Coroutine       generation_coroutine                ;   

[Space(10)][Header("References")]
                        public  GameObject      generation_spawn_point_ref          ;
                        public  GameObject      generated_objects_container_ref     ;
       
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        generation_coroutine = StartCoroutine("GenerationCoroutine");
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Resets the generation behavior.
    /// </summary>
    public void ResetLevelGeneration()
    {
        actual_generation_index = 0;

        return;
    }

    /// <summary>
    /// Instantiates a new object as part of level generation.
    /// </summary>
    public void GenerateLevelObject(GameObject object_to_generate)
    {
        GameObject instance;

        instance = Instantiate(object_to_generate, generation_spawn_point_ref.transform.position, Quaternion.identity);
        instance.transform.SetParent(generated_objects_container_ref.transform, true);

        Debug.Log("Level item generated");

        return;
    }

// = = =

// = = = [ COROUTINES ] = = =

    /// <summary>
    /// Coroutine used to control the level objects' generation over time.
    /// </summary>
    public IEnumerator GenerationCoroutine()
    {
        // kill coroutine if there is no more objects to generate
        if (actual_generation_index > LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list.Count-1)
        { Debug.LogWarning("All level item generated"); StopAllCoroutines(); yield return null;}
        
        // wait for actual object delay time
        Debug.Log("Generation delay launched:" + LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list[actual_generation_index].delay_before_generation);
        yield return new WaitForSeconds(LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list[actual_generation_index].delay_before_generation);

        // actually generate the object
        GenerateLevelObject(LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].generation_list[actual_generation_index].object_prefab);

        // increments actual generated object index
        actual_generation_index += 1;

        // restart coroutine
        generation_coroutine = StartCoroutine("GenerationCoroutine");
    }


// = = =

}
