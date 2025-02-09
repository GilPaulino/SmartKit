using System;
using System.Threading;
using System.Text.RegularExpressions;

namespace Cronometro
{
    class Program
    {
        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("=================== Cronômetro ===================");
            Console.WriteLine("| Informe quanto tempo deseja contar:            |");
            Console.WriteLine("| Exemplos:                                      |");
            Console.WriteLine("| * 10s para contar 10segundos                   |");
            Console.WriteLine("| * 1m para 1 minuto                             |");
            Console.WriteLine("| * 1h para 1 hora                               |");
            Console.WriteLine("| Digite 0 ou sair, para sair.                   |");
            Console.WriteLine("--------------------------------------------------");
            string entrada = Console.ReadLine().ToLower();
            if (entrada == "0" || entrada == "sair")
            {
                return;
            }
            entrada = ValidarEntrada(entrada);
            Multiplicador(entrada);
        }
        static string ValidarEntrada(string entrada)
        {
            string padrao = @"^\d+[smh]$";

            while (!Regex.IsMatch(entrada, padrao))
            {
                Console.WriteLine("Entrada inválida! Exemplo correto: 10s, 5m, 1h.");
                Console.Write("Digite novamente o tempo: ");
                entrada = Console.ReadLine().ToLower();
            }

            return entrada;
        }

        static void Multiplicador(string entrada)
        {
            char tipoContagem = entrada[^1];
            int tempo = int.Parse(entrada[..^1]);
            int multiplicador = tipoContagem == 'm' ? 60 : tipoContagem == 'h' ? 3600 : 1;

            PreStart(tempo * multiplicador);
        }

        static void PreStart(int tempo)
        {

            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("| Preparar...                                    |");
            Console.WriteLine("--------------------------------------------------");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("| Preparar...                                    |");
            Console.WriteLine("| vai...                                         |");
            Console.WriteLine("--------------------------------------------------");
            Thread.Sleep(1000);
            Iniciar(tempo);
        }

        static void Iniciar(int tempo)
        {
            for (int i = 1; i <= tempo; i++)
            {
                Console.Clear();
                Console.WriteLine("==================== Acabou ======================");
                Console.WriteLine($"| Tempo: {i}                                       |");
                Console.WriteLine("--------------------------------------------------");
                Thread.Sleep(1000);
            }

            Console.Clear();
            Console.WriteLine("==================== Acabou ======================");
            Console.WriteLine("| Cronômetro finalizado..                        |");
            Console.WriteLine("| Retornando para o menu.                        |");
            Console.WriteLine("--------------------------------------------------");
            Thread.Sleep(2500);
            Menu();
        }
    }
}
