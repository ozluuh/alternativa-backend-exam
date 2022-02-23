\connect examdb

CREATE TABLE tb_category
(
    id           serial        PRIMARY KEY,
    name         VARCHAR(80)   NOT NULL,
    description  VARCHAR(255)  NOT NULL
);

ALTER TABLE "tb_category" OWNER TO devuser;

INSERT INTO tb_category (name, description)
VALUES ('Quiche Assorted', 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Proin risus. Praesent lectus.');

INSERT INTO tb_category (name, description)
VALUES ('Lid - High Heat, Super Clear', 'Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.');

INSERT INTO tb_category (name, description)
VALUES ('Beer - Steamwhistle', 'Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus.');

INSERT INTO tb_category (name, description)
VALUES ('Cheese - Brick With Pepper', 'Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.');

INSERT INTO tb_category (name, description)
VALUES ('Trueblue - Blueberry 12x473ml', 'Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.');

INSERT INTO tb_category (name, description)
VALUES ('Flour - Corn, Fine', 'Phasellus in felis. Donec semper sapien a libero. Nam dui.');

INSERT INTO tb_category (name, description)
VALUES ('Squash - Pattypan, Yellow', 'In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.');

INSERT INTO tb_category (name, description)
VALUES ('Garlic - Peeled', 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.');

INSERT INTO tb_category (name, description)
VALUES ('Milk - 2%', 'Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.');

INSERT INTO tb_category (name, description)
VALUES ('Strawberries', 'Morbi non lectus. Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis.');

INSERT INTO tb_category (name, description)
VALUES ('Bols Melon Liqueur', 'Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.');

INSERT INTO tb_category (name, description)
VALUES ('Wine - Niagara,vqa Reisling', 'Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.');

INSERT INTO tb_category (name, description)
VALUES ('Water Chestnut - Canned', 'Proin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl.');

INSERT INTO tb_category (name, description)
VALUES ('Milk - Chocolate 250 Ml', 'Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.');

INSERT INTO tb_category (name, description)
VALUES ('Fish - Scallops, Cold Smoked', 'Pellentesque at nulla. Suspendisse potenti. Cras in purus eu magna vulputate luctus.');
