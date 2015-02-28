using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;



public class creerTableau : MonoBehaviour
{
	public QuestionClass[] qTab = new QuestionClass[]{};
	public string nomFichier = "questions.xml";
    // Use this for initialization
    void Start()
    { 

        GetQuestions();
    }

    // Update is called once per frame
    void Update()
    {

    }

 [ContextMenu("Generate")]
    public QuestionClass[] GetQuestions()
    {
		string q = "";
		string tag = "";
		string r1 = "";
		string r2 = "";
		string r3 = "";
		int i = 0;

		//string xml = System.IO.File.ReadAllText(@"D:\Mes documents D\MesProjetsUnity\UnityPlayboyWonderJam2015\UnityPlayboy\Assets\Ressources\questions.xml");
		string xml = System.IO.File.ReadAllText("questions.xml");

		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(xml); // load the file.
        XmlNodeList QuestionList = xmlDoc.GetElementsByTagName("question"); // array of the level nodes.
		var tmpTable = new List<QuestionClass> ();
        foreach (XmlNode Qinfo in QuestionList)
        {
            XmlNodeList QuestionContent = Qinfo.ChildNodes;
			foreach (XmlNode Qitems in QuestionContent )
			{
				if (Qitems.Name == "q" )
				{
					q = Qitems.InnerText;
					//var name = Qitems.Attributes["name"];
				}
				if (Qitems.Name == "tag" )
				{
					tag = Qitems.InnerText;
				}
				if (Qitems.Name == "r1" )
				{
					r1 = Qitems.InnerText;
				}
				if (Qitems.Name == "r2" )
				{
					r2 = Qitems.InnerText;
				}
				if (Qitems.Name == "r3" )
				{
					r3 = Qitems.InnerText;
				}

			}
			tmpTable.Add (new QuestionClass(q , tag , r1 , r2, r3));
			i++;
        }
		qTab = tmpTable.ToArray ();
        return qTab;
    }
}
