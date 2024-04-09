using MensajesClienteHTTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MensajesClienteHTTP.Services
{
    public class DiscoveryService
    {

        public DiscoveryService() 
        {
            SolicitarServidores();
            new Thread(RecibirServidores) { IsBackground=true}.Start();
        }
        void SolicitarServidores()
        {
            UdpClient server = new UdpClient
            {
                EnableBroadcast = true
            };
            server.Send(new byte[] { 1 }, 1, new IPEndPoint(IPAddress.Broadcast, 7001));
            server.Close();

        }
        UdpClient cliente=new UdpClient();
        public event EventHandler<Server>? ServidorRecibido;
        void RecibirServidores()
        {
            while (true)
            {
                IPEndPoint endPoint = new(IPAddress.Any,0);

                byte[] buffer =cliente.Receive(ref endPoint);

                Server server = new()
                {
                    NombreServer = Encoding.UTF8.GetString(buffer),
                    IPEndpoint=endPoint,
                    KeepAlive=DateTime.Now
                };
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    ServidorRecibido?.Invoke(this, server);
                }));

            }
        }
    }
}
