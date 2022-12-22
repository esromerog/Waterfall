using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Windows;

public class Move_cube : MonoBehaviour
{

    [SerializeField] private GameObject cube;
    public float timer;
    public float press_time;
    public float pos;
    private EEG valor;
    private GameObject player;



    private void Start()
    {
        player=GameObject.Find("XR Origin");
        pos = cube.transform.position.y;
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
        cube.transform.position = new Vector3(cube.transform.position.x, Mathf.Abs((2.5f * Mathf.Sin(10 * timer))) + pos, cube.transform.position.z);

    }
}
