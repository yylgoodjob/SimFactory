using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory
{
    public enum Music
    {
        PLAYFACTROY,//建造工厂
        PLAYROAD,//铺路
        PLAYWATER,//水
        PLAYTEAR,//拆除
        PLAYBACKMUSIC,//背景音乐
    }
    public class VideoData : MonoBehaviour
    {
        

        private Dictionary<Music, AudioClip> audioDic = new Dictionary<Music, AudioClip>();

        private AudioSource audioSource;
        private AudioClip audioFactroy;
        private AudioClip audioRoad;
        private AudioClip audioWater;
        private AudioClip audioTear;
        private AudioClip audioBackMusic;
        //private AudioClip

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

            audioFactroy = AudioLoad("工厂", 0);
            audioRoad = AudioLoad("铺路", 1);
            audioWater = AudioLoad("水", 2);
            audioTear = AudioLoad("拆除", 3);
            audioBackMusic = AudioLoad("背景音乐", 4);

            EventCenter.AddListener<Music>(EventType.PLAYMUSIC, PlayMusic);

            PlayMusic(Music.PLAYBACKMUSIC);
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener<Music>(EventType.PLAYMUSIC, PlayMusic);
        }
        //加载音乐
        AudioClip AudioLoad(string name, int n)
        {
            AudioClip audio = Resources.Load<AudioClip>(name);
            audioDic[(Music)n] = audio;
            return audio;
        }
        //播放音乐
        void PlayMusic(Music music)
        {
            audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
            audioSource.clip = audioDic[music];
            audioSource.Play();
        }
    }

}