## Simulate connection timeouts

### Configure DB

All SQL in schema.sql.

Create DB:

```sql
create database "npgsql-spike";
create user "npgsql-spike" with password 'password';
```

Connect to the DB and run:

```sql
create table public.assignment
(
    assignment_id integer generated always as identity
        constraint assignment_pk
            primary key,
    name          varchar(64)              not null,
    created_utc   timestamp with time zone not null
);

create unique index assignment_name_uindex on public.assignment (name);

grant all privileges on all tables in schema public to "npgsql-spike";
grant all privileges on all sequences in schema public to "npgsql-spike";

insert into public.assignment (name, created_utc)
select generate_series, now() from generate_series(1, 100000);
```

### Start the API

Open in IDE and run or run via command line.

### Run the K6 load test

Install K6 locally.

Run the test in a terminal from the root of the cloned repo:

```
k6 run k6.js
```