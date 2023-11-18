using System;
using System.Collections.Generic;
using System.IO;

class Bloco
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public double Peso { get; set; }
}
class Program
{
    static List<Bloco> blocos = new List<Bloco>();
    static string caminhoArquivo = "blocos.txt";

    static void Main()
    {
        CarregarBlocosDoArquivo();
        while (true)
        {
            Console.WriteLine("Gestão de Blocos:");
            Console.WriteLine("1. Adicionar Bloco:");
            Console.WriteLine("2. Listar Blocos:");
            Console.WriteLine("3. Buscar Bloco por Código:");
            Console.WriteLine("4. Exibir Conteudo do Arquivo.txt:");
            Console.WriteLine("5. Sair");
            try
            {
                int opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        AdicionarBloco();
                        break;
                    case 2:
                        ListarBlocos();
                        break;
                    case 3:
                        BuscarBlocoPorCodigo();
                        break;
                    case 4:
                        ExibirConteudoDoArquivo();
                        break;
                    case 5:
                        SalvarBlocosNoArquivo();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato inválido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }

    static void AdicionarBloco()
    {
        try
        {
            Console.WriteLine("Informe o tipo do bloco: ");
            string tipo = Console.ReadLine();

            Console.WriteLine("Informe o peso do bloco: ");
            double peso = double.Parse(Console.ReadLine());

            Bloco bloco = new Bloco
            {
                Id = blocos.Count + 1,
                Tipo = tipo,
                Peso = peso
            };

            blocos.Add(bloco);
            Console.WriteLine("Bloco adicionado com sucesso");
        }
        catch (FormatException)
        {
            Console.WriteLine("Formato inválido.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar o bloco: {ex.Message}");
        }
    }
    static void ListarBlocos()
    {
        try
        {
            Console.WriteLine("Lista de Blocos:");
            foreach (var bloco in blocos)
            {
                Console.WriteLine($"ID: {bloco.Id}, Tipo: {bloco.Tipo}, Peso: {bloco.Peso}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao listar os blocos: {ex.Message}");
        }
    }
    static void BuscarBlocoPorCodigo()
    {
        try
        {
            Console.WriteLine("Informe o código do bloco: ");
            int codigo = int.Parse(Console.ReadLine());
            Bloco blocoEncontrado = blocos.Find(b => b.Id == codigo);
            if (blocoEncontrado != null)
            {
                Console.WriteLine($"Bloco encontrado - Tipo: {blocoEncontrado.Tipo}, Peso: {blocoEncontrado.Peso}");
            }
            else
            {
                Console.WriteLine("Bloco não encontrado.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Formato inválido.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar o bloco: {ex.Message}");
        }
    }

    static void ExibirConteudoDoArquivo()
    {
        try
        {
            if (File.Exists(caminhoArquivo))
            {
                string conteudo = File.ReadAllText(caminhoArquivo);
                Console.WriteLine("Conteúdo do Arquivo:");
                Console.WriteLine(conteudo);
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao exibir o conteúdo do arquivo: {ex.Message}");
        }
    }

    static void CarregarBlocosDoArquivo()
    {
        try
        {
            if (File.Exists(caminhoArquivo))
            {
                string[] linhas = File.ReadAllLines(caminhoArquivo);

                foreach (string linha in linhas)
                {
                    string[] dados = linha.Split(';');
                    Bloco bloco = new Bloco
                    {
                        Id = int.Parse(dados[0]),
                        Tipo = dados[1],
                        Peso = double.Parse(dados[2])
                    };
                    blocos.Add(bloco);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar os blocos do arquivo: {ex.Message}");
        }
    }

    static void SalvarBlocosNoArquivo()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(caminhoArquivo))
            {
                foreach (var bloco in blocos)
                {
                    sw.WriteLine($"{bloco.Id};{bloco.Tipo};{bloco.Peso}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar os blocos no arquivo: {ex.Message}");
        }
    }
}