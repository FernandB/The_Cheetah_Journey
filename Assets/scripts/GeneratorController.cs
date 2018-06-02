using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour {

    [SerializeField]
    int minPlatformSize = 1;
    [SerializeField]
    int maxPlatformSize = 10;
    [SerializeField]
    int maxHazardSize = 3;
    [SerializeField]
    int maxHeight = 3;
    [SerializeField]
    int maxDrop = -3;
  
   

    [SerializeField]
    [Range(0.0f,1.0f)]
    float hazardChance = 0.5f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float spikesChance = 0.5f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float holeChance = 0.1f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float enemyChance = 0.1f;

   
    [SerializeField]
    GameObject spikes;
    [SerializeField]
    private GameObject flag;

    [SerializeField]
    GameObject floorTop;
    [SerializeField]
    GameObject floorBottom;
    [SerializeField]
    private GameObject floorTopLeft;
    [SerializeField]
    private GameObject floorTopRight;

    [SerializeField]
    private GameObject enemy;

    private int blockHeight = 0;
    private bool[] blockIsHole;
    private float blockNum = 0;
    private bool isHazard = false;
    private float posXArrival= 121f;
    private const int YMAX= 34;
    private const int YMIN= -41;
    private const float yDelta = 0.8f;
  
    // Use this for initialization
    void Start ()
    {

        Random.InitState(System.DateTime.Now.Millisecond);
        //Calculate the maximum number of platform on x axis
        int maxNbPlatformsHorizontal = Mathf.RoundToInt(posXArrival / floorTop.GetComponent<Renderer>().bounds.size.x-0.2f);
        blockIsHole = new bool[maxNbPlatformsHorizontal+1];
       
        for (int i = 0; i < maxNbPlatformsHorizontal; i++)
        {



            if (i >=0 && Random.value > holeChance||i>0&&blockIsHole[i-1]||i==0)
            {
                int heightTemp = blockHeight;
                blockHeight = blockHeight + Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));


                if (blockHeight > YMAX)
                    blockHeight = YMAX;

                if (blockHeight < YMIN)
                    blockHeight = YMIN;


                if (i < maxNbPlatformsHorizontal - 3 && (i > 0&&!blockIsHole[i - 1])&&heightTemp>=blockHeight)
                    PopSpikes();
                else if(i == maxNbPlatformsHorizontal - 1)
                    Instantiate(flag, new Vector2(blockNum, blockHeight + flag.GetComponent<Renderer>().bounds.size.y/2f), Quaternion.identity);


                int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));


                Instantiate(floorTop, new Vector2(blockNum, blockHeight), Quaternion.identity);

                for (int x = 1; x < platformSize; x++)
                {
                    Instantiate(floorBottom, new Vector2(blockNum, blockHeight - x), Quaternion.identity);
                }
                if (i != maxNbPlatformsHorizontal - 1)
                    PopEnnemi();
                blockIsHole[i] = false;
            }
            else
            {
                blockIsHole[i]=true;
            }
          
            blockNum +=floorTop.GetComponent<Renderer>().bounds.size.x-0.2f;


            


        }
            



        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

       

        
    
   void PopSpikes()
    {
        
        if (isHazard)
        {
            isHazard = false;
        }
        else
        {
            if (Random.value < hazardChance)
                isHazard = true;
            else
                isHazard = false;
        }

        if (isHazard)
        {
            if (Random.value < spikesChance)
            {
               
                //Generate spikes
                if (blockNum>2)
              Instantiate(spikes, new Vector2(blockNum, blockHeight+yDelta), Quaternion.identity);
               
                
            }
            
        }
    }

    void PopEnnemi()
    {
        if (blockNum > 2) {
            if (!isHazard)
            {
                if (Random.value < enemyChance)
                {

                    //Generate three platforms
                    int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));

                    blockNum += floorTop.GetComponent<Renderer>().bounds.size.x - 0.2f;
                    Instantiate(floorTopLeft, new Vector2(blockNum, blockHeight), Quaternion.identity);

                    for (int x = 1; x < platformSize; x++)
                    {
                        Instantiate(floorBottom, new Vector2(blockNum, blockHeight - x), Quaternion.identity);
                    }

                    blockNum += floorTop.GetComponent<Renderer>().bounds.size.x - 0.2f;
                    Instantiate(floorTop, new Vector2(blockNum, blockHeight), Quaternion.identity);

                    for (int x = 1; x < platformSize; x++)
                    {
                        Instantiate(floorBottom, new Vector2(blockNum, blockHeight - x), Quaternion.identity);
                    }
                    blockNum += floorTop.GetComponent<Renderer>().bounds.size.x - 0.2f;
                    Instantiate(floorTopRight, new Vector2(blockNum, blockHeight), Quaternion.identity);

                    for (int x = 1; x < platformSize; x++)
                    {
                        Instantiate(floorBottom, new Vector2(blockNum, blockHeight - x), Quaternion.identity);
                    }

                    //Generate enemy
                    GameObject enemyTemp=Instantiate(enemy, new Vector2(blockNum- floorTop.GetComponent<Renderer>().bounds.size.x/2f, blockHeight + 1f), Quaternion.identity);
                    enemyTemp.SetActive(true);
                    blockNum += floorTop.GetComponent<Renderer>().bounds.size.x-0.2f;


                }
            }
        }
    }

}
