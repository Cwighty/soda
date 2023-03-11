SET search_path TO public;
DROP SCHEMA IF EXISTS soda CASCADE;
CREATE SCHEMA soda;
SET search_path TO soda;

CREATE TABLE customer (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255)
);

CREATE TABLE product (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    description TEXT,
    price DECIMAL(10, 2),
    image_url VARCHAR(255)
);


CREATE TABLE purchase (
    id INT PRIMARY KEY,
    customer_id INT,
    purchase_id INT,
    created_at TIMESTAMP,
    status VARCHAR(255),
    FOREIGN KEY (customer_id) REFERENCES customer(id),
    FOREIGN KEY (purchase_id) REFERENCES purchase(id)
);

CREATE TABLE purchase_item (
    id INT PRIMARY KEY,
    order_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (order_id) REFERENCES purchase(id),
    FOREIGN KEY (product_id) REFERENCES product(id)
   );

CREATE TABLE category (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    description TEXT,
    image_url VARCHAR(255)
);

CREATE TABLE category_product (
    id INT PRIMARY KEY,
    category_id INT,
    product_id INT,
    FOREIGN KEY (category_id) REFERENCES category(id),
    FOREIGN KEY (product_id) REFERENCES product(id)
);

CREATE TABLE ingredient (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    price DECIMAL(10, 2)
);

CREATE TABLE recipe_ingredient (
    id INT PRIMARY KEY,
    product_id INT,
    ingredient_id INT,
    quantity INT,
    FOREIGN KEY (product_id) REFERENCES product(id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient(id)
);

CREATE TABLE purchase_history (
    id INT PRIMARY KEY,
    purchase_id INT,
    status VARCHAR(255),
    completed_at TIMESTAMP,
    FOREIGN KEY (purchase_id) REFERENCES purchase(id)
);

-- Insert sample data into the customer table
INSERT INTO customer (id, name, email, password) VALUES
(1, 'John Doe', 'johndoe@example.com', 'password123'),
(2, 'Jane Smith', 'janesmith@example.com', 'password456'),
(3, 'Bob Johnson', 'bobjohnson@example.com', 'password789');

-- Insert sample data into the product table
INSERT INTO product (id, name, description, price, image_url) VALUES
(1, 'Classic Cola', 'A refreshing classic cola', 2.50, 'https://example.com/classic-cola.jpg'),
(2, 'Orange Fizz', 'An orange-flavored soda with a fizzy kick', 3.00, 'https://example.com/orange-fizz.jpg'),
(3, 'Lemon Lime Twist', 'A tangy lemon-lime soda with a twist', 2.75, 'https://example.com/lemon-lime-twist.jpg');

-- Insert sample data into the order table
INSERT INTO purchase (id, customer_id, purchase_id, created_at, status) VALUES
(1, 1, 1, '2023-03-10 12:30:00', 'Processing'),
(2, 2, 2, '2023-03-10 13:30:00', 'Pending Payment');

-- Insert sample data into the purchase_item table
INSERT INTO purchase_item (id, order_id, product_id, quantity) VALUES
(1, 1, 1, 2),
(2, 1, 2, 1),
(3, 2, 3, 3);

-- Insert sample data into the special table
INSERT INTO category (id, name, description, image_url) VALUES
(1, 'Summer Special', 'Beat the heat with our refreshing summer drinks!', 'https://example.com/summer-special.jpg'),
(2, 'Winter Special', 'Warm up with our cozy winter drinks!', 'https://example.com/winter-special.jpg');

-- Insert sample data into the special_product table
INSERT INTO category_product (id, category_id, product_id) VALUES
(1, 1, 2),
(2, 1, 3),
(3, 2, 1),
(4, 2, 3);

-- Insert sample data into the ingredient table
INSERT INTO ingredient (id, name, price) VALUES
(1, 'Cola syrup', .20),
(2, 'Orange flavoring', .25),
(3, 'Lemon juice', .20),
(4, 'Lime juice', .05),
(5, 'Carbonated water', .7),
(6, 'Whipped cream', .28);
