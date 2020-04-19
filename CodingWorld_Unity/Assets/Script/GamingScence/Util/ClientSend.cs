using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    

    public static void SendName(string name)
    {
        Packet _packet = new Packet((int)ClientPackets.scriptName);
        _packet.Write(Client.instance.id);
        _packet.Write(name);
        Debug.Log("sending...");
        SendTCPData(_packet);
    }

    //调用此方法以发送code
    public static void SendCode(string code)
    {
        Packet _packet = new Packet((int)ClientPackets.code);
        _packet.Write(Client.instance.id);
        _packet.Write(code);
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
            _packet.Write("Ready to send code");

            SendTCPData(_packet);
        }
    }
    #endregion
}
