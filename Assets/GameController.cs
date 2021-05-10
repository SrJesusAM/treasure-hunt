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

    private int vidas;
    private int flashbacks;

    private int contador = 0;
    private string ultimoMov;

    private int[] coordenada = { 0, 0 };
    private int[] coordenadaResultado = { 2, -2 };

    // Vida y flasback extra
    private int[] coordenadaVida = { 1, -1 };
    private int[] coordenadaFlashback = { 2, 0 };
    private bool ganarVida = false;
    private bool ganarFlashback = false;

    // GameObjects
    private GameObject btnReiniciar;
    private GameObject panelVictoria;
    private GameObject gameManager;

    // Managers
    private ManagerScene managerScene;

    // Start is called before the first frame update
    void Start()
    {

        btnReiniciar = GameObject.Find("btnRepetir");
        btnReiniciar.SetActive(false);

        panelVictoria = GameObject.Find("panelVictoria");
        panelVictoria.SetActive(false);

        this.gameManager = GameObject.Find("GameManagerLevels");
        this.managerScene = gameManager.GetComponent<ManagerScene>();

        GameObject barcoObject = GameObject.Find("Barco");
        
        switch (this.managerScene.barco)
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

        this.vidas = managerScene.vidas;
        actualizarVidas();

        this.flashbacks = managerScene.flashbacks;
        actualizarFlashbacks();

        

    }  
    
    private void actualizarVidas()
    {
        GameObject vidaNObject = GameObject.Find("VidaNumber");
        vidaNObject.GetComponent<Text>().text = "x " + this.vidas;
    }

    private void actualizarFlashbacks()
    {
        GameObject flashbackNObject = GameObject.Find("FlashbackNumber");
        flashbackNObject.GetComponent<Text>().text = "x " + this.flashbacks;
    }


    public void movimiento(string flecha)
    {
        

        if (contador < 5)
        {
            contador++;
            string imgSolPanelName = "imgSol" + contador;

            GameObject imgSolObj = GameObject.Find(imgSolPanelName);
            ultimoMov = flecha;
            switch (flecha)
            {

                case "ABAJO":
                    imgSolObj.GetComponent<Image>().sprite = flechaAbajo;
                    coordenada[1]--;
                    break;
                case "ARRIBA":
                    imgSolObj.GetComponent<Image>().sprite = flechaArriba;                    
                    coordenada[1]++;
                    break;
                case "DERECHA":
                    imgSolObj.GetComponent<Image>().sprite = flechaDerecha;
                    coordenada[0]++;
                    break;
                default:
                    imgSolObj.GetComponent<Image>().sprite = flechaIzquierda;
                    coordenada[0]--;
                    break;
            }

            if (coordenadaVida[0] == coordenada[0] && coordenadaVida[1] == coordenada[1]) ganarVida = true;
            if (coordenadaFlashback[0] == coordenada[0] && coordenadaFlashback[1] == coordenada[1]) ganarVida = true;


            if (contador >= 5)
            {
                contador = 5;
            }
            
        }
        
    }

    public void borrarMovimientoAnterior()
    {
        if (flashbacks > 0)
        {

            if (contador > 0)
            {
                string imgSolPanelName = "imgSol" + contador;
                GameObject imgSolObj = GameObject.Find(imgSolPanelName);

                imgSolObj.GetComponent<Image>().sprite = vacio;

                contador--;
                if (contador <= 0) contador = 0;

                flashbacks--;
                if (flashbacks <= 0) flashbacks = 0;
                actualizarFlashbacks();

                switch (ultimoMov)
                {

                    case "ABAJO":
                        coordenada[1]++;
                        break;
                    case "ARRIBA":
                        coordenada[1]--;
                        break;
                    case "DERECHA":
                        coordenada[0]--;
                        break;
                    default:
                        coordenada[0]++;
                        break;
                }
            }
            
        }
        
    }

    public void finalizar()
    {
        if (this.vidas >= 0)
        {
            
            if (coordenada[0] == coordenadaResultado[0] && coordenada[1] == coordenadaResultado[1])
            {

                if (ganarVida) this.vidas++;
                if (ganarFlashback) this.flashbacks++;

                actualizarVidas();
                actualizarFlashbacks();

                this.managerScene.vidas = this.vidas;
                this.managerScene.flashbacks = this.flashbacks;


                panelVictoria.SetActive(true);
            }
            else
            {
                this.vidas--;
                actualizarVidas();

                bool fin = false;
                if (this.vidas < 0) fin = true;

                mostrarMensaje(fin);
            }
        }
        else
        {

        }
    }

    private void mostrarMensaje(bool fin)
    {
        
        GameObject flashbackNObject = GameObject.Find("message");

        if (fin)
        {
            flashbackNObject.GetComponent<Text>().text = "No te quedan Vidas, ¿Reiniciamos?";
            flashbackNObject.GetComponent<Text>().color = Color.green;

            if (btnReiniciar != null)
            {
                btnReiniciar.SetActive(true);
            }

        }
        else
        {
            flashbackNObject.GetComponent<Text>().text = "Intentelo de nuevo";
            flashbackNObject.GetComponent<Text>().color = Color.red;
        }


        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
