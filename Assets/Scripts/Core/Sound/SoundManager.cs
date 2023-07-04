using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anchor.Unity.Addressables;

namespace Anchor.Unity
{
    public class SoundManager
    {
        static Dictionary<string, AudioClip> m_AudioClips;
        static AudioSource[] m_AudioSources;

        public static void Initalize()
        {
            GameObject root = new GameObject { name = "@Sound" };

            GameObject bgm = new GameObject("@BGM", typeof(AudioSource));
            GameObject eft = new GameObject("@EFT", typeof(AudioSource));

            bgm.transform.SetParent(root.transform);
            eft.transform.SetParent(root.transform);

            m_AudioSources = new AudioSource[System.Enum.GetValues(typeof(SoundType)).Length];
            m_AudioSources[(int)SoundType.BGM] = bgm.GetComponent<AudioSource>();
            m_AudioSources[(int)SoundType.EFT] = eft.GetComponent<AudioSource>();

            Object.DontDestroyOnLoad(root);

            m_AudioClips = new Dictionary<string, AudioClip>();

            foreach (var assetPath in Define.SoundAssets)
            {
                LoadAsset(assetPath);
            }
        }

        public static void Play(SoundId id, SoundType type, float pitch = 1.0f)
        {
            string path = Define.SoundPath + id.ToString() + ".mp3";
            AudioClip audioClip = null;

            if (m_AudioClips.TryGetValue(path, out audioClip))
            {
                if(type == SoundType.BGM)
                {
                    m_AudioSources[(int)type].clip = audioClip;
                    m_AudioSources[(int)type].Play();
                }
                else if(type == SoundType.EFT)
                {
                    m_AudioSources[(int)type].pitch = pitch;
                    m_AudioSources[(int)type].PlayOneShot(audioClip);
                }
            }
            else
            {
                audioClip = Get(path);
                Play(audioClip, type, pitch);
            }
        }

        private static void Play(AudioClip audioClip, SoundType type, float pitch = 1.0f)
        {
            if (type == SoundType.BGM)
            {
                m_AudioSources[(int)type].clip = audioClip;
                m_AudioSources[(int)type].Play();
            }
            else if (type == SoundType.EFT)
            {
                m_AudioSources[(int)type].pitch = pitch;
                m_AudioSources[(int)type].PlayOneShot(audioClip);
            }
        }

        private static void LoadAsset(string assetPath)
        {
            ResourceManager.LoadAsset<AudioClip>(assetPath, (result) =>
            {
                m_AudioClips.Add(assetPath, result);
            }, false);
        }

        private static AudioClip Get(string assetPath)
        {
            AudioClip item = null;

            if(m_AudioClips.TryGetValue(assetPath, out item))
            {
                return item;
            }
            else
            {
                LoadAsset(assetPath);

                item = m_AudioClips[assetPath];
            }

            return item;
        }
    }
}