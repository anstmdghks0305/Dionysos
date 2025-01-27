using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource BGMPlayer; // 배경음 사용하기 위한 플레이어
        public AudioSource SFXPlayer; // 효과음 사용하기 위한 플레이어

        [SerializeField] public AudioClip[] BGMAudioClip; // 배경음 저장
        [SerializeField] private AudioClip[] SFXAudioClips; // 효과음 저장

        public static float VolumeBGM = 1.0f; // 배경음 사운드
        public static float VolumeSFX = 1.0f; // 효과음 사운드

        //Dictionary<key, value>로 오디오를 불러온다
        Dictionary<string, AudioClip> BGMaudioclipdic = new Dictionary<string, AudioClip>();
        Dictionary<string, AudioClip> SFXaudioclipdic = new Dictionary<string, AudioClip>();
        public ParticleSystem ring;
        private void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            //BGMPlayer = GameObject.Find("BGM").GetComponent<AudioSource>(); // BGM플레이어
            //SFXPlayer = GameObject.Find("SFX").GetComponent<AudioSource>(); // 효과음플레이어

            // AudioClips내에 오디오클립들을 Dictionary내에 저장
            foreach (AudioClip audioclip in BGMAudioClip)
            {
                BGMaudioclipdic.Add(audioclip.name, audioclip);
            }
            foreach (AudioClip audioclip in SFXAudioClips)
            {
                SFXaudioclipdic.Add(audioclip.name, audioclip);
            }
            //PlayBGMSound();
        }

        public void PlaySFXSound(string name, float volum = 1.0f)
        {
            if (SFXaudioclipdic.ContainsKey(name) == false) // 효과음이 없다면
            {
                Debug.Log(name + "가 audioClipsDic에 없습니다.");
                return;
            }

            SFXPlayer.PlayOneShot(SFXaudioclipdic[name], volum * VolumeSFX);

        }

        public void PlayBGMSound(string name)
        {
            if (ring != null)
            {
                //ring.Play();
            }
            BGMPlayer.clip = BGMaudioclipdic[name];
            BGMPlayer.loop = true;
            BGMPlayer.Play();
        }

        private void Update()
        {
            BGMPlayer.volume = VolumeBGM;
            SFXPlayer.volume = VolumeSFX;
            if(Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log(VolumeBGM);
                Debug.Log(VolumeSFX);
            }
        }
    }
}

