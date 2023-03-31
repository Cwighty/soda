SET
    search_path TO public;

DROP SCHEMA IF EXISTS public CASCADE;

CREATE SCHEMA public;

SET
    search_path TO public;

grant usage on schema public to postgres,
anon,
authenticated,
service_role;

--grant all privileges on all tables in schema public to postgres
--anon,
--authenticated,
--service_role;

grant all privileges on all functions in schema public to postgres,
anon,
authenticated,
service_role;

grant all privileges on all sequences in schema public to postgres,
anon,
authenticated,
service_role;

alter default privileges in schema public grant all on tables to postgres,
anon,
authenticated,
service_role;

alter default privileges in schema public grant all on functions to postgres,
anon,
authenticated,
service_role;

alter default privileges in schema public grant all on sequences to postgres,
anon,
authenticated,
service_role;

CREATE TABLE
    customer (
        id uuid references auth.users,
        name VARCHAR(255),
        email VARCHAR(255),
        phone VARCHAR(255),
        primary key (id)
    );

CREATE TABLE 
    size (
        id SERIAL primary key,
        name varchar(15),
        price DECIMAl(10, 2),
        img varchar(250)
    );

CREATE TABLE
    base_type (id SERIAL PRIMARY KEY, name VARCHAR(255));

create table 
    base_type_size (
        base_type_id INT References base_type(id),
        size_id INT References size(id),
        PRIMARY KEY (base_type_id, size_id)
    );

create table
    base (
        id SERIAL primary key,
        name varchar(255),
        description varchar(255),
        price DECIMAL(10, 2),
        type_id INT,
        FOREIGN KEY (type_id) REFERENCES base_type (id)
    );

CREATE TABLE
    product (
        id SERIAL PRIMARY KEY,
        name VARCHAR(255),
        description TEXT,
        special_price DECIMAL(10, 2) null,
        image_url VARCHAR(255),
        base_id int,
        foreign key (base_id) references base (id)
    );

CREATE TABLE
    purchase (
        id SERIAL PRIMARY KEY,
        customer_id uuid,
        created_at TIMESTAMP,
        status VARCHAR(255),
        FOREIGN KEY (customer_id) REFERENCES customer (id)
    );

CREATE TABLE
    purchase_item (
        id SERIAL PRIMARY KEY,
        purchase_id INT,
        product_id INT,
        quantity INT,
        FOREIGN KEY (purchase_id) REFERENCES purchase (id),
        FOREIGN KEY (product_id) REFERENCES product (id)
    );

CREATE TABLE
    category (
        id SERIAL PRIMARY KEY,
        name VARCHAR(255),
        description TEXT,
        image_url VARCHAR(255)
    );

CREATE TABLE
    category_product (
        category_id INT,
        product_id INT,
        PRIMARY KEY (category_id, product_id),
        FOREIGN KEY (category_id) REFERENCES category (id),
        FOREIGN KEY (product_id) REFERENCES product (id)
    );

CREATE TABLE
    addon_type (
        id SERIAL PRIMARY KEY,
        name VARCHAR(255)
    );

CREATE TABLE
    addon (
        id SERIAL PRIMARY KEY,
        name VARCHAR(255),
        price DECIMAL(10, 2),
        addon_type_id INT REFERENCES addon_type(id)
    );

CREATE TABLE
    product_addon (
        product_id INT,
        addon_id INT,
        quantity INT,
        PRIMARY KEY (product_id, addon_id),
        FOREIGN KEY (product_id) REFERENCES product (id),
        FOREIGN KEY (addon_id) REFERENCES addon (id)
    );

CREATE TABLE
    purchase_history (
        id SERIAL PRIMARY KEY,
        purchase_id INT,
        status VARCHAR(255),
        completed_at TIMESTAMP,
        FOREIGN KEY (purchase_id) REFERENCES purchase (id)
    );

INSERT INTO 
    size (name, price, img)
VALUES
    ('Small', 0, 'drink_small.png'),
    ('Medium', 0.50, 'drink_medium.png'),
    ('Large', 1.00, 'drink_large.png');

INSERT INTO
    base_type (name)
VAlUES
    ('Soda'),
    ('Coffee'),
    ('Tea');

INSERT INTO 
    base_type_size (base_type_id, size_id)
VALUES
    (1, 1),
    (1, 2),
    (1, 3);

-- sample data for the base table (assuming it already exists and has been populated)
INSERT INTO
    base (name, description, price, type_id)
VALUES
    (
        'Cola',
        'Classic carbonated soft drink with caffeine',
        1.99,
        1
    ),
    (
        'Lemon-Lime',
        'Citrus-flavored carbonated soft drink',
        1.99,
        1
    ),
    (
        'Ginger Ale',
        'Carbonated soft drink with ginger flavor',
        1.99,
        1
    );

-- sample data for the product table
INSERT INTO
    product (
        base_id,
        name,
        description,
        special_price,
        image_url
    )
VALUES
    (
        1,
        'Classic Cola',
        'A classic carbonated soft drink with caffeine',
        1.99,
        'https://example.com/cola.jpg'
    ),
    (
        2,
        'Lemon-Lime Twist',
        'A citrusy twist on a classic carbonated soft drink',
        2.49,
        'https://example.com/lemon-lime.jpg'
    ),
    (
        3,
        'Ginger Ale Splash',
        'A refreshing ginger-flavored carbonated soft drink',
        2.99,
        'https://example.com/ginger-ale.jpg'
    );

-- sample data for the addon_type table
INSERT INTO
    addon_type (name)
VALUES
    ('Syrup'),
    ('Coffee'),
    ('Tea'),
    ('Milk'),
    ('Ice');

-- sample data for the addon table
INSERT INTO
    addon (name, price, addon_type_id)
VALUES
    ('Whipped Cream', 0.50, 5),
    ('Caramel Drizzle', 0.75, 2),
    ('Chocolate Syrup', 0.75, 2),
    ('Vanilla Syrup', 0.50, 2),
    ('Strawberry Syrup', 0.75, 2);

-- sample data for the product_addon table
INSERT INTO
    product_addon (product_id, addon_id, quantity)
VALUES
    (1, 1, 1),
    (1, 2, 1),
    (2, 1, 1),
    (2, 3, 1),
    (3, 2, 2),
    (3, 4, 1),
    (3, 5, 1);

-- Insert sample data into the order table
--INSERT INTO
--    purchase (customer_id, created_at, status)
--VALUES
--    (1, '2023-03-10 12:30:00', 'Processing'),
--    (2, '2023-03-10 13:30:00', 'Pending Payment');

-- Insert sample data into the purchase_item table
--INSERT INTO
--    purchase_item (purchase_id, product_id, quantity)
--VALUES
--    (1, 1, 2),
--    (1, 2, 1),
--    (2, 3, 3);

-- Insert sample data into the special table
INSERT INTO
    category (name, description, image_url)
VALUES
    (
        'Summer Special',
        'Beat the heat with our refreshing summer drinks!',
        'https://example.com/summer-special.jpg'
    ),
    (
        'Winter Special',
        'Warm up with our cozy winter drinks!',
        'https://example.com/winter-special.jpg'
    ),
    ('Popular', 'The most popular!', NULL);

-- Insert sample data into the special_product table
INSERT INTO
    category_product (category_id, product_id)
VALUES
    (1, 2),
    (1, 3),
    (2, 1),
    (2, 3),
    (3, 1),
    (3, 2);





-- POLICIES
ALTER TABLE customer ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Enable all access for customer owned tables" ON "public"."customer"
AS PERMISSIVE FOR ALL
TO public
USING (auth.uid() = id)
WITH CHECK (auth.uid() = id);





-- TRIGGERS
--CREATE or replace function delete_user()
--	returns void
--LANGUAGE SQL SECURITY DEFINER
--AS $$
--	--delete from public.profiles where id = auth.uid();
--	delete from auth.users where id = auth.uid();
--$$;
--
--CREATE TRIGGER delete_user_trigger
--AFTER DELETE ON customer
--FOR EACH ROW
--EXECUTE FUNCTION delete_user();