using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


public class Tests : MonoBehaviour
{

    public string[,] allPersonalite = new string[10, 10] { { "Aime beaucoup sa maman", "Reserve" , "Animaux" , "Quotidien", "Pickup", "Religieux", "Social", "Rude", "Ose", "Desespere"}, 
		{ "A toujours été le dernier choisi", "Desespere" , "Religieux", "Reserve", "Quotidien", "Animaux", "Pickup", "Rude", "Ose", "Social" },
		{ "Sort dans les bars tous les soirs", "Pickup" , "Ose", "Social", "Rude", "Quotidien", "Religieux", "Reserve", "Desespere", "Animaux"}, 
		{ "Est tout à fait normal", "Social" , "Quotidien", "Reserve", "Pickup", "Desespere", "Ose", "Religieux", "Rude", "Animaux"},
		{ "Aime les nuits torrides", "Rude" , "Pickup", "Ose", "Animaux", "Reserve", "Social", "Desespere", "Quotidien", "Religieux"},
		{ "Est un régulier de la messe du dimanche", "Religieux" , "Quotidien", "Social", "Animaux", "Reserve", "Desespere", "Pickup", "Ose", "Rude"},
		{ "Est comparable au lion", "Animaux" , "Rude", "Pickup", "Religieux", "Ose", "Social", "Reserve", "Desespere", "Quotidien"},
		{ "N'aime pas ce que lui renvoie le miroir", "Social" , "Desespere", "Rude", "Quotidien", "Reserve", "Ose", "Pickup", "Religieux", "Animaux"},	
		{ "S'excite de maniere etrange", "Desespere" , "Religieux", "Ose", "Animaux", "Pickup", "Rude", "Quotidien", "Reserve", "Social"},
		{ "A des gouts particuliers", "Animaux" , "Ose", "Reserve", "Desespere", "Rude", "Religieux", "Pickup", "Social", "Quotidien"}};


    private GUIText Texte;
    public List<QuestionClass> ListeQuestion;
    public List<int> ListeId;
    public creerTableau creerTableauScript;
    public QuestionClass QuestionCourrante;

	public GameObject gameManager;


    public bool BonneReponse = false;
    public int Nombre,
               i,
               j = 0,
               PositionY = 0,
               PersonnaliteVal,
               ValeurRetournee = -3,
               Stress;
    public string Indice,
                  Phrase1,
                  Phrase2,
                  Phrase3,
                  VariableTampon;
    
    public void Start()
    {
        
   		PersonnaliteVal = gameManager.GetComponent<GameManager>().personaliteRecu;
		Debug.Log (PersonnaliteVal);
       creerTableauScript = gameManager.GetComponent<creerTableau>();
       QuestionClass[] qTab = creerTableauScript.GetQuestions();
        Texte = GetComponent<GUIText>();

        for (i = 0; i < 3; i++)
        {
            while (!BonneReponse)
            {
                Nombre = Random.Range(0, qTab.Length);
                if (qTab[Nombre].findRep(allPersonalite, PersonnaliteVal) == 1)
                {
                    BonneReponse = true;
                    Debug.Log(qTab[Nombre].question);
                }
                   
                
            }
            
            while(ListeId.Contains(Nombre))
                Nombre = Random.Range(0, qTab.Length);
           
            ListeId.Add(Nombre);
            ListeQuestion.Add(qTab[Nombre]);
        }

        for (i = 0; i < 3; i++)
        {
            Nombre = Random.Range(0, 2);
            if (Nombre == 1)
            {
                Nombre = Random.Range(0, 3);
                QuestionClass tmpSHit = ListeQuestion[Nombre];
                ListeQuestion[Nombre] = ListeQuestion[i];
                ListeQuestion[i] = tmpSHit;
            }
        }
    }

    void Update()
    {

            //Ajustement des 3 lignes (ajout d'espaces devant et modification de la fontSize si besoin)
            if (j == 0)
            {
                Indice = allPersonalite[PersonnaliteVal, 0];
                Phrase1 = ListeQuestion[0].question;
                Phrase2 = ListeQuestion[1].question;
                Phrase3 = ListeQuestion[2].question;

                Phrase1 = Phrase1.Insert(0, "   ");
                Phrase2 = Phrase2.Insert(0, "   ");
                Phrase3 = Phrase3.Insert(0, "   ");
                if (Phrase1.Length > 49 || Phrase2.Length > 49 || Phrase3.Length > 49)
                {
                    Texte.fontSize = 15;
                    Texte.lineSpacing = 2;
                }

                if (Phrase1.Length > 76 || Phrase2.Length > 76 || Phrase3.Length > 76)
                {
                    Texte.fontSize = 13;
                    Texte.lineSpacing = 2.7F;
                }
                j++;
            }
            //Phrase1 = MelangerChaine(Phrase1, 10);
        
    }

    void FixedUpdate()
    {
        Texte.text = "\t\t\t\t\t\t\t\t\tRobert " + Indice + "\n\n" + Phrase1 + "\n" + Phrase2 + "\n" + Phrase3;

        /*if (Input.GetAxis("Vertical") > 0.0F && PositionY != -2)
            PositionY--;

        else if (Input.GetAxis("Vertical") < 0.0F && PositionY != 0)
            PositionY++;*/
        if (Input.GetKeyDown("down") && PositionY != -2)
            PositionY--;

        else if (Input.GetKeyDown("up") && PositionY != 0)
            PositionY++;

        //Changement de position de la flèche 
        switch (PositionY)
        {
            case 0:
                Phrase1 = Phrase1.Replace("   ", "-> ");
                Phrase2 = Phrase2.Replace("-> ", "   ");
                Phrase3 = Phrase3.Replace("-> ", "   ");
                break;
            case -1:
                Phrase2 = Phrase2.Replace("   ", "-> ");
                Phrase1 = Phrase1.Replace("-> ", "   ");
                Phrase3 = Phrase3.Replace("-> ", "   ");
                break;
            case -2:
                Phrase3 = Phrase3.Replace("   ", "-> ");
                Phrase2 = Phrase2.Replace("-> ", "   ");
                Phrase1 = Phrase1.Replace("-> ", "   ");
                break;
        }


        //Ablation de la flèche pour l'output si le bouton de confirmation est appuyé
        if (Input.GetKeyDown("space"))
            switch (PositionY)
            {
                case 0:
                    QuestionCourrante = ListeQuestion[0];
                    ValeurRetournee = QuestionCourrante.findRep(allPersonalite,PersonnaliteVal);
                    break;
                case -1:
                    QuestionCourrante = ListeQuestion[1];
                    ValeurRetournee = QuestionCourrante.findRep(allPersonalite,PersonnaliteVal);
                    break;
                case -2:
                    QuestionCourrante = ListeQuestion[2];
                    ValeurRetournee = QuestionCourrante.findRep(allPersonalite,PersonnaliteVal);
                    break;
                    
            }
        if (ValeurRetournee != -3)
        {

            Phrase1 = Phrase3 = "";
            Phrase2 = QuestionCourrante.giveRep(ValeurRetournee);
        }
    }

    string MelangerChaine(string Chaine, int Stress)
    {
        char CaractereTemp;
        char[] TableauChar = Chaine.ToCharArray();
        int i;

        for (i = 0; i < Stress; i += 2)
        {
            CaractereTemp = Chaine[Stress];
            TableauChar[Stress] = Chaine[Chaine.Length - Stress];
            TableauChar[Chaine.Length - Stress] = CaractereTemp;
            Chaine = new string(TableauChar);
        }
        return Chaine;
    }
    
}
