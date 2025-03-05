using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    // класс представляющий книгу в библиотеке
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public double Rating { get; set; }
        public bool IsAvailable { get; set; }

        public Book() { } // необходим для сериализации/десериализации если потребуется

        // конструктор для инициализации свойств книги
        public Book(int id, string title, string author, string genre, DateTime publicationDate, double rating, bool isAvailable)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            PublicationDate = publicationDate;
            Rating = rating;
            IsAvailable = isAvailable;
        }


        // метод для получения строкового представления книги
        public override string ToString()
        {
            return $"ID: {Id}, Название: {Title}, Автор: {Author}, Жанр: {Genre}, " +
                $"Дата публикации: {PublicationDate.ToShortDateString()}, Рейтинг: {Rating}, Доступна: {IsAvailable}";
        }
    }


    // интерфейс для управления библиотекой
    public interface ILibraryManager
    {
        void AddBook(Book book);
        void RemoveBook(int id);
        void UpdateBook(int id, Book updatedBook);
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        List<Book> SearchBooks(string searchTerm); // поиск
        int GetBookCount();
        List<Book> GetAvailableBooks();
        List<Book> GetBooksByRating(double minRating);
        List<Book> GetBooksByPublicationDate(DateTime startDate, DateTime endDate);

    }


    // класс для управления библиотекой
    public class LibraryManager : ILibraryManager
    {
        private readonly List<Book> _books = new List<Book>();
        private int _nextId = 1;  // для автоматической генерации айди

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.Id = _nextId++; //автоматическое присвоение айди
            _books.Add(book);
        }

        public void RemoveBook(int id)
        {
            _books.RemoveAll(b => b.Id == id);
        }

        // метод для обновления информации о книге
        public void UpdateBook(int id, Book updatedBook)
        {
            if (updatedBook == null)
            {
                throw new ArgumentNullException(nameof(updatedBook));
            }

            Book existingBook = _books.FirstOrDefault(b => b.Id == id);
            if (existingBook != null)
            {
                existingBook.Title = updatedBook.Title;
                existingBook.Author = updatedBook.Author;
                existingBook.Genre = updatedBook.Genre;
                existingBook.PublicationDate = updatedBook.PublicationDate;
                existingBook.Rating = updatedBook.Rating;
                existingBook.IsAvailable = updatedBook.IsAvailable;
            }
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetAllBooks()
        {
            return _books.ToList(); // возвращаем копию списка
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return _books.Where(b => b.Author.ToLower().Contains(searchTerm) || b.Genre.ToLower().Contains(searchTerm)).ToList();
        }

        public int GetBookCount()
        {
            return _books.Count;
        }

        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable).ToList();
        }

        public List<Book> GetBooksByRating(double minRating)
        {
            return _books.Where(b => b.Rating >= minRating).ToList();
        }

        public List<Book> GetBooksByPublicationDate(DateTime startDate, DateTime endDate)
        {
            return _books.Where(b => b.PublicationDate >= startDate && b.PublicationDate <= endDate).ToList();
        }
    }
}
