using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1.test
{
    [TestClass]
    public class LibraryManagerTests
    {
        private ILibraryManager _libraryManager;

        [TestInitialize]
        public void Setup()
        {
            _libraryManager = new LibraryManager(); // Создаем экземпляр перед каждым тестом
        }

        [TestMethod]
        public void AddBook_ShouldAddBookToList()
        {
            // подготовка
            var book = new Book(1, "Test Book", "Test Author", "Test Genre", DateTime.Now, 4.5, true);

            // действие
            _libraryManager.AddBook(book);

            // проверка
            Assert.AreEqual(1, _libraryManager.GetBookCount());
            Assert.IsNotNull(_libraryManager.GetBookById(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddBook_ShouldThrowArgumentNullException_WhenBookIsNull()
        {
            // подготовка

            // действие
            _libraryManager.AddBook(null);

            // проверка 
        }

        [TestMethod]
        public void RemoveBook_ShouldRemoveBookFromList()
        {
            // подготовка
            var book = new Book(1, "Test Book", "Test Author", "Test Genre", DateTime.Now, 4.5, true);
            _libraryManager.AddBook(book);

            // действие
            _libraryManager.RemoveBook(1);

            // проверка
            Assert.AreEqual(0, _libraryManager.GetBookCount());
            Assert.IsNull(_libraryManager.GetBookById(1));
        }

        [TestMethod]
        public void UpdateBook_ShouldUpdateBookInformation()
        {
            // подготовка
            var book = new Book(1, "Test Book", "Test Author", "Test Genre", DateTime.Now, 4.5, true);
            _libraryManager.AddBook(book);
            var updatedBook = new Book(1, "Updated Book", "Updated Author", "Updated Genre", DateTime.Now.AddDays(1), 3.8, false);

            // действие
            _libraryManager.UpdateBook(1, updatedBook);
            var retrievedBook = _libraryManager.GetBookById(1);

            // проверка
            Assert.AreEqual("Updated Book", retrievedBook.Title);
            Assert.AreEqual("Updated Author", retrievedBook.Author);
            Assert.AreEqual("Updated Genre", retrievedBook.Genre);
            Assert.AreEqual(3.8, retrievedBook.Rating);
            Assert.IsFalse(retrievedBook.IsAvailable);
        }

        [TestMethod]
        public void GetBookById_ShouldReturnCorrectBook()
        {
            // подготовка
            var book1 = new Book(1, "Book 1", "Author 1", "Genre 1", DateTime.Now, 4.0, true);
            var book2 = new Book(2, "Book 2", "Author 2", "Genre 2", DateTime.Now, 3.5, false);
            _libraryManager.AddBook(book1);
            _libraryManager.AddBook(book2);

            // действие
            var retrievedBook = _libraryManager.GetBookById(2);

            // проверка
            Assert.AreEqual("Book 2", retrievedBook.Title);
        }

        [TestMethod]
        public void GetBookById_ShouldReturnNullIfBookNotFound()
        {
            
            // действие
            var retrievedBook = _libraryManager.GetBookById(1);

            // проверка
            Assert.IsNull(retrievedBook);
        }

        [TestMethod]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // подготовка
            var book1 = new Book(1, "Book 1", "Author 1", "Genre 1", DateTime.Now, 4.0, true);
            var book2 = new Book(2, "Book 2", "Author 2", "Genre 2", DateTime.Now, 3.5, false);
            _libraryManager.AddBook(book1);
            _libraryManager.AddBook(book2);

            // действие
            var allBooks = _libraryManager.GetAllBooks();

            // проверка
            Assert.AreEqual(2, allBooks.Count);
        }

        [TestMethod]
        public void SearchBooks_ShouldReturnMatchingBooks()
        {
            // подготовка
            var book1 = new Book(1, "Book 1", "Author 1", "Genre 1", DateTime.Now, 4.0, true);
            var book2 = new Book(2, "Book 2", "Author 2", "Genre 2", DateTime.Now, 3.5, false);
            _libraryManager.AddBook(book1);
            _libraryManager.AddBook(book2);

            // действие
            var searchResults = _libraryManager.SearchBooks("Author 1");

            // проверка
            Assert.AreEqual(1, searchResults.Count);
            Assert.AreEqual("Book 1", searchResults.First().Title);
        }

        [TestMethod]
        public void GetBookCount_ShouldReturnCorrectCount()
        {
            // подготовка
            var book1 = new Book(1, "Book 1", "Author 1", "Genre 1", DateTime.Now, 4.0, true);
            var book2 = new Book(2, "Book 2", "Author 2", "Genre 2", DateTime.Now, 3.5, false);
            _libraryManager.AddBook(book1);
            _libraryManager.AddBook(book2);

            // действие
            var count = _libraryManager.GetBookCount();

            // проверка
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void GetAvailableBooks_ShouldReturnOnlyAvailableBooks()
        {
            // подготовка
            var book1 = new Book(1, "Book 1", "Author 1", "Genre 1", DateTime.Now, 4.0, true);
            var book2 = new Book(2, "Book 2", "Author 2", "Genre 2", DateTime.Now, 3.5, false);
            _libraryManager.AddBook(book1);
            _libraryManager.AddBook(book2);

            // действие
            var availableBooks = _libraryManager.GetAvailableBooks();

            // проверка
            Assert.AreEqual(1, availableBooks.Count);
            Assert.AreEqual("Book 1", availableBooks.First().Title);
        }

        [TestMethod]
        public void GetBooksByRating_ShouldReturnBooksWithRatingGreaterThanOrEqualToMinRating()
        {
            // подготовка
            var book1 = new Book(1, "Book 1", "Author 1", "Genre 1", DateTime.Now, 4.0, true);
            var book2 = new Book(2, "Book 2", "Author 2", "Genre 2", DateTime.Now, 3.5, false);
            var book3 = new Book(3, "Book 3", "Author 3", "Genre 3", DateTime.Now, 4.5, false);
            _libraryManager.AddBook(book1);
            _libraryManager.AddBook(book2);
            _libraryManager.AddBook(book3);

            // действие
            var booksWithHighRating = _libraryManager.GetBooksByRating(4.0);

            // проверка
            Assert.AreEqual(2, booksWithHighRating.Count);
            Assert.IsTrue(booksWithHighRating.Any(b => b.Title == "Book 1"));
            Assert.IsTrue(booksWithHighRating.Any(b => b.Title == "Book 3"));
        }

        [TestMethod]
        public void GetBooksByPublicationDate_ShouldReturnBooksPublishedWithinDateRange()
        {
            // подготовка
            var book1 = new Book(1, "Book 1", "Author 1", "Genre 1", new DateTime(2023, 01, 15), 4.0, true);
            var book2 = new Book(2, "Book 2", "Author 2", "Genre 2", new DateTime(2023, 02, 20), 3.5, false);
            var book3 = new Book(3, "Book 3", "Author 3", "Genre 3", new DateTime(2023, 03, 10), 4.5, false);
            _libraryManager.AddBook(book1);
            _libraryManager.AddBook(book2);
            _libraryManager.AddBook(book3);

            // действие
            var booksInFebruary = _libraryManager.GetBooksByPublicationDate(new DateTime(2023, 02, 01), new DateTime(2023, 02, 28));

            // проверка
            Assert.AreEqual(1, booksInFebruary.Count);
            Assert.AreEqual("Book 2", booksInFebruary.First().Title);
        }





    }
}
