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

    // Metodos 

    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void SeleccionNivel(int nivel)
    {
        print("Nivel anterior:" + this.nivel);
        this.nivel = nivel;
        print("Nivel asignado:" + this.nivel);

        

        CambiarEscena("Barcos");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(NIVEL_NOMBRE, this.nivel);
    }

    private void LoadData()
    {
        this.nivel = PlayerPrefs.GetInt(NIVEL_NOMBRE, 1);
    }
}
