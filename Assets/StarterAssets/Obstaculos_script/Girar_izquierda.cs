using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Girar_izquierda : MonoBehaviour
{

    [SerializeField] private GameObject obstaculo;
    public float timer;
    public float press_time;
    private EEG valor;
    [SerializeField] private GameObject cube;
    public float pos;
    public GameObject player;

    private void Start()
    {
        player=GameObject.Find("XR Origin");
        pos = cube.transform.position.x;
        valor = player.GetComponent<EEG>();
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
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
        obstaculo.transform.rotation = Quaternion.Euler(new Vector3(0, 500 * timer, 90));
    }
}