using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstruccionesController : MonoBehaviour
{

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;
    public GameObject panel5;

    private int panel = 1;

    public GameObject panelTutorial;


    // Start is called before the first frame update
    void Start()
    {

        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void siguiente()
    {
        modificarPanel(panel, false);
        panel++;
        modificarPanel(panel, true);

    }

    public void anterior()
    {
        modificarPanel(panel, false);
        panel--;
        modificarPanel(panel, true);

    }

    public void finalizarTutorial()
    {
        panelTutorial.SetActive(false);
    }

    public void modificarPanel(int numero, bool valor)
    {

        switch (numero)
        {

            case 1:
                panel1.SetActive(valor);
                break;
            case 2:
                panel2.SetActive(valor);
                break;
            case 3:
                panel3.SetActive(valor);
                break;
            case 4:
                panel4.SetActive(valor);
                break;
            case 5:
                panel5.SetActive(valor);
                break;
        }

    }
}
