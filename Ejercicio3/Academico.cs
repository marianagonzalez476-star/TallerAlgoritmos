using System;

namespace Taller
{
    static class Academico
    {
        public static void MenuAcademico()
        {
            const int MAX = 25;
            string[] codigo = new string[MAX];
            string[] nombre = new string[MAX];
            double[,] notas = new double[MAX, 3];
            double[] asistencia = new double[MAX];
            int n = 0, op;

            do
            {
                Console.WriteLine();
                Console.WriteLine("--- ACADEMICO ---");
                Console.WriteLine("1. Registrar estudiante");
                Console.WriteLine("2. Mostrar estudiantes (definitiva y estado)");
                Console.WriteLine("3. Promedio general del grupo");
                Console.WriteLine("4. Mejor y peor promedio");
                Console.WriteLine("0. Volver");
                Console.Write("Opcion: ");

                int.TryParse(Console.ReadLine(), out op);

                switch (op)
                {
                    case 1:
                        RegistrarEstudiante(codigo, nombre, notas, asistencia, ref n, MAX);
                        break;
                    case 2:
                        MostrarEstudiantes(codigo, nombre, notas, asistencia, n);
                        break;
                    case 3:
                        if (n == 0)
                            Console.WriteLine("No hay estudiantes.");
                        else
                            Console.WriteLine("Promedio general: " + PromedioGeneral(notas, n));
                        break;
                    case 4:
                    {
                        int mejor = MejorPromedio(notas, n);
                        int peor = PeorPromedio(notas, n);
                        if (mejor == -1)
                        {
                            Console.WriteLine("No hay estudiantes.");
                        }
                        else
                        {
                            Console.WriteLine("Mejor: " + nombre[mejor]
                                + " (" + CalcularDefinitiva(notas, mejor) + ")");
                            Console.WriteLine("Peor: " + nombre[peor]
                                + " (" + CalcularDefinitiva(notas, peor) + ")");
                        }
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

        static void RegistrarEstudiante(string[] codigo, string[] nombre, double[,] notas,
            double[] asistencia, ref int n, int max)
        {
            if (n >= max)
            {
                Console.WriteLine("No hay cupo.");
                return;
            }

            Console.Write("Codigo: ");
            string c = Console.ReadLine();

            for (int i = 0; i < n; i++)
            {
                if (codigo[i] == c)
                {
                    Console.WriteLine("Codigo ya registrado.");
                    return;
                }
            }

            Console.Write("Nombre: ");
            string nom = Console.ReadLine();

            double n1, n2, n3;
            Console.Write("Nota 1: ");
            if (!double.TryParse(Console.ReadLine(), out n1)) { Console.WriteLine("Nota invalida."); return; }
            Console.Write("Nota 2: ");
            if (!double.TryParse(Console.ReadLine(), out n2)) { Console.WriteLine("Nota invalida."); return; }
            Console.Write("Nota 3: ");
            if (!double.TryParse(Console.ReadLine(), out n3)) { Console.WriteLine("Nota invalida."); return; }

            if (n1 < 0 || n1 > 5 || n2 < 0 || n2 > 5 || n3 < 0 || n3 > 5)
            {
                Console.WriteLine("Las notas deben estar entre 0 y 5.");
                return;
            }

            Console.Write("Asistencia (%): ");
            double asis;
            if (!double.TryParse(Console.ReadLine(), out asis) || asis < 0 || asis > 100)
            {
                Console.WriteLine("La asistencia debe estar entre 0 y 100.");
                return;
            }

            codigo[n] = c;
            nombre[n] = nom;
            notas[n, 0] = n1;
            notas[n, 1] = n2;
            notas[n, 2] = n3;
            asistencia[n] = asis;
            n++;
            Console.WriteLine("Estudiante registrado.");
        }

        static double CalcularDefinitiva(double[,] notas, int i)
        {
            return (notas[i, 0] + notas[i, 1] + notas[i, 2]) / 3.0;
        }

        static int DeterminarEstado(double def)
        {
            if (def >= 4.0) return 1;
            if (def >= 3.0) return 2;
            return 3;
        }

        static void MostrarEstudiantes(string[] codigo, string[] nombre, double[,] notas,
            double[] asistencia, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("No hay estudiantes.");
                return;
            }
            for (int i = 0; i < n; i++)
            {
                double def = CalcularDefinitiva(notas, i);
                int est = DeterminarEstado(def);
                string estTxt;
                if (est == 1) estTxt = "Aprobado";
                else if (est == 2) estTxt = "Habilita";
                else estTxt = "Reprueba";

                Console.WriteLine(codigo[i] + " | " + nombre[i]
                    + " | Def: " + def
                    + " | Asistencia: " + asistencia[i] + "%"
                    + " | Estado: " + estTxt);
            }
        }

        static double PromedioGeneral(double[,] notas, int n)
        {
            if (n == 0) return 0.0;
            double suma = 0.0;
            for (int i = 0; i < n; i++)
                suma += CalcularDefinitiva(notas, i);
            return suma / n;
        }

        static int MejorPromedio(double[,] notas, int n)
        {
            if (n == 0) return -1;
            int idx = 0;
            for (int i = 1; i < n; i++)
            {
                if (CalcularDefinitiva(notas, i) > CalcularDefinitiva(notas, idx))
                    idx = i;
            }
            return idx;
        }

        static int PeorPromedio(double[,] notas, int n)
        {
            if (n == 0) return -1;
            int idx = 0;
            for (int i = 1; i < n; i++)
            {
                if (CalcularDefinitiva(notas, i) < CalcularDefinitiva(notas, idx))
                    idx = i;
            }
            return idx;
        }
    }
}
