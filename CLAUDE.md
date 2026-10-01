# CLAUDE.md — GerenciadorDeTarefas

Este arquivo orienta qualquer assistente de IA (Claude Code, Copilot, Cursor, etc.) que for trabalhar neste repositório. Objetivo: reduzir ambiguidade e evitar que a IA sugira algo fora do padrão do projeto.

## Sobre o projeto

`GerenciadorDeTarefas` é um console app em C#/.NET que simula um gerenciador de tarefas simples, usado como base de prática para a disciplina de Tecnologias Emergentes e IA. O foco da atividade não é adicionar funcionalidades novas, e sim usar um assistente de IA de forma orientada sobre este código já existente.

- **Linguagem/stack**: C# com .NET 8 (`net8.0`, `ImplicitUsings` e `Nullable` habilitados), projeto de console (`GerenciadorDeTarefas.sln` / `GerenciadorDeTarefas/GerenciadorDeTarefas.csproj`), execução via `dotnet run` ou Visual Studio.
- **Como rodar**: `cd GerenciadorDeTarefas && dotnet run` (o programa termina com `Console.ReadLine()`, então aguarda um Enter antes de fechar)
- **Como buildar**: `dotnet build`
- **Testes**: não há projeto de testes; a verificação é feita por `dotnet build` + execução manual com `dotnet run`.

## Estrutura real do projeto

Todo o código está em um único arquivo, **`GerenciadorDeTarefas/Program.cs`**, usando *top-level statements* (sem classe `Program`, sem `Main` explícito):

- **Dados**: `tarefas` — uma `List<(int Id, string Titulo, bool Concluida)>` (lista de tuplas nomeadas, não existe classe `Tarefa`), e `proximoId` — contador usado para gerar o `Id` da próxima tarefa.
- **Operações** (funções locais, declaradas no topo do arquivo):
  - `Adicionar(string titulo)` — cria uma tarefa pendente com o próximo Id.
  - `Concluir(int id)` — marca como concluída a tarefa com o Id informado (tuplas são imutáveis, então o item da lista é substituído).
  - `Listar()` — imprime cada tarefa no formato `[ ] #1 — Título` / `[X] #1 — Título`.
- **Fluxo de demonstração** (abaixo das funções): adiciona 3 tarefas de exemplo, imprime um cabeçalho `=== ... ===`, lista, conclui a tarefa #1 e lista de novo. Não existe menu interativo.
- Sem banco de dados/persistência externa: os dados vivem só em memória durante a execução.

## Convenções de código

- Nomenclatura em **PascalCase** para funções/métodos, **camelCase** para variáveis locais, seguindo a convenção padrão do C#.
- Nomes de funções no infinitivo, curtos e em português (`Adicionar`, `Concluir`, `Listar`).
- Novas operações entram como **funções locais** em `Program.cs`, junto das existentes, mantendo a lista de tuplas como modelo de dados — não criar classes novas sem pedido explícito.
- Saída no console sem emojis, com cabeçalhos no formato `=== Título ===` e tarefas no mesmo formato de `Listar()`.
- Comentários e nomes de variáveis em **português**, para manter consistência com o restante do projeto.
- Validar os parâmetros antes de processar (ex.: texto vazio, Id inexistente) e tratar esses casos com uma mensagem no console, sem lançar exceção não tratada.
- Preferir métodos pequenos e coesos a métodos longos fazendo várias coisas.

## O que a IA PODE fazer

- Sugerir e implementar pequenas melhorias no código existente, seguindo o padrão de nomenclatura e organização já usado no projeto.
- Explicar trechos de código antes de alterá-los.
- Adicionar novas operações (ex.: remover, buscar, listar pendentes) seguindo o mesmo padrão das funções já existentes — ver a Skill `.claude/skills/adicionar-funcionalidade/SKILL.md`.
- Escrever documentação (comentários, docstring-style) para métodos que ainda não têm.

## O que a IA NÃO deve fazer

- Não alterar arquivos fora do escopo pedido na tarefa atual.
- Não adicionar dependências/pacotes externos sem indicação explícita.
- Não reescrever a arquitetura do projeto (ex.: trocar para persistência em banco, extrair classes/serviços, transformar o fluxo em menu interativo) sem pedido explícito.
- Não inventar funcionalidades que não foram solicitadas.

## Fluxo de trabalho esperado

1. Ler o código relevante antes de propor mudanças (não assumir estrutura sem checar).
2. Propor a mudança de forma incremental — uma funcionalidade/correção por vez.
3. Após a mudança, garantir que o projeto builda (`dotnet build`) sem erros.
4. Explicar em poucas linhas o que foi alterado e por quê.

## Contexto da avaliação

Este `CLAUDE.md` é entregue como parte de uma avaliação individual (Módulo 11 — Tecnologias Emergentes e IA), cujo objetivo é demonstrar uso real e orientado de um assistente de IA sobre este projeto, com evidência documentada em `EVIDENCIAS.md`.
