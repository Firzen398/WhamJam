
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField]
    float width = 10;

    [SerializeField]
    float height = 10;

    [SerializeField]
    int maxNrOfBubbles = 10;

    [SerializeField]
    BubbleScript bubblePrefab;

    List<BubbleScript> availableBubbles = new List<BubbleScript>();

    public int MaxBubbles
    {
        get { return maxNrOfBubbles; }
    }

    // Define an event.
    public event System.EventHandler BubblePop;

    public void Start()
    {
        for(int i = 0; i < maxNrOfBubbles; i++)
        {
            BubbleScript bubble = Instantiate<BubbleScript>(bubblePrefab);
            bubble.transform.SetParent(transform);
            bubble.gameObject.SetActive(false);
            availableBubbles.Add(bubble);            
        }
    }

    /// <summary>
    /// Returns the bubble that was spawned.
    /// </summary>
    /// <param name="word"></param>
    /// <param name="duration"> How long the bubble should be active on screen </param>
    /// <returns></returns>
    public BubbleScript SpawnNewBubble(string word, float duration)
    {
        BubbleScript obj ;
        int lastAvailableIndex = availableBubbles.Count - 1;
        if (lastAvailableIndex >= 0)
        {
            obj = availableBubbles[lastAvailableIndex];
            availableBubbles.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);

            Vector3 position = new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2), -3f + lastAvailableIndex * 0.1f);
            Vector3 direction = new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-height / 2, height / 2), 0);

            obj.Initialise(position, direction.normalized, word, duration, this);
            return obj;
        }

        // No new bubbles can be spawned.
        return null;
    }

    /// <summary>
    /// Handle the poping of the bubble.
    /// </summary>
    /// <param name="obj"></param>
    public void PopBubble(BubbleScript obj)
    {
        availableBubbles.Add(obj);
        obj.gameObject.SetActive(false);

        OnBubblePop(System.EventArgs.Empty);
    }

    private void OnBubblePop(System.EventArgs e)
    {
        System.EventHandler handler = BubblePop;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}
