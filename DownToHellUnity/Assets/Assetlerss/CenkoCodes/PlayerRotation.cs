using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform CameraRotation;
   

    // Update is called once per frame
    void Update()
    {
        transform.rotation = CameraRotation.rotation;     
    }
}
