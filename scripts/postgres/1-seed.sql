\connect examdb

INSERT INTO tb_category (name, description)
VALUES ('Pharmacy', 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Proin risus. Praesent lectus.');

INSERT INTO tb_category (name, description)
VALUES ('Health', 'Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.');

INSERT INTO tb_category (name, description)
VALUES ('Grocery', 'Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus.');

INSERT INTO tb_product(name, description, value, brand, category_id)
VALUES('Longos - Greek Salad', 'Phasellus in felis.', 26.63, 'Clean Harbors, Inc.', 3);

INSERT INTO tb_product(name, description, value, brand, category_id)
VALUES('Soup - Knorr, Chicken Noodle', 'Morbi vel lectus in quam fringilla rhoncus.', null, 'Anthem, Inc.', 3);

INSERT INTO tb_product(name, description, value, brand, category_id)
VALUES('Lettuce - Frisee', 'Curabitur at ipsum ac tellus semper interdum.', 15.99, 'Vector Group Ltd.', 2);
