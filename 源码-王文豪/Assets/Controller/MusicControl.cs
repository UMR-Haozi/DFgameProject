using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioClip[] musics;
    public AudioSource musicControl;
    float musicVolume;
    // Start is called before the first frame update
    public void PlayMusic(int type)
    {
        musicControl.clip = musics[type];
        musicControl.Play();
    }
    void Start()
    {
        musicVolume = 0.5f;
        musicControl = gameObject.GetComponent<AudioSource>();
        musics = new AudioClip[7];
        musics[0] = (AudioClip) Resources.Load("Musics/MainMenu");
        musics[1] = (AudioClip)Resources.Load("Musics/Mission1");
        musics[2] = (AudioClip)Resources.Load("Musics/Boss1");
        musics[3] = (AudioClip)Resources.Load("Musics/Mission2");
        musics[4] = (AudioClip)Resources.Load("Musics/Boss2");
        musics[5] = (AudioClip)Resources.Load("Musics/Mission3");
        musics[6] = (AudioClip)Resources.Load("Musics/Boss3");
        PlayMusic(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        musicVolume = GUI.HorizontalSlider(new Rect(0, Screen.height * 0.96f, Screen.width * 0.07f, Screen.height * 0.04f),musicVolume,0,1);
        musicControl.volume = musicVolume;
    }
}
