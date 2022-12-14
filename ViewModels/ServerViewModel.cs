
using Comandas_Server;
using Comandas_Server.Models;
using Comandas_Server.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class ServerViewModel:INotifyPropertyChanged
    {
        private ComandasServices Server;
        public Comanda Com { get; set; }
        public Producto Prod { get; set; }

        private bool iniciado;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Iniciado
        {
            get { return iniciado; }
            set { iniciado = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Iniciado))); }
        }
        public ObservableCollection<Comanda> ListaComandas { get; set; }
        public ObservableCollection<Comanda> ListaEntregados { get; set; }
        public int contador { get; set; }
        public ICommand IniciarCommand { get; set; }
        public ICommand CancelarCommand { get; set; }

        public ICommand EntregadoCommand { get; set; }
        public ServerViewModel()
        {
            Iniciado = false;
            ListaEntregados = new ObservableCollection<Comanda>();
        Server = new ComandasServices();
            Server.ComandaRecibida += Server_ComandaRecibida;
            IniciarCommand = new RelayCommand(Iniciar);
            CancelarCommand = new RelayCommand<Comanda>(CancelarComanda);
            EntregadoCommand = new RelayCommand<Comanda>(EntregarComanda);
            ListaComandas = new ObservableCollection<Comanda>();


            //prueba sin cliente
            //Com = new Comanda();
           
            //Com.Pedidos = new Dictionary<string, Producto>();
            //Prod = new Producto();
            //Prod.Descripcion = "Rica hamburgesa con doble carne";
            //Prod.Nombre = "Hambirguesasdaddssdsdsdsdsdsd";
            //Prod.Tipo = Tipo.platillo;
            //Prod.Precio = 50;
            //Prod.Cantidad = 5;
            //Com.Pedidos.Add(Prod.Nombre,Prod);
            //Prod = new Producto();
            //Prod.Descripcion = "Rico taco de pastor";
            //Prod.Nombre = "Tacos al pastor";
            //Prod.Tipo = Tipo.platillo;
            //Prod.Precio = 40;
            //Prod.Cantidad = 5;
            //Com.Pedidos.Add(Prod.Nombre,Prod);
            //Prod = new Producto();
            //Prod.Descripcion = "Rica hamburgesa con doble carne";
            //Prod.Nombre = "2";
            //Prod.Tipo = Tipo.platillo;
            //Prod.Precio = 50;
            //Prod.Cantidad = 5;
            //Com.Pedidos.Add(Prod.Nombre, Prod);
            //Prod = new Producto();
            //Prod.Descripcion = "Rica hamburgesa con doble carne";
            //Prod.Nombre = "5";
            //Prod.Tipo = Tipo.platillo;
            //Prod.Precio = 50;
            //Prod.Cantidad = 5;
            //Com.Pedidos.Add(Prod.Nombre, Prod);
            //Com.Id = 1231;
            //Com.Hora = DateTime.Now.ToShortTimeString();
            //Com.Total =100;
            //Com.Mesa = "Mesa 1";
            //Com.Notas = "La hamburguesa sin tomate, y los tacos sin cebollaLa hamburguesa sin tomate, y los tacos sin cebollaLa hamburguesa sin tomate, y los tacos sin cebollaLa hamburguesa sin tomate, y los tacos sin cebolla";
            //Com.Pedidos[Prod.Nombre].Cantidad+=1;
         
            //ListaComandas.Add(Com);
           
        }

        private void EntregarComanda(Comanda obj)
        {
            ListaEntregados.Add(obj);
            ListaComandas.Remove(obj);
            
        }

        private void CancelarComanda(Comanda obj)
        {
            ListaComandas.Remove(obj);
        }

        private void Server_ComandaRecibida(Comanda obj)
        {
            Application.Current.Dispatcher.Invoke(delegate // <--- HERE
            {
                contador += 1;
                obj.Id = contador;
                ListaComandas.Add(obj);
               
                  
            


            });

        }

        private void Iniciar()
        {
            Iniciado = true;
            Server.Iniciar();
          
        }
    }
}
