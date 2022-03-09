using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

#region Explicação

/* 
Spawna os tijolos, lida com as funções que tem que serem chamadas qnd um é destruido 
 
*/

#endregion

public enum BrickType
{
    Fosfolipídio,
    Glicolipídio,
    Colesterol,
    P_Integral,
    P_Periférica
}

public enum BrickColors
{
    Rosa,
    Verde,
    Azul,
    Laranja,
    Roxo
}

public class BrickManager : MonoBehaviour
{

    [SerializeField] Vector3 InstatiateOffset = new Vector3();
    [SerializeField] int BricksByColumn = 5;
    [SerializeField] int BricksOnGame = 25;
    [SerializeField] List<BrickSprites> BrickSprites = new List<BrickSprites>();
    [SerializeField] GameObject[] BricksGameObjs;
     public Vector3 InitialInstatiatePos = new Vector3();
    [SerializeField] float TargetXPos;
    GameObject Brick1;
    public static Action<Brick> OnBrickDestruction;
    public Dictionary<BrickType, int> BrickTypeCount = new Dictionary<BrickType, int>() {
        {BrickType.Colesterol,  0},
        {BrickType.Fosfolipídio,  0},
        {BrickType.Glicolipídio,  0},
        {BrickType.P_Integral,  0},
        {BrickType.P_Periférica,  0}
    };
    public List<ProgressBar> progressBars = new List<ProgressBar>();
    Dictionary<BrickType, ProgressBar> progressBarsAccessor = new Dictionary<BrickType, ProgressBar>();
    public int BricksDestroyed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        InitBricks();
        
        OnBrickDestruction += OnDrestroyed;
        //  ViewportHandler.Instance.SetUnitResolution(Brick1.transform);
        InitializeProgressBars();


    }

    // Update is called once per frame
    void Update()
    {
       

    }

    void InitBricks()
    {
        Vector3 InstatiatePos = InitialInstatiatePos;
        for (int i = 1; i <= BricksOnGame; i++)
        {
            GameObject Brick = Instantiate(BricksGameObjs[Random.Range(0, BricksGameObjs.Length)], this.transform);
            Brick BrickScript = Brick.GetComponent<Brick>();
           BrickSprites RandomSprites = BrickSprites[Random.Range(0, BrickSprites.Count)];
           foreach (Sprite sprite in RandomSprites.SpritesBrick)
           {
                BrickScript.BrickSprites.Add(sprite);
           }
            Brick.GetComponent<SpriteRenderer>().sprite = BrickScript.BrickSprites[BrickScript.hitLives - 1];
            Brick.GetComponent<CameraAnchor>().anchorOffset = InstatiatePos;
            Brick.GetComponent<CameraAnchor>().SetAnchor(ViewportHandler.Instance.TopLeft);
            if (i == 5)
            {
                Brick1 = Brick;
            }
            if (i % BricksByColumn == 0)
            {
                
                InstatiatePos.x = InitialInstatiatePos.x;
                InstatiatePos.y -= InstatiateOffset.y;
                
            }else
            {
                InstatiatePos.x += InstatiateOffset.x;
                
            }
            BrickTypeCount[BrickScript.BrickType]++;
        }
        
    }

    public void OnDrestroyed(Brick brick)
    {
        progressBarsAccessor[brick.BrickType].AddOneInProgressValue();
        BricksDestroyed++;
        if (BricksDestroyed == BricksOnGame)
        {
            MembranaGameManager.Instance.IsGameStarted = false;
            MembranaGameManager.Instance.OnGameFinishedMethod(levelFinishedStatus.Won);
            BallsManager.Instance.DestroyBall();
        }
    }

    void InitializeProgressBars()
    {
        
        int i = 0;
        foreach (KeyValuePair<BrickType, int> kvp in BrickTypeCount)
        {
            progressBarsAccessor.Add(kvp.Key, progressBars[i]);
            progressBarsAccessor[kvp.Key].SetProgressSettings(0, kvp.Value);
            i++;
        }
    }

    
}

[System.Serializable]
public class BrickSprites
{
    public Sprite[] SpritesBrick;
    public BrickColors brickColor;
}
