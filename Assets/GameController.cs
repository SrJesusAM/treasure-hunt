using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Sprite barco1;
    public Sprite barco2;
    public Sprite barco3;


    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManagerLevels");
        ManagerScene managerScene = gameManager.GetComponent<ManagerScene>();

        print("Barco elegido scene"+ managerScene.barco);

        switch (managerScene.barco)
        {

            case 1:
                GetComponent<Image>().sprite = barco1;
                break;
            case 2:
                GetComponent<Image>().sprite = barco2;
                break;
            case 3:
                GetComponent<Image>().sprite = barco3;
                break;
            default:
                GetComponent<Image>().sprite = barco1;
                break;
        }


    }        



    // Update is called once per frame
    void Update()
    {
        
    }
}
