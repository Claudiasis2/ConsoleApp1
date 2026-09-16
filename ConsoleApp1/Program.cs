using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;


using static System.Runtime.InteropServices.JavaScript.JSType;
class Progas

{
    public static async Task Main()
    {
        //HelloWorld();
        //CompareDates();
        await Gemini();    
    }

    public static void HelloWorld()
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
    public static void CompareDates()
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

    public static async Task<int> Gemini(string[] args = null!)
    {
        string? key = "AQ.Ab8RN6LgvaTZNjVAo2zzj0_yGTRnMUQrPVLl1DmzdcNMcEwb7A";
        if (string.IsNullOrWhiteSpace(key))
        {
            Console.Error.WriteLine("Configura GEMINI_API_KEY primero.");
            return 1;
        }
        string model = Environment.GetEnvironmentVariable("GEMINI_MODEL")
        ?? "gemini-3.8-flash";
        //string prompt = args.Length > 0 ? string.Join(" ", args) : "Explica una variable en una frase.";
        string prompt = "Explica una variable en una frase.";
        string uri = "https://generativelanguage.googleapis.com/"
        + "v1beta/interactions";
        try
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(120);
            using var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add("x-goog-api-key", key);
            request.Headers.Add("Api-Revision", "2026-05-20");
            string json = JsonSerializer.Serialize(new
            {
                model,
                input = prompt,
                store = false
            });
            request.Content = new StringContent(
            json, Encoding.UTF8, "application/json");
            using var response = await client.SendAsync(request);
            string raw = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.Error.WriteLine($"HTTP {(int)response.StatusCode}");
                Console.Error.WriteLine(raw.Replace(key, "[CLAVE]"));
                return 1;
            }
            using var document = JsonDocument.Parse(raw);
            var root = document.RootElement;
            if (root.GetProperty("status").GetString() != "completed")
                throw new InvalidOperationException("Respuesta incompleta.");
            var text = new StringBuilder();
            foreach (var step in root.GetProperty("steps").EnumerateArray())
            {
                if (step.GetProperty("type").GetString() != "model_output")
                    continue;
                foreach (var part in step.GetProperty("content").EnumerateArray())
                {
                    if (part.GetProperty("type").GetString() == "text")
                        text.AppendLine(part.GetProperty("text").GetString());
                }
            }
            if (text.Length == 0)
                throw new InvalidOperationException("Respuesta sin texto.");
            Console.WriteLine(text.ToString().TrimEnd());
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message.Replace(key, "[CLAVE]"));
            return 1;
        }
    }

}
