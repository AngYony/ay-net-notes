using AForge.Controls;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ay.Utils
{
    /// <summary>
    /// 摄像头采集帮助类
    /// </summary>
    public class CameraHelper
    {
        public CameraHelper(int cameraIndex, VideoSourcePlayer videoSourcePlayer)
        {
            this.CameraIndex = cameraIndex;
            this.VideoSourcePlayer = videoSourcePlayer;
        }

        public int CameraIndex { get; set; }

        public VideoSourcePlayer VideoSourcePlayer { get; set; }

        /// <summary>
        /// 设备源
        /// 用来操作摄像头
        /// </summary>
        private VideoCaptureDevice videoCapture;

        /// <summary>
        /// 摄像头设备集合
        /// </summary>
        private FilterInfoCollection infoCollection;

        public FilterInfoCollection GetCameraList()
        {
            return new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        public void StartCamera()
        {
            try
            {
                this.infoCollection = GetCameraList();
            }
            catch (Exception ex)
            {
                throw new Exception($"设备获取失败:{ex.Message}");
            }

            //已有连接的摄像头时先关闭
            if (videoCapture != null)
            {
                StopCamera();
            }

            if (infoCollection.Count > this.CameraIndex && this.CameraIndex>=0)
            {
                videoCapture = new VideoCaptureDevice(infoCollection[this.CameraIndex].MonikerString);
                //启动摄像头
                this.VideoSourcePlayer.VideoSource = videoCapture;
                this.VideoSourcePlayer.Start();
            }
        }

        /// <summary>
        /// 关闭摄像头
        /// </summary>
        public void StopCamera()
        {
            if (this.VideoSourcePlayer.VideoSource != null)
            {
                //也可以用 Stop() 方法关闭
                //指示灯停止且等待
                this.VideoSourcePlayer.SignalToStop();
                //停止且等待
                this.VideoSourcePlayer.WaitForStop();
                this.VideoSourcePlayer.VideoSource = null;
            }
        }
    }
}
