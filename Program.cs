using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscaminas
{
    internal class Program
    {
       //variables constantes
        const int FILAS = 5;
        const int COLUMNAS = 5;
        const int MINAS = 5;
        //metodo principal --------------------------------
        static void Main(string[] args){
        //creando arreglos bidimensionales de tipo char con dimensiones ya definidas en esta caso 5 
            char[,] tableroOculto=new char[FILAS,COLUMNAS];
            char[,] tableroUsuario=new char[FILAS,COLUMNAS];

            CrearTaberos(tableroOculto,tableroUsuario);
            ColocarMinas(tableroOculto);
            CalcularNumeros(tableroOculto);
           
            //banderas de verificacion
            bool juegoTerminado=false;
            bool victoria =false;

            //bucle principal del juego-----------------------
            while (!juegoTerminado) 
            {
                Console.Clear();
                Console.WriteLine("=== BUSCAMINAS ===\n");
                MostrarTablero(tableroUsuario);

                // leer coordenadas con validación
                int fila = LeerCoordenada("Ingrese Fila (1-5): ") - 1;
                int columna = LeerCoordenada("Ingrese Columna (1-5): ") - 1;

                //condicional para saaber si toco mina
                if(tableroOculto[fila,columna]=='*')
                {
                    juegoTerminado = true;
                    victoria = false;

                }
                else
                {
                    // descubrir la casilla
                    MostrarPosicion(tableroUsuario,tableroOculto, fila, columna);

                    // verificar si gano
                    if (VerificarVictoria(tableroUsuario))
                    {
                        juegoTerminado = true;
                        victoria = true;
                    }

                }


            }
            Console.Clear();
            if (victoria)
            {
                Console.WriteLine("¡FELICIDADES! ¡HAS GANADO EL JUEGO!\n");
            }
            else
            {
                Console.WriteLine("¡BOOM! Has tocado una mina. Fin del juego.\n");
            }

            Console.WriteLine("Tablero Final:");
            MostrarTablero(tableroOculto);
            Console.ReadLine();


        }

        // metodo para iniciar tableros
        static void CrearTaberos(char[,]oculto,char[,] usuario){
            for(int i=0;i<FILAS;i++)
            {
                for(int j=0;j<COLUMNAS;j++)
                {
                    oculto[i, j] = '0' ;
                    usuario[i, j] = '■';
                }
            }
        }

        //metodo de colocar minas al azar
        static void ColocarMinas(char[,]oculto)
        {
            //inicializacion del objeto rand
            Random rand=new Random();
            int minasCont=0;
            while(minasCont<MINAS)
            {

                int f=rand.Next(0,FILAS);
                int c = rand.Next(0, COLUMNAS);
                if(oculto[f,c]!='*')
                {
                    oculto[f, c] = '*';
                    minasCont++;
                }
            }
        }

        //introduce los numeros en el tablero
        static void CalcularNumeros(char[,]oculto){
            for(int i=0;i<FILAS;i++)
            {
                for(int j=0;j<COLUMNAS;j++)
                {
                    if(oculto[i,j]!='*')
                    {
                        //llama a metodo para contar minas en un tablero de 3*3
                        int cantM = ContarMinas(oculto,i,j);
                        oculto[i, j] = (char)(cantM+'0');
                    }
                }
            }
        }

        //metodo contador de minas alrededor de una posicion recibida
        static int ContarMinas(char[,] oculto,int fila, int columna)
        {
            int cont = 0;
            //limites para recorrer alrededor de la posicion
            for(int i=fila-1;i<=fila+1;i++)
            {
                for (int j=columna-1;j<=columna+1;j++)
                {
                    //condicional para no salirse de la matriz y romper codigo
                    if(i>=0 && i<FILAS && j>=0 && j<COLUMNAS)
                    {
                       if(oculto[i,j]=='*')
                       {  cont++; }
                    }
                }
            }
            return cont;
        }

        // validacion que el usuario introduzca un número entre 1 y 5
        static int LeerCoordenada(string mensaje)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= 1 && valor <= 5)
                {
                    return valor;
                }
                Console.WriteLine("Error, Ingrese un número válido entre 1 y 5");
            }
        }

        //metodo encargado de copiar del tablero oculto al tablero del usuario el visual
        static void MostrarPosicion(char[,]usuario,char[,]oculto,int fila,int columna)
        {
            usuario[fila,columna]=oculto[fila,columna];
        }


        // verifica si el jugador ya descubrió todas las casillas que no son minas
        static bool VerificarVictoria(char[,] usuario)
        {
            int casillasDescubiertas = 0;
            int casillasObjetivo = (FILAS * COLUMNAS) - MINAS; // 25 - 5 = 20

            for (int i = 0; i < FILAS; i++)
            {
                for (int j = 0; j < COLUMNAS; j++)
                {
                    if (usuario[i, j] != '■')
                    {
                        casillasDescubiertas++;
                    }
                }
            }
            return casillasDescubiertas == casillasObjetivo;
        }

        //impresion de tablero
        static void MostrarTablero(char[,] usuario)
        {
            for (int i=0; i<FILAS;i++)
            {
                for(int j=0;j<COLUMNAS;j++)
                {
                    Console.Write(usuario[i,j]+" ");
                
                }
                Console.WriteLine();

            }
        }


    }
}