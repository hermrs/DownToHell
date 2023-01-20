using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationC : MonoBehaviour
{
    [Header("Animasyon")]
    public Animation IdleAn;
    public Animation SprintAn;
    public Animation RunAn;
    public PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.Animo == 2)
        {
            RunAn.Play();
        }
        else if (pm.Animo == 3)
        {
            SprintAn.Play();
            // zýplama
        }
    }
}
