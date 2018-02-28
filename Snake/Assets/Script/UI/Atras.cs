using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Atras : MonoBehaviour {

    public enum Acciones { CargarEscena }

    public Acciones accion;
    public string nombreEscena;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
        {
            switch (accion)
            {
                case Acciones.CargarEscena:
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(nombreEscena);
                    break;
            }


        }
	}
}
