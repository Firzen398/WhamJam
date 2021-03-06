﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    [SerializeField]
    private SongManager songManager;

    [SerializeField]
    private BubbleManager bubbleManager;


    public Word [] WordsArray;

    private int wordIndexSpawn;
    private int wordIndexPop;

    private float gameStartTime;

    private readonly IDictionary<Word, BubbleScript> currentBubbles = new Dictionary<Word, BubbleScript>();

    public int Score;

    [SerializeField]
    public Text ScoreText;

    private bool gamePlaying = false;

    void Start()
    {
        TextAsset json = Resources.Load<TextAsset>("lyrics");

        WordsArray = (Word[])JsonConvert.DeserializeObject<Lyrics>(json.text).lyrics;
        Debug.Log($"Loaded {WordsArray.Length} WordBubbles");

        StartGame();
    }

    void Update()
    {
     /*   if(Input.GetKeyUp(KeyCode.S) && !gamePlaying)
        {
            StartGame();
        }*/

     /*   if (Input.GetKeyUp(KeyCode.P))
        {
            StopGame();
        }*/
    }

    public void StartGame()
    {
        gamePlaying = true;
        Score = 0;
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

    private void StopGame()
    {
        gamePlaying = false;
        songManager.StopGame();
     //   bubbleManager.StopGame();
    }

    public void AddScore(int score)
    {
        Score += score;
        ScoreText.text = $"Score: {Score}";
    }

    
    private void SpawnNewWord()
    {
        if (wordIndexSpawn >= 0 && wordIndexSpawn < WordsArray.Length)
        {
            Word word = WordsArray[wordIndexSpawn];
            Debug.Log($"Spawn {word.SpawnTime} : {word.Text}");
            wordIndexSpawn++;

            var bubbleScript = bubbleManager.SpawnNewBubble(word.Text, word.SoundDuration, word.PopTime-13);
            currentBubbles.Add(word, bubbleScript);            
        }
    }

    private void BubblePoped(object sender, System.EventArgs e)
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

