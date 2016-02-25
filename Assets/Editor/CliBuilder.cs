using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;

public static class CliBuilder {   

    static string folder = "/Users/Chris/dev/tangrams/Builds/";

    static string[] GetScenes(){        
        var scenes = (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray();
        Debug.Log(scenes.Length + " scenes found");
        return scenes;

    }

    [MenuItem ("Builds/ALL")]
    public static void ALL(){       
        WebGL();
        Win32();
        Win64();
        OSX();
        Linux();
    }

    public static void BuildPlayer(string buildName, BuildTarget buildTarget){
        ClearConsole();
        Debug.Log("\nBuilding " + buildName + "...");
        var result = BuildPipeline.BuildPlayer(GetScenes(), folder + buildName, buildTarget, BuildOptions.None);
        Debug.Log("Done: " + result);
    }

    [MenuItem ("Builds/WebGL")]
    public static void WebGL(){         
        BuildPlayer("WebGL", BuildTarget.WebGL);
    }

    [MenuItem ("Builds/Win32")]
    public static void Win32(){ 
        BuildPlayer("Win32/SGT-Win32.exe", BuildTarget.StandaloneWindows);
    }

    [MenuItem ("Builds/Win64")]
    public static void Win64(){ 
        BuildPlayer("Win64/SGT-Win64.exe", BuildTarget.StandaloneWindows64);
    }

    [MenuItem ("Builds/OSX")]
    public static void OSX(){ 
        BuildPlayer("OSX/StainedGlassTangrams", BuildTarget.StandaloneOSXUniversal);
    }

    [MenuItem ("Builds/Linux")]
    public static void Linux(){ 
        BuildPlayer("Linux/StainedGlassTangrams-Linux", BuildTarget.StandaloneLinuxUniversal);
    }

    [MenuItem ("Tools/Clear Console")]
    static void ClearConsole () {
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null,null);
    }

//    [MenuItem ("MyBuilds/Test")]
//    public static void Test(){
//        Debug.Log(Application.dataPath);
//    }


}
