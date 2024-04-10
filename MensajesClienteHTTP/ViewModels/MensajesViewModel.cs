using CommunityToolkit.Mvvm.ComponentModel;
using MensajesClienteHTTP.Models;
using MensajesClienteHTTP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajesClienteHTTP.ViewModels
{
    public class MensajesViewModel:ObservableObject
    {
        DiscoveryService discoveryService=new();
        MensajesService mensajesService=new MensajesService();
        public ObservableCollection<Server> Servers { get; set; } =new ObservableCollection<Server>();
        public MensajesViewModel()
        {
            discoveryService.ServidorRecibido += DiscoveryService_ServidorRecibido;
        }

        private void DiscoveryService_ServidorRecibido(object? sender, Models.Server e)
        {
            Servers.Add(e);
        }
    }
}
