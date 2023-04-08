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
        id uuid references auth.users On delete CASCADE,
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
        customer_id uuid null,
        created_at TIMESTAMP,
        completed_at TIMESTAMP,
        price_paid DECIMAL(10, 2),
        status VARCHAR(255)
    );

CREATE TABLE
    purchase_item (
        id SERIAL PRIMARY KEY,
        purchase_id INT,
        product_id INT null,
        base_id INT,
        size_id INT,
        FOREIGN KEY (purchase_id) REFERENCES purchase (id),
        FOREIGN KEY (product_id) REFERENCES product (id),
        FOREIGN KEY (base_id) REFERENCES base (id),
        foreign key (size_id) references size(id)
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
    purchase_item_addon (
        purchase_item_id INT,
        addon_id INT,
        PRIMARY KEY (purchase_item_id, addon_id),
        FOREIGN KEY (purchase_item_id) REFERENCES purchase_item (id),
        FOREIGN KEY (addon_id) REFERENCES addon (id)
    );

CREATE TABLE
    product_addon (
        product_id INT,
        addon_id INT,
        PRIMARY KEY (product_id, addon_id),
        FOREIGN KEY (product_id) REFERENCES product (id),
        FOREIGN KEY (addon_id) REFERENCES addon (id)
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
    product_addon (product_id, addon_id)
VALUES
    (1, 1),
    (1, 2),
    (2, 1),
    (2, 3),
    (3, 2),
    (3, 4),
    (3, 5);

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

ALTER TABLE purchase ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Enable read access for customer owned records" ON "public"."purchase"
FOR SELECT
TO public
USING (auth.uid() = customer_id);

--ALTER TABLE purchase ENABLE ROW LEVEL SECURITY;
--
--CREATE POLICY "Allow access for authenticated user for purchase table" ON "public"."purchase"
--AS PERMISSIVE FOR SELECT
--TO public
--USING ((auth.uid() = customer_id));





-- TRIGGERS
create function public.handle_new_user() 
returns trigger as $$
begin
  insert into public.customer (id, name, email)
  values (new.id, split_part(new.email, '@', '1'), new.email);
  return new;
end;
$$ language plpgsql security definer;
create trigger on_auth_user_created
  after insert on auth.users
  for each row execute procedure public.handle_new_user();