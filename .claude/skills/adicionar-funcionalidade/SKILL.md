---
name: adicionar-funcionalidade
description: Adiciona uma nova operação sobre as tarefas (ex.: remover, buscar por palavra-chave, listar pendentes) ao GerenciadorDeTarefas/Program.cs, como função local no mesmo estilo de Adicionar/Concluir/Listar, e a demonstra no fluxo do programa. Use sempre que for pedida uma nova funcionalidade para o gerenciador de tarefas.
---

# Skill: Adicionar Funcionalidade ao GerenciadorDeTarefas

## Quando usar esta skill

Use sempre que precisar adicionar uma nova operação sobre as tarefas do `GerenciadorDeTarefas` (ex.: "remover tarefa", "buscar tarefas por palavra-chave no título", "listar só as pendentes", "contar tarefas concluídas"). É a tarefa recorrente do projeto: toda nova funcionalidade segue exatamente o mesmo formato de integração em `Program.cs`.

## Contexto que você precisa saber antes

- Todo o código está em `GerenciadorDeTarefas/Program.cs`, com *top-level statements*.
- O modelo de dados é `tarefas`, uma `List<(int Id, string Titulo, bool Concluida)>`. Não existe classe `Tarefa` nem classe de serviço.
- As operações existentes são funções locais: `Adicionar(string titulo)`, `Concluir(int id)` e `Listar()`.
- Abaixo das funções há um fluxo de demonstração (adiciona tarefas, imprime `=== ... ===`, lista, conclui, lista de novo) que termina com `Console.ReadLine()`.

## Passo a passo

1. **Ler o `Program.cs` inteiro** antes de mudar qualquer coisa — confirmar que a estrutura acima continua valendo.
2. **Criar a nova função local** logo abaixo das funções existentes (antes do fluxo de demonstração):
   - Nome em PascalCase, no infinitivo e em português (ex.: `Remover`, `BuscarPorPalavraChave`, `ListarPendentes`).
   - Operar diretamente sobre a lista `tarefas`; para alterar um item, substituir a tupla inteira (como `Concluir` faz), pois tuplas são imutáveis.
   - Reaproveitar o que já existe: se a função precisa exibir tarefas, usar o mesmo formato de `Listar()` (`[ ] #Id — Título` / `[X] #Id — Título`) em vez de inventar outro.
   - Usar LINQ ou laços simples do C# — nada de pacotes externos.
3. **Validar os parâmetros e tratar os casos limite** com mensagem no console, sem lançar exceção:
   - texto vazio ou só espaços;
   - Id inexistente;
   - lista vazia ou nenhum resultado encontrado.
4. **Demonstrar a nova função no fluxo**: adicionar, antes do `Console.ReadLine()` final, um bloco com `Console.WriteLine();`, um cabeçalho `=== ... ===` e a(s) chamada(s) da função — pelo menos um caso válido e um caso inválido/sem resultado.
5. **Rodar `dotnet build`** dentro de `GerenciadorDeTarefas/` e garantir 0 erros e 0 avisos novos.
6. **Rodar `dotnet run`** e conferir a saída do novo bloco (o programa espera um Enter no final).
7. **Explicar em poucas linhas** o que foi adicionado e por quê.

## Padrão de saída esperado

- Mensagens sem emojis, no mesmo tom das existentes, em português.
- Cabeçalhos no formato `=== Descrição ===`.
- Tarefas sempre no formato de `Listar()`.
- O fluxo de demonstração existente continua funcionando igual; a nova parte só é acrescentada ao final.

## O que NÃO fazer

- Não criar classes, arquivos novos ou menu interativo — isso é mudança de arquitetura e precisa de pedido explícito.
- Não alterar o comportamento de `Adicionar`, `Concluir` ou `Listar`, a menos que seja explicitamente pedido.
- Não adicionar bibliotecas externas para algo que dá para fazer com C# puro.
- Não mexer em arquivos fora de `GerenciadorDeTarefas/Program.cs`.

## Exemplo de uso

Prompt: "Usando a skill adicionar-funcionalidade, adicione uma função para buscar tarefas por palavra-chave no título."

Resultado esperado: nova função local `BuscarPorPalavraChave(string palavraChave)` em `Program.cs`, busca sem diferenciar maiúsculas/minúsculas, mensagem para palavra-chave vazia e para nenhum resultado, bloco de demonstração no fluxo, projeto compilando sem erros.
