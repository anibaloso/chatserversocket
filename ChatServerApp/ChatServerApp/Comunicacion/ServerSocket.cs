using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp.Comunicacion
{
    public class ServerSocket //lo cambie a publico **1**
    {
        private int puerto;
        private Socket servidor;
        private Socket comCliente;
        private StreamReader reader;
        private StreamWriter writer;

        public ServerSocket(int puerto)
        {
            puerto = this.puerto;
        }

        public bool Iniciar() //creamos todas estas firmas 
        {
            try
            {
                //1.crear el socket
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //creee una nueva instancia de socket

                //2.tomar control del puerto
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));
                //3.definir cuantos clientes puede atender "simultaneamente"
                this.servidor.Listen(10);
                return true;

            }catch(Exception ex)
            {
                return false;
            }
        }

        public bool ObtenerCliente()
        {
            try
            {
                this.comCliente = this.servidor.Accept();
                Stream stream = new NetworkStream(this.comCliente);
                this.writer = new StreamWriter(stream);
                this.reader = new StreamReader(stream);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);//solo envio los datos
                this.writer.Flush();//con este flush se limpia y muestra que llegaron los datos
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public string Leer()
        {
            try { 
                return this.reader.ReadLine().Trim();
            }catch(Exception ex)
            {
                return null;
            }
        }

        public void CerrarConexion()
        {
            this.comCliente.Close();
        }
    }
}
