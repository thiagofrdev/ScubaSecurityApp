# ScubaSecurityApp - Sistema de Monitoramento de Mergulho

Este projeto foi desenvolvido como parte da avaliação da Unidade I da disciplina de Complexidade de Algoritmos do curso de Bacharelado em Sistemas de Informação no IFBA - Campus Vitória da Conquista.

**Desenvolvedor:** Thiago Ferreira Ribeiro  
**Professor:** Luis Paulo da Silva Carvalho  
**Data de Entrega:** 29/04/2026

---

## 1. Contexto (Mini-Mundo)
O **ScubaSecurityApp** é uma solução de sensoriamento voltada para a segurança no mergulho autônomo (SCUBA). O sistema realiza a monitoração em tempo real de múltiplos mergulhadores, coletando e processando dados vitais de profundidade e pressão dos cilindros para garantir a integridade do grupo e facilitar operações de resgate.

## 2. Tecnologias Utilizadas
* **Linguagem:** C# (.NET 10.0)
* **Paradigma:** Orientação a Objetos
* **Suporte a Threads:** Sim, conforme exigido pela comanda da atividade para simulação de sensoriamento

### Pré-requisitos para Compilação
Se você optar por compilar o projeto a partir dos arquivos-fonte (pasta ```/exe```) em vez de usar os executáveis pré-compilados, será necessário instalar o ambiente de desenvolvimento:

- **SDK do .NET 10.0**: Necessário para compilar e rodar o código-fonte via CLI.
    - [Download Oficial do .NET](https://dotnet.microsoft.com/pt-br/download)
- **C# Dev Kit (Opcional)**: Recomendado caso vá utilizar o VS Code.
- **Visual Studio 2022 (ou superior - Opcional)**: Caso prefira utilizar a IDE completa.

## 3. Funcionalidades e Análise de Complexidade
O sistema cumpre todos os requisitos de implementação manual, sem o uso de bibliotecas externas ou métodos automatizados (como LINQ ou Sort() nativo) para garantir a transparência da análise algorítmica.

| Funcionalidade | Descrição | Complexidade (Big-O) | Razão/Justificativa |
| :--- | :--- | :--- | :--- |
| **Geração de Dados** | Criação randômica de N mergulhadores com ID, profundidade e pressão. | **O(N)** | Percorre um único laço de repetição para instanciar e preencher os objetos. |
| **Listagem/Impressão** | Exibição formatada das leituras identificadas por objeto. | **O(N)** | Itera sobre a coleção uma única vez para saída no console. |
| **Ordenação (Bubble Sort)** | Organização crescente da lista com base nos valores de pressão (Bar). | **O(N²)** | Utiliza dois laços aninhados para comparação e troca manual de elementos adjacentes. |
| **Análise de Autonomia** | Funcionalidade extra: identifica parceiros de resgate viáveis por proximidade e reserva de ar. | **O(N²)** | Realiza uma análise cruzada (Matriz de Segurança) comparando cada mergulhador com todos os outros. |

## 4. Estrutura do Projeto
* ```/Algorithms:``` Contém os códigos manuais de ordenação e a funcionalidade extra de análise cruzada.
* ```/Models:``` Definição da classe `Mergulhador`.
* ```/Services:``` Motor de geração randômica de dados de sensoriamento.
* ```/exe:``` Pasta contendo os executáveis compilados (Self-Contained) para Windows, Linux e macOS.

## 5. Como Executar
### Via Código-Fonte (Requer .NET 10 SDK)
Na raiz do projeto, onde está o arquivo ```.slnx```, execute:
```bash
dotnet run --project ScubaSecurityApp/ScubaSecurityApp.csproj
```

### Via Executável (Não requer instalação de .NET)
Acesse a pasta ```/exe``` e escolha a subpasta correspondente ao seu sistema operacional:

- **Windows**: Execute ```ScubaSecurityApp.exe```.
- **Linux/macOS**: 
    1. Abra o terminal na pasta.
    2. Conceda permissão: ```chmod +x ScubaSecurityApp```
    3. Execute: ```./ScubaSecurityApp```

---

Este software foi desenvolvido estritamente para fins acadêmicos, visando o estudo da eficácia e eficiência de algoritmos conforme as diretrizes do IFBA.