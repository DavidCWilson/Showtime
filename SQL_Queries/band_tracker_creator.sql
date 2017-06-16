USE master;
CREATE DATABASE band_tracker;
GO
USE band_tracker;
CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(100), city VARCHAR(50));
CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(150));
CREATE TABLE genres (id INT IDENTITY(1,1), name VARCHAR(100));
CREATE TABLE shows (id INT IDENTITY(1,1), bands_id INT, venues_id INT, tickets_left INT);
CREATE TABLE bands_genres (id INT IDENTITY(1,1), bands_id INT, genres_id INT);