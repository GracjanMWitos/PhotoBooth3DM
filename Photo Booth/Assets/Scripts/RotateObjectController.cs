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

    float PCRotationSpeed = 20f;
    Vector2 mousePosition;
    bool mouseIsClicked;
    //Drag the camera object here

    void MouseDrag()
    {
        if (mouseIsClicked)
        {
            float rotX = mousePosition.x * PCRotationSpeed * Time.deltaTime;
            float rotY = mousePosition.y * PCRotationSpeed * Time.deltaTime;

            Vector3 right = Vector3.Cross(camera.transform.up, transform.position - camera.transform.position);
            Vector3 up = Vector3.Cross(transform.position - camera.transform.position, right);
            transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
            transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
        }
    }

    void Update ()
    {
        mousePosition = gameControls.Models.MousePosition.ReadValue<Vector2>();

        gameControls.Models.MouseClick.performed += ctx => mouseIsClicked = true;
        gameControls.Models.MouseClick.canceled += ctx => mouseIsClicked = false;

        MouseDrag();
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
