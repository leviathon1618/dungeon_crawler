using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public LayerMask IgnoreMe;
    public GameObject camera_obj;
    public GameObject hide_box;
    public float speed = 2;
    public float xpos;
    public float zpos;
    public bool box_enabled = false;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (box_enabled != true)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, camera_obj.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~IgnoreMe))
        {
            Debug.DrawRay(transform.position, camera_obj.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.transform.name == "floor")
            {
                Vector3 box_position = hide_box.transform.position;
                //box_position = hit.point;
                box_position = new Vector3(hit.point.x, 0.7f, hit.point.z);
                hide_box.transform.position = box_position;
            }

        }
    }

    void Update()
    {
        xpos = transform.position.x;
        zpos = transform.position.z;

        if (Input.GetKey("a") && xpos > 1f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey("d") && xpos < 12f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey("w")&& zpos < 2f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey("s") && zpos > -9f)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
    }
}