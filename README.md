# Avaliação Individual — Módulo 11 — Tecnologias Emergentes e IA

**Data de entrega:** DD/MM/AAAA
**Formato:** individual, de consulta aberta — use slides, anotações e a própria IA à vontade para pesquisar e testar suas respostas.

## Como participar

1. Faça um **fork** deste repositório.
2. Clone o seu fork localmente.
3. Responda as questões teóricas **direto neste README**, abaixo de cada uma.
4. Complete a parte prática (veja abaixo) editando `CLAUDE.md`, `.claude/skills/minha-skill/SKILL.md` e `EVIDENCIAS.md`.
5. Abra um **Pull Request** do seu fork de volta para este repositório.

> O PR não será mergeado — ele existe só para eu avaliar o seu diff. Pode deixar aberto depois de enviar.

O objetivo não é decorar definições, e sim demonstrar que você entende os conceitos e sabe aplicá-los para ganhar eficiência ao usar IA no seu projeto de TCC. Responda com suas próprias palavras — copiar e colar resposta pronta de IA sem entender não demonstra o aprendizado esperado.

---

## Questões dissertativas

### Questão 1 — O que é um "agent"?
O que é um "agent" (agente de IA)? Explique com suas próprias palavras e dê um exemplo de situação em que faz mais sentido usar um agente do que um chat comum.

Um *agent* (agente de IA) é um sistema que, além de responder a uma pergunta, decide sozinho **quais ações tomar** para cumprir um objetivo — chamando ferramentas, executando comandos, lendo/escrevendo arquivos, buscando informação na web, e repetindo esse ciclo várias vezes até considerar a tarefa concluída. A diferença central para um chat comum é o **loop de decisão**: num chat, o modelo só gera texto e é o usuário quem executa cada passo manualmente; num agente, o próprio modelo decide o próximo passo, executa, observa o resultado e ajusta o plano, sem que o usuário precise mediar cada ação.
Exemplo: no meu TCC (Sincro), se eu pedir "adicione o endpoint de relatório de pedidos por período, seguindo o padrão dos outros controllers", um chat comum só me devolveria o código para eu copiar e colar. Um agente (como o Claude Code) pode abrir o repositório, ler os controllers existentes para entender o padrão, criar o arquivo, rodar o build, corrigir erros de compilação sozinho e só então me entregar o resultado pronto — faz mais sentido usar o agente aqui porque a tarefa depende de contexto espalhado em vários arquivos do projeto, não só da pergunta em si.

### Questão 2 — O que são guidelines?
O que são "guidelines" (diretrizes) ao usar uma IA generativa? Qual é o papel delas na qualidade das respostas geradas pelo modelo?

Guidelines são instruções de contexto — regras, convenções, preferências e restrições — fornecidas à IA para orientar *como* ela deve se comportar e gerar respostas dentro de um projeto ou situação específica, além do que já está na pergunta pontual. Podem vir do usuário (arquivo tipo `CLAUDE.md`, prompt de sistema) ou ser combinadas ao longo da conversa (ex.: "prefiro respostas curtas", "sempre em português", "não altere arquivos fora do escopo pedido").
O papel delas na qualidade das respostas é direto: sem guidelines, o modelo assume padrões genéricos (que podem não bater com as convenções do projeto, o estilo de código do time, ou o nível de detalhe que o usuário quer). Com guidelines bem escritas, a IA reduz o número de iterações necessárias, evita retrabalho (como gerar código em um padrão diferente do resto do repositório) e produz respostas mais alinhadas ao contexto real — funcionam como uma espécie de "memória de curto prazo declarada" que substitui o que teria que ser reexplicado em cada prompt.


### Questão 4 — Escolha de modelo e nível de esforço
Qual modelo de IA utilizar para cada tipo de tarefa? Dê um exemplo de tarefa simples e outra mais complexa, explicando como você escolheria o modelo em cada caso. O que é o "nível de esforço" (effort level) e quando faz sentido aumentá-lo ou diminuí-lo?

A escolha do modelo geralmente segue a relação entre **complexidade da tarefa** e **custo/velocidade**. Tarefas simples, bem definidas, com resposta previsível (ex.: "formate esse JSON", "escreva um regex para validar CPF", "resuma esse texto de 3 parágrafos") não precisam do modelo mais avançado disponível — um modelo menor e mais rápido resolve com qualidade equivalente e menor custo/latência. Já tarefas complexas, que exigem raciocínio em várias etapas, entendimento de um contexto grande e ambíguo, ou decisões de arquitetura (ex.: "desenhe a estrutura de camadas do backend do meu TCC considerando Clean Architecture, EF Core e os requisitos de tempo real") se beneficiam de um modelo mais capaz, mesmo sendo mais lento/caro, porque o custo de uma resposta errada ali é maior do que a economia de usar um modelo menor.
"Nível de esforço" (effort level) é um parâmetro que controla **quanto o modelo "pensa" antes de responder** — quanto raciocínio interno, exploração de alternativas e verificação ele faz antes de gerar a resposta final. Faz sentido aumentar o esforço em tarefas com muitas variáveis, alto custo de erro, ou que exigem juntar informações de fontes diferentes (ex.: revisar uma decisão de arquitetura, depurar um bug intermitente). Faz sentido diminuir o esforço em tarefas triviais ou repetitivas, onde mais "pensamento" só aumenta o tempo de resposta sem melhorar o resultado — como pedir a sintaxe de um comando ou uma pequena correção de digitação.


### Questão 5 — Como estruturar um bom prompt
Descreva os elementos que tornam um prompt mais eficaz (ex.: contexto, objetivo, formato esperado, exemplos, restrições).

Um prompt eficaz normalmente reúne:

- **Contexto**: o que já existe, em que situação a tarefa se insere (ex.: "este é um projeto ASP.NET Core com Clean Architecture, já tenho os controllers X e Y").
- **Objetivo claro**: o que exatamente deve ser produzido ou respondido, sem ambiguidade.
- **Formato esperado**: como a resposta deve ser entregue — código, texto corrido, lista, tabela, JSON, etc.
- **Exemplos** (quando aplicável): um caso de entrada/saída esperado, ou um trecho de código já existente para servir de padrão.
- **Restrições**: o que não pode ser feito (ex.: "não altere outros arquivos", "não use bibliotecas externas", "mantenha compatibilidade com .NET 8").
- **Critério de sucesso**: como saber se a resposta está correta (ex.: "deve compilar sem warnings", "deve seguir o mesmo padrão de nomenclatura dos outros métodos").

Quanto mais desses elementos estiverem explícitos, menos a IA precisa "adivinhar" a intenção, e menor a chance de retrabalho.


### Questão 6 — Iteração de prompt
O que significa "iterar" um prompt? Por que a primeira resposta de uma IA geralmente não é a versão final, e como você usaria a resposta recebida para melhorar o próximo prompt?

Iterar um prompt significa refinar a pergunta (ou o pedido) com base na resposta recebida, em vez de aceitar a primeira resposta como definitiva. A primeira resposta raramente é a versão final porque, na primeira tentativa, o modelo está trabalhando com o que foi explicitado — e é comum que o pedido inicial esteja incompleto, ambíguo, ou que só depois de ver uma resposta o usuário perceba o que realmente precisava (é mais fácil corrigir algo concreto do que descrever a expectativa perfeitamente de antemão).
Na prática, eu uso a resposta recebida como um diagnóstico: se o resultado está tecnicamente certo mas no padrão errado, o próximo prompt aponta o padrão específico a seguir (ex.: "use o mesmo estilo de tratamento de erro do `PedidoController`"); se a resposta é genérica demais, o próximo prompt adiciona contexto específico do projeto; se algo foi mal interpretado, reformulo a instrução deixando explícito o que antes ficou implícito. Cada iteração reduz o espaço de ambiguidade.


### Questão 7 — Zero-shot vs. few-shot
Qual é a diferença entre um prompt "zero-shot" e um prompt "few-shot"? Dê um exemplo de situação em que vale a pena incluir exemplos dentro do próprio prompt.

Um prompt **zero-shot** pede para a IA realizar uma tarefa sem fornecer nenhum exemplo de como a resposta deve ser — só a instrução e o contexto. Um prompt **few-shot** inclui um ou mais exemplos de entrada/saída dentro do próprio prompt, para que o modelo "aprenda" o padrão esperado por demonstração, não só por descrição.
Vale a pena usar few-shot quando o formato de saída é específico e difícil de descrever só com regras — por exemplo, se eu quero que a IA gere mensagens de commit seguindo um padrão específico do time (`tipo(escopo): descrição curta`), é mais eficaz mostrar 2-3 exemplos reais de commits já usados no projeto do que tentar explicar todas as regras de estilo em texto. O mesmo vale para extrair dados de um formato de NF-e específico: mostrar um exemplo de entrada (XML) e a saída esperada (JSON estruturado) costuma dar resultado mais consistente do que só descrever os campos.


### Questão 8 — Memória e contexto entre sessões
O que significa uma IA "ter memória" entre sessões diferentes de conversa? Por que, em um projeto longo como o TCC, é importante decidir o que precisa ser "lembrado" e como fornecer esse contexto para a IA a cada nova conversa?

Uma IA "ter memória" entre sessões significa que ela consegue reter e reutilizar informações de conversas anteriores, sem que o usuário precise repeti-las do zero a cada nova sessão — diferente do comportamento padrão de um modelo, que normalmente não tem acesso a nada fora da conversa atual, a menos que essa informação seja explicitamente fornecida de novo (via arquivo de contexto, resumo, ou um sistema de memória persistente).
Em um projeto longo como o TCC, isso importa porque o projeto acumula decisões que não estão necessariamente escritas no código: por que optamos por adiar o SignalR e usar polling primeiro, por que a arquitetura ficou dividida em três trilhas verticais, quais nomes de entidades já foram fixados. Se essas decisões não forem "lembradas" (seja por um arquivo tipo `CLAUDE.md`, seja por um resumo que eu forneço no início de cada sessão), cada nova conversa corre o risco de a IA sugerir algo que já foi decidido e descartado, ou desalinhado com o padrão que o time já adotou — gerando retrabalho e inconsistência entre as partes do projeto feitas por pessoas diferentes.


### Questão 9 — Avaliar a resposta da IA
Antes de aplicar a sugestão de uma IA no seu projeto, como você verifica se ela está correta? Descreva pelo menos 2 formas práticas de checar a confiabilidade de uma resposta gerada por IA.

Antes de aplicar uma sugestão da IA no projeto, duas formas práticas que uso para checar confiabilidade:

1. **Rodar e testar de fato**: compilar o código, rodar os testes existentes (ou escrever um teste mínimo para o caso), e verificar o comportamento real — não assumir que "parece certo" é suficiente. Isso pega erros de sintaxe, mas também erros de lógica que só aparecem em tempo de execução.
2. **Comparar com a documentação oficial ou com o padrão já usado no projeto**: por exemplo, se a IA sugere uma forma de configurar autenticação JWT no ASP.NET Core, eu confiro se aquilo bate com a documentação oficial da Microsoft (ou com a forma como já está configurado em outra parte do projeto), porque modelos podem sugerir APIs desatualizadas ou métodos que não existem mais na versão que estou usando.

Além dessas duas, também ajuda pedir para a própria IA explicar o "porquê" da sugestão — se a justificativa não fizer sentido ou for vaga, é sinal de que vale revisar com mais cuidado antes de aplicar.


### Questão 10 — Dividir tarefas complexas em etapas
Por que, em tarefas mais complexas, pode ser melhor dividir o trabalho em um fluxo de etapas (ex.: primeiro classificar/organizar, depois processar, depois revisar) em vez de pedir tudo em um único prompt? Dê um exemplo aplicado a uma tarefa do seu TCC.

Pedir tudo em um único prompt sobrecarrega o modelo com decisões simultâneas demais, aumentando a chance de erro em cada uma delas e dificultando saber, depois, *onde* algo deu errado. Dividir em etapas (classificar/organizar → processar → revisar) permite validar cada parte antes de seguir para a próxima, reduzindo o custo de corrigir um erro (é mais barato corrigir um erro de classificação antes de gerar todo o processamento em cima dela do que descobrir o erro só no final).
Exemplo aplicado ao meu TCC: ao implementar o `RelatoriosController` (parte da minha trilha), em vez de pedir de uma vez "crie o relatório de pedidos por período com filtros, paginação e exportação", divido em etapas — primeiro defino e valido a query/agregação no banco (EF Core) isoladamente, depois construo o endpoint que expõe essa query com os filtros, e só depois adiciono paginação e formatação de saída. Cada etapa é testável separadamente, e se o resultado da agregação estiver errado, eu descubro isso antes de construir a camada de API em cima de um dado incorreto — em vez de ter que refazer tudo de uma vez.


> **Questão 3** (como escrever um bom CLAUDE.md) e a **Questão 11** (prática, evidência de uso real da IA) são respondidas nos próprios arquivos `CLAUDE.md` e `EVIDENCIAS.md` — veja a parte prática abaixo.

---

## Parte prática

1. **Complete o `CLAUDE.md`** na raiz deste repositório — é onde você responde a Questão 3, documentando o projeto para orientar um assistente de IA.
2. **Complete a Skill** em `.claude/skills/minha-skill/SKILL.md`, com instruções reutilizáveis para uma tarefa recorrente do projeto. Renomeie a pasta `minha-skill/` para o nome real da sua skill.
3. **Conecte um assistente de IA ao código local** (Claude Code, GitHub Copilot, Cursor, ou outro de sua escolha) e use-o pelo menos uma vez de verdade, aplicando o `CLAUDE.md` e/ou a Skill que você criou em uma tarefa real do projeto `GerenciadorDeTarefas`.
4. **Complete o `EVIDENCIAS.md`** — é onde você responde a Questão 11, documentando essa experiência (ferramenta usada, prompt exato, o que a IA fez, se seguiu suas instruções).

### O que NÃO fazer

- ❌ Copiar as respostas, o CLAUDE.md ou a Skill de um colega
- ❌ Inventar uma evidência que não aconteceu de verdade
- ❌ Alterar arquivos fora do escopo pedido

## Sobre o projeto de exemplo

Dentro de `GerenciadorDeTarefas/` tem um console app simples em C# — um gerenciador de tarefas fictício — que serve de base para você praticar. Não é necessário adicionar funcionalidades novas ao app; o foco é a configuração e o uso da IA em cima desse código.

Abra `GerenciadorDeTarefas.sln` no Visual Studio, ou rode pelo terminal:

```bash
cd GerenciadorDeTarefas
dotnet run
```

---

## Critérios de avaliação (10 pontos)

| Critério | Pontos |
|---|---|
| Questões dissertativas (conjunto) | 4 |
| `CLAUDE.md` bem estruturado e específico ao projeto (Questão 3) | 2 |
| Skill funcional e realmente reutilizável | 2 |
| `EVIDENCIAS.md` — uso real da IA, seguindo (ou não) o CLAUDE.md/Skill (Questão 11) | 1 |
| Qualidade do Pull Request (descrição clara, organizado, dentro do escopo) | 1 |

## Entrega

Envie o **link do seu Pull Request** pelo Akademos até a data acima.
