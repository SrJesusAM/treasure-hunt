using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private string[] movimientos;
    private string ultimoMov;

    public int[] coordenada = new int[2]; // Nivel 1 { 0, 1 };
    public int[] coordenadaResultado = new int[2]; // { 1, 0 };
    public int[] posicionInicial = new int[2]; // { 0, 1 };
    public int[] limitesX = new int[2]; // { 0, 1 };
    public int[] limitesY = new int[2]; // { 0, 1 };

    // Vida y flasback extra
    public int[] coordenadaVida = new int[2]; // { 0, 0 };
    public int[] coordenadaFlashback = new int[2]; // { 1, 1 };
    private bool ganarVida = false;
    private bool ganarFlashback = false;

    // Movimiento
    public float velocidad;
    private bool finalizado = false;
    private bool finAnimacion = false;
    private float xMov;
    private float yMov;

    public int[] coordenadaMovActual = new int[2]; // Nivel 1 { 0, 1 };
    private string ultimoMovAnimacion;
    private int contadorMovimientos = 0;
    private int maxMovimientos;

    // GameObjects
    private GameObject panelDerrota;
    private GameObject panelVictoria;
    private GameObject gameManager;
    private GameObject barcoObject;

    // Managers
    private ManagerScene managerScene;

    // Start is called before the first frame update
    void Start()
    {

        panelDerrota = GameObject.Find("panelDerrota");
        panelDerrota.SetActive(false);

        panelVictoria = GameObject.Find("panelVictoria");
        panelVictoria.SetActive(false);

        this.gameManager = GameObject.Find("GameManagerLevels");
        this.managerScene = gameManager.GetComponent<ManagerScene>();

        barcoObject = GameObject.Find("Barco");
        this.xMov = barcoObject.transform.position.x;
        this.yMov = barcoObject.transform.position.y;

        this.managerScene.nivel = 4;
        if (this.managerScene.nivel < 4)
        {
            maxMovimientos = 5;
        } else
        {
            maxMovimientos = 10;
            
        }

        movimientos = new string[maxMovimientos];


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


    // Update is called once per frame
    void Update()
    {

        if (finalizado)
        {
           
            if (!finAnimacion)
            {
                moverBarcoAnimacion();
            } else
            {
                if (!panelVictoria.activeSelf) Thread.Sleep(1500);
                panelVictoria.SetActive(true);
            }
            
        }         
        

    }

    private void moverBarcoAnimacion()
    {
        float x = barcoObject.transform.position.x;
        float y = barcoObject.transform.position.y;

        // print(contador + " - " + this.ultimoMovAnimacion);

        switch (this.ultimoMovAnimacion)
        {
            
            case "ABAJO":
                
                if (y >= this.yMov)
                {
                    barcoObject.transform.position += Vector3.down * this.velocidad * Time.deltaTime;
                }
                else
                {
                    siguienteMovimiento();
                }
                break;
            case "ARRIBA":
                if (y <= this.xMov)
                {
                    barcoObject.transform.position += Vector3.up * this.velocidad * Time.deltaTime;
                }
                else
                {
                    siguienteMovimiento();
                }
                break;
            case "DERECHA":
                if (x <= this.xMov)
                {
                    barcoObject.transform.position += Vector3.right * this.velocidad * Time.deltaTime;
                }
                else
                {
                    siguienteMovimiento();
                }
                break;
            case "IZQUIERDA":
                if (x >= this.xMov)
                {
                    barcoObject.transform.position += Vector3.left * this.velocidad * Time.deltaTime;
                }
                else
                {
                    siguienteMovimiento();
                }
                break;
            default:
                finAnimacion = true;
                break;
        }

    }


    private void siguienteMovimiento()
    {
        if (contadorMovimientos < contador - 1)
        {
            this.contadorMovimientos++;

            this.ultimoMovAnimacion = this.movimientos[contadorMovimientos];
            actualizarPosicionMovimiento();
        }
        else
        {
            finAnimacion = true;
        }

    }

    private void actualizarPosicionMovimiento()
    {
        switch (this.movimientos[contadorMovimientos])
        {

            case "ABAJO":
                coordenadaMovActual[1]--;
                break;
            case "ARRIBA":
                coordenadaMovActual[1]++;
                break;
            case "DERECHA":
                coordenadaMovActual[0]++;
                break;
            default:
                coordenadaMovActual[0]--;
                break;
        }


        string name = "point_" + coordenadaMovActual[0] + "_" + coordenadaMovActual[1];
        print(name);
        GameObject posNueva = GameObject.Find(name);
        this.yMov = posNueva.transform.position.y;
        this.xMov = posNueva.transform.position.x;

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
        if (contador < maxMovimientos)
        {

            movimientos[contador] = flecha;

            contador++;
            string imgSolPanelName = "imgSol" + contador;
            print(imgSolPanelName);

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


            if (contador >=  maxMovimientos)
            {
                contador = maxMovimientos;
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
            if (coordenada[0] == coordenadaResultado[0] && coordenada[1] == coordenadaResultado[1] && comprobarCamino())
            {
                if (ganarVida) this.vidas++;
                if (ganarFlashback) this.flashbacks++;

                actualizarVidas();
                actualizarFlashbacks();

                this.managerScene.vidas = this.vidas;
                this.managerScene.flashbacks = this.flashbacks;

                this.finalizado = true;
                this.ultimoMovAnimacion = this.movimientos[contadorMovimientos];
                actualizarPosicionMovimiento();

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


    private bool comprobarCamino()
    {

        bool caminoGanador = true;
        int[] posActual = posicionInicial;
        for (int i = 0; i < contador && caminoGanador; i++)
        {

            posActual = devolverPosicionActual(posActual, movimientos[i]);
            // print(posicionInicial[0] +","+ posicionInicial[1]);
            if (!(limitesX[0] <= posActual[0] && posActual[0] <= limitesX[1] && 
                limitesY[0] <= posicionInicial[1] && posicionInicial[1] <= limitesY[1]))
            {
                caminoGanador = false;
            }
        }

        print(caminoGanador);

        return caminoGanador;
    }

    private int[] devolverPosicionActual(int[] posicionActual, string movimientoActual)
    {


        switch (movimientoActual)
        {

            case "ABAJO":
                posicionActual[1]--;
                break;
            case "ARRIBA":
                posicionActual[1]++;
                break;
            case "DERECHA":
                posicionActual[0]++;
                break;
            default:
                posicionActual[0]--;
                break;
        }

        return posicionActual;
    }

    private void mostrarMensaje(bool fin)
    {
        
        GameObject flashbackNObject = GameObject.Find("message");

        if (fin)
        {
            panelDerrota.SetActive(true);

        }
        else
        {
            flashbackNObject.GetComponent<Text>().text = "Intentelo de nuevo";
            flashbackNObject.GetComponent<Text>().color = Color.red;
        }


        
    }


}
