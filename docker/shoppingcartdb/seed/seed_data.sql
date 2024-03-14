-- public."CarrinhoCliente" definition

-- Drop table

-- DROP TABLE public."CarrinhoCliente";

CREATE TABLE "CarrinhoCliente" (
	"Id" uuid NOT NULL,
	"ClienteId" uuid NOT NULL,
	"ValorTotal" numeric NOT NULL,
	"VoucherUtilizado" bool NOT NULL,
	"Desconto" numeric NOT NULL,
	"Percentual" numeric NULL,
	"ValorDesconto" numeric NULL,
	"VoucherCodigo" varchar(50) NULL,
	"TipoDesconto" int4 NULL,
	CONSTRAINT "PK_CarrinhoCliente" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_Cliente" ON "CarrinhoCliente" USING btree ("ClienteId");


-- public."CarrinhoItens" definition

-- Drop table

-- DROP TABLE public."CarrinhoItens";

CREATE TABLE "CarrinhoItens" (
	"Id" uuid NOT NULL,
	"ProdutoId" uuid NOT NULL,
	"Nome" varchar(100) NULL,
	"Quantidade" int4 NOT NULL,
	"Valor" numeric NOT NULL,
	"Imagem" varchar(100) NULL,
	"CarrinhoId" uuid NOT NULL,
	CONSTRAINT "PK_CarrinhoItens" PRIMARY KEY ("Id")
);
CREATE INDEX "IX_CarrinhoItens_CarrinhoId" ON "CarrinhoItens" USING btree ("CarrinhoId");


-- public."CarrinhoItens" foreign keys

ALTER TABLE "CarrinhoItens" ADD CONSTRAINT "FK_CarrinhoItens_CarrinhoCliente_CarrinhoId" FOREIGN KEY ("CarrinhoId") REFERENCES "CarrinhoCliente"("Id") ON DELETE CASCADE;