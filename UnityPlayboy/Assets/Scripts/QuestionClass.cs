using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class QuestionClass {
		
	//Ceci contiendera un obejet la question, les 3 reponse, le tag.
	//Le tout sera dans un tableau.
		
	public string question = "";
	public string tague = "";
	public string RepAime = "";
	public string RepNeutre = "";
	public string RepAimePas = "";
		
		
		
		
	public QuestionClass( string q ,string tg ,string r1 ,string r2 ,string r3) 
		{
			this.question = q;
			this.tague = tg;
			this.RepAime = r1;
			this.RepNeutre = r2;
			this.RepAimePas = r3;
		}
		
		
	public int findRep(string[,] TabPersonalite , int valOfMeuf )
		{
			var i = 1;
			while ( this.tague != TabPersonalite[ valOfMeuf , i] || i > TabPersonalite.Length ) // cherche le tag dans le tableau
			{
				i++;
			}

			if( i <= 3)
			{
				return 1;
			}

			else if(i>3 && i<=6)
			{
				return 0;
			}

			else
			{
				return -1;
			}
			
		}
		
		public string giveRep(int val )
		{
			switch (val)
			{
			case 1 : 
				return "\t"+RepAime;
			case 0 :
                return "\t" + RepNeutre;
			case -1 :
                return "\t" + RepAimePas;
			default :
				return "MarchePas";
			}
		}
		
		/*exemple
    
    var val = q.findRep( TableauRecuDeLaMeuf );
    ReponseAAfficher = q.giveRep( val );
    joueur.gererStress( val )
    joueur.updateSmiley( val )
    joueur.updateScore( val )
    */
		
	}

