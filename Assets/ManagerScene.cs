using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public int nivel;
    public int barco;

    private string NIVEL_NOMBRE = "Nivel";
    private string BARCO_NOMBRE = "Barco";

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
    }

    private void LoadData()
    {
        this.nivel = PlayerPrefs.GetInt(NIVEL_NOMBRE, 1);
        this.barco = PlayerPrefs.GetInt(BARCO_NOMBRE, 1);
        print("Barco elegido:" + this.barco);
        print("Nivel elegido:" + this.nivel);
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
    
    public void test()
    {
        print("test");
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
        CambiarEscena("Level" + this.nivel);

    }
}
