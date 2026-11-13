using System;

namespace Taller
{
    static class Cafeteria
    {
        public static void MenuCafeteria()
        {
            const int MAX = 20;
            string[] codigo = new string[MAX];
            string[] nombre = new string[MAX];
            double[] precio = new double[MAX];
            int[] cantidad = new int[MAX];
            int[] categoria = new int[MAX];
            int n = 0, op;
            double totalVendido = 0.0;

            do
            {
                Console.WriteLine();
                Console.WriteLine("--- CAFETERIA ---");
                Console.WriteLine("1. Registrar producto");
                Console.WriteLine("2. Realizar venta");
                Console.WriteLine("3. Actualizar inventario (reabastecer)");
                Console.WriteLine("4. Mostrar productos agotados");
                Console.WriteLine("5. Total vendido del dia");
                Console.WriteLine("0. Volver");
                Console.Write("Opcion: ");

                int.TryParse(Console.ReadLine(), out op);

                switch (op)
                {
                    case 1:
                        RegistrarProducto(codigo, nombre, precio, cantidad, categoria, ref n, MAX);
                        break;
                    case 2:
                        RealizarVenta(codigo, precio, cantidad, n, ref totalVendido);
                        break;
                    case 3:
                        Reabastecer(codigo, cantidad, n);
                        break;
                    case 4:
                        MostrarAgotados(codigo, nombre, cantidad, n);
                        break;
                    case 5:
                        Console.WriteLine("Total vendido del dia: " + totalVendido);
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            } while (op != 0);
        }

        static void RegistrarProducto(string[] codigo, string[] nombre, double[] precio,
            int[] cantidad, int[] categoria, ref int n, int max)
        {
            if (n >= max)
            {
                Console.WriteLine("No hay cupo para mas productos.");
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

            Console.Write("Precio unitario: ");
            double p;
            if (!double.TryParse(Console.ReadLine(), out p) || p <= 0)
            {
                Console.WriteLine("Precio invalido.");
                return;
            }

            Console.Write("Cantidad disponible: ");
            int cant;
            if (!int.TryParse(Console.ReadLine(), out cant) || cant < 0)
            {
                Console.WriteLine("Cantidad invalida.");
                return;
            }

            Console.Write("Categoria (1=Bebidas, 2=Panaderia, 3=Snacks): ");
            int cat;
            if (!int.TryParse(Console.ReadLine(), out cat) || cat < 1 || cat > 3)
            {
                Console.WriteLine("Categoria invalida.");
                return;
            }

            codigo[n] = c;
            nombre[n] = nom;
            precio[n] = p;
            cantidad[n] = cant;
            categoria[n] = cat;
            n++;
            Console.WriteLine("Producto registrado.");
        }

        static int BuscarProducto(string[] codigo, int n, string buscado)
        {
            for (int i = 0; i < n; i++)
            {
                if (codigo[i] == buscado)
                    return i;
            }
            return -1;
        }

        static void RealizarVenta(string[] codigo, double[] precio, int[] cantidad,
            int n, ref double totalVendido)
        {
            if (n == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                return;
            }

            Console.Write("Codigo del producto: ");
            string c = Console.ReadLine();

            int idx = BuscarProducto(codigo, n, c);
            if (idx == -1)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            Console.Write("Cantidad a vender: ");
            int cant;
            if (!int.TryParse(Console.ReadLine(), out cant) || cant <= 0)
            {
                Console.WriteLine("Cantidad invalida.");
                return;
            }
            if (cant > cantidad[idx])
            {
                Console.WriteLine("Stock insuficiente. Disponible: " + cantidad[idx]);
                return;
            }

            cantidad[idx] -= cant;
            double total = cant * precio[idx];
            totalVendido += total;
            Console.WriteLine("Venta realizada. Total de esta venta: " + total);
        }

        static void Reabastecer(string[] codigo, int[] cantidad, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("No hay productos.");
                return;
            }

            Console.Write("Codigo del producto: ");
            string c = Console.ReadLine();

            int idx = BuscarProducto(codigo, n, c);
            if (idx == -1)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            Console.Write("Cantidad a agregar: ");
            int cant;
            if (!int.TryParse(Console.ReadLine(), out cant) || cant <= 0)
            {
                Console.WriteLine("Cantidad invalida.");
                return;
            }

            cantidad[idx] += cant;
            Console.WriteLine("Inventario actualizado. Nueva cantidad: " + cantidad[idx]);
        }

        static void MostrarAgotados(string[] codigo, string[] nombre, int[] cantidad, int n)
        {
            if (n == 0)
            {
                Console.WriteLine("No hay productos.");
                return;
            }
            bool hay = false;
            Console.WriteLine("Productos agotados:");
            for (int i = 0; i < n; i++)
            {
                if (cantidad[i] == 0)
                {
                    Console.WriteLine(" - " + codigo[i] + " " + nombre[i]);
                    hay = true;
                }
            }
            if (!hay)
                Console.WriteLine("Ninguno.");
        }
    }
}
