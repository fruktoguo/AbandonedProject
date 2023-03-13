using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using UnityEngine.Events;
using UnityEngine;
using System;

namespace YuoTools
{
    [System.Serializable]
    public class SocketClient
    {
        private const int Port = 921;
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] result = new byte[1024];

        public string IP = "47.94.88.156";
        // public static string IP = "192.168.1.100";

        //public static string IP = "127.0.0.1";
        public UnityAction<string> OnReceiveMessage;
        public UnityAction<bool> OnConnected;
        public List<string> ReceiveMessageList = new List<string>();
        public List<string> SendMessageList = new List<string>();
        public void Init()
        {

            receiveThread = new Thread(StartClinet);
            //receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        public IEnumerator LoadMsg()
        {
            Debug.Log("LoadMsg");
            while (true)
            {
                yield return YuoWait.GetWait(0.1f);
                // Debug.Log(ReceiveMessageList.Count);
                if (ReceiveMessageList.Count > 0)
                {
                    foreach (var item in ReceiveMessageList)
                    {
                        OnReceiveMessage?.Invoke(Base64From(item));
                    }
                    ReceiveMessageList.Clear();
                }
                if (IsClient)
                {
                    if (SendMessageList.Count > 0)
                    {
                        foreach (var item in SendMessageList)
                        {
                            clientSocket.Send(Encoding.UTF8.GetBytes(Base64To(item)));
                        }
                        SendMessageList.Clear();
                    }
                }
            }
        }
        bool IsClient = false;
        Thread receiveThread;
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private void StartClinet()
        {
            IsClient = false;
            //receiveThread.IsBackground = false;
            //设定服务器IP地址
            IPAddress ip = IPAddress.Parse(IP);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, Port)); //配置服务器IP与端口
                ReceiveMessageList.Add(Base64To("连接服务器成功"));
                //OnConnected?.Invoke(true);
                //Debug.Log("连接服务器成功");
            }
            catch
            {
                //OnConnected?.Invoke(false);
                ReceiveMessageList.Add(Base64To("连接服务器失败"));
                //Debug.Log("连接服务器失败");
                Stop();
                return;
            }
            IsClient = true;
            while (true)
            {
                try
                {
                    //Debug.Log("接收服务器消息");
                    //通过clientSocket接收数据                
                    int receiveNumber = clientSocket.Receive(result);
                    if (result.Length != 0)
                    {
                        string strContent = Encoding.UTF8.GetString(result, 0, receiveNumber);
                        ReceiveMessageList.Add(strContent);
                    }
                    //Debug.Log(ReceiveMessageList.Count);
                    //Console.WriteLine("接收服务端{0}消息{1}", clientSocket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(result, 0, receiveNumber));
                }
                catch (Exception ex)
                {
                    // Console.WriteLine(ex.Message);
                    Debug.Log(ex.Message);
                    Stop();
                    // clientSocket.Shutdown(SocketShutdown.Both);
                    // clientSocket.Close();
                    break;
                }
            }
        }
        public void Stop()
        {
            Debug.Log("Stop");
            // if (receiveThread == null)
            //     return;
            receiveThread.Abort();
            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        public void SendMessage(string message)
        {
            // string sendMessage = message;
            // var bts = Encoding.UTF8.GetBytes(sendMessage);
            // clientSocket.Send(bts);
            SendMessageList.Add(message);
        }
        public static string Base64From(string message)
        {
            Debug.Log(message);
            return Encoding.UTF8.GetString(Convert.FromBase64String(message));
        }

        public static string Base64To(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            return Convert.ToBase64String(buffer);
        }
    }
}