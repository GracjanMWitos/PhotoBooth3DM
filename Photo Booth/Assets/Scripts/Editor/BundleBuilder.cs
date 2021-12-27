using UnityEditor;
using UnityEngine;

public class BundleBuilder : MonoBehaviour
{
    [MenuItem("Assets/ BuildAssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(Application.dataPath + @"\[Input]", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}
