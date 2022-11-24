
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

        public ObservableCollection<Producto> ListaProductos { get; set; }
        public ICommand inicio { get; set; }
        public ICommand Cancelar { get; set; }
        public ServerViewModel()
        {   
        Server = new ComandasServices();
            Server.ComandaRecibida += Server_ComandaRecibida;
            inicio = new RelayCommand(Iniciar);
            Cancelar = new RelayCommand<Comanda>(CancelarComanda);
            ListaComandas = new ObservableCollection<Comanda>();
            Com = new Comanda();
           
            Com.Pedidos = new Dictionary<string, Producto>();
            Prod = new Producto();
            Prod.Descripcion = "Rica hamburgesa con doble carne";
            Prod.Nombre = "Hambirguesasdaddssdsdsdsdsdsd";
            Prod.Tipo = Tipo.platillo;
            Prod.Precio = 50;
            Prod.Cantidad = 5;
            Com.Pedidos.Add(Prod.Nombre,Prod);
            Prod = new Producto();
            Prod.Descripcion = "Rico taco de pastor";
            Prod.Nombre = "Tacos al pastor";
            Prod.Tipo = Tipo.platillo;
            Prod.Precio = 40;
            Prod.Cantidad = 5;
            Com.Pedidos.Add(Prod.Nombre,Prod);
            Prod = new Producto();
            Prod.Descripcion = "Rica hamburgesa con doble carne";
            Prod.Nombre = "2";
            Prod.Tipo = Tipo.platillo;
            Prod.Precio = 50;
            Prod.Cantidad = 5;
            Com.Pedidos.Add(Prod.Nombre, Prod);
            Prod = new Producto();
            Prod.Descripcion = "Rica hamburgesa con doble carne";
            Prod.Nombre = "5";
            Prod.Tipo = Tipo.platillo;
            Prod.Precio = 50;
            Prod.Cantidad = 5;
            Com.Pedidos.Add(Prod.Nombre, Prod);
            Com.Id = 1231;
            Com.Fecha = DateTime.Now.ToShortTimeString();
            Com.total =100;
            Com.Pedidos[Prod.Nombre].Cantidad+=1;
         
            ListaComandas.Add(Com);
           
        }

        private void CancelarComanda(Comanda obj)
        {
            ListaComandas.Remove(obj);
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
