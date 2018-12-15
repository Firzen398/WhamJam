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

        gameStartTime = Time.realtimeSinceStartup;
        Debug.Log($"gameStartTime {gameStartTime}");
    }
    
    private void Update()
    {
        float timeSinceGameStart = Time.realtimeSinceStartup - gameStartTime;

        if (wordIndexSpawn >= 0 && wordIndexSpawn < WordsArray.Length)
        {
            Word word = WordsArray[wordIndexSpawn];
            if (timeSinceGameStart >= word.SpawnTime)
            {
                Debug.Log($"Spawn {word.SpawnTime} : {word.Text}");
                wordIndexSpawn++;

                var bubbleScript = bubbleManager.SpawnNewBubble(word.Text, word.PopTime - word.SpawnTime);
                currentBubbles.Add(word, bubbleScript);
            }
        }


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

    }
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

