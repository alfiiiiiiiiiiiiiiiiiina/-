DROP DATABASE IF EXISTS LibraryDB;
CREATE DATABASE IF NOT EXISTS LibraryDB;
USE LibraryDB;

CREATE TABLE Authors (
    id_author INT AUTO_INCREMENT PRIMARY KEY,
    firstname VARCHAR(255) NOT NULL,
    lastname VARCHAR(255) NOT NULL
);

CREATE TABLE Genres (
    id_genre INT AUTO_INCREMENT PRIMARY KEY,
    genre_name VARCHAR(100) NOT NULL
);

CREATE TABLE Books (
    id_book INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    id_author INT,
    id_genre INT,
    year_published INT,
    available BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (id_author) REFERENCES Authors(id_author),
    FOREIGN KEY (id_genre) REFERENCES Genres(id_genre)
);

CREATE TABLE Readers (
    id_reader INT AUTO_INCREMENT PRIMARY KEY,
    firstname VARCHAR(255) NOT NULL,
    lastname VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    address VARCHAR(255)
);

CREATE TABLE Borrowings (
    id_borrowing INT AUTO_INCREMENT PRIMARY KEY,
    id_book INT,
    id_reader INT,
    borrow_date DATE NOT NULL,
    return_date DATE,
    FOREIGN KEY (id_book) REFERENCES Books(id_book),
    FOREIGN KEY (id_reader) REFERENCES Readers(id_reader)
);



DELIMITER //

CREATE TRIGGER after_book_borrow
AFTER INSERT ON Borrowings
FOR EACH ROW
BEGIN
    UPDATE Books
    SET available = 0
    WHERE id_book = NEW.id_book;
END;
//

DELIMITER ;

DELIMITER //

CREATE TRIGGER after_book_return
AFTER UPDATE ON Borrowings
FOR EACH ROW
BEGIN
    IF NEW.return_date IS NOT NULL THEN
        UPDATE Books
        SET available = 1
        WHERE id_book = NEW.id_book;
    END IF;
END;
//

DELIMITER ;

select * from books;

INSERT INTO Authors (firstname, lastname) VALUES 
('Лев', 'Толстой'),
('Фёдор', 'Достоевский'),
('Александр', 'Пушкин'),
('Антон', 'Чехов'),
('Николай', 'Гоголь'),
('Иван', 'Тургенев'),
('Михаил', 'Лермонтов'),
('Александр', 'Грибоедов'),
('Фёдор', 'Тютчев'),
('Афанасий', 'Фет');

INSERT INTO Genres (genre_name) VALUES 
('Роман'),
('Поэзия'),
('Детектив'),
('Драма'),
('Фантастика'),
('Приключения'),
('Исторический'),
('Юмор'),
('Трагедия'),
('Лирика');

INSERT INTO Books (title, id_author, id_genre, year_published) VALUES 
('Война и мир', 1, 1, 1869),
('Преступление и наказание', 2, 1, 1866),
('Евгений Онегин', 3, 2, 1833),
('Вишнёвый сад', 4, 4, 1904),
('Мёртвые души', 5, 1, 1842),
('Отцы и дети', 6, 1, 1862),
('Герой нашего времени', 7, 1, 1840),
('Горе от ума', 8, 4, 1825),
('Стихотворения', 9, 2, 1854),
('Стихотворения', 10, 2, 1850),
('Анна Каренина', 1, 1, 1877),
('Братья Карамазовы', 2, 1, 1880),
('Капитанская дочка', 3, 1, 1836),
('Чайка', 4, 4, 1896),
('Ревизор', 5, 8, 1836);

INSERT INTO Readers (firstname, lastname, phone, address) VALUES 
('Иван', 'Иванов', '79161234567', 'ул. Ленина, 1'),
('Пётр', 'Петров', '79167654321', 'ул. Пушкина, 10'),
('Мария', 'Сидорова', '79169998877', 'пр. Мира, 5'),
('Алексей', 'Кузнецов', '79165554433', 'ул. Гагарина, 15'),
('Анна', 'Смирнова', '79162223344', 'ул. Садовая, 20'),
('Дмитрий', 'Васильев', '79163334455', 'пр. Ленинградский, 30'),
('Елена', 'Николаева', '79164445566', 'ул. Центральная, 25'),
('Сергей', 'Павлов', '79167778899', 'ул. Школьная, 12'),
('Ольга', 'Фёдорова', '79168889900', 'ул. Лесная, 8'),
('Виктор', 'Алексеев', '79160001122', 'пр. Победы, 17');

INSERT INTO Borrowings (id_book, id_reader, borrow_date, return_date) VALUES 
(1, 1, '2023-01-15', '2023-02-10'),
(3, 2, '2023-02-20', '2023-03-15'),
(4, 7, '2023-07-08', '2023-08-01'),
(6, 8, '2023-08-15', NULL),
(11, 1, '2023-11-05', '2023-11-30'),
(15, 3, '2024-01-15', '2024-02-05'),
(14, 5, '2024-03-01', '2024-03-25');