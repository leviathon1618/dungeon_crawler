using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
    public List<GameObject> cars;
    public List<GameObject> endpoints;
    public GameObject particle;
    //public GameObject car_hold_script;
    public List<GameObject> car_list;
    public Text population;
    public int population_int =0;
    public bool cars_spawnable;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public IEnumerator spawn_cars()
    {
        if (cars_spawnable == false)
        {
            yield break;
        }
        yield return new WaitForSeconds(3);
        int spawn_point = 0;

        while (true)
        {
            population_int++;
            population.text = population_int.ToString();
            yield return new WaitForSeconds(5);
            GameObject car = Instantiate(cars[Random.Range(0, 7)], endpoints[spawn_point].transform.position, Quaternion.identity);
            car_list.Add(car);
            if (spawn_point == 0)
            {
                spawn_point = 1;
            }
            else
            {
                spawn_point = 0;
            }
        }

    }
}