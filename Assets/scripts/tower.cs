using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    public GameObject box_hide;
    public float move_speed = 10;
    public bool move_up_bool;
    public bool move_down_bool;
 
    public void Update()
    {
        float dist;
        dist = Vector3.Distance(box_hide.transform.position, transform.position);
        if (dist < 4f)
        {
            if (transform.position.y > -2.5f)
            {
                if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
                {
                    transform.Translate(Vector3.down * Time.deltaTime * move_speed);
                }
            }
        }
        if (dist > 4f)
        {
            if (transform.position.y <0.45f)
            {
                //move_up()
                if (Input.GetKey("w")|| Input.GetKey("a")|| Input.GetKey("s")|| Input.GetKey("d"))
                {
                    transform.Translate(Vector3.up * Time.deltaTime * move_speed);
                }
            }
        }
    }
}