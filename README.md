# ScubaSecurityApp - Sistema de Monitoramento de Mergulho

Este projeto foi desenvolvido como parte da avaliação da Unidade I da disciplina de Complexidade de Algoritmos do curso de Bacharelado em Sistemas de Informação no IFBA - Campus Vitória da Conquista.

**Desenvolvedor:** Thiago Ferreira Ribeiro  
**Professor:** Luis Paulo da Silva Carvalho  
**Data de Entrega:** 08/06/2026

---

## 1. Contexto (Mini-Mundo)
O **ScubaSecurityApp** é uma solução de sensoriamento voltada para a segurança no mergulho autônomo (SCUBA). O sistema realiza a monitoração em tempo real de múltiplos mergulhadores, coletando e processando dados vitais de profundidade e pressão dos cilindros para garantir a integridade do grupo e facilitar operações de resgate.

## 2. Tecnologias Utilizadas
* **Linguagem:** C# (.NET 9.0)
* **Paradigma:** Orientação a Objetos e Arquitetura Cliente-Servidor (TCP/IP)
* **Concorrência:** Uso intensivo de *Threads* no Cliente para simulação paralela de sensores reais.

### Pré-requisitos para Compilação
Se você optar por compilar o projeto a partir dos arquivos-fonte (pasta ```/Executaveis```) em vez de usar os executáveis pré-compilados, será necessário instalar o ambiente de desenvolvimento:

- **SDK do .NET 9.0**: Necessário para compilar e rodar o código-fonte via CLI.
    - [Download Oficial do .NET](https://dotnet.microsoft.com/pt-br/download)
- **C# Dev Kit (Opcional)**: Recomendado caso vá utilizar o VS Code.
- **Visual Studio 2022 (ou superior - Opcional)**: Caso prefira utilizar a IDE completa.

## 3. Funcionalidades e Análise de Complexidade
O sistema cumpre todos os requisitos de implementação manual, sem o uso de bibliotecas externas prontas para ordenação ou criptografia.

| Funcionalidade | Descrição | Complexidade (Big-O) | Justificativa |
| :--- | :--- | :--- | :--- |
| **Geração/Envio de Dados (Cliente)** | Threads paralelas simulando sensores e disparando dados contínuos. | **O(1)** | Operações de tempo constante dentro de um loop de rede assíncrono. |
| **Leitura de Rede (Servidor)** | Processamento do fluxo de dados recebido de cada conexão TCP. | **O(M)** | Lê linearmente as $M$ mensagens enviadas pelo *NetworkStream*. |
| **Ordenação (Bubble Sort)** | Organização crescente da lista global com base na pressão (Bar). | **O(N²)** | Utiliza dois laços aninhados para comparação e troca de elementos. |
| **Análise de Autonomia Cruzada** | Identifica parceiros de resgate viáveis por proximidade e reserva de ar. | **O(N²)** | Matriz que compara o estado de cada mergulhador com todos os outros. |

## 4. Estrutura do Projeto
A arquitetura foi dividida para cumprir as exigências de sistemas distribuídos:
* `ScubaSecurityClient/`: Aplicação responsável por disparar 10 Threads (simulando 10 mergulhadores) e enviar os dados dos sensores.
* `ScubaSecurityServer/`: Aplicação que atua como o painel do barco, recebendo conexões na porta 8080 e processando a carga algorítmica pesada.
* `Executaveis/`: Pasta (gerada externamente) contendo os binários *Self-Contained* compilados para Windows e Linux.

## 5. Como Executar

### Via Código-Fonte (Requer .NET 9.0 SDK)
Abra dois terminais na raiz da pasta `Versao1` ou `Versao2`.

**1. Inicie o Servidor (Painel do Barco):**
```bash
cd ScubaSecurityServer
dotnet run
```

**2. Inicie os Clientes (Sensores dos Mergulhadores):**
```bash
cd ScubaSecurityClient
dotnet run
```

### Via Executável Binário (Não requer instalação de .NET)
Acesse a pasta raiz `Executaveis/Versao1/` (ou `Versao2`) e navegue pelas subpastas correspondentes ao seu sistema operacional. 

**⚠️ Importante:** Você deve iniciar o Servidor primeiro e, em seguida, o Cliente.

**No Windows:** 
1. Acesse a pasta `Servidor/win-x64` e dê um duplo clique em `ScubaSecurityServer.exe`.
2. Acesse a pasta `Cliente/win-x64` e dê um duplo clique em `ScubaSecurityClient.exe`.

**No Linux:** 
1. Abra um terminal na pasta `Servidor/linux-x64` (ou `osx-x64` para Mac).
2. Conceda permissão de execução: `chmod +x ScubaSecurityServer`
3. Execute o servidor: `./ScubaSecurityServer`
4. Abra um **segundo terminal** na pasta `Cliente/linux-x64` (ou `osx-x64` para Mac).
5. Conceda permissão de execução: `chmod +x ScubaSecurityClient`
6. Execute o cliente: `./ScubaSecurityClient`

---

Este software foi desenvolvido estritamente para fins acadêmicos, visando o estudo da eficácia e eficiência de algoritmos conforme as diretrizes do IFBA.