using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dissolve : MonoBehaviour
{

    public Material DissolveMaterial;

    public float TargetTime = 0.0f;

    public bool AttackHouse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackHouse == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackHouse = true;
            }
        }
        if (AttackHouse == true)
        {
            DissolveMaterial.SetFloat("Vector1_EA211A19", TargetTime);
            if (TargetTime < 1.0f)
            {
                TargetTime += (Time.smoothDeltaTime / 5);
            }
        }
    }
}
