
using Comandas_Server;
using Comandas_Server.Models;
using Comandas_Server.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Comandas_Server.ViewModels
{
    public class ServerViewModel
    {

        private ComandasServices Server;
        public Comanda Com { get; set; }
        public Producto Prod { get; set; }

        public ObservableCollection<Comanda> ListaComandas { get; set; }


        public ICommand inicio { get; set; }

        public ServerViewModel()
        {
            Server = new ComandasServices();
            Server.ComandaRecibida += Server_ComandaRecibida;
            inicio = new RelayCommand(Iniciar);
            ListaComandas = new ObservableCollection<Comanda>();
            Com = new Comanda();
            Com.total = 0;
            Com.Pedidos = new Dictionary<string, int>();
            Prod = new Producto();
            Prod.Descripcion = "Rica hamburgesa con doble carne";
            Prod.Nombre = "Hambirguesa";
            Prod.Tipo = Tipo.platillo;
            Prod.Precio = 50;
            Com.Pedidos.Add(Prod.Nombre, 1);
            Prod = new Producto();
            Prod.Descripcion = "Rico taco de pastor";
            Prod.Nombre = "Tacos al pastor";
            Prod.Tipo = Tipo.platillo;
            Prod.Precio = 40;
            Com.Pedidos.Add(Prod.Nombre, 2);
            Com.Id = 1231;
            Com.Fecha = DateTime.Now.ToShortTimeString();
            ListaComandas.Add(Com);

        }


        private void Server_ComandaRecibida(Comanda obj)
        {
            Application.Current.Dispatcher.Invoke(delegate // <--- HERE
            {
                ListaComandas.Add(obj);


            });

        }

        private void Iniciar()
        {
            Server.Iniciar();
        }
    }
}
