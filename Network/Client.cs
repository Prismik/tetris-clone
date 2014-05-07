using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;

using System.Threading;

using MarshalHelper;

namespace JNetwork
{
    public class Client
    {
        UdpClient   _udpClient;
        IPEndPoint  _ipEndPoint;
        Thread      _listenThread;
        bool        _connected;
        public string _name;
        public Dictionary<string, SimulatedClient> _simulatedClients;

        public Client(string n)
        {
            _simulatedClients = new Dictionary<string, SimulatedClient>();
            _name             = n;
            _ipEndPoint       = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
            _udpClient        = new UdpClient();
            _udpClient.Connect(_ipEndPoint);

            _listenThread = new Thread(listenForStructs);
            _listenThread.Start();

            // Connect
            DataStruct connectMessage   = new DataStruct();
            connectMessage.action       = "connect";
            connectMessage.name         = _name;
            sendStruct(connectMessage);
        }

        void listenForStructs()
        {
            while (true)
            {
                byte[] data     = _udpClient.Receive(ref _ipEndPoint);
                object      d   = MarshalHelper.MarshalHelper.DeserializeMsg<DataStruct>(data);
                DataStruct ds   = (DataStruct)d;

                if (ds.action == "accept" && ds.name == _name)
                {
                    _connected = true;
                }

                if (_connected)
                {
                    switch (ds.action)
                    {
                        case "position":
                            if (_simulatedClients.ContainsKey(ds.name))
                            {
                               
                            }
                            else
                            {
                                lock(_simulatedClients)
                                {
                                    _simulatedClients.Add(ds.name, new SimulatedClient(ds.name));
                                }
                            }
                            break;

                        case "aim":
                            if (_simulatedClients.ContainsKey(ds.name))
                            {

                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void sendStruct(string _action, string _name, float _x, float _y)
        {
            DataStruct data = new DataStruct();
            data.action     = _action;
            data.name       = _name;
            sendStruct(data);
        }

        public void sendStruct(DataStruct s)
        {
            byte[] b = MarshalHelper.MarshalHelper.SerializeMessage<DataStruct>(s);
            _udpClient.Send(b, b.Length);
        }

        public void sendMessage(string message)
        {
            message = _name + " " + message;
            byte[] data = Encoding.ASCII.GetBytes(message);
            _udpClient.Send(data, data.Length);
        }

        public override string  ToString()
        {
            return _name;
        }
    }
}

