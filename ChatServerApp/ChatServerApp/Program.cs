﻿using ChatServerApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en el puerto {0}",puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {

                    //esperar a que se conecte un cliente 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("esperando conexion de cliente...");
                    if (servidor.ObtenerCliente())
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("cliente conectado");
                        string mensaje = "";
                        while(mensaje.ToLower() != "chao")
                        {
                            mensaje = servidor.Leer();
                            Console.WriteLine("C:{0}", mensaje);
                            if (mensaje.ToLower() != "chao")
                            {
                                Console.WriteLine("S: hola cliente");
                                mensaje = Console.ReadLine().Trim();
                                Console.WriteLine("S:{0}", mensaje);
                                servidor.Escribir("hola Cliente");
                            }
                        }
                        servidor.CerrarConexion();

                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error al iniciar servidor");
                Console.ReadLine();
            }
            }

        }
    }

