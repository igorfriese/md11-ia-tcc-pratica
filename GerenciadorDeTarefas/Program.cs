var tarefas = new List<(int Id, string Titulo, bool Concluida)>();
var proximoId = 1;

void Adicionar(string titulo)
{
    tarefas.Add((proximoId++, titulo, false));
}

void Concluir(int id)
{
    for (var i = 0; i < tarefas.Count; i++)
    {
        if (tarefas[i].Id == id)
        {
            tarefas[i] = (tarefas[i].Id, tarefas[i].Titulo, true);
        }
    }
}

void Listar()
{
    foreach (var t in tarefas)
    {
        var status = t.Concluida ? "[X]" : "[ ]";
        Console.WriteLine($"{status} #{t.Id} — {t.Titulo}");
    }
}

// Busca tarefas cujo título contém a palavra-chave (sem diferenciar maiúsculas/minúsculas)
void BuscarPorPalavraChave(string palavraChave)
{
    if (string.IsNullOrWhiteSpace(palavraChave))
    {
        Console.WriteLine("Informe uma palavra-chave para a busca.");
        return;
    }

    var encontradas = tarefas
        .Where(t => t.Titulo.Contains(palavraChave.Trim(), StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (encontradas.Count == 0)
    {
        Console.WriteLine($"Nenhuma tarefa encontrada com \"{palavraChave}\".");
        return;
    }

    foreach (var t in encontradas)
    {
        var status = t.Concluida ? "[X]" : "[ ]";
        Console.WriteLine($"{status} #{t.Id} — {t.Titulo}");
    }
}

Adicionar("Estudar para a avaliação do Módulo 11");
Adicionar("Configurar o CLAUDE.md do projeto");
Adicionar("Criar uma Skill reutilizável");

Console.WriteLine("=== Gerenciador de Tarefas ===");
Listar();

Concluir(1);

Console.WriteLine();
Console.WriteLine("=== Depois de concluir a tarefa #1 ===");
Listar();

Console.WriteLine();
Console.WriteLine("=== Busca por \"claude\" ===");
BuscarPorPalavraChave("claude");

Console.WriteLine();
Console.WriteLine("=== Busca por \"relatório\" ===");
BuscarPorPalavraChave("relatório");

Console.WriteLine();
Console.WriteLine("=== Busca com palavra-chave vazia ===");
BuscarPorPalavraChave("  ");

Console.ReadLine();

// Este projeto é simples de propósito. Use-o como base pra testar sua IA
// conectada localmente — ex.: peça pra ela adicionar um método de remover
// tarefa, ou listar só as pendentes, seguindo o estilo já usado aqui.
