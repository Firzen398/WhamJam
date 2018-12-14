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

    private int wordIndex;

    private void Start()
    {
        songManager.GameStart();

        using (var reader = new StreamReader(LYRICSFILE))
        {
            WordsArray = (Word[])JsonConvert.DeserializeObject<Word[]>(reader.ReadToEnd());
        }
        
    }

    private void Update()
    {
        if (wordIndex >= 0 && wordIndex < WordsArray.Length)
        {
            Word word = WordsArray[wordIndex];


            if (Time.realtimeSinceStartup > word.StartTime)
            {
                Debug.Log($"{word.StartTime} : {word.Text}");
                wordIndex++;
            }



        }

    }
}

[JsonObject]
public class Word
{
    [JsonProperty("starttime")]
    public float StartTime;

    [JsonProperty("endtime")]
    public float EndTime;

    [JsonProperty("text")]
    public string Text;
}