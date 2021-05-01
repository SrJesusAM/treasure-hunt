using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public int nivel;
    public int barco;

    private string NIVEL_NOMBRE = "Nivel";
    // private string BARCO_NOMBRE = "Barco";

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
    }

    private void LoadData()
    {
        this.nivel = PlayerPrefs.GetInt(NIVEL_NOMBRE, 1);
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
        SeleccionNivel(this.nivel);
    }

    public void RepetirNivel()
    {
        SeleccionNivel(this.nivel);
    }

    // Metodos para el manejo de los barcos entre escenas
    public void SeleccionBarco(int barco)
    {
        this.barco = barco;
        print("Barco elegido:"+this.barco);
        print("Nivel elegido:" + this.nivel);
        //CambiarEscena("JuegoNivel" + this.nivel);

    }
}
