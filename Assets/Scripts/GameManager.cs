using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{ 
    const string LYRICSFILE = "Assets/Data/lyrics.json";


    [SerializeField]
    private SongManager songManager;


    public Word [] WordsArray;

    private int wordIndexSpawn;
    private int wordIndexPop;

    private float gameStartTime;


    void Start()
    {
        using (var reader = new StreamReader(LYRICSFILE))
        {
            WordsArray = (Word[])JsonConvert.DeserializeObject<Word[]>(reader.ReadToEnd());
            Debug.Log($"Loaded {WordsArray.Length} WordBubbles");
        }

        StartGame();
    }

    private void StartGame()
    { 
        songManager.GameStart();

        gameStartTime = Time.fixedDeltaTime;

    }
    
    private void Update()
    {
        float timeSinceGameStart = Time.realtimeSinceStartup - gameStartTime;

        if (wordIndexSpawn >= 0 && wordIndexSpawn < WordsArray.Length)
        {
            Word word = WordsArray[wordIndexSpawn];
            if (timeSinceGameStart > word.SpawnTime)
            {
                Debug.Log($"{word.SpawnTime} : {word.Text}");
                wordIndexSpawn++;
            }
        }


    }
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