using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class OnDeath : MonoBehaviour
{

    [SerializeField] public Transform player;
    [SerializeField] public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger");
        player.transform.position = spawn.transform.position;
    }

}
