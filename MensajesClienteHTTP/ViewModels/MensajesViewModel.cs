using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MensajesClienteHTTP.Models;
using MensajesClienteHTTP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MensajesClienteHTTP.ViewModels
{
    public partial class MensajesViewModel:ObservableObject
    {
        DiscoveryService discoveryService=new();
        MensajesService mensajesService=new MensajesService();
        public MensajesDto mensajesDto { get; set; }=new MensajesDto();
        public Server Seleccionado { get; set; } = null!;
        public List<SolidColorBrush> Colores { get; set; }=new List<SolidColorBrush>();
        public ObservableCollection<Server> Servers { get; set; } =new ObservableCollection<Server>();

        [RelayCommand]
        void Enviar()
        {
            mensajesService.EnviarMensaje(Seleccionado, mensajesDto);
        }
        public MensajesViewModel()
        {
            foreach (var item in typeof(Brushes).GetProperties())
            {
                Colores.Add((SolidColorBrush) (item.GetValue(null)?? new SolidColorBrush()));
            }
            discoveryService.ServidorRecibido += DiscoveryService_ServidorRecibido;
        }

        private void DiscoveryService_ServidorRecibido(object? sender, Models.Server e)
        {
            var server=Servers.FirstOrDefault(x=>x.NombreServer==e.NombreServer);
            if (server == null)
            {

                Servers.Add(e);
            }
            else
            {
                server.KeepAlive = e.KeepAlive;
            }
            foreach (var item in Servers.ToList())
            {
                if((DateTime.Now- item.KeepAlive).TotalSeconds > 30)
                {
                    Servers.Remove(item);
                }
            }
        }
    }
}
