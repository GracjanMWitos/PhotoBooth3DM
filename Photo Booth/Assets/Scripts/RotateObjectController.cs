using UnityEngine;

public class RotateObjectController : MonoBehaviour
{
    #region Assignment
    Camera camera;
    GameControls gameControls;

    private void Awake()
    {
        camera = Camera.main;
        gameControls = new GameControls();
    }

    #endregion Assignment

    public float PCRotationSpeed = 10f;
    public float MobileRotationSpeed = 0.4f;
    //Drag the camera object here

    void OnMouseDrag()
    {
        float rotX = gameControls.Models.MousePosition.ReadValue<Vector2>().x * PCRotationSpeed;
        float rotY = gameControls.Models.MousePosition.ReadValue<Vector2>().y * PCRotationSpeed;

        Vector3 right = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
        Vector3 up = Vector3.Cross(transform.position - camera.transform.position, right);
        transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
    }

    void Update ()
    {
        
    }

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
