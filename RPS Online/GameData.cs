using LiteNetLib;
using LiteNetLib.Utils;
using SharpDX.X3DAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS_Online
{
  internal class GameData
  {
    bool isHost = false, isClient = false;
    EventBasedNetListener hostListener, clientListener;
    NetManager host, client;
    public bool gameActive = false;
    List<NetPeer> peers = new List<NetPeer>();
    string p1Select, p2Select;
    public bool hostWaiting;
    public string p1, p2, result;

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
        }
        dataReader.Recycle();
      };
    }


  }
}
