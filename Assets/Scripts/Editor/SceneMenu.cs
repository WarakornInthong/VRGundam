using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneMenu
{
    [MenuItem("Scenes/MechScene")]
    public static void OpenMechScene(){
        OpenScene("MechScene");
    }

    [MenuItem("Scenes/MapHill")]
    public static void OpenMapHill(){
        OpenScene("MapHill");
    }
    private static void OpenScene(string sceneName){
        EditorSceneManager.OpenScene("Assets/Scenes/CharacterScene.unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity", OpenSceneMode.Additive);
    }
}
