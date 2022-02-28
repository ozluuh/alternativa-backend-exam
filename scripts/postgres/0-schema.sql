\connect examdb

DROP TABLE IF EXISTS tb_product;

DROP TABLE IF EXISTS tb_category;

CREATE TABLE tb_category
(
    id           serial        PRIMARY KEY,
    name         VARCHAR(80)   NOT NULL,
    description  VARCHAR(255)  NOT NULL
);

ALTER TABLE "tb_category" OWNER TO devuser;

CREATE TABLE tb_product
(
    id           serial        PRIMARY KEY,
    name         VARCHAR(80)   NOT NULL,
    description  TEXT          NOT NULL,
    value        NUMERIC(7,2)  ,
    brand        VARCHAR(60)   ,
    category_id  INT           NOT NULL
);

ALTER TABLE "tb_product" OWNER TO devuser;

ALTER TABLE "tb_product" ADD CONSTRAINT fk_category
FOREIGN KEY (category_id) REFERENCES tb_category(id);
