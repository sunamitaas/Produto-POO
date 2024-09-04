// Trabalho POO
// Sunamita Santos Nascimento


using trabalhoPoo;
using trabalhoPoo.Models;
using trabalhoPoo.DAO;
using Mysqlx.Crud;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

Conexao.Abrir();

//insert

try
{

    Produto pdto = new Produto
    {
        name_produto = "laranja",
        descricao_produto = "A laranja é o fruto da laranjeira, árvore da família Rutaceae. " +
        "Possui porte médio que pode atingir até 8 metros de altura, tronco de cor castanho " +
        "e copa de formato arredondado",
        precovenda_produto = 6.54m,
        id_categoria_fk = new Categoria { id_categoria = 1 }

    };

    ProdutoDAO prodiDAO = new ProdutoDAO();
    prodiDAO.Insert(pdto);

    Console.WriteLine("Produto inserido com sucesso!");

}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao inserir o produto: {ex.Message}");
}

//delete

try
{
    ProdutoDAO prodDAO = new ProdutoDAO();
    List<Produto> produtos = prodDAO.List();

    if (produtos.Count > 0)
    {
        foreach (var prod in produtos)
        {
            Console.WriteLine($"ID: {prod.id_produto}, Nome: {prod.name_produto}, Preço: {prod.precovenda_produto}");
        }

        Console.Write("Qual é o ID do produto que deseja deletar? ");
        if (int.TryParse(Console.ReadLine(), out int id_produto))
        {
            Produto produto = new Produto { id_produto = id_produto };

            prodDAO.Delete(produto.id_produto);    // erro aqui, colocar no chat (sim chat mas se eu alterar para receber um int, vai dar erro aqui, porque não há conversão aqui.

            Console.WriteLine("Produto deletado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro. Por favor, digitar número válido para o ID.");
        }
        
    }
    else
    {
        Console.WriteLine("Nenhum produto encontrado.");
    }

}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao deletar o produto: {ex.Message}");
}

//list

try
{
    List<Produto> produtoList = new List<Produto>();
    ProdutoDAO produto_DAO = new ProdutoDAO();

    produtoList = produto_DAO.List();

    foreach (Produto p in produtoList)
    {
        Console.WriteLine("Id: " + p.id_produto);
        Console.WriteLine("Nome: " + p.name_produto);
        Console.WriteLine("Descrição: " + p.descricao_produto);
        Console.WriteLine($"Preço de Venda: {p.precovenda_produto.ToString("F2")}");
        Console.WriteLine("Categoria (Id): " + p.id_categoria_fk.id_categoria);
        Console.WriteLine();
    }
}
catch (Exception es)
{
    Console.WriteLine(es.Message);
}

//Update

ProdutoDAO produtoDAO = new ProdutoDAO();

Produto produtoUpdate = new Produto
{
    id_produto = 1,
    name_produto = "Novo Nome do Produto",
    descricao_produto = "Nova descrição do produto",
    precovenda_produto = 8.23m,
    id_categoria_fk = new Categoria { id_categoria = 1 }
};


try
{
    bool updateSuccess = produtoDAO.Update(produtoUpdate);
    if (updateSuccess)
    {
        Console.WriteLine("Produto atualizado com sucesso!");
    }
    else
    {
        Console.WriteLine("Nenhum registro foi atualizado.");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


/* BANCO DE DADOS

create database produto;
use produto;

create table categoria(    
id_categoria int unsigned auto_increment not null,
name_categoria varchar(100),
primary key(id_categoria)
);

alter table categoria 
add column descricao varchar(200);

create table produto(
id_produto int unsigned auto_increment not null,
name_produto varchar(100),
descricao_produto varchar(200),
precovenda_produto decimal(10,2),
id_categoria_fk int unsigned,
primary key (id_produto),
foreign key (id_categoria_fk) references categoria (id_categoria)
);

INSERT INTO categoria (name_categoria, descricao) VALUES
('Laranja', 'Fruta Citrica'),
('Cenoura', 'Legume'),
('Alface', 'Hortaliças');

select *from produto;
select *from categoria;
describe categoria;

*/