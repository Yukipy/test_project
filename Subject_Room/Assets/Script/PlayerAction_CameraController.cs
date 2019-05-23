using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerAction_CameraController : MonoBehaviour
{

    public EventSystem eventsystem;
    public Ray ray;
    public Ray rayItem;
    public RaycastHit hit;
    public GameObject selectedGameObject;
    public string myItem;
    bool once;
    public GameObject itemListCamera;

    //key&door
    public GameObject item_key;
    public GameObject itemBtn_key;
    public GameObject door;
    public AudioClip door_open;

    /// Camera
    public GameObject mainCamera;
    public GameObject subCamera_plant;
    public GameObject SubCamera_stool;

    //darumaotoshi
    public GameObject woodhammer;
    public GameObject item_woodhammer;
    public GameObject woodhammer_set;
    public GameObject arrows;

    //bus
    public GameObject red_bus;
    public GameObject item_red_bus;
   



    // Use this for initialization
    void Start()

    {
        //鍵
        GameObject.Find("itemBtn_key_plane").GetComponent<Renderer>().enabled = false;
        itemBtn_key = GameObject.Find("itemBtn_key");
        itemBtn_key.SetActive(false);
        myItem = "noitem";
        door = GameObject.Find("door");
        mainCamera.SetActive(true);
        subCamera_plant.SetActive(false);
        SubCamera_stool.SetActive(false);

        once = true;

        //木槌とやじるし
        GameObject.Find("item_woodhammer_plane").GetComponent<Renderer>().enabled = false;
        item_woodhammer = GameObject.Find("item_woodhammer");
        item_woodhammer.SetActive(false);
        woodhammer_set = GameObject.Find("woodhammer_set");
        woodhammer_set.SetActive(false);
        arrows = GameObject.Find("arrows");
        arrows.SetActive(false);



        //赤いバス
        GameObject.Find("item_red_bus_plane").GetComponent<Renderer>().enabled = false;
        item_red_bus = GameObject.Find("item_red_bus");
        item_red_bus.SetActive(false);

    }




    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("戻る");
            mainCamera.SetActive(true);
            subCamera_plant.SetActive(false);
            SubCamera_stool.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (eventsystem.currentSelectedGameObject == null)
            {
                SearchRoom();
            }

        }
    }

    public void SearchRoom()
    {

        selectedGameObject = null;
        Debug.Log(Camera.main);
        mainCamera.gameObject.GetComponent<Camera>();

        Ray ray = new Ray();

        if(mainCamera.activeInHierarchy == true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else if (subCamera_plant.activeInHierarchy == true)
        {
            ray = subCamera_plant.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        }
        else if (SubCamera_stool.activeInHierarchy == true)
        {
            ray = SubCamera_stool.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        }

       
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {

            selectedGameObject = hit.collider.gameObject;
            switch (selectedGameObject.name)
            {


                case "Button":
                    Debug.Log("ボタンを押した");
                    item_key.SetActive(true);

                    return;

                case "item_key":
                    Debug.Log("カギを触った");
                    item_key.SetActive(false);
                    itemBtn_key.SetActive(true);
                    return;

                case "door":
                    if (myItem == "key")
                    {
                        Debug.Log("ドアをクリックした");
                        GameObject.Find("door").GetComponent<Renderer>().enabled = true;
                        AudioSource.PlayClipAtPoint(door_open, transform.position);
                        once = false;
                    }
                    return;

                case "woodhammer_set":
                    if(myItem == "woodhammer")
                    {
                        Debug.Log("woodhammerをとりつけた");
                        woodhammer_set.SetActive(true);
                        item_woodhammer.SetActive(false);
                        arrows.SetActive(true);
                    }
                    return;

                case "woodhammer":
                    Debug.Log("ハンマーを取得");
                    item_woodhammer.SetActive(true);
                    woodhammer.SetActive(false);
                    return;

                case "red_bus":
                     Debug.Log("赤いバスを取得");
                     item_red_bus.SetActive(true);
                     red_bus.SetActive(false);
                     return;
                                                      
            }


           


            rayItem = GameObject.Find("itemListCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayItem, out hit, Mathf.Infinity, 1 << 9))
            {
                selectedGameObject = hit.collider.gameObject;
                switch (selectedGameObject.name)
                {
                    case "itemBtn_key_plane":

                        if (myItem == "key")
                        {
                            Debug.Log("カギを戻した");
                            GameObject.Find("itemBtn_key_plane").GetComponent<Renderer>().enabled = false;
                            myItem = "noitem";

                        }
                        else
                        {
                            Debug.Log("カギを装備した");
                            GameObject.Find("itemBtn_key_plane").GetComponent<Renderer>().enabled = true;
                            myItem = "key";
                        }
                        break;


                    case "item_woodhammer_plane":
                        {
                            if (myItem == "woodhammer")
                            {
                                Debug.Log("木槌を戻した");
                                GameObject.Find("item_woodhammer_plane").GetComponent<MeshRenderer>().enabled = false;
                                myItem = "noitem";
                            }
                            else
                            {
                                Debug.Log("木槌を装備した");
                                GameObject.Find("item_woodhammer_plane").GetComponent<MeshRenderer>().enabled = true;
                                myItem = "woodhammer";

                            }
                            break;
                        }

                    case "item_red_bus_plane":
                        {
                            if (myItem == "red_bus")
                            {
                                Debug.Log("バスを戻した");
                                GameObject.Find("item_red_bus_plane").GetComponent<MeshRenderer>().enabled = false;
                                myItem = "noitem";
                            }
                            else
                            {
                                Debug.Log("バスを装備した");
                                GameObject.Find("item_red_bus_plane").GetComponent<MeshRenderer>().enabled = true;
                                myItem = "red_bus";

                            }
                            break;
                        }
                }
                Debug.Log("MainCamera");

            }

            
            if (Physics.Raycast(ray, out hit, 10000.0f))
            {

                if (hit.collider.name == "Plant")
              {
                Debug.Log("subCamera_plant");
              subCamera_plant.SetActive(true);
                  mainCamera.SetActive(false);
                 itemListCamera.GetComponent<Camera>().enabled = false;
                  itemListCamera.GetComponent<Camera>().enabled = true;
            
               }

              if (hit.collider.name == "hint_stool")
                {
                  Debug.Log("subCamera_stool");
                SubCamera_stool.SetActive(true);
                 mainCamera.SetActive(false);
                itemListCamera.GetComponent<Camera>().enabled = false;
                itemListCamera.GetComponent<Camera>().enabled = true;

                }


           }
  

        }
    }
}