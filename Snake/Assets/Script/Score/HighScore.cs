using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;


public class HighScore : MonoBehaviour {

	GameController puntaje;
	public GameObject marcadorSnake;

	private string conexionString;
	private List<HighScoreFuncion> highScores = new List<HighScoreFuncion> ();

	public GameObject scorePrefab;
	public Transform scoreParent;

	//variable para delimitar el top de rangos que se podra mostrar en pantalla
	public int highRanks;

	public int saveScores;

	//Variables para ingreso de nombvre en el campo 
	public InputField ingresaNombre;
	public GameObject nombrePlayer;

	void Start () {
		//Encuentre la ruta del archivo sin poner la ruta especifica
		conexionString = "URI=file:" + Application.dataPath + "/HighScoreDB.sqlite";

		CrearTabla ();

		BorrarExtraScore ();

		//IngreseScore ("Test", 33);

		MostrarScore();
	}


	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			nombrePlayer.SetActive (!nombrePlayer.activeSelf);
		}
		*/
	}

	private void CrearTabla(){
		
		using (IDbConnection dbConexion = new SqliteConnection(conexionString)) 
		{
			dbConexion.Open();

			using (IDbCommand dbCmd = dbConexion.CreateCommand()) 
			{
				//En values le pasamos lod detos de la funcion
				string sqlQuery = string.Format("CREATE TABLE if not exists HighScores (JugadorID INTEGER PRIMARY KEY AUTOINCREMENT  NOT NULL  UNIQUE , Nombre TEXT NOT NULL , Score INTEGER NOT NULL, Dia DATETIME NOT NULL  DEFAULT CURRENT_DATE)");

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConexion.Close ();
			}
		}

	}




	public void llamaScore(){
		nombrePlayer.SetActive (!nombrePlayer.activeSelf);
	}





//Ingresa el texto una vez ingreado despliega todo los score
	public void IngresaNombre(){
		puntaje = new GameController ();

		if (ingresaNombre.text != string.Empty)
		{
			//int score = UnityEngine.Random.Range (1, 50);
			IngreseScore (ingresaNombre.text, puntaje.puntuacionActual());
			ingresaNombre.text = string.Empty;
			ingresaNombre.text = string.Empty;
			marcadorSnake.SendMessage ("GameListo");

			MostrarScore ();
		}
	}

	//Funcion para editar base de datos desde Unity 
	private void IngreseScore(string nombre, int nuevoScore){

		GetScore ();

		int hscontador = highScores.Count;

		//Encuetra el puntaje más bajo en Score y lo elimina suponiendo que el registro
		//solo permite 10 y se ingreso un puntaje mas alto
		if (highScores.Count > 0)
		{
			HighScoreFuncion menorScore = highScores [highScores.Count - 1];
			if (menorScore != null && saveScores > 0 && highScores.Count >= saveScores && nuevoScore > menorScore.Score) 
			{
				BorrarScore (menorScore.ID);
				hscontador--;
			}
		}

		if (hscontador < saveScores)
		{
			using (IDbConnection dbConexion = new SqliteConnection(conexionString)) 
			{
				dbConexion.Open();

				using (IDbCommand dbCmd = dbConexion.CreateCommand()) 
				{
					//En values le pasamos lod detos de la funcion
					string sqlQuery = string.Format("INSERT INTO HighScores(Nombre,Score) VALUES(\"{0}\",\"{1}\")",nombre,nuevoScore);

					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					dbConexion.Close ();
				}
			}
		}
	}


	//Funcion para comunicar base de datos con Unity
	private void GetScore(){

		highScores.Clear ();

		using (IDbConnection dbConexion = new SqliteConnection(conexionString)) 
		{
			dbConexion.Open();

			using (IDbCommand dbCmd = dbConexion.CreateCommand()) 
			{
				string sqlQuery = "SELECT * FROM HighScores";
			
				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader()) 
				{
					while (reader.Read ()) 
					{
						highScores.Add (new HighScoreFuncion (reader.GetInt32 (0), reader.GetString (1), reader.GetInt32 (2), reader.GetDateTime (3)));
					}

					dbConexion.Close ();
					reader.Close ();
				}
			}
		}
		highScores.Sort ();
	}


	//Elimita los regristros de la tabla desde Unity

	private void BorrarScore(int id){

		using (IDbConnection dbConexion = new SqliteConnection(conexionString)) 
		{
			dbConexion.Open();
			using (IDbCommand dbCmd = dbConexion.CreateCommand()) 
			{
				//En values le pasamos lod detos de la funcion
				string sqlQuery = string.Format("DELETE FROM HighScores WHERE JugadorID = \"{0}\"",id);

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteScalar ();
				dbConexion.Close ();
			}
		}
	}




//Mostrar los score en el camvas clonando desde los prefabs
//limita la aparcion de score en canvas
	private void MostrarScore(){
		
		GetScore ();

		foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score")) //Desruye score pasados
		{
			Destroy (score);
		}

		for (int i = 0; i < highRanks; i++) 
		{
			if(i<= highScores.Count - 1) //Evita error de desbordamiento si no hay más score que msotrar
			{
				
			GameObject tmpObjec = Instantiate (scorePrefab);
			HighScoreFuncion tmpScore = highScores [i];

			tmpObjec.GetComponent<HighScorePrefab> ().SetScore (tmpScore.Nombre, tmpScore.Score.ToString(), "#" + (i + 1).ToString ());
		
			tmpObjec.transform.SetParent (scoreParent);
			tmpObjec.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);

			}
		}
	}


	private void BorrarExtraScore(){

		GetScore ();

		if (saveScores <= highScores.Count)
		{
			int borrarCount = highScores.Count - saveScores;
			highScores.Reverse ();

			using (IDbConnection dbConexion = new SqliteConnection(conexionString)) 
			{
				dbConexion.Open();
				using (IDbCommand dbCmd = dbConexion.CreateCommand()) 
				{

					for (int i = 0; i < borrarCount; i++)
					{
						//En borrar  contreo elimina todos los demas registros y solo deja los que queremos que sean visibles
						string sqlQuery = string.Format("DELETE FROM HighScores WHERE JugadorID = \"{0}\"", highScores[i].ID);

						dbCmd.CommandText = sqlQuery;
						dbCmd.ExecuteScalar ();
					}

					dbConexion.Close ();
				}
			}
		}
	}
		

}
