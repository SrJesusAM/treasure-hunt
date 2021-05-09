using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Sprite barco1;
    public Sprite barco2;
    public Sprite barco3;

    public Sprite flechaArriba;
    public Sprite flechaAbajo;
    public Sprite flechaDerecha;
    public Sprite flechaIzquierda;

    public Sprite vacio;

    private int contador = 0;
    private string[] solucion = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManagerLevels");
        ManagerScene managerScene = gameManager.GetComponent<ManagerScene>();

        GameObject barcoObject = GameObject.Find("Barco");
        print("Barco elegido scene"+ managerScene.barco);

        switch (managerScene.barco)
        {

            case 1:
                barcoObject.GetComponent<Image>().sprite = barco1;
                break;
            case 2:
                barcoObject.GetComponent<Image>().sprite = barco2;
                break;
            case 3:
                barcoObject.GetComponent<Image>().sprite = barco3;
                break;
            default:
                barcoObject.GetComponent<Image>().sprite = barco1;
                break;
        }


    }        


    public void movimiento(string flecha)
    {
        print(contador);

        if (contador < 5)
        {
            contador++;
            string imgSolPanelName = "imgSol" + contador;

            GameObject imgSolObj = GameObject.Find(imgSolPanelName);
            switch (flecha)
            {

                case "ABAJO":
                    imgSolObj.GetComponent<Image>().sprite = flechaAbajo;
                    solucion[contador - 1] = "ABAJO";
                    break;
                case "ARRIBA":
                    imgSolObj.GetComponent<Image>().sprite = flechaArriba;
                    solucion[contador - 1] = "ARRIBA";
                    break;
                case "DERECHA":
                    imgSolObj.GetComponent<Image>().sprite = flechaDerecha;
                    solucion[contador - 1] = "DERECHA";
                    break;
                default:
                    imgSolObj.GetComponent<Image>().sprite = flechaIzquierda;
                    solucion[contador - 1] = "IZQUIERDA";
                    break;
            }


            if (contador >= 5)
            {
                contador = 5;
            }
            
        }
    }

    public void borrarMovimientoAnterior()
    {

        if (contador > 0)
        {
            string imgSolPanelName = "imgSol" + contador;
            GameObject imgSolObj = GameObject.Find(imgSolPanelName);

            imgSolObj.GetComponent<Image>().sprite = vacio;
            solucion[contador - 1] = "";

            contador--;
            if (contador <= 0)
            {
                contador = 0;
            }
            
        }

        
    }

    public void finalizar()
    {

        for (int i =0; i < contador; i++)
        {
            print("i:"+i +" - " + solucion[i]);
        } 

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
