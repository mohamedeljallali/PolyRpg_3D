using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Transform player;
    public Transform Enemy;
    public Transform legRight;
    public Transform legLeft;
    //public Transform damage;

    public AudioSource AudioSource;


    public int score;
    public int levelItems;
    public bool playerDead = false;


    public AudioClip[] levelSounds;
    public Transform[] particleSystem;

    

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Enemy = GameObject.FindWithTag("Enemy").gameObject.transform;
        //damage = Enemy.GetChild(1).transform;


    }

    //Sound
    public void PlaySound(AudioClip sound,Vector3 ownerPos)
    {
        GameObject obj = SoundFXPooler.pooling.GetPool();
        AudioSource audio = obj.GetComponent<AudioSource>();

        obj.transform.position = ownerPos;
        obj.SetActive(true);
        audio.PlayOneShot(sound);
        StartCoroutine(DisableSound(audio));

    }

    IEnumerator DisableSound(AudioSource audio)
    {
        while(audio.isPlaying)
            yield return new WaitForSeconds(0.5f);
        audio.gameObject.SetActive(false);
    }

}
