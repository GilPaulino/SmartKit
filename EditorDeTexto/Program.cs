using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace EditorDeTexto
{
    class Program
    {
        enum EOpcoesEditorTexto
        {
            Abrir = 1,
            Criar = 2,
            Sair = 0,
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("================= Editor de texto =================");
            Console.WriteLine("| 1 - Abrir um txt                                |");
            Console.WriteLine("| 2 - Criar um novo txt                           |");
            Console.WriteLine("| 0 - Sair                                        |");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("O que deseja fazer?");

            string entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int valor) && Enum.IsDefined(typeof(EOpcoesEditorTexto), valor))
            {
                EOpcoesEditorTexto opcaoSelecionada = (EOpcoesEditorTexto)valor;
                Console.WriteLine($"Opção selecionada: {opcaoSelecionada}");
                DefinicaoAcao(opcaoSelecionada);
            }
            else
            {
                MensagemErro();
            }
        }

        static void DefinicaoAcao(EOpcoesEditorTexto opcaoSelecionada)
        {
            switch (opcaoSelecionada)
            {
                case EOpcoesEditorTexto.Abrir: Abrir(); break;
                case EOpcoesEditorTexto.Criar: Criar(); break;
                case EOpcoesEditorTexto.Sair: return;
                default: Menu(); break;
            }
        }

        static void Abrir()
        {
            Console.Clear();
            Console.WriteLine("Qual nome do arquivo?");
            string nomeArquivo = $"C:/dev/cursos/balta/SmartKit/TextosSalvos/{Console.ReadLine()}.txt";
            using (var pasta = new StreamReader(nomeArquivo))
            {
                string texto = pasta.ReadToEnd();
                Console.WriteLine(texto);
            }
            Console.WriteLine("");
            Console.ReadLine();
            Menu();
        }

        static void Salvar(string texto)
        {
            Console.Clear();
            Console.WriteLine("================== Editor de texto ================");
            Console.WriteLine("| Qual nome e extenção do arquivo para salvar?    |");
            Console.WriteLine("| Exemplo:                                        |");
            Console.WriteLine("|   * exemploArquivoTexto                         |");
            Console.WriteLine("--------------------------------------------------");
            string diretorioRaiz = Directory.GetCurrentDirectory();
            string caminhoPastaTextosSalvos = Path.Combine(diretorioRaiz, "TextosSalvos");
            if (!Directory.Exists(caminhoPastaTextosSalvos))
            {
                Directory.CreateDirectory(caminhoPastaTextosSalvos);
            }
            var caminho = Path.Combine(caminhoPastaTextosSalvos, $"{Console.ReadLine()}.txt");
            using (var arquivo = new StreamWriter(caminho))
            {
                arquivo.Write(texto);
            }
            MensagemSucesso(caminho);
        }

        static void Criar()
        {
            Console.Clear();
            Console.WriteLine("================= Editor de texto =================");
            Console.WriteLine("| Digite seu texto abaixo.                        |");
            Console.WriteLine("| Pressione a tecla 'ESC' para salvar)            |");
            Console.WriteLine("---------------------------------------------------");
            string texto = "";

            do
            {
                texto += Console.ReadLine();
                texto += Environment.NewLine;
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

            Salvar(texto);
        }
        static void MensagemErro()
        {
            Console.Clear();
            Console.WriteLine("====================== Erro ======================");
            Console.WriteLine($"| Opção inválida!                               |");
            Console.WriteLine($"| Pressione qualquer tecla para                 |");
            Console.WriteLine($"| tentar novamente...                           |");
            Console.WriteLine("--------------------------------------------------");
            Thread.Sleep(1500);
            Menu();
        }
        static void MensagemSucesso(string caminhoArquivo)
        {
            Console.Clear();
            Console.WriteLine("===================== Sucesso ====================");
            Console.WriteLine($"| Arquivo salvo com sucesso!                    |");
            Console.WriteLine($"| Deseja abrir o diretório do arquivo?          |");
            Console.WriteLine($"| [s] - Sim, [n] - Não                          |");
            Console.WriteLine("--------------------------------------------------");

            var resposta = Console.ReadLine()?.ToLower();

            if (resposta == "s")
            {
                AbrirDiretorio(caminhoArquivo);
            }
            MensagemRetornandoMenu();
        }
        static void AbrirDiretorio(string caminhoArquivo)
        {
            var diretorio = Path.GetDirectoryName(caminhoArquivo);
            var nomeArquivo = Path.GetFileName(caminhoArquivo);

            Process.Start(new ProcessStartInfo
            {
                FileName = diretorio,
                Arguments = $"/select,\"{caminhoArquivo}\"",
                UseShellExecute = true
            });
        }
        static void MensagemRetornandoMenu()
        {
            Console.Clear();
            Console.WriteLine("===================== Sucesso ====================");
            Console.WriteLine($"| Diretório aberto com sucesso.                 |");
            Console.WriteLine($"| Retornando ao menu do editor de texto...      |");
            Console.WriteLine("--------------------------------------------------");
            Thread.Sleep(3000);
            Menu();
        }
    }
}
