using System;

namespace Taller
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("===== MENU PRINCIPAL =====");
                Console.WriteLine("1. Sistema IPS de turnos");
                Console.WriteLine("2. Cafeteria universitaria");
                Console.WriteLine("3. Control academico");
                Console.WriteLine("0. Salir");
                Console.Write("Opcion: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: Ips.MenuIps(); break;
                    case 2: Cafeteria.MenuCafeteria(); break;
                    case 3: Academico.MenuAcademico(); break;
                    case 0: Console.WriteLine("Saliendo..."); break;
                    default: Console.WriteLine("Opcion invalida."); break;
                }
            } while (opcion != 0);
        }
    }
}
