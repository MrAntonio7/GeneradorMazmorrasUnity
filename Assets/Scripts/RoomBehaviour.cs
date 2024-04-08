using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] wall; //0 - top; 1 - down; 2 - left; 3 - right
    [SerializeField] private GameObject[] doors; //0 - top; 1 - down; 2 - left; 3 - right
    public bool connected;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSala(bool[] status)
    {
        connected = false;

        for (int i = 0; i < status.Length; i++)
        {
            wall[i].SetActive(!status[i]); //Desactivamos los muros de las entradas
            doors[i].SetActive(status[i]); //Activamos las de las entradas


            //Activamos puertas de las entradas
            if (status[i])
            {
                connected = true;
            }
        }

        if (!connected)
        {
            Destroy(gameObject);
        }
    }
}
