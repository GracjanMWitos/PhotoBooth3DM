using System;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    #region Assignment
    private Camera mainCamera;
    private static GameObject panel;
    private AssetBundle loadedAssetBundle;
    private GameControls gameControls;
    private Transform showUpPoint;
    private UnityEngine.Object[] prefabs;

    private void Awake()
    {
        gameControls = new GameControls();

        panel = GameObject.Find("Panel");
        showUpPoint = GameObject.Find("ShowUpPoint").transform;
        mainCamera = Camera.main;

        loadedAssetBundle = AssetBundle.LoadFromFile(Application.dataPath + @"\[Input]\models");
        prefabs = loadedAssetBundle.LoadAllAssets();

        gameControls.Models.MouseScrollY.performed += ctx => mouseScrollY = ctx.ReadValue<float>();
    }

    #endregion Assignment

    #region Variables
    [Header("Index")]
    [SerializeField] private int currentModelIndex;
    [Space]
    [Header("Speed")]
    [SerializeField] private int rotationSpeed = 20;
    [SerializeField] private int zoomSpeed = 2;

    private static float timer;
    private Vector2 mouseDelta;
    private float mouseScrollY;
    private bool mouseIsClicked;

    #endregion Variables

    void Update()
    {
        #region Timer
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer <= 0)
        {
            panel.SetActive(true);
        }

        #endregion Timer

        #region MouseUpdate

        #region Rotation
        mouseDelta = gameControls.Models.MousePosition.ReadValue<Vector2>();
        gameControls.Models.MouseClick.performed += ctx => mouseIsClicked = true;
        gameControls.Models.MouseClick.canceled += ctx => mouseIsClicked = false;

        if (mouseIsClicked)
        {
            float rotX = mouseDelta.x * rotationSpeed * Time.deltaTime;
            float rotY = mouseDelta.y * rotationSpeed * Time.deltaTime;

            Vector3 right = Vector3.Cross(mainCamera.transform.up, showUpPoint.position - mainCamera.transform.position);
            Vector3 up = Vector3.Cross(showUpPoint.position - mainCamera.transform.position, right);
            showUpPoint.rotation = Quaternion.AngleAxis(-rotX, up) * showUpPoint.rotation;
            showUpPoint.rotation = Quaternion.AngleAxis(rotY, right) * showUpPoint.rotation;
        }

        #endregion Rotation

        #region Zoom
        if ( mouseScrollY > 0 && mainCamera.fieldOfView > 20)
            mainCamera.fieldOfView -= zoomSpeed;

        if (mouseScrollY < 0 && mainCamera.fieldOfView < 80)
            mainCamera.fieldOfView += zoomSpeed;

        #endregion Zoom

        #endregion MouseUpdate
    }

    #region Photos

    public static void TakePhoto()
    {
        panel.SetActive(false);
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/[Output]/Screenshot-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
        timer = 0.02f;
    }

    #endregion Photos

    #region InstantiateModels
    private void Start()
    {
        InstantiateObjectFromBundle(0);
    }

    public void InstantiateObjectFromBundle(int indexChangeBy)
    {
        if (currentModelIndex + indexChangeBy >= 0 && currentModelIndex + indexChangeBy < prefabs.Length)
        {
            if (indexChangeBy != 0)
            {
                Destroy(GameObject.FindGameObjectWithTag("Respawn"));
                showUpPoint.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            currentModelIndex += indexChangeBy;
            Instantiate(prefabs[currentModelIndex], showUpPoint);

        }
    }
    #endregion InstantiateModels

    #region OnEnable OnDisable
    private void OnEnable()
    {
        gameControls.Enable();
    }

    private void OnDisable()
    {
        gameControls.Disable();
    }
    #endregion OnEnable OnDisable
}