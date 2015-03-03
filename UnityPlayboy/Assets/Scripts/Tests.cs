using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


public class Tests : MonoBehaviour
{

    public string[,] allPersonalite = new string[11, 10] { { "aime beaucoup sa maman", "Reserve" , "Animaux" , "Quotidien", "Pickup", "Religieux", "Social", "Rude", "Ose", "Desespere"}, 
		{ "a toujours été le dernier choisi", "Desespere" , "Religieux", "Reserve", "Quotidien", "Animaux", "Pickup", "Rude", "Ose", "Social" },
		{ "sort dans les bars tous les soirs", "Pickup" , "Ose", "Social", "Rude", "Quotidien", "Religieux", "Reserve", "Desespere", "Animaux"}, 
		{ "est tout à fait normal", "Social" , "Quotidien", "Reserve", "Pickup", "Desespere", "Ose", "Religieux", "Rude", "Animaux"},
		{ "aime les nuits torrides", "Rude" , "Pickup", "Ose", "Animaux", "Reserve", "Social", "Desespere", "Quotidien", "Religieux"},
		{ "est un régulier de la messe du dimanche", "Religieux" , "Quotidien", "Social", "Animaux", "Reserve", "Desespere", "Pickup", "Ose", "Rude"},
		{ "est comparable au lion", "Animaux" , "Rude", "Pickup", "Religieux", "Ose", "Social", "Reserve", "Desespere", "Quotidien"},
		{ "n'aime pas ce que lui renvoie le miroir", "Social" , "Desespere", "Rude", "Quotidien", "Reserve", "Ose", "Pickup", "Religieux", "Animaux"},	
		{ "s'excite de maniere etrange", "Desespere" , "Religieux", "Ose", "Animaux", "Pickup", "Rude", "Quotidien", "Reserve", "Social"},
		{ "a des gouts particuliers", "Animaux" , "Ose", "Reserve", "Desespere", "Rude", "Religieux", "Pickup", "Social", "Quotidien"},
        { "la roche", "Rude", "Religieux", "Desespere", "Pickup", "Animaux", "Quotidien", "Ose", "Reserve", "Social"}};

    private GUIText Texte;
    public Animator animator;
    public List<QuestionClass> ListeQuestion;
    public List<int> ListeId;
    public creerTableau creerTableauScript;
    public QuestionClass QuestionCourrante;
    public int valStress = 0;
    public int valAlcool = 0;
    public int limiteCap = -2;
    public int PhraseRoche;
    public string prenom;

	public GameObject gameManager;


    public bool BonneReponse = false;
    public bool ajouterStress = false;
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
    
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void afficherTexte()
    {
        ListeQuestion.Clear();
        ListeId.Clear();
       
        if (gameManager.GetComponent<GameManager>().tourJoueur1)
        {
            valStress = gameManager.GetComponent<GameManager>().joueur1Stress;
        }
        else
        {
            valStress = gameManager.GetComponent<GameManager>().joueur2Stress;
        }
        prenom = gameManager.GetComponent<GameManager>().prenomRecu;
        PersonnaliteVal = gameManager.GetComponent<GameManager>().personaliteRecu;
        creerTableauScript = gameManager.GetComponent<creerTableau>();
        QuestionClass[] qTab = creerTableauScript.GetQuestions();
        Texte = GetComponent<GUIText>();
        limiteCap = -2;

        for (i = 0; i < 3; i++)
        {
            while (!BonneReponse && i == 0)
            {
                Nombre = Random.Range(0, qTab.Length);
                if (qTab[Nombre].findRep(allPersonalite, PersonnaliteVal) == 1)
                {
                    BonneReponse = true;
                }
            }

            BonneReponse = false;

            while (ListeId.Contains(Nombre))
                Nombre = Random.Range(0, qTab.Length);
            ListeId.Add(Nombre);
            ListeQuestion.Add(qTab[Nombre]);
        }

        for (i = 0; i < 3; i++)
        {
            Nombre = Random.Range(0, 7);
            if (Nombre != 1)
            {
                Nombre = Random.Range(1, 3);
                QuestionClass tmpSHit = ListeQuestion[Nombre];
                ListeQuestion[Nombre] = ListeQuestion[i];
                ListeQuestion[i] = tmpSHit;
            }
        }

        Indice = allPersonalite[PersonnaliteVal, 0];

        Phrase1 = ListeQuestion[0].question;
        Phrase2 = ListeQuestion[1].question;
        Phrase3 = ListeQuestion[2].question;

        Phrase1 = MelangerChaine(Phrase1);
        Phrase2 = MelangerChaine(Phrase2);
        Phrase3 = MelangerChaine(Phrase3);

        switch (giveMalusByStress(valStress))
        {
            case 1:
                Phrase3 = "";
                limiteCap = -1;
                break;
            case 0:
                break;
            case -1:
                Phrase2 = "";
                Phrase3 = "";
                limiteCap = 0;
                break;
            default:
                Debug.Log("PROBLEME DANS LE SWITCH");
                break;
        }


        Phrase1 = Phrase1.Insert(0, "-> ");
        Phrase2 = Phrase2.Insert(0, "   ");
        Phrase3 = Phrase3.Insert(0, "   ");
        PositionY = 0;


     
        Texte.fontSize = 12;
        Texte.lineSpacing = 2.7F;
        

        Texte.text = "\t"+ prenom +" "+ Indice + "\n\n" + Phrase1 + "\n" + Phrase2 + "\n" + Phrase3;
        Texte.transform.position = new Vector3(0.105f, 0.23f, 0);
    }

    public void gererInput(int input)
    {
       
        if (input == 1)
        {
            PositionY++;
            if (PositionY >= 0)
                PositionY = 0;
        }
        else if (input == 2)
        {
            PositionY--;
            if (PositionY <= limiteCap)
                PositionY = limiteCap;
        }
        else
        {
            switch (PositionY)
            {
                case 0:
                    QuestionCourrante = ListeQuestion[0];
                    ValeurRetournee = QuestionCourrante.findRep(allPersonalite, PersonnaliteVal);
                    break;
                case -1:
                    QuestionCourrante = ListeQuestion[1];
                    ValeurRetournee = QuestionCourrante.findRep(allPersonalite, PersonnaliteVal);
                    break;
                case -2:
                    QuestionCourrante = ListeQuestion[2];
                    ValeurRetournee = QuestionCourrante.findRep(allPersonalite, PersonnaliteVal);
                    break;
            
            }
        
            if (ValeurRetournee != -3)
            {
                if (PersonnaliteVal == 10)
                {
                    Phrase1 = Phrase3 = "";

                    PhraseRoche = Random.Range(0, 5);

                    switch(PhraseRoche)
                    {
                        case 0:
                            Phrase2 = "*Awkward silence*";
                            break;
                        case 1:
                            Phrase2 = "...";
                            break;
                        case 2:
                            Phrase2 = "*On peut entendre les mouches voler*";
                            break;
                        case 3:
                            Phrase2 = "*Sons de criquets*";
                            break;
                        case 4:
                            Phrase2 = "(Aucune réponse)";
                            break;
                    }
                }
                else
                {
                    Phrase1 = Phrase3 = "";
                    Phrase2 = QuestionCourrante.giveRep(ValeurRetournee);
                }
                if (ValeurRetournee == -1)
                {
                    ajouterStress = true;
                }
            }
        }

        switch (PositionY)
        {
            case 0:
                Phrase1 = Phrase1.Replace("   ", "-> ");
                Phrase2 = Phrase2.Replace("-> ", "   ");
                Phrase3 = Phrase3.Replace("-> ", "   ");
                break;
            case -1:
                Phrase1 = Phrase1.Replace("-> ", "   ");
                Phrase2 = Phrase2.Replace("   ", "-> ");
                Phrase3 = Phrase3.Replace("-> ", "   ");
                break;
            case -2:
                Phrase1 = Phrase1.Replace("-> ", "   ");
                Phrase2 = Phrase2.Replace("-> ", "   ");
                Phrase3 = Phrase3.Replace("   ", "-> ");
                break;
        }


        Texte.text = "\t" + prenom + " " + Indice + "\n\n" + Phrase1 + "\n" + Phrase2 + "\n" + Phrase3;
    }

    string MelangerChaine(string Chaine)
    {
        int Alcool = 0;

        if (gameManager.GetComponent<GameManager>().tourJoueur1)
        {
            Alcool = gameManager.GetComponent<GameManager>().alcoholJoueur1;
        }
        else
        {
            Alcool = gameManager.GetComponent<GameManager>().alcoholJoueur2;
        }

        char CaractereTemp;
        char[] TableauChar = Chaine.ToCharArray();
        int i;

        float pourcentage = (Chaine.Length * 0.03f * Alcool);
        int positionRandom1;
        int positionRandom2;
        if (pourcentage > 0 && pourcentage < 1)
            pourcentage = 1;

        for (i = 0; i < pourcentage; i++)
        {
            positionRandom1 = Random.Range(0, Chaine.Length);
            positionRandom2 = Random.Range(0, Chaine.Length);
            while ((Chaine[positionRandom1] == ' ') || (Chaine[positionRandom2] == ' '))
            {
                positionRandom1 = Random.Range(0, Chaine.Length);
                positionRandom2 = Random.Range(0, Chaine.Length);
            }
            CaractereTemp = Chaine[positionRandom1];
            TableauChar[positionRandom1] = Chaine[positionRandom2];
            TableauChar[positionRandom2] = CaractereTemp;
            Chaine = new string(TableauChar);
        }
        return Chaine;
    }

    public void clearTextBox() {
        Texte.text = "";
    }

    public int giveMalusByStress( int stressValue ) //valeur sur 10
{
	int nombre = Random.Range(0 , 101);
	switch(stressValue)
	{
		case 0 :
			return 0;
		case 1 :
            if (nombre < 2)
                return 1;
            else
                return 0;
		case 2:
		if(nombre < 4)
			return 1;
		else 
			return 0;
		case 3 : 
		if(nombre < 8)
			return 1;
		else 
			return 0;
		case 4 : 
		if(nombre < 16)
			return 1;
		else 
			return 0;
		case 5 :
		if(nombre < 32)
			return 1;
		else 
			return 0;
		case 6 :
		if(nombre < 64)
			return 1;
		else 
			return 0;
		case 7 :
		if(nombre < 75)
			return 1;
		else 
			return 0;
		case 8 :
		if(nombre < 85)
			return 1;
		else 
			return 0;
		case 9 :
		if (nombre < 51)
			return 1;
		else 
			return -1;
		case 10 : 
			return -1;
        default :
            return -6;

	}
}

    public int addToStressValue()
{
	
    if (ajouterStress)
    {
        ajouterStress = false;
        return 2;
    }

    ajouterStress = false;
    return 0;
 }

    public int giveScore ( int val ) 
{
	switch(val)
	{
		case 1 :
			return 2;
		case 0 :
			return 0;
		case -1 : 
			return -1;
        default:
            return -6;
	}
}

    
}
