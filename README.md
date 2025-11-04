# 🚀 GridCore Manager: Sistema CRUD WPF com Arquitetura em Camadas

## Visão Geral e Objetivo

O **GridCore Manager** é uma aplicação desktop (WPF) desenvolvida em C# que implementa um sistema de **Gerenciamento de Produtos e Categorias**. O objetivo central deste projeto é demonstrar a aplicação correta dos princípios de **Programação Orientada a Objetos (POO)** e a utilização de uma arquitetura de software de **cinco camadas (MVC + Service Layer + DAO)**.

---

## 🏛️ 1. Organização e Separação das Camadas

A arquitetura do projeto garante o **baixo acoplamento** e a **separação de responsabilidades**, um princípio fundamental de engenharia de software.

| Camada | Pasta | Responsabilidade Principal | Item de Avaliação |
| :--- | :--- | :--- | :--- |
| **Views / UI** | `Views` | Interface Gráfica (XAML) e Eventos de Clique. | ✔️ |
| **Controllers** | `Controllers` | Gerenciamento do Fluxo e Repasse de Requisições (Livre de Lógica de Negócio). | ✔️ |
| **Service Layer** | `Services` | **Lógica de Negócio**, Validação de Regras e Transações. | ✔️ |
| **DAO** | `DAO` | Acesso e Persistência de Dados (SQL e `MySqlConnection`). | ✔️ |
| **Model** | `Models` | Estrutura de Entidades (`Produto`, `Categoria`, `Usuario`). | ✔️ |

### Exemplo de Fluxo (Controller e Service)

O `ProdutoController` apenas coordena, garantindo que a lógica de validação fique no Service:

```csharp
// No ProdutoController (NÃO há lógica de negócio):
public void SalvarProduto(Produto produto)
{
    // Ação: Delega a responsabilidade de Salvar para a camada Service.
    _produtoService.Salvar(produto); 
    // O Controller não sabe nem se é INSERT/UPDATE ou se o Nome é válido.
}

// No CategoriaServiceDAO (A LÓGICA DE NEGÓCIO reside aqui):
public void Salvar(Categoria categoria)
{
    // Regra de Negócio: Impede que o nome seja vazio.
    if (string.IsNullOrWhiteSpace(categoria.Nome))
    {
        throw new System.Exception("O nome da categoria não pode ser vazio.");
    }
    _categoriaDAO.Salvar(categoria); 
}

💾 2. Persistência e Scripts SQL Corretos
O sistema utiliza MySQL para persistência. O script de criação de tabelas garante a integridade dos dados e estabelece as relações necessárias.

Scripts SQL (Tabelas e Relacionamentos)
SQL

-- CRIAÇÃO DAS TABELAS (Categoria, Produto, Usuario)
CREATE TABLE Categoria (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL UNIQUE
);
CREATE TABLE Produto (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    IdCategoria INT NOT NULL,
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(Id)
        -- Restrição de Integridade: Impede DELETE de Categoria com produtos associados.
        ON DELETE RESTRICT 
);
CREATE TABLE Usuario (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL UNIQUE, 
    PasswordHash VARCHAR(255) NOT NULL
    -- Tabela utilizada para o recurso de Login e Segurança.
);
🤝 3. Service Layer e Uso de Interface para Backends
O Service Layer é construído sobre Interfaces, cumprindo o requisito de facilidade para troca de backend.

Contrato: Interfaces como IProdutoService definem o contrato de serviço.

Abstração: O Controller depende apenas deste contrato (IProdutoService).

Benefício: Isso permite criar implementações alternativas (ex: ProdutoServiceInMemory ou ProdutoServicePostgreSQL) sem modificar as camadas Controller ou View.

🎨 4. Funcionamento da Interface (Operações Básicas e Busca)
A interface (MainWindow.xaml - SistemadeCadastroProduto.PNG) demonstra a operação integrada.

Atualização Dinâmica: Após qualquer operação de escrita (Salvar ou Deletar), a DataGrid é recarregada imediatamente com os dados mais recentes do MySQL.

Busca por Filtro: O campo de busca utiliza a lógica do SQL LIKE implementada no DAO, permitindo a filtragem de produtos por nome em tempo real.

🔒 5. Implementação e Documentação do Recurso Novo: Login
O recurso novo implementado é o Sistema de Login e Controle de Acesso.

Segurança: O sistema inicia na LoginWindow (SistemadeCadastroLogin.PNG) e exige autenticação contra a tabela Usuario.

Gerenciamento de Usuários: A tela de Cadastro de Novo Usuário (SistemaCadastroNovoUsuario.PNG) permite a expansão da base de usuários, utilizando o fluxo completo Service/DAO.

✅ 6. Qualidade do Código (Boas Práticas e Padrão)
O código adere às boas práticas de engenharia de software:

Injeção de Dependência (DI): Classes (Controller, Service) recebem suas dependências via construtor, eliminando a criação manual de objetos (new).

Segurança (SQL Parametrizado): Todas as consultas de escrita e busca que recebem dados do usuário (Salvar, Deletar, BuscarPorId) utilizam cmd.Parameters.AddWithValue(). Isto é essencial para prevenir ataques de SQL Injection.

Gerenciamento de Recursos: O bloco using é utilizado em todas as operações de banco de dados (MySqlConnection, MySqlCommand), garantindo o fechamento e liberação (dispose) automático dos recursos.
