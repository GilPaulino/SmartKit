using System;
using System.Data;

namespace Calculadora
{
    class Program
    {
        enum EOpcoes
        {
            Soma = 1,
            Subtracao = 2,
            Divisao = 3,
            Multiplicacao = 4,
            Expressao = 5,
            Continuar = 6,
            Sair = 9
        }
        enum EValores
        {
            PrimeiroValor = 1,
            SegundoValor = 2
        }
        static double ultimoResultado = 0;

        static void DefinicaoAcao(EOpcoes opcaoSelecionada)
        {
            switch (opcaoSelecionada)
            {
                case EOpcoes.Soma: Soma(); break;
                case EOpcoes.Subtracao: Subtracao(); break;
                case EOpcoes.Divisao: Divisao(); break;
                case EOpcoes.Multiplicacao: Multiplicacao(); break;
                case EOpcoes.Expressao: Expressao(); break;
                case EOpcoes.Continuar: Menu(); break;
                case EOpcoes.Sair: return;
                default: Menu(); break;
            }
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("================== Calculadora ===================");
            Console.WriteLine("| 1 - Somar                                      |");
            Console.WriteLine("| 2 - Subtrair                                   |");
            Console.WriteLine("| 3 - Multiplicação                              |");
            Console.WriteLine("| 4 - Divisão                                    |");
            Console.WriteLine("| 5 - Expressão                                  |");
            Console.WriteLine("| 9 - Sair                                       |");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Selecione uma opção: ");
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int valor) && Enum.IsDefined(typeof(EOpcoes), valor))
            {
                EOpcoes opcaoSelecionada = (EOpcoes)valor;
                Console.WriteLine($"Opção selecionada: {opcaoSelecionada}");
                DefinicaoAcao(opcaoSelecionada);
            }
            else
            {
                Console.WriteLine("Opção inválida! Tente novamente.");
                Menu();
            }
        }

        static bool ValidarValor(out float valor)
        {
            string entrada = Console.ReadLine();
            bool isValid = float.TryParse(entrada, out valor) && !string.IsNullOrWhiteSpace(entrada);

            if (!isValid)
            {
                Console.WriteLine("Valor inválido. Por favor, insira um número válido.");
            }

            return isValid;
        }

        static float ObterValor(EValores tipoValor)
        {
            string prompt = tipoValor == EValores.PrimeiroValor ? "Primeiro valor: " : "Segundo valor: ";
            Console.WriteLine(prompt);

            float valor;
            while (!ValidarValor(out valor))
            {
            }
            return valor;
        }

        static void ExibirUltimoResultado()
        {
            if (ultimoResultado != 0)
            {
                Console.WriteLine($"Último resultado: {ultimoResultado}");
            }
        }

        static void OpcaoContinuar()
        {
            Console.WriteLine("Deseja continuar? [s] - Sim, [n] - Não ");
            string entrada = Console.ReadLine();

            if (entrada == "s")
            {
                Menu();
            }
            else if (entrada == "n")
            {
                DefinicaoAcao(EOpcoes.Sair);
            }
            else
            {
                Console.WriteLine("Opção inválida, tente novamente.");
                OpcaoContinuar();
            }
        }

        static void Soma() => ExecutarOperacao((primeiroValor, segundoValor) => primeiroValor + segundoValor, "soma");
        static void Subtracao() => ExecutarOperacao((primeiroValor, segundoValor) => primeiroValor - segundoValor, "subtração");
        static void Divisao() => ExecutarOperacao((primeiroValor, segundoValor) => primeiroValor / segundoValor, "divisão");
        static void Multiplicacao() => ExecutarOperacao((primeiroValor, segundoValor) => primeiroValor * segundoValor, "multiplicação");
        static void Expressao()
        {
            Console.Clear();
            ExibirUltimoResultado();
            Console.WriteLine("Digite sua expressão: ");
            string expressao = Console.ReadLine();
            if (expressao != null)
            {
                var resultado = Convert.ToDouble(new DataTable().Compute(expressao, null));
                ultimoResultado = resultado;
                Console.WriteLine($"O resultado da sua expressão é: {resultado}");
            }
            else
            {
                Console.WriteLine("Expressão inválida.");
            }
            OpcaoContinuar();
        }

        static void ExecutarOperacao(Func<float, float, float> operacao, string nomeOperacao)
        {
            Console.Clear();
            ExibirUltimoResultado();

            float primeiroValor = ObterValor(EValores.PrimeiroValor);
            float segundoValor = ObterValor(EValores.SegundoValor);

            float resultado = operacao(primeiroValor, segundoValor);
            ultimoResultado = resultado;
            Console.WriteLine($"O resultado da {nomeOperacao} é {resultado}");
            OpcaoContinuar();
        }
    }
}
