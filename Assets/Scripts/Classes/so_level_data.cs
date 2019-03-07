using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelData", order = 1)]
public class so_level_data : ScriptableObject
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
    public  int                                 id                          ;
    public  string                              label                       ;
    [Space(5)]
    public GameObject                           prefab_tree_type            ;
    [Space(5)]
    public  List<Mesh>                          meshes_to_spawn_list        ;
    [Space(5)]
    public  int                                 best_score                  ;

[Space(10)][Header("Gameplay")]
    public  float                               level_scroll_speed          = 5.00f;
    
[Space(10)][Header("Content")]
    public  List<struct_object_to_generate>     generation_list             ;

       
// = = =

// = = = [ CLASS METHODS ] = = =



// = = =

}
