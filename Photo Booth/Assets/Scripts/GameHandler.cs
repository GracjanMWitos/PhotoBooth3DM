using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class GameHandler : MonoBehaviour
{
    #region Assignment
    static GameObject panel;
    AssetBundle loadedAssetBundle;
    UnityEngine.Object[] prefabs;
    Transform showUpPoint;

    private void Awake()
    {
        panel = GameObject.Find("Panel");
        showUpPoint = GameObject.Find("ShowUpPoint").transform;

        loadedAssetBundle = AssetBundle.LoadFromFile(Application.dataPath + @"\[Input]\models");
        prefabs = loadedAssetBundle.LoadAllAssets();
    }

    #endregion Assignment

    #region Variables
    static float timer;
    public int currentModelIndex;

    #endregion Variables

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer <= 0)
        {
            panel.SetActive(true);
        }

        
    }

    #region Photos

    public static void TakePhoto()
    {
        panel.SetActive(false);
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/[Output]/Screenshot-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
        timer = 0.02f;
    }

    #endregion Photos

    #region Actions with models
    private void Start()
    {
        InstantiateObjectFromBundle(0);
    }

    public void InstantiateObjectFromBundle(int indexChangeBy)
    {
        if (currentModelIndex + indexChangeBy >= 0 && currentModelIndex + indexChangeBy < prefabs.Length)
        {
            if (indexChangeBy != 0)
                Destroy(GameObject.FindGameObjectWithTag("Respawn"));
            currentModelIndex += indexChangeBy;
            Instantiate(prefabs[currentModelIndex], showUpPoint);

        }
    }
    #endregion Actions with models
}
