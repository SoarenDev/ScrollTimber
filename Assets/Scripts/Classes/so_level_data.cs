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

[Space(10)][Header("Content")]
    public  List<struct_object_to_generate>     generation_list             ;

       
// = = =

// = = = [ CLASS METHODS ] = = =



// = = =

}
