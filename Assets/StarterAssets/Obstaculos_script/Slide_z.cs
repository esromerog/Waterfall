using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows;

public class Slide_z : MonoBehaviour
{

    [SerializeField] private GameObject cube;
    public float timer;
    public float press_time;
    public float pos;
    private EEG valor;
    public GameObject player;



    private void Start()
    {
        pos = cube.transform.position.z;
        player = GameObject.Find("XR Origin");
        valor = player.GetComponent<EEG>();
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        float nuevo_valor;
        nuevo_valor = valor.value;
        if (nuevo_valor > 0.5)
        {

            press_time += Time.deltaTime;
            timer += Time.deltaTime*(float)(1-nuevo_valor+0.1);
        }
        else
        {

            timer+=Time.deltaTime;
        }
        cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, Mathf.Abs((2.5f * Mathf.Sin(10 * timer))) + pos);

    }
}
