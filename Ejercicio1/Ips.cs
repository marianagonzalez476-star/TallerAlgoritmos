using System;

namespace Taller
{
    static class Ips
    {
        public static void MenuIps()
        {
            const int MAX = 30;
            string[] doc = new string[MAX];
            string[] nombre = new string[MAX];
            int[] edad = new int[MAX];
            int[] tipo = new int[MAX];
            int[] prioridad = new int[MAX];
            int n = 0, op;

            do
            {
                Console.WriteLine();
                Console.WriteLine("--- IPS TURNOS ---");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Mostrar listado");
                Console.WriteLine("3. Buscar por documento");
                Console.WriteLine("4. Contar por tipo");
                Console.WriteLine("5. Paciente con mayor prioridad");
                Console.WriteLine("0. Volver");
                Console.Write("Opcion: ");

                int.TryParse(Console.ReadLine(), out op);

                switch (op)
                {
                    case 1:
                        RegistrarPaciente(doc, nombre, edad, tipo, prioridad, ref n, MAX);
                        break;
                    case 2:
                        MostrarPacientes(doc, nombre, edad, tipo, prioridad, n);
                        break;
                    case 3:
                    {
                        Console.Write("Documento a buscar: ");
                        string b = Console.ReadLine();
                        int idx = BuscarPaciente(doc, n, b);
                        if (idx == -1)
                            Console.WriteLine("Paciente no encontrado.");
                        else
                            Console.WriteLine("Encontrado: " + nombre[idx]
                                + " | Edad: " + edad[idx]
                                + " | Tipo: " + tipo[idx]
                                + " | Prioridad: " + prioridad[idx]);
                        break;
                    }
                    case 4:
                        ContarPorTipo(tipo, n);
                        break;
                    case 5:
                    {
                        int idx = IndiceMayorPrioridad(prioridad, n);
                        if (idx == -1)
                            Console.WriteLine("No hay pacientes.");
                        else
                            Console.WriteLine("Mayor prioridad: " + nombre[idx]
                                + " (prioridad " + prioridad[idx] + ")");
                        break;
                    }
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            } while (op != 0);
        }

        static void RegistrarPaciente(string[] doc, string[] nombre, int[] edad,
            int[] tipo, int[] prioridad, ref int n, int max)
        {
            if (n >= max)
            {
                Console.WriteLine("No hay cupos disponibles.");
                return;
            }

            Console.Write("Documento: ");
            string d = Console.ReadLine();

            for (int i = 0; i < n; i++)
            {
                if (doc[i] == d)
                {
                    Console.WriteLine("El documento ya esta registrado.");
                    return;
                }
            }

            Console.Write("Nombre completo: ");
            string nom = Console.ReadLine();

            Console.Write("Edad: ");
            int e;
            if (!int.TryParse(Console.ReadLine(), out e) || e <= 0)
            {
                Console.WriteLine("Edad invalida.");
                return;
            }

            Console.Write("Tipo (1=Urgencias, 2=Consulta, 3=Prioritaria): ");
            int t;
            if (!int.TryParse(Console.ReadLine(), out t) || t < 1 || t > 3)
            {
                Console.WriteLine("Tipo invalido.");
                return;
            }

            Console.Write("Prioridad (1 a 5): ");
            int p;
            if (!int.TryParse(Console.ReadLine(), out p) || p < 1 || p > 5)
            {
                Console.WriteLine("Prioridad invalida.");
                return;
            }

            doc[n] = d;
            nombre[n] = nom;
            edad[n] = e;
            tipo[n] = t;
            prioridad[n] = p;
            n++;
            Console.WriteLine("Paciente registrado.");
        }

        static void MostrarPacientes(string[] doc, string[] nombre, int[] edad,
            int[] tipo, int[] prioridad, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine((i + 1) + ". Doc: " + doc[i]
                    + " | Nombre: " + nombre[i]
                    + " | Edad: " + edad[i]
                    + " | Tipo: " + tipo[i]
                    + " | Prioridad: " + prioridad[i]);
            }
        }

        static int BuscarPaciente(string[] doc, int n, string buscado)
        {
            for (int i = 0; i < n; i++)
            {
                if (doc[i] == buscado)
                    return i;
            }
            return -1;
        }

        static void ContarPorTipo(int[] tipo, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("No hay pacientes.");
                return;
            }
            int urgencias = 0, consulta = 0, prioritaria = 0;
            for (int i = 0; i < n; i++)
            {
                if (tipo[i] == 1) urgencias++;
                else if (tipo[i] == 2) consulta++;
                else if (tipo[i] == 3) prioritaria++;
            }
            Console.WriteLine("Urgencias: " + urgencias);
            Console.WriteLine("Consulta general: " + consulta);
            Console.WriteLine("Prioritaria: " + prioritaria);
        }

        static int IndiceMayorPrioridad(int[] prioridad, int n)
        {
            if (n == 0)
                return -1;
            int idx = 0;
            for (int i = 1; i < n; i++)
            {
                if (prioridad[i] > prioridad[idx])
                    idx = i;
            }
            return idx;
        }
    }
}
