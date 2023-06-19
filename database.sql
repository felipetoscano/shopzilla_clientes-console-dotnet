CREATE DATABASE DB_CLIENTES;

USE DB_CLIENTES;

DROP TABLE CLIENTES;

CREATE TABLE CLIENTES(
	ID INT IDENTITY(1, 1) PRIMARY KEY,
	NOME VARCHAR(100) NOT NULL,
	EMAIL VARCHAR(50) UNIQUE NOT NULL
);

INSERT INTO CLIENTES (NOME, EMAIL) VALUES ('FELIPE TOSCANO', 'FELIPETOSCANO02@GMAIL.COM');