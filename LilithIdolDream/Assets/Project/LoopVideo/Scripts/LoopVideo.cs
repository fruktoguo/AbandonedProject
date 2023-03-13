using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

namespace YuoTools
{
    public class LoopVideo : MonoBehaviour
    {
        public VideoPlayer videoplayer;
        VideoPlayer NextVideoplayer;
        VideoPlayer LoopPlayer1;
        VideoPlayer LoopPlayer2;
        public double time;
        public double length;
        [HideInInspector]
        public bool looplock;
        float FrameDelay;
        public double ClipTime;
        private void Start()
        {
            LoopPlayer1 = videoplayer;
            FrameDelay = 1 / videoplayer.frameRate;
            length = LoopPlayer1.length;
            if (LoopPlayer1.isLooping && !looplock)
            {
                var temp = Instantiate(this);
                temp.looplock = true;
                LoopPlayer2 = temp.videoplayer;
            }
            else
            {
                Destroy(this);
                return;
            }
            this.YuoDelay(() =>
            {
                length = videoplayer.length;
            }, 1f);
            Play();
            NextVideoplayer = LoopPlayer2;
            LoopPlayer1.isLooping = false;
            LoopPlayer2.isLooping = false;
            LoopPlayer2.targetCamera = null;
        }
        void Play()
        {
            LoopPlayer1.time = 0;
            LoopPlayer1.Play();
            LoopPlayer2.time = 0;
            LoopPlayer2.Pause();
        }

        /// <summary>
        /// 切换播放器
        /// </summary>
        void SwitchVideo()
        {
            if (videoplayer == LoopPlayer1)
            {
                videoplayer = LoopPlayer2;
                NextVideoplayer = LoopPlayer1;
            }
            else
            {
                videoplayer = LoopPlayer1;
                NextVideoplayer = LoopPlayer2;
            }
        }
        void Loop()
        {
            ClipTime = videoplayer.length;
            if (videoplayer.time > length - FrameDelay)
            {
                NextVideoplayer.Play();
            }
            if (videoplayer.time > length - Time.deltaTime)
            {
                videoplayer.time = 0;
                videoplayer.Pause();
                videoplayer.targetCamera = null;
                SwitchVideo();
                videoplayer.targetCamera = Camera.main;
                videoplayer.Play();
            }
            time = videoplayer.time;
        }

        private void Update()
        {
            Loop();
        }
    }
}