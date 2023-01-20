using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BıçFırlat : MonoBehaviour
{
    [Header("Değişkenler")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Ayarlamalar")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Fırlat")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float fırlatmaGücü;
    public float yokuşAtışı;

    [Header("Cephane")]
    public Slider Cephane;
    bool readyToThrow;
    private void Start()
    {
        readyToThrow = true;
        Cephane.maxValue = totalThrows;
        Cephane.minValue = 0;

    }
    private void Update()
    {
        Cephane.value = totalThrows;
        if(Input.GetKey(throwKey)&& readyToThrow && totalThrows > 0 )
        {
            Throw();
        }
    }
    private void Throw()
    {
        readyToThrow = false;
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = cam.transform.forward;
        Vector3 forceToAdd = forceDirection * fırlatmaGücü + transform.up * yokuşAtışı;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        RaycastHit hit;
        if(Physics.Raycast(cam.position,cam.forward,out hit, 500f))
        {
            forceDirection =  (hit.point - attackPoint.position).normalized;
        }
        
        totalThrows--;
        Invoke(nameof(ResetThrow), throwCooldown);
    }
    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
