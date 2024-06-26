﻿using MensajesClienteHTTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MensajesClienteHTTP.Services
{
    public class MensajesService
    {
        public async void EnviarMensaje(Server server,MensajesDto mensaje)
        {
            var url = $"http://{server.IPEndpoint?.Address.ToString()}:" +
                $"7002/mensajitos/?texto={mensaje.Texto}&colorletra={HttpUtility.UrlEncode( mensaje.ColorLetra)}" +
                $"&colorfondo={HttpUtility.UrlEncode(mensaje.ColorFondo)}";
            HttpClient client = new HttpClient();
            var result=await client.GetStringAsync(url);


        }
    }
}
