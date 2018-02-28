using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class Snake : MonoBehaviour {

	public GameObject game;
	public GameObject score;

	public Transform Top, Bot, Left, Right;
	public GameObject colaprefab,comidaSnake;
	public AudioClip choque;
	public AudioClip pickUp;

	private AudioSource musicaJugador;

	//velocidad frames
	public float velocidad = 0.1f;

	Vector2 direccion = Vector2.zero;
	Vector2 moverVector;

	List<Transform> cola = new List<Transform>();


	bool comida = false;
	bool vertical= true;
	bool horizontal= true;

	void Start () {
		comidaAleatoria ();
		InvokeRepeating("Move", 0.2f, velocidad);    
		musicaJugador = GetComponent<AudioSource>();
	}

	void Update () {
		bool gamePlaying = game.GetComponent<GameController> ().gameState == GameState.jugando;


			if (Input.GetKey (KeyCode.RightArrow) && horizontal) {
				vertical = true;
				horizontal = false;
				direccion = Vector2.right;
			} else if (Input.GetKey (KeyCode.DownArrow) && vertical) {
				vertical = false;
				horizontal = true;
				direccion = -Vector2.up;
			} else if (Input.GetKey (KeyCode.LeftArrow) && horizontal) {
				vertical = true;
				horizontal = false;
				direccion = -Vector2.right;
			} else if (Input.GetKey (KeyCode.UpArrow) && vertical) {
				vertical = false;
				horizontal = true;
				direccion = Vector2.up;
			}
		moverVector = direccion / 4.0f;

	}


	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag ("comida")) {
			comida = true;		
			Destroy (col.gameObject);
			comidaAleatoria ();
			game.SendMessage("incremetaPuntos");

			musicaJugador.clip = pickUp;
			musicaJugador.Play();
				
		} else if (col.CompareTag ("pared")) {
			Time.timeScale = 0f;
			game.GetComponent<GameController> ().gameState = GameState.finalizado;

			score.SendMessage ("llamaScore");

			game.GetComponent<AudioSource> ().Stop ();
			musicaJugador.clip = choque;
			musicaJugador.Play();

			//game.SendMessage ("RestartGame");
			print ("perdiste");
		}
	}


	void Move() {
			Vector2 v = transform.position;
			transform.Translate (direccion);

			if (comida) {
				if (velocidad > 0.002) {
					velocidad = velocidad - 0.1f;
				}
				GameObject g = (GameObject)Instantiate (colaprefab, v, Quaternion.identity);
				cola.Insert (0, g.transform);
				comida = false;
			} else if (cola.Count > 0) {
				cola.Last ().position = v;
				cola.Insert (0, cola.Last ());
				cola.RemoveAt (cola.Count - 1);
			} 
		transform.Translate (moverVector);
	}



	public void GameListo(){
		game.GetComponent<GameController> ().gameState = GameState.listo;
	}


	public void comidaAleatoria () 
	{
		int x = (int)Random.Range(Left.position.x, Right.position.x);
		int y = (int)Random.Range(Bot.position.y, Top.position.y);
		Instantiate(comidaSnake,new Vector2(x, y),Quaternion.identity); 
	}
		
}
