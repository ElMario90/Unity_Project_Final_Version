using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab; 
    public int numberOfPlatforms = 10; 
    public float platformLength = 5f; 
    public float zigzagOffset = 2f; 

    private Vector3 lastPosition; 
    private float prefabHeight; 

    void Start()
    {
        Collider prefabCollider = platformPrefab.GetComponent<Collider>();
        if (prefabCollider != null)
        {
            prefabHeight = prefabCollider.bounds.size.y;
        }
        else
        {
            
            prefabHeight = 1f;
        }

        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        lastPosition = Vector3.zero;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            bool isZigZag = i % 2 == 0;

            Vector3 newPosition = lastPosition + new Vector3(0f, 0f, platformLength);

            if (isZigZag)
            {
                float xOffset = (i % 4 == 0) ? zigzagOffset : -zigzagOffset;
                newPosition += new Vector3(xOffset, 0f, 0f);
            }

            newPosition.y = platformPrefab.transform.position.y;

            Instantiate(platformPrefab, newPosition, Quaternion.identity);

            lastPosition = newPosition;
        }
    }
}
