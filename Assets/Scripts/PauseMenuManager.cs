using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PauseMenuManager : MonoBehaviour
{

    public GameObject Panel;
    public GameManager gm;

    public Toggle togg;

    public AudioSource music;

    public static bool isMusic ;
    public bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("MusicSetting")==1)
        {
            isMusic = false;
        }
        if (PlayerPrefs.GetInt("MusicSetting") == 0)
        {
            isMusic = true;
        }



        music.mute = !isMusic;
        togg.isOn = isMusic;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }
    public void ToggleMusic(bool toggleVal)
    {

        music.mute = !toggleVal;

        if (toggleVal)//sets  true
        {
            PlayerPrefs.SetInt("MusicSetting", 0);
        }
        else
        { //sets false
            PlayerPrefs.SetInt("MusicSetting", 1);
        }
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            Panel.SetActive(true);
            Panel.GetComponent<RectTransform>().SetAsLastSibling();
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Panel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void SaveGame()
    {
        //create save object
        Save save = CreateSaveGameObject();

        //save object to file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        //reset game to default state
        ResetGame();

        Debug.Log("Game saved");
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        
        save.points = GameManager.points;
        save.playerName = GameManager.playerName;
        save.lives = gm.lives;
        save.time = gm.time;

        return save;
    }

    public void ResetGame()
    {
        GameManager.points = 0;
        gm.lives = GameManager.startLives;
        gm.time = GameManager.startTime;
        GameManager.playerName = "Default";
        gm.updateText();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //Clear game
            ResetGame();

            //open save file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();


            GameManager.points = save.points;
            GameManager.playerName = save.playerName;
            gm.lives = save.lives;
            gm.time = save.time;
            gm.updateText();

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");

        }
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as json " + json);
    }
}
