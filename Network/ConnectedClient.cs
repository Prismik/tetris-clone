using System.Net;
using Tvector;

namespace JNetwork
{
    public class ConnectedClient
    {
        public string       name;
        public IPEndPoint   ipendpoint;
        public float        x;
        public float        y;
        public float        aim_x;
        public float        aim_y;

        public ConnectedClient(string n, IPEndPoint ipep, int X, int Y)
        {
            name        = n;
            ipendpoint  = ipep;
            x           = X;    // sets to random when created from server..
            y           = Y;
        }
    }
}