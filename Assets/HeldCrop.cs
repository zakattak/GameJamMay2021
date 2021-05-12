using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldCrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 modifiedPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        modifiedPoint.z = 0;
        this.transform.position = modifiedPoint;
    }
}
