using System.Collections;
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
    /// <param name="duration"></param>
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

            Vector3 position = new Vector3(Random.Range(-width / 2, width / 2), 0, Random.Range(-height / 2, height / 2));
            Vector3 direction = new Vector3(Random.Range(-width / 2, width / 2), 0, Random.Range(-height / 2, height / 2));

            obj.Initialise(position, direction.normalized, word, this);
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
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        float wHalf = (width * .5f);
        float hHalf = (height * .5f);

        Vector3 topLeftCorner = new Vector3(transform.position.x - wHalf, transform.position.y + hHalf, 1f);
        Vector3 topRightCorner = new Vector3(transform.position.x + wHalf, transform.position.y + hHalf, 1f);
        Vector3 bottomLeftCorner = new Vector3(transform.position.x - wHalf, transform.position.y - hHalf, 1f);
        Vector3 bottomRightCorner = new Vector3(transform.position.x + wHalf, transform.position.y - hHalf, 1f);

        Gizmos.DrawLine(topLeftCorner, topRightCorner);
        Gizmos.DrawLine(topRightCorner, bottomRightCorner);
        Gizmos.DrawLine(bottomRightCorner, bottomLeftCorner);
        Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
    }
}
