using System;
using Unity.VisualScripting;
using UnityEngine;




public class Bomb : MonoBehaviour
{
  bool isDebugMode = true;
    const float Gravity = -9.81f; //重力加速度を定義します。
   
    [SerializeField]  float gravityScale = 1.0f;//重力の適用具合を定義します。

    [SerializeField] AudioClip audioCliopStart; 
    [SerializeField] AudioClip audioClipLoop; 
    private AudioSource audioSource;


    private void Start()
    {
        GameManager.Instance.OnGameModeChanged += onGameManagerChanged;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioCliopStart); 
        audioSource.Play();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameModeChanged -= onGameManagerChanged;
    }

    private void onGameManagerChanged(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if(isDebugMode) return;
        Vector3 gameInputMoveDir = GameInput.Instance.GetDeviceGyroNormalized(); 
        Physics.gravity = Gravity * gameInputMoveDir  * gravityScale ;
        if(this.transform.position.y < -1.0f) FallFromPlane();
    }



    
    private void FallFromPlane() 
    { 
        GameManager.Instance.SetGameOverMode(this);
    }

    



    
}