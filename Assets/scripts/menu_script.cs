using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_script : MonoBehaviour
{
    public camera main_camera;
    public GameObject rotate_point;
    public AudioSource click_sound;
    public bool in_menu = true;
    public Vector3 play_pos;
    public Vector3 play_rot;
    public bool hidden_box;
    public Vector3 menu_pos;
    public Transform start_transform;
    public GameObject traffic_cone;
    public GameObject play_button;
    public spawner spawn_script;
    // Start is called before the first frame update
    void Start()
    {
        main_camera.box_enabled = false;
        
        play_pos = new Vector3(7, 9, -8);
        menu_pos = new Vector3(7, 12, -8);
        
        if (in_menu == true)
        {
            main_camera.transform.SetParent( rotate_point.transform) ;
            main_camera.transform.position = menu_pos;
        }
    }

    public void play_game()
    {
        spawn_script.cars_spawnable = true;
        in_menu = false;
        main_camera.box_enabled = true;
        traffic_cone.GetComponent<slow_gun>().in_menu = false;
        main_camera.transform.SetParent(null);
        main_camera.transform.position = start_transform.position;
        main_camera.transform.rotation = start_transform.rotation;
        play_button.gameObject.SetActive(false);
        spawn_script.StartCoroutine(spawn_script.spawn_cars());
    }
    public void play_click()
    {
        click_sound.Play();
    }
    public void restart_game()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    void Update()
    {
        rotate_point.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 10);
    }
}
