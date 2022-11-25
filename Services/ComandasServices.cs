using Comandas_Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Comandas_Server.Services
{
    internal class ComandasServices
    {
        public Producto Prod { get; set; }

        public ObservableCollection<Comanda> ListaComandas { get; set; }

        public Comanda Com { get; set; }

        HttpListener listener = new();
        public void Iniciar()
        {
            listener.Prefixes.Add("http://*:45455/Comandas/");
            listener.Start();
            Thread hilo = new Thread(new ThreadStart(Escuchar));
            hilo.IsBackground = true;
            hilo.Start();
        }

        private void Detener()
        {
           listener.Stop();
        }
        public event Action<Comanda>? ComandaRecibida;
        private void Escuchar()
        {
            while (listener.IsListening)
            {
                var context = listener.GetContext();
                if (context.Request.Url != null)
                {
                   if (context.Request.Url.LocalPath == "/Comandas/Nueva")
                    {
                        
                        Com = new Comanda();

                       
                        var stream = new StreamReader(context.Request.InputStream);
                        var json = stream.ReadToEnd();
                        Com = JsonConvert.DeserializeObject<Comanda>(json);


        


                        ComandaRecibida?.Invoke(Com);
                        context.Response.StatusCode = 200;

                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                    }
                    context.Response.Close();
               



                
                }

            }
        }
    }
}

