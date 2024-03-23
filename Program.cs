using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
namespace BookLibrary
{
    #region Bookclass
    public class Book {
        static private int Id = 0;
        public string BookName { get; set; }
        public string Author { get; set; }
        public int YearOf { get; set; }
        public int NumberOfBooks { get; set; }
        public Book(string name, string author, int year, int numberOfBooks )
        {
            BookName = name;
            Author = author;
            YearOf = year;
            Id ++;
            NumberOfBooks = numberOfBooks;
        }
        public override string ToString()
        {
            return $"Book Name: {BookName}\nAuthor: {Author}\nPublication Year: {YearOf}\n BookID{Id}";
        }
    }
    #endregion
    #region Library class
    public class Library{

        public List<Book> books=new List<Book>();
        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine("Book added successfully!");
        }
        public void RemoveBook(Book book)
        {
            books.Remove(book);
        }
        public void DisplayBooks()
        {
            foreach (Book book in books)
            {
                Console.WriteLine(book.ToString());
                
            }

        }
        public bool CheckBookAvalibilty(Book book)
        {
            if (book.NumberOfBooks > 0)
            {
                Console.WriteLine("The Book is Avalible");
                return true;
            }
            else
            {
                Console.WriteLine("the Book is currently checked out");
                return false;
            }

        }
        public void SearchBooks(string searchTerm)
        {
            List<Book> searchResults = new List<Book>();

            foreach (var book in books)
            {
                if (book.BookName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    searchResults.Add(book);
                }
            }

            if (searchResults.Count > 0)
            {
                Console.WriteLine("Search Results:");
                foreach (var book in searchResults)
                {
                    Console.WriteLine($"{book.BookName} by {book.Author} ({book.YearOf})");
                }
            }
            else
            {
                Console.WriteLine("No books found matching the search term.");
            }
        }
    }
    #endregion

    internal class Program
    {

        static void Main(string[] args)
        {
            Library library = new Library();
            while (true)
            {
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Display all books");
                Console.WriteLine("3. Remove a book");
                Console.WriteLine("4. Search a book");
                Console.WriteLine("5. Check if a book is avalible");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter your choice:");

                int choice;
                if(int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            //Add new book
                            Console.WriteLine("Enter the book details");
                            Console.WriteLine("Enter the book name:");
                            string title = Console.ReadLine();
                            Console.WriteLine("Author: ");
                            string author = Console.ReadLine();
                            bool hasOnlyLettersA = System.Text.RegularExpressions.Regex.IsMatch(author, @"^[a-zA-Z]");
                            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || hasOnlyLettersA==false)
                            {
                                Console.WriteLine("invalid input!!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Publication Year: ");
                                string Syear = Console.ReadLine();
                                int year;
                                bool successYearOfPub = int.TryParse(Syear, out year);
                                Console.WriteLine("Number of copies of that book: ");
                                string SNumOfBooks = Console.ReadLine();
                                int NumOfBooks;
                                bool successNumOfBooks = int.TryParse(SNumOfBooks, out NumOfBooks);

                                if(successNumOfBooks&& successYearOfPub)
                                { Book book =new Book(title,author,year, NumOfBooks);
                                library.AddBook(book);
                                

                                }
                                else
                                {
                                    Console.WriteLine("invalid input!");
                                }
                                break;
                               
                            }
                        case 2:
                            library.DisplayBooks();
                            break;
                        case 3:
                            Console.WriteLine("Enter the title of the book to remove: ");
                            string bookToRemove = Console.ReadLine();

                            Book book1 = library.books.Find(b => b.BookName.Equals(bookToRemove, StringComparison.OrdinalIgnoreCase));
                            if (book1 != null)
                            {
                                library.RemoveBook(book1);
                                library.DisplayBooks();
                            }
                            else
                            {
                                Console.WriteLine("Book not found in the library inventory.");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Enter search term (title or author): ");
                            string searchTerm = Console.ReadLine();
                            library.SearchBooks(searchTerm);
                            break;

                        case 5:
                            Console.WriteLine("Enter the title of the book to check if avalibile: ");
                            string BookNameToCheck = Console.ReadLine();
                            Book CheckBook = library.books
                                .Find(b => b.BookName
                                .Equals(BookNameToCheck, StringComparison.OrdinalIgnoreCase));
                            if (CheckBook != null)
                            {
                                library.CheckBookAvalibilty(CheckBook);
                            }
                            else
                            {
                                Console.WriteLine("Book not found in the library inventory.");
                            }


                            break;


                        case 6:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;

                    }
                }
            }
        }
    }
}
