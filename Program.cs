using System;
using System.Threading;
using System.Diagnostics;
using Cronometro;
using Calculadora;

enum OpcoesMenu
{
    Sair = 0,
    Calculadora = 1,
    Cronometro = 2,
    Editor = 3
}

class Program
{
    static void Main()
    {
        while (true)
        {
            OpcoesMenu escolha = ExibirMenuPrincipal();

            switch (escolha)
            {
                case OpcoesMenu.Calculadora:
                    Calculadora.Program.Menu();
                    break;
                case OpcoesMenu.Cronometro:
                    Cronometro.Program.Menu();
                    break;
                case OpcoesMenu.Editor:
                    EditorDeTexto.Program.Menu();
                    break;
                case OpcoesMenu.Sair:
                    Console.WriteLine("Saindo do SmartKit...");
                    return;
            }
        }
    }

    static OpcoesMenu ExibirMenuPrincipal()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==================== SmartKit ====================");
            Console.WriteLine("| 1 - Calculadora                                |");
            Console.WriteLine("| 2 - Cronômetro                                 |");
            Console.WriteLine("| 3 - Editor de texto                            |");
            Console.WriteLine("| 0 - Sair                                       |");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("O que deseja usar?");

            string entrada = Console.ReadLine();
            if (ValidarEscolha(entrada, out OpcoesMenu escolha))
            {
                return escolha;
            }

            MensagemErro();
        }
    }

    static bool ValidarEscolha(string entrada, out OpcoesMenu escolha)
    {
        bool valid = Enum.TryParse(entrada, out escolha) && Enum.IsDefined(typeof(OpcoesMenu), escolha);

        if (valid && escolha == OpcoesMenu.Sair)
        {
            MensagemFinalizandoApp();

        }
        return Enum.TryParse(entrada, out escolha) && Enum.IsDefined(typeof(OpcoesMenu), escolha);
    }
    static void MensagemErro()
    {
        Console.Clear();
        Console.WriteLine("====================== Erro =======================");
        Console.WriteLine($"| Opção inválida!                                |");
        Console.WriteLine($"| Tente novamente.                               |");
        Console.WriteLine("---------------------------------------------------");
        Thread.Sleep(1500);
    }
    static void MensagemFinalizandoApp()
    {
        Console.Clear();
        Console.WriteLine("==================== SmartKit =====================");
        Console.WriteLine($"| App finalizado..                                |");
        Console.WriteLine($"| Obrigado, até mais!                             |");
        Console.WriteLine("---------------------------------------------------");
        Thread.Sleep(3000);
        System.Environment.Exit(0);
    }
}
