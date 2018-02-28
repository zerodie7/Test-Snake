using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameState{espera, jugando, finalizado, listo}

public class GameController : MonoBehaviour {

	public GameState gameState = GameState.espera; //Estado por defecto

	public GameObject uiIdle, uiScore;
	public Text recordText, puntosText;

	//Pausa
	public  bool juegoPausa = false;
	public GameObject pausaUI;

	public int puntos = 0 ;
	private AudioSource musicaJuego;

	void Start () {
		musicaJuego = GetComponent<AudioSource>();
		recordText.text = "Hi: " + puntuacionMaxima().ToString ();
	}
	
	void Update () {

		bool userAction = Input.GetKeyDown ("up") || Input.GetKeyDown ("down") || Input.GetKeyDown ("left") || Input.GetKeyDown ("right") || Input.GetMouseButtonDown (0);
			

		//Inicio del juego
		if (gameState == GameState.espera && (userAction)){
			gameState = GameState.jugando;
			uiIdle.SetActive (false);
			uiScore.SetActive (true);
			musicaJuego.Play ();
			if (userAction) 
			{
				Time.timeScale = 1f;
			}
		} 
		else if (gameState == GameState.jugando) {

			if (Input.GetKeyDown (KeyCode.Escape))
			{
				if (juegoPausa)
				{
					Resumen ();
				} else{
					Pausa();
				}
			}
		}

		//Se prepara para iniciar despues del score
		else if (gameState == GameState.listo) {
				if (userAction){
				RestartGame ();
			}
		}
	}


	public void incremetaPuntos(){
		puntosText.text = (++puntos).ToString ();
		if (puntos >= puntuacionMaxima ()) {
			recordText.text = "Hi: " + puntos.ToString();
			SaveScore(puntos);
		}
			SavePuntuacionActual (puntos);
	}

	public int puntuacionMaxima(){
		return PlayerPrefs.GetInt ("Score", 0);
	}		

	public void SaveScore(int puntuacionActual){
		PlayerPrefs.SetInt ("Score", puntuacionActual);
	}

	public int puntuacionActual(){
		return PlayerPrefs.GetInt ("ScoreActual", 0);
	}		

	public void SavePuntuacionActual(int puntuacionreciente){
		PlayerPrefs.SetInt ("ScoreActual", puntuacionreciente);
	}


	public void Resumen()
	{
		pausaUI.SetActive (false);
		Time.timeScale = 1f;
		juegoPausa = false;
		musicaJuego.Play ();
	}

	public void Pausa()
	{
		pausaUI.SetActive (true);
		Time.timeScale = 0f;
		juegoPausa = true;
		musicaJuego.Pause ();
	}

	public void RestartGame(){
		SceneManager.LoadScene ("02a)Classic");
	}

	public void QuitRequest(){
		print ("Quit requested");
		Application.Quit ();
	}


	public void LoadLevel(string name){
		print ("New Level load: " + name);
		SceneManager.LoadScene(name);
	}

}
