using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public int nivel;
    public int barco;
    public int vidas;
    public int flashbacks;

    private string NIVEL_NOMBRE = "Nivel";
    private string BARCO_NOMBRE = "Barco";
    private string NUMERO_VIDAS = "Vidas";
    private string NUMERO_FLASHBACKS = "Flashbacks";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        LoadData();
    }


    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(NIVEL_NOMBRE, this.nivel);
        PlayerPrefs.SetInt(BARCO_NOMBRE, this.barco);
        PlayerPrefs.SetInt(NUMERO_VIDAS, this.vidas);
        PlayerPrefs.SetInt(NUMERO_FLASHBACKS, this.flashbacks);

    }

    private void LoadData()
    {
        this.nivel = PlayerPrefs.GetInt(NIVEL_NOMBRE, 1);
        this.barco = PlayerPrefs.GetInt(BARCO_NOMBRE, 1);
        this.vidas = PlayerPrefs.GetInt(NUMERO_VIDAS, 3);
        this.flashbacks = PlayerPrefs.GetInt(NUMERO_FLASHBACKS, 3);

    }


    // Metodos comunes
    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void Salir()
    {
        Application.Quit();
    }
    

    // Metodos manejo niveles 
    public void SeleccionNivel(int nivel)
    {
        this.nivel = nivel;
        CambiarEscena("Barcos");
    }

    public void SiguienteNivel()
    {
        this.nivel++;
        if (this.nivel > 5) this.nivel = 5;
        CambiarEscena("Level" + this.nivel);
    }

    public void RepetirNivel()
    {
        CambiarEscena("Level"+this.nivel);
    }

    // Metodos para el manejo de los barcos entre escenas
    public void SeleccionBarco(int barco)
    {
        this.barco = barco;

        switch (barco)
        {

            case 1:
                this.vidas = 3;
                this.flashbacks = 5;
                break;
            case 2:
                this.vidas = 4;
                this.flashbacks = 4;
                break;
            case 3:
                this.vidas = 5;
                this.flashbacks = 3;
                break;
            default:
                this.vidas = 3;
                this.flashbacks = 5;
                break;
        }

        CambiarEscena("Level" + this.nivel);

    }
}
