SET search_path TO public;
DROP SCHEMA IF EXISTS public CASCADE;
CREATE SCHEMA public;
SET search_path TO public;

grant usage on schema public to postgres, anon, authenticated, service_role;

grant all privileges on all tables in schema public to postgres, anon, authenticated, service_role;
grant all privileges on all functions in schema public to postgres, anon, authenticated, service_role;
grant all privileges on all sequences in schema public to postgres, anon, authenticated, service_role;

alter default privileges in schema public grant all on tables to postgres, anon, authenticated, service_role;
alter default privileges in schema public grant all on functions to postgres, anon, authenticated, service_role;
alter default privileges in schema public grant all on sequences to postgres, anon, authenticated, service_role;

CREATE TABLE customer (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    email VARCHAR(255)
);

create table base (
	id int primary key,
	name varchar(255),
	description varchar(255),
	price DECIMAL(10, 2)
);

CREATE TABLE product (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    description TEXT,
    special_price DECIMAL(10, 2) null,
    image_url VARCHAR(255),
    base_id int,
    foreign key (base_id) references base(id)
);


CREATE TABLE purchase (
    id INT PRIMARY KEY,
    customer_id INT,
    created_at TIMESTAMP,
    status VARCHAR(255),
    FOREIGN KEY (customer_id) REFERENCES customer(id)
);

CREATE TABLE purchase_item (
    id INT PRIMARY KEY,
    purchase_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (purchase_id) REFERENCES purchase(id),
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

CREATE TABLE addon (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    price DECIMAL(10, 2)
);

CREATE TABLE product_addon (
    id INT PRIMARY KEY,
    product_id INT,
    addon_id INT,
    quantity INT,
    FOREIGN KEY (product_id) REFERENCES product(id),
    FOREIGN KEY (addon_id) REFERENCES addon(id)
);

CREATE TABLE purchase_history (
    id INT PRIMARY KEY,
    purchase_id INT,
    status VARCHAR(255),
    completed_at TIMESTAMP,
    FOREIGN KEY (purchase_id) REFERENCES purchase(id)
);

-- Insert sample data into the customer table
INSERT INTO customer (id, name, email) VALUES
(1, 'John Doe', 'johndoe@example.com'),
(2, 'Jane Smith', 'janesmith@example.com'),
(3, 'Bob Johnson', 'bobjohnson@example.com');


-- sample data for the base table (assuming it already exists and has been populated)
INSERT INTO base (id, name, description, price) VALUES
(1, 'Cola', 'Classic carbonated soft drink with caffeine', 1.99),
(2, 'Lemon-Lime', 'Citrus-flavored carbonated soft drink', 1.99),
(3, 'Ginger Ale', 'Carbonated soft drink with ginger flavor', 1.99);

-- sample data for the product table
INSERT INTO product (id, base_id, name, description, special_price, image_url) VALUES
(1, 1, 'Classic Cola', 'A classic carbonated soft drink with caffeine', 1.99, 'https://example.com/cola.jpg'),
(2, 2, 'Lemon-Lime Twist', 'A citrusy twist on a classic carbonated soft drink', 2.49, 'https://example.com/lemon-lime.jpg'),
(3, 3, 'Ginger Ale Splash', 'A refreshing ginger-flavored carbonated soft drink', 2.99, 'https://example.com/ginger-ale.jpg');

-- sample data for the addon table
INSERT INTO addon (id, name, price) VALUES
(1, 'Whipped Cream', 0.50),
(2, 'Caramel Drizzle', 0.75),
(3, 'Chocolate Syrup', 0.75),
(4, 'Vanilla Syrup', 0.50),
(5, 'Strawberry Syrup', 0.75);

-- sample data for the product_addon table
INSERT INTO product_addon (id, product_id, addon_id, quantity) VALUES
(1, 1, 1, 1),
(2, 1, 2, 1),
(3, 2, 1, 1),
(4, 2, 3, 1),
(5, 3, 2, 2),
(6, 3, 4, 1),
(7, 3, 5, 1);

-- Insert sample data into the order table
INSERT INTO purchase (id, customer_id, created_at, status) VALUES
(1, 1, '2023-03-10 12:30:00', 'Processing'),
(2, 2, '2023-03-10 13:30:00', 'Pending Payment');

-- Insert sample data into the purchase_item table
INSERT INTO purchase_item (id, purchase_id, product_id, quantity) VALUES
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