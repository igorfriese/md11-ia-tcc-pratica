# EVIDENCIAS.md

## Questão 11 — Evidência de uso real da IA

**Ferramenta usada:** Claude Code (aba Code do app desktop do Claude, no Windows), modelo Claude Opus 5.5.

**Contexto:** abri o Claude Code na raiz do repositório `md11-ia-tcc-pratica`, com o `CLAUDE.md` na raiz (carregado automaticamente pelo Claude Code como instruções do projeto) e a skill em `.claude/skills/`. Na mesma sessão, pedi para a IA preparar os arquivos da atividade (colar as respostas no README, ajustar o `CLAUDE.md` e a skill) e depois executar uma tarefa real no `GerenciadorDeTarefas` usando a skill.

**Prompt exato utilizado:**

A sessão teve duas etapas. Na primeira, mandei uma lista de passos (colar respostas no README, colocar o `CLAUDE.md`, colocar a skill, usar a IA de verdade, preencher este arquivo), incluindo este trecho:

```
Rode um prompt real, por exemplo: "Usando a skill adicionar-comando-menu, adicione uma opção para buscar tarefas por palavra-chave no título."
```

Antes de continuar, a IA leu o `Program.cs` e **apontou que a skill que eu tinha preparado (`adicionar-comando-menu`) não batia com o projeto real**. Ela pressupunha um menu interativo e as classes `Tarefa.cs`/`GerenciadorTarefas.cs`, mas o projeto é um único `Program.cs` com *top-level statements*, lista de tuplas e funções locais, sem menu. A IA me perguntou como seguir e eu escolhi **adaptar a skill** (renomeada para `adicionar-funcionalidade`) em vez de criar um menu, que seria uma mudança de arquitetura proibida pelo próprio `CLAUDE.md`. Com isso, a tarefa executada ficou:

```
Usando a skill adicionar-funcionalidade, adicione uma função para buscar tarefas por palavra-chave no título.
```

(a IA carregou a skill `adicionar-funcionalidade` pela ferramenta de skills do Claude Code, passando como argumento "adicione uma função para buscar tarefas por palavra-chave no título").

**O que a IA fez:**

1. Leu o `Program.cs` inteiro para confirmar a estrutura descrita na skill (passo 1 da skill).
2. Adicionou a função local `BuscarPorPalavraChave(string palavraChave)` logo abaixo de `Listar()`, com:
   - validação de palavra-chave vazia ou só com espaços;
   - busca por `Contains` sem diferenciar maiúsculas/minúsculas (LINQ, sem pacote externo);
   - mensagem quando nenhuma tarefa é encontrada;
   - exibição no mesmo formato de `Listar()` (`[ ] #Id — Título`).
3. Acrescentou ao fluxo de demonstração, antes do `Console.ReadLine()`, três chamadas: uma que encontra resultado (`"claude"`), uma sem resultado (`"relatório"`) e uma com entrada inválida (`"  "`).
4. Rodou `dotnet build` (0 avisos, 0 erros) e `dotnet run`, e conferiu a saída.

Único arquivo de código alterado: `GerenciadorDeTarefas/Program.cs`.

**Trecho de código gerado:**
```csharp
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
```

**Saída do `dotnet run`:**
```
=== Gerenciador de Tarefas ===
[ ] #1 — Estudar para a avaliação do Módulo 11
[ ] #2 — Configurar o CLAUDE.md do projeto
[ ] #3 — Criar uma Skill reutilizável

=== Depois de concluir a tarefa #1 ===
[X] #1 — Estudar para a avaliação do Módulo 11
[ ] #2 — Configurar o CLAUDE.md do projeto
[ ] #3 — Criar uma Skill reutilizável

=== Busca por "claude" ===
[ ] #2 — Configurar o CLAUDE.md do projeto

=== Busca por "relatório" ===
Nenhuma tarefa encontrada com "relatório".

=== Busca com palavra-chave vazia ===
Informe uma palavra-chave para a busca.
```

**Seguiu o CLAUDE.md / Skill?**

Sim, em todos os pontos que verifiquei:
- **CLAUDE.md**: nomes em português e PascalCase, nenhuma dependência nova, nenhuma classe ou arquivo novo, nenhuma mudança de arquitetura, validação de entrada sem exceção, build conferido depois da mudança. O ponto mais relevante: a regra "ler o código relevante antes de propor mudanças (não assumir estrutura sem checar)" fez a IA perceber que a skill original não correspondia ao código, em vez de sair criando um menu e classes que não existiam.
- **Skill**: seguiu o passo a passo na ordem (ler → criar função local → validar casos limite → demonstrar no fluxo → build → run). O formato de saída (`=== ... ===`, sem emojis, mesmo formato de `Listar()`) foi respeitado, e `Adicionar`, `Concluir` e `Listar` não foram alterados.
- Um ponto de tensão: a skill pede para "reaproveitar o formato de `Listar()`" e ao mesmo tempo "não alterar `Listar()`". Por isso a IA repetiu as duas linhas de formatação dentro da nova função em vez de extrair um método auxiliar compartilhado, o que seria mais limpo mas mexeria em `Listar()`. Aceitei a duplicação por ser pequena. Se o projeto crescer, vale ajustar a skill para permitir essa extração.

**Ajustes que você precisou fazer manualmente (se houver):**

Não precisei corrigir o código gerado: compilou e funcionou de primeira. O ajuste necessário foi **nos arquivos de orientação, não no código**. O `CLAUDE.md` e a skill que eu tinha preparado descreviam uma estrutura genérica (menu, `Tarefa.cs`, `GerenciadorTarefas.cs`) que não existe neste projeto. Com a ajuda da IA, reescrevi a seção de estrutura do `CLAUDE.md` com os nomes reais (`tarefas`, `proximoId`, `Adicionar`, `Concluir`, `Listar`) e troquei a skill `adicionar-comando-menu` por `adicionar-funcionalidade`. A lição é que um guia de projeto genérico demais pode levar a IA para o caminho errado, e ele precisa ser conferido contra o código real.
