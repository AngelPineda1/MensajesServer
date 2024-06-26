﻿using MensajesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MensajesServer.Services
{
    public class MensajesService
    {
        HttpListener sevidor = new HttpListener();
        public MensajesService()
        {
            sevidor.Prefixes.Add("http://*:7002/mensajitos/");
            sevidor.Start();
            new Thread(RecibirPeticiones) { IsBackground = true }.Start();
        }

        public event EventHandler<Mensaje>? MensajeRecibido;
        void RecibirPeticiones()
        {
            while (true)
            {
                var context = sevidor.GetContext();
                if (context != null)
                {
                    if (context.Request.QueryString["texto"] != null)
                    {
                        Mensaje mensaje = new Mensaje()
                        {

                            Texto = context.Request.QueryString["texto"] ?? "",
                            ColorFondo = context.Request.QueryString["colorfondo"] ?? "#fff",
                            ColorLetra = context.Request.QueryString["colorletra"] ?? "#000"
                        };
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MensajeRecibido?.Invoke(this, mensaje);
                        });
                        context.Response.StatusCode=200;
                        context.Response.Close();
                    }
                }
            }
        }
    }
}
