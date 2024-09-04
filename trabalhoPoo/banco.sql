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
