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
            listener.Prefixes.Add("http://*:11010/Comandas/");
            listener.Start();
            Thread hilo = new Thread(new ThreadStart(Escuchar));
            hilo.IsBackground = true;
            hilo.Start();
        }
        public event Action<Comanda>? ComandaRecibida;
        private void Escuchar()
        {
            while (listener.IsListening)
            {
                var context = listener.GetContext();
                if (context.Request.Url != null)
                {
                    if (context.Request.Url.LocalPath == "/Comandas")
                    {


                        context.Response.StatusCode = 200; //ok

                    }
                    else if (context.Request.Url.LocalPath == "/Comandas/Nueva")
                    {
                        ListaComandas = new ObservableCollection<Comanda>();
                        Com = new Comanda();

                        Com.Pedidos = new Dictionary<string, int>();
                        Prod = new Producto();
                        Prod.Descripcion = "Rica hamburgesa con doble carne";
                        Prod.Nombre = "Hambirguesa";
                        Prod.Tipo = Tipo.platillo;
                        Prod.Precio = 50;
                        Com.Pedidos.Add(Prod.Nombre, 2);
                        Prod = new Producto();
                        Prod.Descripcion = "Rico taco de pastor";
                        Prod.Nombre = "Tacos al pastor";
                        Prod.Tipo = Tipo.platillo;
                        Prod.Precio = 40;
                        Com.Pedidos.Add(Prod.Nombre, 4);
                        Com.Id = 1232;
                        Com.Fecha = DateTime.Now.ToShortTimeString();
                        Com.total = 100;
                        var json = JsonConvert.SerializeObject(Com);


                        //var stream = new StreamReader(context.Request.InputStream).ReadToEnd();

                        Com = JsonConvert.DeserializeObject<Comanda>(json);

                    }

                    //Tomar solo lo que este despues del MIME typef



                    ComandaRecibida?.Invoke(Com);
                    context.Response.StatusCode = 200;//estado de proceso OK
                                                      //context.Response.Redirect("/album/");//redirecciona a la pagina principal
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

