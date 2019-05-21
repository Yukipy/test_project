using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class PlayerAction_OtherAction : MonoBehaviour
{
    public GameObject mainCamera;
    public EventSystem eventsystem;
    public Ray ray;
    public Ray rayItem;
    public RaycastHit hit;
    public GameObject selectedGameObject;
    public GameObject item_key;
    public Transform[] wall = new Transform[4];
    private Transform target;
    int i;
    private Quaternion targetRotation;
    public GameObject subCamera;
        
    // Use this for initialization
    void Start()
    {
        target = wall[0];
        i = 0;
        targetRotation = Quaternion.LookRotation(target.position - transform.position);

       
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            if (eventsystem.currentSelectedGameObject == null)
            {
                
            }
            else
            {
                switch (eventsystem.currentSelectedGameObject.name)
                {
                    case "turnLBtn":

                        i++;
                        if (i >= 4)
                        {
                            i = 0;
                        }
                        target = wall[i];
                        targetRotation = Quaternion.LookRotation(target.position - transform.position);
                        break;

                    case "turnRBtn":

                        i--;
                        if (i <= -1) 
                        {
                            i = 3;
                        }
                        target = wall[i];
                        targetRotation = Quaternion.LookRotation(target.position - transform.position);
                        

                        break;
                }
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

        


    }
   
}
        
