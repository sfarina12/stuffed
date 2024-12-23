using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCompass : MonoBehaviour
{
    public Transform Target;
    public float RotationSpeed;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Update is called once per frame
    void Update()
    {
        
        _direction = (Target.position - transform.position).normalized;
        
        _lookRotation = Quaternion.LookRotation(_direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }
}
