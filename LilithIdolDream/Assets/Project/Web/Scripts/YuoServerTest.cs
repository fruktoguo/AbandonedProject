using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine.UI;
using System;
using YuoTools.Chat;
using Sirenix.OdinInspector;

namespace YuoTools
{
    public class YuoServerTest : MonoBehaviour
    {
        public SocketClient client;
        ChatManager chat;
        private void Start()
        {
            chat = ChatManager.Instance;
            Init();
        }
        private void Init()
        {
            client = new SocketClient();
            client.IP = "47.94.88.156";
            chat.OnSendMessage = (x) =>
            {
                client.SendMessage(x);
            };
            client.OnConnected = OnConnected;
            client.OnReceiveMessage = OnReceiveMessage;
            client.ReceiveMessageList.Clear();
            StartCoroutine(client.LoadMsg());
            client.Init();
            //client.SendMessage("login-uyuohira-p092197");
        }

        private void OnReceiveMessage(string arg0)
        {
            chat.Send(arg0, chat.us[0]);
        }

        private void OnConnected(bool arg0)
        {
            chat.Send(arg0 ? "连接成功" : "连接失败", chat.us[0]);
        }
        private void OnDestroy()
        {
            client.Stop();
        }
    }
}