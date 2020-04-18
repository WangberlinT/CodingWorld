using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    //调用此方法以发送code
    public static void SendCode()
    {
        Packet _packet = new Packet((int)ClientPackets.code);
        _packet.Write(Client.instance.id);
        _packet.Write(UIManager.instance.userInput.text);
        Debug.Log("sending...");
        SendTCPData(_packet);
    }
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))//此处不太明白
        {
            _packet.Write(Client.instance.id);
            _packet.Write(UIManager.instance.userInput.text);

            SendTCPData(_packet);
        }
    }
    #endregion
}
