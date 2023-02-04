using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

class CustomBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("MyCustomBuildProcessor.OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            Debug.Log("trying scene number " + i);
            Scene currScene = SceneManager.GetSceneByBuildIndex(i);
            Debug.Log("The scene has path " + currScene.path);
            EditorSceneManager.OpenScene(currScene.path);
            TilemapSerializer serializer = GameObject.FindObjectOfType<TilemapSerializer>();
            if (serializer != null) {
                serializer.LoadTilemap();
            }
            EditorSceneManager.SaveScene(currScene);
            EditorSceneManager.CloseScene(currScene, false);
        }
    }
}