using UnityEngine;

public class PlayerControlCam : MonoBehaviour
{
    public float senX;
    public float senY;
    public Transform d�nme;
    float xD�nme;
    float yD�nme;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;
        yD�nme += mouseX;
        xD�nme -= mouseY;
        xD�nme = Mathf.Clamp(xD�nme, -90f, 90f);
        // kamery� d�nd�rme &6
        transform.rotation = Quaternion.Euler(xD�nme, yD�nme, 0);
        d�nme.rotation = Quaternion.Euler(0, yD�nme, 0);
       
    } 
}
