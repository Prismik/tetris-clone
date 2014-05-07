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
    public class UDPServer
    {
        private List<Thread> _threads;
        private Thread      _listenThread;
        private Thread      _sendThread;
        private Thread      _logicThread;

        private UdpClient   _udp;
        private bool        _running;
        Random              _rand;

        private Dictionary<string, ConnectedClient> _connected;
        private Dictionary<string, Thread> _actions;

        public UDPServer()
        {
            _running     = false;
            _connected   = new Dictionary<string,ConnectedClient>();
            _rand        = new Random();
            _threads     = new List<Thread>();
            _actions     = new Dictionary<string,Thread>();
        }

        // Adds a function to the server, this function is specified outside of this class
        public void addAction(string action, ThreadStart function)
        {
            Thread t = new Thread(function);
            _actions.Add(action, t);
        }

        public void startServer()
        {
            #region Startup
            Console.WriteLine("Starting server");
           
            _udp          = new UdpClient(80);
            _running      = true;
            
            _listenThread = new Thread(listenAndHandleMessages);
            _listenThread.Start();

            //sendthread  = new Thread(updateAndSendStructs);
            //sendthread.Start();

            //logicThread = new Thread(updateLogic);
            //logicThread.Start();
            #endregion
        }

        public void updateLogic()
        {
            // Nothing
        }

        
        void updateAndSendStructs(Object obj)
        {
           #region send messages
           while (true)
           {
               System.Threading.Thread.Sleep(10);
              
               if (_connected.Count > 0)
               {
                   lock (_connected)
                   {
                       foreach (KeyValuePair<string, ConnectedClient> c in _connected)
                       {
                           foreach (KeyValuePair<string, ConnectedClient> c2 in _connected)
                           {
                               if (c2.Key != c.Key)
                               {
                                   // Update position.
                                   DataStruct msg = new DataStruct();
                                   msg.action     = "position";
                                   msg.name       = c2.Key;
                                   sendStruct(msg, c.Key);

                                   // Update aim.
                                   msg.action = "aim";
                                   sendStruct(msg, c.Key);
                               }
                           }
                       }
                    }
               }
           }
          #endregion
        }

        public void listenAndHandleMessages()
        {
            #region handle messages / re-route to functions
            while (_running)
            {
                IPEndPoint ipendpoint   = new IPEndPoint(IPAddress.Any, 80);  // Listen for anywhere
                byte[] data             = _udp.Receive(ref ipendpoint);        // Get some data
                object d                = MarshalHelper.MarshalHelper.DeserializeMsg<DataStruct>(data);
                DataStruct ds           = (DataStruct)d;

                if (ds.action == "connect")
                {
                    handleConnection(ds.name, ipendpoint);
                }
                else if( _actions.ContainsKey(ds.action) )
                {
                    Console.WriteLine(ds.action);
                    _actions[ds.action].Start();
                }
                else
                {
                    Console.WriteLine("Action is not \"connect\" ("+ds.action+")");
                }
            }
            #endregion
        }

        public void sendStruct(DataStruct msg, string name)
        {
            #region Send a message to a specific person (client..)
            byte[] data = MarshalHelper.MarshalHelper.SerializeMessage<DataStruct>(msg);
            if (_connected.ContainsKey(name))
            {
                if (_connected[name]._ipEndPoint != null)
                {
                    _udp.Send(data, data.Length, _connected[name]._ipEndPoint);
                }
            }
            else
            {
                #region console it
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(name);
                Console.ResetColor();
                Console.Write(" could not be found");
                Console.WriteLine();
                #endregion
            }
            #endregion
        }

        public void handleConnection(string name, IPEndPoint ipep)
        {
            #region Accept/Reject
            #region console it
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(name);
            Console.ResetColor();
            Console.Write(" is trying to connect");
            #endregion

            lock (_connected)
            {
                if (!_connected.ContainsKey(name))
                {
                    // Add to connections
                    _connected.Add(name, new ConnectedClient(name, ipep));
                    #region console it
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" Accepted");
                    Console.ResetColor();
                    Console.Write(" ({0})\n", ipep.ToString());
                    #endregion

                    // Send out authorized.msg
                    DataStruct ds = new DataStruct();
                    ds.action     = "accept";
                    ds.name       = name;
                    sendStruct(ds, name);

                }
                else
                {
                    #region console it
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" Rejected");
                    Console.ResetColor();
                    Console.Write(" (name exists)\n");
                    #endregion
                }
            }
            #endregion
            //Console.Title = "Connections: " + connected.Count.ToString();
        }
    }
}