-- my_sequence definition

-- DROP SEQUENCE my_sequence;

CREATE SEQUENCE my_sequence
	INCREMENT BY 1
	MINVALUE 1
	MAXVALUE 2147483647
	START 1000
	CACHE 1
	NO CYCLE;

CREATE TABLE "PedidoItems" (
	"Id" uuid NOT NULL,
	"PedidoId" uuid NOT NULL,
	"ProdutoId" uuid NOT NULL,
	"ProdutoNome" varchar(250) NOT NULL,
	"Quantidade" int4 NOT NULL,
	"ValorUnitario" numeric NOT NULL,
	"ProdutoImagem" varchar(100) NULL,
	CONSTRAINT "PK_PedidoItems" PRIMARY KEY ("Id")
);
CREATE INDEX "IX_PedidoItems_PedidoId" ON "PedidoItems" USING btree ("PedidoId");


-- "Pedidos" definition

-- Drop table

-- DROP TABLE "Pedidos";

CREATE TABLE "Pedidos" (
	"Id" uuid NOT NULL,
	"Codigo" int4 DEFAULT nextval('my_sequence'::regclass) NOT NULL,
	"ClienteId" uuid NOT NULL,
	"VoucherId" uuid NULL,
	"VoucherUtilizado" bool NOT NULL,
	"Desconto" numeric NOT NULL,
	"ValorTotal" numeric NOT NULL,
	"DataCadastro" timestamptz NOT NULL,
	"PedidoStatus" int4 NOT NULL,
	"Logradouro" varchar(100) NULL,
	"Numero" varchar(100) NULL,
	"Complemento" varchar(100) NULL,
	"Bairro" varchar(100) NULL,
	"Cep" varchar(100) NULL,
	"Cidade" varchar(100) NULL,
	"Estado" varchar(100) NULL,
	CONSTRAINT "PK_Pedidos" PRIMARY KEY ("Id")
);
CREATE INDEX "IX_Pedidos_VoucherId" ON "Pedidos" USING btree ("VoucherId");



-- "Vouchers" definition

-- Drop table

-- DROP TABLE "Vouchers";

CREATE TABLE "Vouchers" (
	"Id" uuid NOT NULL,
	"Codigo" varchar(100) NOT NULL,
	"Percentual" numeric NULL,
	"ValorDesconto" numeric NULL,
	"Quantidade" int4 NOT NULL,
	"TipoDesconto" int4 NOT NULL,
	"DataCriacao" timestamptz NOT NULL,
	"DataUtilizacao" timestamptz NULL,
	"DataValidade" timestamptz NOT NULL,
	"Ativo" bool NOT NULL,
	"Utilizado" bool NOT NULL,
	CONSTRAINT "PK_Vouchers" PRIMARY KEY ("Id")
);

-- "Pedidos" foreign keys

ALTER TABLE "Pedidos" ADD CONSTRAINT "FK_Pedidos_Vouchers_VoucherId" FOREIGN KEY ("VoucherId") REFERENCES "Vouchers"("Id");

-- "PedidoItems" foreign keys

ALTER TABLE "PedidoItems" ADD CONSTRAINT "FK_PedidoItems_Pedidos_PedidoId" FOREIGN KEY ("PedidoId") REFERENCES "Pedidos"("Id");