using UnityEngine;

public class PlayerControlCam : MonoBehaviour
{
    public float senX;
    public float senY;
    public Transform dönme;
    float xDönme;
    float yDönme;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;
        yDönme += mouseX;
        xDönme -= mouseY;
        xDönme = Mathf.Clamp(xDönme, -90f, 90f);
        // kameryý döndürme &6
        transform.rotation = Quaternion.Euler(xDönme, yDönme, 0);
        dönme.rotation = Quaternion.Euler(0, yDönme, 0);
       
    } 
}
