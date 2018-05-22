using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour {
    private static float maxYposFlag;
    private static float maxXposFlag;
    
    private static float smallSpace = 1f;
    private static float bigSpace = 1f;
    private static float firstPlatformPosX;
    private static float firstPlatformPosY;
    private static bool posXOK = false;
    private static bool posYOK = false;
    private static ArrayList objectsToErase = new ArrayList();
    private static Transform platformPos;
    private static Transform obstaclePos;
    private static GameObject[] obstacles;
    [SerializeField]
    GameObject[] obstaclesTemp;
    [SerializeField]
   GameObject platformTemp;
    [SerializeField]
    Transform maxPosFlag;
    private static GameObject platform;
    [SerializeField]
    Transform firstPlatform;
    [SerializeField]
    GameObject lvl1;
    private static GameObject levelOne;
    [SerializeField]
    GameObject[] floorPlatformTemp;
    private static GameObject[] floorPlatform;
    [SerializeField]
     GameObject levelTemp;
    private static GameObject level;
    // Use this for initialization
    void Start () {
        maxXposFlag = maxPosFlag.localPosition.x;
        maxYposFlag = maxPosFlag.localPosition.y;
        levelOne = lvl1;
        firstPlatformPosX = 1f;
        firstPlatformPosY = 0f;
        platformPos = transform;
        level = levelTemp;
        floorPlatform = floorPlatformTemp;
        platform = platformTemp;
        obstacles = obstaclesTemp;
        obstaclePos = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void buildChunk()
    {
        levelOne.SetActive(false);
        CreateMultipleFloorPlatformsOnPath(new Vector3(0, 0));
    }

    public static void CreateMultipleFloorPlatformsOnPath(Vector3 previousPlatformPos)
    {
       //I check the position of the previous platform to instantiate the next platform
        if (previousPlatformPos.x == 0 && previousPlatformPos.y == 0)
        {
            platformPos.position= new Vector3(firstPlatformPosX, firstPlatformPosY);
          
            platform = Instantiate(floorPlatform[0], platformPos);
            //obstaclePos.position = new Vector3(firstPlatformPosX + platform.GetComponent<Renderer>().bounds.size.x, firstPlatformPosY + 0.1f);
            platform.transform.localPosition = platformPos.localPosition;
            //GameObject obstacle = Instantiate(obstacles[(int)Mathf.Round(UnityEngine.Random.Range(0f, 1f))],obstaclePos);
            objectsToErase.Add(platform);
           // objectsToErase.Add(obstacle);
            previousPlatformPos = platform.transform.localPosition;

        }
        //While the flag is not near from the last platform, I continue to place new platforms
        while (!posXOK || !posYOK)
        {
           
              float spaceXF = 0.00000000000000000f;
                if (previousPlatformPos.x < maxXposFlag)
                {
                    int spaceX = (int)Mathf.Round(UnityEngine.Random.Range(1, 2));
                    if (spaceX == 1)
                        spaceXF = smallSpace;
                    else
                        spaceXF = bigSpace;
                }
                else
                {
                    posXOK = true;
                }
                float spaceYF = 0.00000000000000000f;
                if (previousPlatformPos.y < maxYposFlag)
                {
                    int spaceY = (int)Mathf.Round(UnityEngine.Random.Range(1, 2));
                    if (spaceY > 1)
                        spaceYF = smallSpace;
                }
                else
                {
                    posYOK = true;
                }
                int NbfloorPrefabs = floorPlatform.Length;
                GameObject nextPlatform = floorPlatform[(int)Mathf.Round(UnityEngine.Random.Range(0, NbfloorPrefabs))];

                platformPos.Translate(new Vector3(spaceXF,spaceYF));
            //obstaclePos.Translate(new Vector3(spaceXF,spaceYF));
            platform = Instantiate(nextPlatform, platformPos);
                platform.transform.localPosition =platformPos.localPosition;
           // GameObject obstacle = Instantiate(obstacles[(int)Mathf.Round(UnityEngine.Random.Range(0f, 1f))], obstaclePos);
            objectsToErase.Add(platform);
            //objectsToErase.Add(obstacle);
            if (previousPlatformPos != platform.transform.localPosition)
                    previousPlatformPos = platform.transform.localPosition;
                else
                    break;
            }

            
        }

        
    

    public static void DeleteObjects()
    {
        int i = 0;
        int length = objectsToErase.Count;
        while (i < length)
        {
            GameObject obj = (GameObject)objectsToErase[i];
            Destroy(obj);
            i++;
        }

        objectsToErase = new ArrayList();
    }
}
