using System.Net;

namespace JNetwork
{
    public class ConnectedClient
    {
        public string       _name;
        public IPEndPoint   _ipEndPoint;

        public ConnectedClient(string n, IPEndPoint ipep)
        {
            _name        = n;
            _ipEndPoint  = ipep;
        }
    }
}