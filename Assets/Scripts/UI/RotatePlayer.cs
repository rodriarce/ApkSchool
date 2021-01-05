using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public float speedRotate;
    public bool isTouch;


    

    private void OnMouseDrag()
    {
        transform.Rotate(0f, -speedRotate, 0f);
        isTouch = true;
    }

    

}
