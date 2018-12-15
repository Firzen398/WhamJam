using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{ 
    const string LYRICSFILE = "Assets/Data/lyrics";


    [SerializeField]
    private SongManager songManager;

    [SerializeField]
    private BubbleManager bubbleManager;


    public Word [] WordsArray;

    private int wordIndexSpawn;
    private int wordIndexPop;

    private float gameStartTime;

    private readonly IDictionary<Word, BubbleScript> currentBubbles = new Dictionary<Word, BubbleScript>();


    void Start()
    {
        //string json = File.ReadAllText(Application.dataPath + LYRICSFILE);
        TextAsset json = Resources.Load<TextAsset>("lyrics");

        //Lyrics lyrics = JsonUtility.FromJson<Lyrics>(json.text);
        //WordsArray = lyrics.lyrics;

        WordsArray = (Word[])JsonConvert.DeserializeObject<Lyrics>(json.text).lyrics;
        Debug.Log($"Loaded {WordsArray.Length} WordBubbles");

        StartGame();
    }

    private void StartGame()
    { 
        songManager.GameStart();

        bubbleManager.BubblePop += BubblePoped;

        gameStartTime = Time.time;
        Debug.Log($"gameStartTime {gameStartTime}");

        wordIndexSpawn = 0;
        for(int i = 0; i < bubbleManager.MaxBubbles; i++)
        {
            SpawnNewWord();
        }
    }
    
    private void SpawnNewWord()
    {
        if (wordIndexSpawn >= 0 && wordIndexSpawn < WordsArray.Length)
        {
            Word word = WordsArray[wordIndexSpawn];
            Debug.Log($"Spawn {word.SpawnTime} : {word.Text}");
            wordIndexSpawn++;

            var bubbleScript = bubbleManager.SpawnNewBubble(word.Text, Time.time, word.SoundDuration, word.PopTime);
            currentBubbles.Add(word, bubbleScript);            
        }
    }

    private void BubblePoped(object sende, System.EventArgs e)
    {
        SpawnNewWord();
    }
    /*
    private void Update()
    {
        float timeSinceGameStart = Time.realtimeSinceStartup - gameStartTime;

        Word bubbleToRemove = null;
        foreach (var bubble in currentBubbles)
        {
            if (Time.realtimeSinceStartup > bubble.Key.PopTime)
            {
                Debug.Log($"Pop {bubble.Key.Text}");
                bubbleManager.PopBubble(bubble.Value);
                bubbleToRemove = bubble.Key;
            }
        }

        if (bubbleToRemove != null)
        {
            currentBubbles.Remove(bubbleToRemove);
        }

    }*/
}

[JsonObject]
public class Lyrics
{
    [JsonProperty("lyrics")]
    public Word[] lyrics;
}


[JsonObject]
public class Word
{
    [JsonProperty("spawntime")]
    public float SpawnTime;

    [JsonProperty("poptime")]
    public float PopTime;

    [JsonProperty("soundduration")]
    public float SoundDuration;

    [JsonProperty("text")]
    public string Text;
}

