using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource SFXPlayer; // 효과음 사용하기 위한 플레이어
        public AudioSource BGMPlayer; // 배경음 사용하기 위한 플레이어

        [SerializeField] public AudioClip[] MainAudioClip; // 배경음 저장
        [SerializeField] private AudioClip[] SFXAudioClips; // 효과음 저장

        public static float VolumeBGM = 1.0f; // 배경음 사운드
        public static float VolumeSFX = 1.0f; // 효과음 사운드

        //Dictionary<key, value>로 오디오를 불러온다
        Dictionary<string, AudioClip> audioclipdic = new Dictionary<string, AudioClip>();
        private void Awake()
        {
            BGMPlayer = GameObject.Find("BGMPlayer").GetComponent<AudioSource>(); // BGM플레이어
            SFXPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>(); // 효과음플레이어

            // SFXAudioClips내에 오디오클립들을 Dictionary내에 저장
            foreach (AudioClip audioclip in SFXAudioClips)
            {
                audioclipdic.Add(audioclip.name, audioclip);
            }

            PlayBGMSound();
        }

        public void PlaySFXSound(string name, float volum = 1.0f)
        {
            if (audioclipdic.ContainsKey(name) == false) // 효과음이 없다면
            {
                Debug.Log(name + "가 audioClipsDic에 없습니다.");
                return;
            }

            SFXPlayer.PlayOneShot(audioclipdic[name], volum * VolumeSFX);

        }

        public void PlayBGMSound()
        {
            //BGMPlayer.clip = MainAudioClip[0];
            BGMPlayer.loop = true;
            BGMPlayer.volume *= 0.5f;
        }

        private void Update()
        {
            BGMPlayer.volume = VolumeBGM;
        }
    }
}

