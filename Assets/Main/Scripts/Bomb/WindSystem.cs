using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class WindSystem : Singleton<WindSystem>
{
    

  
    
    

    [SerializeField] private int startWindCount = 4;

    [SerializeField] private Transform windIndicator;
    [SerializeField] private TextMeshProUGUI windForceIndicator; 

    [SerializeField] private ParticleSystem windParticleSystem;

    
    private AudioSource windAudioSource;
    private ParticleSystem.MainModule windModule;


    private Rigidbody windedObject;
    private Vector3 windDirVec; 
    
    private bool isWind = false;
    private int windDirInd = 0;
    
    

    struct WindParameter
    {
        public float windForce; 
        public float windParticleSize; 
        public float windParticleSpeed;
        public float windSoundPitch;
    }
    private WindParameter[] windForceParam = new WindParameter[]
    {
        new WindParameter{windForce = 0.3f, windParticleSize = 0.1f, windParticleSpeed = 1f , windSoundPitch = 1f },
        new WindParameter{windForce = 0.5f, windParticleSize = 0.15f, windParticleSpeed = 2 , windSoundPitch = 1.5f},
        new WindParameter{windForce = 0.8f, windParticleSize = 0.2f, windParticleSpeed = 3 , windSoundPitch = 2.0f},

    
    };

    private int windForceInd = 0; 
    

    
    private   List<int[]> windDir = new List<int[]>
    {
        new int[] {0, 1},
        new int[] {1, 1},
        new int[] {1, 0},
        new int[] {1, -1},
        new int[] {0, -1},
        new int[] {-1, -1},
        new int[] {-1, 0},
        new int[] {-1, 1}
    };
    private void Start()
    {
        windAudioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnPassCountChanged += OnPassCountChanged;
 

        windModule = windParticleSystem.main;
        windModule.startSpeed = windForceParam[windForceInd].windParticleSpeed;
        windModule.startSize = windForceParam[windForceInd].windParticleSize;

      
        
      ChangeWindDir();
    }


    public void SetWindedObject(Rigidbody setWindedObject)
    {
        
        windedObject = setWindedObject;
    }

    private void FixedUpdate()
    {
        
        if (!windedObject) return;
        if (!isWind) return;

       
        // // Rigidbodyに風の力を適用
        windedObject.AddForce(windDirVec * windForceParam[windForceInd].windForce);
    }

    private void OnPassCountChanged(object sender, EventArgs e)
    {


        Debug.Log($"<color=red>{windForceParam[windForceInd].windForce}</color>");
   
        
        int passCount = GameManager.Instance.PassCount;
      
        if (passCount < startWindCount) return;
    
        isWind = IsGenerateThisTime(passCount);

        
        windIndicator.gameObject.SetActive(isWind);
        windForceIndicator.gameObject.SetActive(isWind);

        if(!isWind) {
            windAudioSource.Stop(); 
            return;}   
        
        if(!windAudioSource.isPlaying)windAudioSource.Play();
 

        windForceIndicator.text = windForceParam[windForceInd].ToString();

        if (!IsChangingDirection()) return;

        ChangeWindDir();


        int forceAvailable = 1;

        if (passCount > 15 && passCount <= 20)
        {
            forceAvailable = 2;
        }else if(passCount > 20)
        {
            forceAvailable = 3; 
        }
        
        windForceInd = Random.Range(0, forceAvailable);
        SetWindParticleParameter(windForceInd);
        

    }


    // private void Update()
    // {
    //     Debug.Log($"<color=red>x: {windDir[windDirInd][0]} z: {windDir[windDirInd][1]}</color>");
    // }


    private void ChangeWindDir()
    {
        windDirInd = Random.Range(0, windDir.Count);
        windDirVec =  new Vector3(windDir[windDirInd][0], 0f, windDir[windDirInd][1]);
        Vector2 angleVector = new Vector2(windDir[windDirInd][0], windDir[windDirInd][1]);
        float angleRadians = Mathf.Atan2(angleVector.y, angleVector.x);
        float angleDegrees = Mathf.Rad2Deg * angleRadians + 90;

        if(windIndicator) windIndicator.transform.rotation = Quaternion.Euler(0, -angleDegrees, 0);
        Debug.Log("change dir");
    }


    private bool IsGenerateThisTime(int passCount)
    {
        float weightedRange = Random.Range(0f, 1f);

        float thresh = 0.5f;
        if (passCount >= 20 && passCount <27)
        {
            thresh = 0.35f;
        }else if (passCount >= 27)
        {
            thresh = 0.2f;
        }
        return weightedRange >= thresh; 
    }


    private bool IsChangingDirection()
    {
        float weightedRange = Random.Range(0f, 1f);
        
        return weightedRange >= 0.4f; 
    }


    private void SetWindParticleParameter(int windForceParamInd)
    {
            windModule.startSpeed = windForceParam[windForceParamInd].windParticleSpeed;
            windModule.startSize = windForceParam[windForceParamInd].windParticleSize;
            windAudioSource.pitch = windForceParam[windForceParamInd].windSoundPitch;
    }
    
    
   
    
}
