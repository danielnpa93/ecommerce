-- public."Produtos" definition

-- Drop table

-- DROP TABLE public."Produtos";

CREATE TABLE "Produtos" (
	"Id" uuid NOT NULL,
	"Nome" varchar(250) NOT NULL,
	"Descricao" varchar(500) NOT NULL,
	"Ativo" bool NOT NULL,
	"Valor" numeric NOT NULL,
	"DataCadastro" timestamptz NOT NULL,
	"Imagem" varchar(250) NOT NULL,
	"QuantidadeEstoque" int4 NOT NULL,
	CONSTRAINT "PK_Produtos" PRIMARY KEY ("Id")
);


INSERT INTO "Produtos" ("Id", "Nome", "Descricao", "Ativo", "Valor", "DataCadastro", "Imagem", "QuantidadeEstoque")
VALUES 
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta 4 Head', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/4head.webp', 5),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta 4 Head Branca', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/Branca4head.webp', 5),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta Tiltado', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/tiltado.webp', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta Tiltado Branca', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/BrancoTiltado.webp', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta Heisenberg', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/Heisenberg.webp', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta Kappa', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/Kappa.webp', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta MacGyver', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/MacGyver.webp', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta Maestria', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/Maestria.webp', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta Code Life Preta', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/camiseta2.jpg', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta My Yoda', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/MyYoda.webp', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Camiseta Pato Amarela', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', true, 50.00, '2019-07-19', 'http://example.com/AmarelaPato.webp', 10);


INSERT INTO "Produtos" ("Id", "Nome", "Descricao", "Ativo", "Valor", "DataCadastro", "Imagem", "QuantidadeEstoque")
VALUES 
(uuid_in(md5(random()::text || random()::text)::cstring), 'Smartphone TechPro X', 'Smartphone com processador octa-core, 8GB de RAM e tela Super AMOLED de 6.5 polegadas.', true, 899.99, '2022-01-15', 'http://example.com/techprox.jpg', 20),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Notebook UltraBook 15', 'Notebook ultrafino com processador Intel Core i7, SSD de 512GB e tela Full HD de 15.6 polegadas.', true, 1499.99, '2022-01-16', 'http://example.com/ultrabook15.jpg', 15),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Fone de Ouvido BeatWave Pro', 'Fone de ouvido Bluetooth com cancelamento de ruído ativo, bateria de longa duração e qualidade de som premium.', true, 199.99, '2022-01-17', 'http://example.com/beatwavepro.jpg', 30),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Câmera Fotográfica SnapShot 5000', 'Câmera digital com sensor de 24MP, lente intercambiável e gravação de vídeo em 4K.', true, 699.99, '2022-01-18', 'http://example.com/snapshot5000.jpg', 10),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Console de Videogame NeoGamer', 'Console de videogame de última geração com suporte a jogos em 8K, SSD de alta velocidade e controle ergonômico.', true, 549.99, '2022-01-19', 'http://example.com/neogamer.jpg', 25),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Cafeteira Expresso BaristaMaster', 'Cafeteira automática com moedor integrado, sistema de vapor e opções de preparo personalizáveis.', true, 299.99, '2022-01-20', 'http://example.com/baristamaster.jpg', 15),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Mochila Outdoor Adventure', 'Mochila de alta resistência para aventuras ao ar livre, com compartimentos modulares e sistema de hidratação.', true, 79.99, '2022-01-21', 'http://example.com/adventuremochila.jpg', 40),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Relógio Inteligente FitPro', 'Relógio inteligente com monitoramento de saúde, GPS integrado e resistência à água.', true, 129.99, '2022-01-22', 'http://example.com/fitpro.jpg', 50),
(uuid_in(md5(random()::text || random()::text)::cstring), 'Jogo de Panelas ChefMaster', 'Conjunto de panelas antiaderentes com revestimento cerâmico e cabo ergonômico.', true, 89.99, '2022-01-23', 'http://example.com/chefmaster.jpg', 20);

