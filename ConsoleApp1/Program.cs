using static System.Runtime.InteropServices.JavaScript.JSType;
class Progas
{
    public static void Main()
    {
        //HelloWorld();
        CompareDates();
    }

    static void HelloWorld()
    {
        Console.WriteLine("Hello, World!");

        Console.WriteLine("¿Cómo te llamas?");
        string nombre = Console.ReadLine() ?? "";
        Console.WriteLine($"Hola, {nombre}");

        Console.WriteLine("Año de nacimiento:");
        int anioNacimiento =
        int.Parse(Console.ReadLine() ?? "");

        Console.WriteLine("Precio del producto:");
        decimal precio =
        decimal.Parse(Console.ReadLine() ?? "");

        int anioActual = DateTime.Now.Year;
        int edadAproximada =
        anioActual - anioNacimiento;

        decimal precioFinal = precio * 0.90m;

        Console.WriteLine(
        $"{nombre}: edad aprox. {edadAproximada}");
        Console.WriteLine($"Precio: {precioFinal:F2}");
    }
    static void CompareDates()
    {
        Console.WriteLine("Anio nacimiento A:");
        int anioNacimientoA = int.Parse(Console.ReadLine() ?? "");
        Console.WriteLine("Anio nacimiento B:");
        int anioNacimientoB = int.Parse(Console.ReadLine() ?? "");

        if (anioNacimientoA == anioNacimientoB)
        {
            Console.WriteLine("Mes nacimiento A:");
            int mesNacimientoA = int.Parse(Console.ReadLine() ?? "");
            Console.WriteLine("Mes nacimiento B:");
            int mesNacimientoB = int.Parse(Console.ReadLine() ?? "");
            if (mesNacimientoA == mesNacimientoB)
            {
                Console.WriteLine("Dia nacimiento A:");
                int diaNacimientoA = int.Parse(Console.ReadLine() ?? "");
                Console.WriteLine("Dia nacimiento B:");
                int diaNacimientoB = int.Parse(Console.ReadLine() ?? "");
                if (diaNacimientoA == diaNacimientoB)
                {
                    Console.WriteLine("Tienen la misma edad");
                }
                else
                {
                    if (diaNacimientoA > diaNacimientoB)
                    {
                        Console.WriteLine("A es menor que B");
                    }
                    else
                    {
                        Console.WriteLine("A es mayor que B");
                    }
                }
            }
            else
            {
                if (mesNacimientoA > mesNacimientoB)
                {
                    Console.WriteLine("A es menor que B");
                }
                else
                {
                    Console.WriteLine("A es mayor que B");
                }
            }
        }
        else
        {
            if (anioNacimientoA > anioNacimientoB)
            {
                Console.WriteLine("anio nacimiento A es menor que B");
            }
            else
            {
                Console.WriteLine("anio nacimiento A es mayor que B");
            }
        }
    }

}