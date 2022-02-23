\connect examdb

CREATE TABLE tb_category
(
    id           serial        PRIMARY KEY,
    name         VARCHAR(80)   NOT NULL,
    description  VARCHAR(255)  NOT NULL
);

ALTER TABLE "tb_category" OWNER TO devuser;
