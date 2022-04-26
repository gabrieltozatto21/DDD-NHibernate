

/*
--------------------------------------
--Autor: Gabriel de Abreu Tozatto
--Data de Criacao: 07/04/2022
--Schema: nhibernatedb
--------------------------------------
*/

CREATE TABLE despesa (
    Id int NOT NULL AUTO_INCREMENT,
    Descricao varchar(255),
    Tipo varchar(255),
    NumPagamentos int,
    ValorTotal double NOT NULL,
    PRIMARY KEY (Id)
);

CREATE TABLE usuarioacesso (
    Id int NOT NULL AUTO_INCREMENT,
    Nome varchar(255) NOT NULL,
    Email varchar(255) NOT NULL,
    Senha varchar(255) NOT NULL,
    Login varchar(255) NOT NULL,
    DataCadastro DateTime,
    PRIMARY KEY (Id)
);

