using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void CargarNivel1()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void CargarAjustes()
    {
        SceneManager.LoadScene("AJUSTES");
    }
}
