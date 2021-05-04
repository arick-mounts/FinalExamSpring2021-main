using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{

    public Transform entryContainer;
    public Transform entryTemplate;




    void Awake()
    {



        addHighscoreEntry(GameManager.points, GameManager.playerName);

        entryTemplate.gameObject.SetActive(false);

        printHighscoreTable();
        /*for (int i = 1; i < 11; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryRectTransform.gameObject.SetActive(true);


            

            string rankString;
            switch (i)
            {
                default:
                    rankString = i + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }
             
            
            entryTransform.Find("POS").GetComponent<Text>().text = rankString;
            
        }*/
        /* Highscores highscores = new Highscores { highscoreList = new List<HighscoreEntry>() { new HighscoreEntry { score = 4, name = "steve"} } };
         string json = JsonUtility.ToJson(highscores);
         PlayerPrefs.SetString("highscoreTable", json);
         PlayerPrefs.Save();*/


    }

    public void printHighscoreTable() {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        float templateHeight = 20f;
        int i = 1;

        foreach (HighscoreEntry highscore in highscores.highscoreList)
        {

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryRectTransform.gameObject.SetActive(true);




            string rankString;
            switch (i)
            {
                default:
                    rankString = i + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }


            entryTransform.Find("POS").GetComponent<Text>().text = rankString;

            entryTransform.Find("Score").GetComponent<Text>().text = highscore.score.ToString();

            entryTransform.Find("Name").GetComponent<Text>().text = highscore.name.ToString();
            i++;

        }
    }

    public void addHighscoreEntry (int score, string name)
    {
        bool isAdded = false;

        //create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name};

        //load highscore from player prefs
        string jsonString = PlayerPrefs.GetString("highscoreTable", "{ \"highscoreList\":[{ \"score\":0,\"name\":\"AAA\"}]}");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
       
        

        //find proper location for highscore and added
        foreach(HighscoreEntry highscore in highscores.highscoreList)// (int i = 0; i < highscores.highscoreList.Count; i++)
        {
            if (highscoreEntry.score >= highscore.score)
            {
                highscores.highscoreList.Insert(highscores.highscoreList.IndexOf(highscore), highscoreEntry);
                isAdded = true;
                break;
            }
        }
        //if the table has fewer than 10 elements add highscore entry at the end.
        if (highscores.highscoreList.Count < 10 && isAdded == false)
        {
            highscores.highscoreList.Add(highscoreEntry);
            isAdded = true;
        }
        //if new entry is less than all previous high scores end function
        
        if (isAdded == false)
        {
            return;   
        }
        //check size of highscores list and remove entries above 10
        while (highscores.highscoreList.Count > 10)
        {
            highscores.highscoreList.RemoveAt(highscores.highscoreList.Count - 1);
        }

        //save highscore table ase playerpref
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save(); 
    }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreList;
    }
    
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;

       
    }

    public void resetHighscores()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        PlayerPrefs.SetString("highscoreTable", "{ \"highscoreList\":[{ \"score\":0,\"name\":\"AAA\"}]}");
        printHighscoreTable();
    }
}
