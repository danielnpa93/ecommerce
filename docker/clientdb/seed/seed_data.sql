CREATE TABLE "Clientes" (
	"Id" uuid NOT NULL,
	"Nome" varchar(200) NOT NULL,
	"Email" varchar(254) NULL,
	"Cpf" varchar(11) NULL,
	"Excluido" bool NOT NULL,
	CONSTRAINT "PK_Clientes" PRIMARY KEY ("Id")
);


CREATE TABLE "Enderecos" (
	"Id" uuid NOT NULL,
	"Logradouro" varchar(200) NOT NULL,
	"Numero" varchar(50) NOT NULL,
	"Complemento" varchar(250) NULL,
	"Bairro" varchar(100) NOT NULL,
	"Cep" varchar(20) NOT NULL,
	"Cidade" varchar(100) NOT NULL,
	"Estado" varchar(50) NOT NULL,
	"ClienteId" uuid NOT NULL,
	CONSTRAINT "PK_Enderecos" PRIMARY KEY ("Id")
);


CREATE UNIQUE INDEX "IX_Enderecos_ClienteId" ON "Enderecos" USING btree ("ClienteId");


-- public."Enderecos" foreign keys

ALTER TABLE "Enderecos" ADD CONSTRAINT "FK_Enderecos_Clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES "Clientes"("Id");