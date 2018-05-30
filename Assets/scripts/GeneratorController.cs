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
    GameObject floorTop;
    [SerializeField]
    GameObject floorBottom;
    [SerializeField]
    GameObject spikes;

    private int blockHeight = 0;

    private float blockNum = 0;
    private bool isHazard = false;
    private float posXArrival= 121f;
    private const int YMAX= 19;
    private const int YMIN= -21;
    private const float yDelta = 0.8f;

    // Use this for initialization
    void Start ()
    {
        //Calculate the maximum number of platform on x axis
       int maxNbPlatformsHorizontal = Mathf.RoundToInt(posXArrival / floorTop.GetComponent<Renderer>().bounds.size.x);
        
       
        for (int i = 0; i < maxNbPlatformsHorizontal; i++)
        {

          
            Random.InitState(System.DateTime.Now.Millisecond);

            blockHeight = blockHeight + Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));
           
            if (blockHeight > YMAX)
                blockHeight = YMAX;

            if (blockHeight < YMIN)
                blockHeight = YMIN;

            PopSpikes();
            Random.InitState(System.DateTime.Now.Millisecond);

            int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
           
                Instantiate(floorTop, new Vector2(blockNum, blockHeight), Quaternion.identity);

                for (int x = 1; x < platformSize; x++)
                {
                    Instantiate(floorBottom, new Vector2(blockNum, blockHeight - x), Quaternion.identity);
                }

                blockNum+=floorTop.GetComponent<Renderer>().bounds.size.x;



           

        }
            



        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


   void PopSpikes()
    {
        
        Random.InitState(System.DateTime.Now.Millisecond);

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
            Random.InitState(System.DateTime.Now.Millisecond);
            if (Random.value < spikesChance)
            {

                //Generate spikes
                
              Instantiate(spikes, new Vector2(blockNum, blockHeight+yDelta), Quaternion.identity);
               
                
            }
            
        }
    }

 
}
