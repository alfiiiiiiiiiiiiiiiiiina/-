using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DataBase
    {
        private MySqlConnection connection = new MySqlConnection("server=localhost; user=root; database=LibraryDB; port=3306; password=123456789");
        public List<books> GetBooks()
        {
            List<books> booksList = new List<books>();
            try
            {
                connection.Open();
                string query = "SELECT b.id_book, b.title, g.genre_name, a.firstname, a.lastname, b.year_published, b.available " +
                    "FROM books b " +
                    "join authors a on b.id_author = a.id_author " +
                    "join genres g on b.id_genre = g.id_genre";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books book = new books
                    {
                        Id = Convert.ToInt32(reader["id_book"]),
                        Title = reader["title"].ToString(),
                        AuthorFirstName = reader["firstname"].ToString(),
                        AuthorLastName = reader["lastname"].ToString(),
                        Genre = reader["genre_name"].ToString(),
                        Year_published = Convert.ToInt32(reader["year_published"]),
                        Available = Convert.ToBoolean(reader["available"]),
                        /* ExpirationDate = reader["expiration_date"] != DBNull.Value ? Convert.ToDateTime(reader["expiration_date"]) : DateTime.MinValue*/
                    };
                    booksList.Add(book);
                    
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return booksList;
        }
        public List<Authors> Getauthors()
        {
            List<Authors> authorsList = new List<Authors>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM authors";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())

                {
                    Authors author = new Authors
                    {
                        Id = Convert.ToInt32(reader["id_author"]),
                        FirstName = reader["firstname"].ToString(),
                        LastName = reader["lastname"].ToString()
                    };
                    authorsList.Add(author);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return authorsList;
        }

        public List<genres> Getgenres()
        {
            List<genres> genresList = new List<genres>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM genres"; ;
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())

                {
                    genres genres = new genres
                    {
                        Id = Convert.ToInt32(reader["id_genre"]),
                        Name = reader["genre_name"].ToString()
                    };
                    genresList.Add(genres);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return genresList;
        }

        public List<readers> Getreaders()
        {
            List<readers> readersList = new List<readers>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM readers";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    readers reade = new readers
                    {
                        Id = Convert.ToInt32(reader["id_reader"]),
                        FirstName = reader["firstname"].ToString(),
                        LastName = reader["lastname"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Address = reader["address"].ToString(),
                    };
                    readersList.Add(reade);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return readersList;
        }
        public List<borrowing> Getborrowing()
        {
            List<borrowing> borrowingList = new List<borrowing>();
            try
            {
                connection.Open();
                string query = "SELECT b.id_borrowing, bo.title, r.firstname, r.lastname, b.borrow_date, b.return_date " +
                    "FROM Borrowings b " +
                    "join Readers r on b.id_reader = r.id_reader " +
                    "join Books bo on b.id_book = bo.id_book";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    borrowing borrowing = new borrowing
                    {
                        Id = Convert.ToInt32(reader["id_borrowing"]),
                        Title = reader["title"].ToString(),
                        ReaderFN = reader["firstname"].ToString(),
                        ReaderLN = reader["lastname"].ToString(),
                        ReturnDate = reader["return_date"] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(reader["return_date"]),
                        Expiration_date = reader["borrow_date"] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(reader["borrow_date"])
                    };
                    borrowingList.Add(borrowing);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return borrowingList;

        }
            public void AddReader(string fname, string lname, string phone, string address)
            {
                try
                {
                        connection.Open();
                        string query = "INSERT INTO Readers (firstname, lastname, phone, address) " +
                                       "VALUES (@fname, @lname, @phone, @address)";

                MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                        
                    mySqlCommand.Parameters.AddWithValue("@fname", fname);
                    mySqlCommand.Parameters.AddWithValue("@lname", lname);
                    mySqlCommand.Parameters.AddWithValue("@phone", phone);
                    mySqlCommand.Parameters.AddWithValue("@address", address);

                    mySqlCommand.ExecuteNonQuery();
                        
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении читателя: {ex.Message}");
                }

                finally
                {
                    connection.Close();
                }
            }

        public void RemoveReader(int readerId)
        {
            try
            {
                connection.Open();

                string query = "delete from Readers where id_reader = @readerId";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@readerId", readerId);

                command.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            finally
            {
                connection.Close();
            }
        }


        public void AddBook(string title, int genreId, int authorId, int yearPub)
        {
            string query = "insert into Books (title, id_genre, id_author, year_published) values (@title, @genreId, @authorId, @yearPub)";

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@genreId", genreId);
                command.Parameters.AddWithValue("@authorId", authorId);
                command.Parameters.AddWithValue("@yearPub", yearPub);

                command.ExecuteNonQuery();
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            finally
            {
                connection.Close();
            }
        }

        public void DeleteBook(int bookId)
        {
            string query = "delete from Books where id_book = @bookId";

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@bookId", bookId);

                command.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

        }

        public void AddBorrowing(int bookId, int readerId)
        {
            string query = "insert into Borrowings (id_book, id_reader, borrow_date) values (@bookId, @readerId, curdate())";


            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@bookId", bookId);
                command.Parameters.AddWithValue("@readerId", readerId);

                command.ExecuteNonQuery();
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UPDBorrowing(int bookId)
        {
            string query = "update Borrowings set return_date = curdate() where id_borrowing = @bookId";


            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@bookId", bookId);

                command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<books> GetBooksCB()
        {
            List<books> booksList = new List<books>();
            try
            {
                connection.Open();
                string query = "SELECT b.id_book, b.title, a.firstname, a.lastname, g.genre_name, b.year_published, b.available " +
                               "FROM Books b " +
                               "JOIN Authors a ON b.id_author = a.id_author " +
                               "JOIN Genres g ON b.id_genre = g.id_genre " +
                               "WHERE b.available = TRUE";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    books book = new books
                    {
                        Id = Convert.ToInt32(reader["id_book"]),
                        Title = reader["title"].ToString(),
                        AuthorFirstName = reader["firstname"].ToString(),
                        AuthorLastName = reader["lastname"].ToString(),
                        Genre = reader["genre_name"].ToString(),
                        Year_published = Convert.ToInt32(reader["year_published"]),
                        Available = Convert.ToBoolean(reader["available"])
                    };
                    booksList.Add(book);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return booksList;
        }

        public void AddAuthor(string firstname, string lastname)
        {
            string query = "insert into Authors (firstname, lastname) " +
                "values (@fname, @lname)";


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@fname", firstname);
                command.Parameters.AddWithValue("@lname", lastname);

                command.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

        }

        public void DelAuthor(int authorId)
        {
            string query = "delete from Authors where id_author = @idAuthor";

            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idAuthor", authorId);

                cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }


        public void AddGenre(string name)
        {
            string query = "insert into genres (genre_name) " +
                "values (@gName)";

            try
            {
                connection.Open();

                MySqlCommand mySqlCommand = new MySqlCommand(query, connection);

                mySqlCommand.Parameters.AddWithValue("@gName", name);

                mySqlCommand.ExecuteNonQuery();


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DelGenre(int genreId)
        {
            string query = "delete from genres where id_genre = @idGenre";

            try
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@idGenre", genreId);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}








        
 
        

    

