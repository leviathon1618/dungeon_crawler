using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daycycle : MonoBehaviour
{
    public Vector3 rotation;
    public GameObject light_holder;
    public bool daytime;
    public bool nighttime;
    public bool lights_on;
    public spawner spawn_script;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject light_holder = GameObject.Find("lights_hold");
        StartCoroutine(rotate_light());
    }

    public IEnumerator rotate_light()
    {
        transform.eulerAngles = new Vector3(0, 0, 70);
        for (float i = 70; i > -128; i-= 0.1f)
        {
            transform.Rotate(new Vector3(0, 0, -0.1f));
            rotation = transform.eulerAngles;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(5);
        if (spawn_script.cars_spawnable == true)
        {
            spawn_script.StartCoroutine(spawn_script.spawn_cars());
        }
        StartCoroutine(rotate_light());
    }

    public IEnumerator turn_on_lights(bool lightswitch)
    {
        int total_lights = light_holder.transform.childCount;
        if (lightswitch == true)
        {
            for (int i = 0; i < total_lights; i++)
            {
                light_holder.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled = false;
                yield return new WaitForSeconds(0.05f);
            }
            lights_on = true;
        }
        if (lightswitch == false)
        {
            for (int i = 0; i < total_lights; i++)
            {
                light_holder.transform.GetChild(i).GetChild(0).GetComponent<Light>().enabled = true;
                yield return new WaitForSeconds(0.05f);
            }
            lights_on = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        daytime = false;
        if (rotation.z < 40 && rotation.z > 0)
        {
            daytime = true;
        }
        if (rotation.z < 360 && rotation.z > 270  )
        {
            daytime = true;
        }

        if (daytime == false)
        {
            if (lights_on == true)
            {
                StartCoroutine(turn_on_lights(false));
            }
        }
        if (daytime == true)
        {
            if (lights_on == false)
            {
                StartCoroutine(turn_on_lights(true));
            }
        }
    }
}