

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