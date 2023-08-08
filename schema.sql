-- run the following first
create database "npgsql-spike";
create user "npgsql-spike" with password 'password';

-- connect to the new db
\c npgsql-spike

-- then run the following when connected to db
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