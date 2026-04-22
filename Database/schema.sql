							--TWORZENIE BAZY DANYCH
CREATE DATABASE ApkaDoCen;
USE ApkaDoCen;



							--TWORZENIE TABEL

CREATE TABLE KATEGORIE (
    idKategorii INT IDENTITY(1,1) PRIMARY KEY,
    nazwa VARCHAR(25) NOT NULL
);

CREATE TABLE PRODUKTY (
    idProduktu INT IDENTITY(1,1) PRIMARY KEY,
    idKategorii INT NOT NULL,
    nazwa VARCHAR(50) NOT NULL,
    opis NVARCHAR(MAX),
    FOREIGN KEY (idKategorii) REFERENCES KATEGORIE(idKategorii) ON DELETE CASCADE
);

CREATE TABLE SKLEPY (
    idSklepu INT IDENTITY(1,1) PRIMARY KEY,
    nazwa VARCHAR(55) NOT NULL,
    stronaWWW VARCHAR(255)
);

CREATE TABLE CENY (
    idProduktu INT,
    idSklepu INT,
    cena DECIMAL(6,2) NOT NULL,
    dostepnosc BIT NOT NULL,
	dataAktualizacji DATE,
    PRIMARY KEY (idProduktu, idSklepu),
    FOREIGN KEY (idProduktu) REFERENCES PRODUKTY(idProduktu) ON DELETE CASCADE,
    FOREIGN KEY (idSklepu) REFERENCES SKLEPY(idSklepu) ON DELETE CASCADE
);



							--WPROWADZANIE DANYCH

			--KATEGORIE
INSERT INTO KATEGORIE (nazwa) VALUES
('P³yty g³ówne'),
('Dyski HDD'),
('Dyski SSD'),
('Pamiêæ RAM'),
('Procesory'),
('Zasilacze');

SELECT * FROM KATEGORIE;

			--SKLEPY
INSERT INTO SKLEPY (nazwa, stronaWWW) VALUES
('Morele.net', 'https://www.morele.net'),
('Komputronik', 'https://www.komputronik.pl'),
('X-kom', 'https://www.x-kom.pl'),
('Media Expert', 'https://www.mediaexpert.pl');

SELECT * FROM SKLEPY;

			--PRODUKTY
-- P³yty g³ówne
INSERT INTO PRODUKTY (idKategorii, nazwa, opis) VALUES
(1, 'ASUS ROG Strix B550-F Gaming', 'P³yta g³ówna do AMD AM4'),
(1, 'MSI MAG Z790 Tomahawk', 'Z790 pod Intel 13/14gen'),
(1, 'Gigabyte B660M DS3H DDR4', 'P³yta mATX pod Intel'),
(1, 'ASRock X670E Steel Legend', 'Nowa generacja AMD AM5'),
(1, 'ASUS TUF Gaming B450-PLUS II', 'Popularna p³yta AM4');

-- Dyski HDD
INSERT INTO PRODUKTY (idKategorii, nazwa, opis) VALUES
(2, 'Seagate Barracuda 2TB', 'Klasyczny dysk HDD 3.5"'),
(2, 'WD Blue 1TB', 'Uniwersalny dysk HDD 7200rpm'),
(2, 'Toshiba P300 3TB', 'Wydajny dysk do desktopów'),
(2, 'WD Black 4TB', 'Dysk HDD dla graczy'),
(2, 'Seagate IronWolf 6TB', 'HDD do NASów i serwerów');

-- Dyski SSD
INSERT INTO PRODUKTY (idKategorii, nazwa, opis) VALUES
(3, 'Samsung 980 PRO 1TB', 'PCIe 4.0 NVMe SSD'),
(3, 'Crucial P3 1TB', 'Tani i szybki SSD NVMe'),
(3, 'Kingston NV2 2TB', 'Œwietna cena do pojemnoœci'),
(3, 'WD Black SN850X 1TB', 'Gamingowy SSD PCIe 4.0'),
(3, 'ADATA XPG GAMMIX S70 2TB', 'Bardzo szybki SSD NVMe');

-- Pamiêæ RAM
INSERT INTO PRODUKTY (idKategorii, nazwa, opis) VALUES
(4, 'Kingston Fury Beast 16GB DDR4 3200MHz', '2x8GB RAM DDR4'),
(4, 'Corsair Vengeance LPX 16GB DDR4 3600MHz', 'Popularny RAM do gamingu'),
(4, 'G.Skill Ripjaws V 32GB DDR4 3600MHz', '2x16GB, szybki RAM'),
(4, 'Patriot Viper Steel 16GB DDR4 4000MHz', 'Wysoka czêstotliwoœæ'),
(4, 'Corsair Dominator Platinum RGB 32GB DDR5 5600MHz', 'Nowa generacja DDR5');

-- Procesory
INSERT INTO PRODUKTY (idKategorii, nazwa, opis) VALUES
(5, 'Intel Core i5-13400F', '10 rdzeni, œwietny stosunek ceny do jakoœci'),
(5, 'AMD Ryzen 5 5600X', '6 rdzeni, AM4'),
(5, 'Intel Core i7-14700K', 'Nowa gen, 20 rdzeni'),
(5, 'AMD Ryzen 7 7800X3D', 'Œwietny CPU do gamingu'),
(5, 'Intel Core i3-13100F', 'Tani i wydajny 4-rdzeniowiec');

-- Zasilacze
INSERT INTO PRODUKTY (idKategorii, nazwa, opis) VALUES
(6, 'SilentiumPC Vero L3 600W', 'Dobry bud¿etowy zasilacz'),
(6, 'Corsair RM750x 750W', 'Modularny, Gold'),
(6, 'Be Quiet! Pure Power 12M 850W', 'Cichy i wydajny PSU'),
(6, 'MSI MAG A750GL 750W', 'Dobra cena/wydajnoœæ'),
(6, 'Seasonic Focus GX 650W', 'Sprawdzona marka');

SELECT * FROM PRODUKTY;


			--CENY
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (1, 3, 561.23, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (1, 1, 591.10, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (1, 2, 600.59, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (1, 4, 599.76, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (2, 1, 952.84, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (2, 4, 1048.55, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (2, 3, 949.56, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (2, 2, 1025.81, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (3, 3, 386.99, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (3, 1, 371.22, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (4, 3, 1183.63, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (4, 1, 1193.99, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (4, 2, 1202.13, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (4, 4, 1246.68, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (5, 1, 410.52, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (5, 3, 432.77, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (6, 2, 241.65, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (6, 3, 242.42, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (6, 4, 239.37, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (6, 1, 259.08, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (7, 2, 217.66, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (7, 3, 221.68, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (7, 1, 220.14, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (8, 4, 346.92, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (8, 1, 335.29, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (8, 3, 347.41, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (8, 2, 331.35, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (9, 4, 543.73, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (9, 1, 537.14, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (9, 2, 500.96, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (9, 3, 530.00, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (10, 1, 292.66, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (10, 3, 280.68, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (10, 2, 293.77, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (10, 4, 300.63, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (11, 3, 475.68, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (11, 2, 518.09, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (11, 1, 498.26, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (11, 4, 508.24, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (12, 1, 273.07, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (12, 4, 265.20, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (13, 4, 392.36, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (13, 2, 402.10, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (13, 3, 379.25, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (13, 1, 369.56, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (14, 1, 621.03, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (14, 3, 587.46, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (15, 4, 667.06, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (15, 3, 677.43, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (15, 2, 697.48, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (16, 2, 205.55, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (16, 4, 196.04, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (17, 3, 246.28, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (17, 2, 248.66, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (18, 1, 495.16, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (18, 4, 508.80, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (19, 2, 532.42, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (19, 1, 535.17, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (19, 4, 526.29, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (19, 3, 547.39, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (20, 1, 783.93, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (20, 2, 756.82, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (20, 4, 810.24, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (21, 2, 787.70, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (21, 4, 814.70, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (21, 1, 822.76, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (21, 3, 818.01, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (22, 3, 672.02, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (22, 2, 710.80, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (22, 4, 649.91, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (22, 1, 669.86, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (23, 1, 1770.06, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (23, 3, 1814.94, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (23, 4, 1709.25, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (24, 2, 1518.86, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (24, 3, 1585.95, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (24, 1, 1489.60, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (25, 4, 519.35, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (25, 2, 520.05, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (25, 3, 544.54, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (25, 1, 533.41, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (26, 3, 218.28, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (26, 1, 220.92, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (26, 4, 220.11, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (27, 4, 458.66, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (27, 2, 484.65, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (27, 1, 463.16, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (28, 4, 614.04, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (28, 2, 601.19, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (29, 2, 399.10, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (29, 3, 429.47, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (29, 1, 432.49, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (30, 3, 476.22, 0);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (30, 2, 461.18, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (30, 1, 439.77, 1);
INSERT INTO CENY (idProduktu, idSklepu, cena, dostepnosc) VALUES (30, 4, 461.30, 1);

SELECT * FROM CENY;

										--POPRAWKI
--Uzupe³nienie dat aktualizacji ceny
UPDATE CENY
SET dataAktualizacji = DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 121, '2025-01-01');

--Wymaganie daty podczas wprowadzania aktualizacji danych / nowych danych
ALTER TABLE CENY
ALTER COLUMN dataAktualizacji DATE NOT NULL;

						
