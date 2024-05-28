using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS_Online
{
  internal class GameData
  {
    public bool isHost = false, isClient = false;
    EventBasedNetListener hostListener, clientListener;
    NetManager host, client;
    public bool gameActive = false;
    List<NetPeer> peers = new List<NetPeer>();
    string p1Select, p2Select;
    public bool hostWaiting, selectionMade = false;
    public string p1, p2, result;
    public int playersConnected { get
      {
        if(host != null) {
          return host.ConnectedPeersCount;
        }
        return 0;
      } }

    public GameData()
    {
      hostListener = new EventBasedNetListener();
      host = new NetManager(hostListener);
      clientListener = new EventBasedNetListener();
      client = new NetManager(clientListener);
    }

    public void Host()
    {
      isHost = true;
      host.Start(34567);

      hostListener.ConnectionRequestEvent += request =>
      {
        if (host.ConnectedPeersCount < 2 /* max connections */)
          request.Accept();
        else
          request.Reject();
      };

      hostListener.PeerConnectedEvent += peer =>
      {
        peers.Add(peer);
        if(host.ConnectedPeersCount == 2) {
          gameActive = true;
        }
      };

      hostListener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
      {
        if (peers.FindIndex(x => x == fromPeer) == 0) {
          p1Select = dataReader.GetString(1);
        }
        if(peers.FindIndex(x => x == fromPeer) == 1) {
          p2Select = dataReader.GetString(1);
        }
        if(p1Select?.Length > 0 && p2Select?.Length > 0) {
          string p1s = p1Select[0].ToString();
          string p2s = p2Select[0].ToString();
          string res = string.Empty;
          if(p1s == p2s) {
            res = "0";
          } else if((p1s == "R" && p2s == "S") || (p1s == "S" && p2s == "P") || (p1s == "P" && p2s == "R")) {
            res = "1";
          }else if((p1s == "P" && p2s == "S") || (p1s == "R" && p2s == "P") || (p1s == "S" && p2s == "R")) {
            res = "2";
          }
          p1Select = "";
          p2Select = "";
          NetDataWriter writer = new NetDataWriter();
          writer.Put($"R{p1s}{p2s}{res}");
          host.SendToAll(writer, DeliveryMethod.ReliableOrdered);
        }
        dataReader.Recycle();
      };

      client.Start();
      client.Connect("localhost", 34567, "Connect");
      clientListener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
      {
        string message = dataReader.GetString(100);
        if (message[0].ToString() == "R") {
          p1 = message[1].ToString();
          p2 = message[2].ToString();
          result = message[3].ToString();
          selectionMade = false;
        }
        dataReader.Recycle();
      };
    }

    public void Client()
    {
      isClient = true;
      client.Start();
      client.Connect("localhost", 34567, "Connect");
      clientListener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
      {
        string message = dataReader.GetString(100);
        if (message[0].ToString() == "R") {
          p1 = message[1].ToString();
          p2 = message[2].ToString();
          result = message[3].ToString();
          selectionMade = false;
        }
        dataReader.Recycle();
      };
    }

    public void Update()
    {
      if (isHost) {
        client.PollEvents();
        host.PollEvents();
      }
      if (isClient) {
        client.PollEvents();
      }
    }

    public void SendSelection(string selection)
    {
      if(client != null) {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(selection);
        client.ConnectedPeerList[0].Send(writer, DeliveryMethod.ReliableOrdered);
      }
    }
  }
}
