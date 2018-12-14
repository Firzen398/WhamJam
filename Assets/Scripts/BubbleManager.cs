using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField]
    float width = 10;
    [SerializeField]
    float height = 10;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
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
