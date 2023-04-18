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

ALTER TABLE customer ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Enable all access for customer owned tables" ON "public"."customer"
AS PERMISSIVE FOR ALL
TO public
USING (auth.uid() = id)
WITH CHECK (auth.uid() = id);


CREATE TABLE 
    size (
        id SERIAL primary key,
        name varchar(15),
        price DECIMAl(10, 2)
    );

CREATE TABLE
    base_type (
    id SERIAL PRIMARY KEY, 
    name VARCHAR(255)
    );

create table 
    base_type_size (
        base_type_id INT References base_type(id) On delete CASCADE,
        size_id INT References size(id) On delete CASCADE,
        PRIMARY KEY (base_type_id, size_id)
    );

create table
    base (
        id SERIAL primary key,
        name varchar(255),
        description varchar(255),
        price DECIMAL(10, 2),
        type_id INT,
        FOREIGN KEY (type_id) REFERENCES base_type (id) On delete CASCADE
    );

CREATE TABLE
    product (
        id SERIAL PRIMARY KEY,
        name VARCHAR(255),
        description TEXT,
        special_price DECIMAL(10, 2) null,
        image_url VARCHAR(255),
        base_id int,
        foreign key (base_id) references base (id) On delete CASCADE
    );

CREATE TABLE
    customer_favorite (
		customer_id uuid,
		product_id INT,
		PRIMARY KEY (customer_id, product_id),
		FOREIGN KEY (customer_id) REFERENCES customer (id) On delete CASCADE,
		FOREIGN KEY (product_id) REFERENCES product (id) On delete CASCADE
	);

ALTER TABLE customer_favorite ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Enable all access for customer owned tables" ON "public"."customer_favorite"
AS PERMISSIVE FOR ALL
TO public
USING (auth.uid() = customer_id)
WITH CHECK (auth.uid() = customer_id);

CREATE TABLE
    purchase (
        id SERIAL PRIMARY KEY,
        customer_id uuid null references customer(id),
        created_at TIMESTAMP,
        completed_at TIMESTAMP,
        pick_up_time TIMESTAMP,
        subtotal DECIMAL(10, 2),
        tax_collected DECIMAL(10, 2),
        status VARCHAR(255)
    );

ALTER TABLE purchase ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Enable read access for customer owned records" ON "public"."purchase"
FOR SELECT
TO public
USING (auth.uid() = customer_id or customer_id is null);

CREATE POLICY "Enable update access for customer owned records" ON "public"."purchase"
FOR UPDATE USING (
  auth.uid() = customer_id and status <> 'COMPLETED'
) WITH CHECK (
  auth.uid() = customer_id and status <> 'COMPLETED'
);

CREATE TABLE
    purchase_item (
        id SERIAL PRIMARY KEY,
        purchase_id INT,
        product_id INT null,
        base_id INT,
        size_id INT,
        FOREIGN KEY (purchase_id) REFERENCES purchase (id) On delete CASCADE,
        FOREIGN KEY (product_id) REFERENCES product (id) On delete CASCADE,
        FOREIGN KEY (base_id) REFERENCES base (id) On delete CASCADE,
        foreign key (size_id) references size(id) On delete CASCADE
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
        FOREIGN KEY (category_id) REFERENCES category (id) On delete CASCADE,
        FOREIGN KEY (product_id) REFERENCES product (id) On delete CASCADE
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
        addon_type_id INT REFERENCES addon_type(id) On delete CASCADE
    );
   
CREATE TABLE
    purchase_item_addon (
        id SERIAL,
        purchase_item_id INT,
        addon_id INT,
        PRIMARY KEY (id, purchase_item_id, addon_id),
        FOREIGN KEY (purchase_item_id) REFERENCES purchase_item (id) On delete CASCADE,
        FOREIGN KEY (addon_id) REFERENCES addon (id) On delete CASCADE
    );

CREATE TABLE
    product_addon (
        id SERIAL,
        product_id INT,
        addon_id INT,
        PRIMARY KEY (id, product_id, addon_id),
        FOREIGN KEY (product_id) REFERENCES product (id) On delete CASCADE,
        FOREIGN KEY (addon_id) REFERENCES addon (id) On delete CASCADE
    );


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
