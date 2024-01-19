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

    [SerializeField] private ParticleSystem windParticleSystem;
    
    private ParticleSystem.MainModule windModule;

    [SerializeField] private ParticleSystem leafParticleSystem;

    private ParticleSystem.MainModule leafModule;
    
    private AudioSource windAudioSource;


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
        new WindParameter{windForce = 0.2f, windParticleSize = 0.1f, windParticleSpeed = 1f , windSoundPitch = 1f },
        new WindParameter{windForce = 0.4f, windParticleSize = 0.15f, windParticleSpeed = 2 , windSoundPitch = 1.5f},
        new WindParameter{windForce = 0.6f, windParticleSize = 0.2f, windParticleSpeed = 3 , windSoundPitch = 2.0f},

    
    };

    private int windForceInd = 2; 
    

    
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
        windModule.simulationSpeed = windForceParam[windForceInd].windParticleSpeed;
        windModule.startSize = windForceParam[windForceInd].windParticleSize;

        leafModule = leafParticleSystem.main;

      
        
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


       
   
        
        int passCount = GameManager.Instance.PassCount;
      
        if (passCount < startWindCount) return;
    
        isWind = IsGenerateThisTime(passCount);

        
        windIndicator.gameObject.SetActive(isWind);
       
        if(!isWind) {
            windAudioSource.Stop(); 
            return;}   
        
        if(!windAudioSource.isPlaying)windAudioSource.Play();
 

      

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



    private void ChangeWindDir()
    {
        windDirInd = Random.Range(0, windDir.Count);
        windDirVec =  new Vector3(windDir[windDirInd][0], 0f, windDir[windDirInd][1]);
        Vector2 angleVector = new Vector2(windDir[windDirInd][0], windDir[windDirInd][1]);
        float angleRadians = Mathf.Atan2(angleVector.y, angleVector.x);
        float angleDegrees = Mathf.Rad2Deg * angleRadians + 90;

        if(windIndicator) windIndicator.transform.rotation = Quaternion.Euler(0, -angleDegrees, 0);

    }


    private bool IsGenerateThisTime(int passCount)
    {
        float weightedRange = Random.Range(0f, 1f);

        float thresh = 0.5f;
        if (passCount >= 10 && passCount <15)
        {
            thresh = 0.35f;
        }else if (passCount >= 15)
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
            windModule.simulationSpeed = windForceParam[windForceParamInd].windParticleSpeed;
            windModule.startSize = windForceParam[windForceParamInd].windParticleSize;
            windAudioSource.pitch = windForceParam[windForceParamInd].windSoundPitch;

            leafModule.simulationSpeed = windForceParam[windForceParamInd].windParticleSpeed;

    }
    
    
   
    
}
