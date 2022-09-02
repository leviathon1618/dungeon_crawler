using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveTo : MonoBehaviour
{
    //public Transform goal;
    public NavMeshAgent agent;
    public Rigidbody rb;
    public float dist;
    public int next_pos;
    public int points_reached;
    private int endpoint;
    private bool hidecar = false;
    public GameObject spawner_script;
    public GameObject particle;
    //public GameObject spawner_script;
    public List<GameObject> end_points;
    public List<GameObject> waypoints;
    public Text Population_text;

    void Start()
    {
        particle = GameObject.Find("explosion");
        spawner_script = GameObject.Find("spawner");
        Population_text = spawner_script.GetComponent<spawner>().population;
        for (int i = 0; i < 10; i++)
        {
            waypoints[i] = GameObject.Find("waypoints").transform.GetChild(i).gameObject;
        }
        end_points[0] = GameObject.Find("end_point_1");
        end_points[1] = GameObject.Find("end_point_2");
        endpoint = Random.Range(0, 2);
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        next_pos = Random.Range(0, 10);
        agent.destination = waypoints[next_pos].transform.position;
    }    

    public IEnumerator shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = GameObject.Find("camera_move").transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            GameObject.Find("camera_move").transform.position += new Vector3(x, 0, z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        GameObject.Find("camera_move").transform.position = orignalPosition;
    }
    public void remove_car()
    {
        Instantiate(spawner_script.GetComponent<spawner>().particle, transform.position, Quaternion.identity);
        hidecar = true;
        spawner_script.GetComponent<spawner>().population_int -= 1;
        if (spawner_script.GetComponent<spawner>().population_int == 0)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        Population_text.text = spawner_script.GetComponent<spawner>().population_int.ToString();
        StartCoroutine(shake(0.1f, 0.02f));
        Destroy(gameObject);
    }

    public void Update()
    {
        dist = Vector3.Distance(waypoints[next_pos].transform.position, transform.position);
        if (dist<=0.5f && points_reached < 10)
        {
            points_reached++;
            next_pos = Random.Range(0, 10);
            agent.SetDestination(waypoints[next_pos].transform.position);
            Vector3 movement = Vector3.forward * Time.deltaTime * 2;
            agent.Move(movement);
        }
        if (hidecar == false)
        {
            foreach (var item in spawner_script.GetComponent<spawner>().car_list)
            {
                if (item == null)
                {
                    continue;
                }
                if (item.transform.name != name)
                {
                    float dist = Vector3.Distance(item.transform.position, transform.position);
                    if (dist <= 0.35f)
                    {
                        remove_car();
                        break;
                    }
                }
            }
        }
        
        if (points_reached == 10)
        {
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(end_points[endpoint].transform.position);
            }
            dist = Vector3.Distance(end_points[endpoint].transform.position, transform.position);
            if (dist <1f)
            {
                spawner_script.GetComponent<spawner>().population_int -= 1;
                Population_text.text = spawner_script.GetComponent<spawner>().population_int.ToString();
                if (spawner_script.GetComponent<spawner>().population_int == 0)
                {
                    SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                }
                Destroy(gameObject);
            }
        }
    }
}