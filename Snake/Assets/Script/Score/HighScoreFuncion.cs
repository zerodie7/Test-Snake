using System;
using System.Collections;
using System.Text;
using System.Linq;
using System.Collections.Generic;


//Uso el IComparable para tener la lsita ordenada

class HighScoreFuncion : IComparable<HighScoreFuncion>
{
	public int Score { get; set;}
	public string Nombre { get; set;}
	public DateTime Dia{ get; set;}
	public int ID{ get; set;}
	public HighScoreFuncion (int id, string nombre, int score, DateTime dia)
	{
		this.Score = score;
		this.Nombre = nombre;
		this.Dia = dia;
		this.ID = id;
		}
		
//Comparacion de los elementos de la lista primero los marcadores si son mayores o menores
// y si existen empates checa la fecha del marcador que fue primero
	public int CompareTo (HighScoreFuncion other){
		if (other.Score < this.Score) {
			return -1;
		} else if (other.Score > this.Score) {
			return 1;
		} else if (other.Dia < this.Dia) {
			return -1;
		} else if (other.Dia > this.Dia) {
			return 1;
		}
		return 0;
	}
}
	
