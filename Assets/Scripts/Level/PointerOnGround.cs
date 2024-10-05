using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PointerOnGround : MonoBehaviour
{
    //create transform variabe for pointer gm
    [SerializeField]
    Transform pointerTransform = null;

    [Space]
    [Space]
    [Space]
    public LayerMask groundMask;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AimGround();
    }

    void AimGround()
    {
        //Ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit
        RaycastHit hit;

        //Physics Raycast out hit 
        // LayerMask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {


            // save the current y pos of the pointer
            float pointerYPos = pointerTransform.position.y;

            //pointerTransform.position = hit.point;
            pointerTransform.position = new Vector3(hit.point.x, pointerYPos, hit.point.z);
        }
    }
}
