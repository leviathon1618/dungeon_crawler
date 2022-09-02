using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slow_gun : MonoBehaviour
{
    public GameObject tractor_beam;
    public GameObject spwanable;
    public bool stationary = false;
    public bool canspawn = true;
    public LayerMask IgnoreMe;
    public MeshRenderer cone_renderer;
    public bool in_menu = true;
    // Start is called before the first frame update
    void Start()
    {
        cone_renderer = GetComponent<MeshRenderer>();
        if (name == "cone_clone" )
        {
            Destroy(gameObject, 4);
        }
    }

    public IEnumerator place_cone()
    {
        canspawn = false;
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreMe))
        {
            if (hit.rigidbody != null)
            {
                GameObject cone = Instantiate(spwanable, tractor_beam.transform.position, Quaternion.identity);
                cone.GetComponent<slow_gun>().stationary = true;
                cone.name = "cone_clone";
                yield return new WaitForSeconds(1.5f);
                canspawn = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canspawn == true && name == "traffic cone")
        {
            cone_renderer.enabled = true;
        }
        if (canspawn == false && name == "traffic cone")
        {
            cone_renderer.enabled = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (name == "traffic cone")
            {
                if (canspawn == true)
                {
                    StartCoroutine( place_cone());
                }
                
            }
        }
        if (stationary == true)
        {
            return;
        }
        if (in_menu == true)
        {
            return;
        }
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreMe))
        {
            if (hit.rigidbody != null)
            {
                Vector3 position = tractor_beam.transform.position;

                tractor_beam.transform.position = hit.point;
                position = new Vector3(position.x, 0.5f, position.z);
            }
        }
    }
}