# 📚 Simple CRUD API with MediatR (.NET 8)

A simple ASP.NET Core Web API (targeting .NET 8) that demonstrates the use of **MediatR** for implementing the **CQRS (Command Query Responsibility Segregation)** pattern with a clean, single-project structure.

## 🛠 Features

- ✅ Create Book
- ✅ Read All Books
- ✅ Read Book by ID
- ✅ Update Book
- ✅ Delete Book
- ✅ Unit testing with xUnit and Moq

## 🧰 Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core (InMemory for unit testing)
- MediatR v11.1.0
- xUnit
- Moq

---

## 📦 Project Structure

```plaintext
BookApi/
│
├── Controllers/
│   └── BooksController.cs       # API Endpoints
│
├── Models/
│   └── Book.cs                  # Book Entity
│
├── Data/
│   └── AppDbContext.cs          # EF Core DbContext
│
├── Features/
│   ├── Books/
│   │   ├── Commands/
│   │   │   ├── CreateBookCommand.cs
│   │   │   ├── UpdateBookCommand.cs
│   │   │   └── DeleteBookCommand.cs
│   │   ├── Handlers/
│   │   │   ├── CreateBookHandler.cs
│   │   │   ├── UpdateBookHandler.cs
│   │   │   └── DeleteBookHandler.cs
│   │   └── Queries/
│   │       ├── GetAllBooksQuery.cs
│   │       ├── GetBookByIdQuery.cs
│   │       ├── GetAllBooksHandler.cs
│   │       └── GetBookByIdHandler.cs
│
├── Program.cs                   # App startup
│
└── BookApi.Tests/               # Unit tests with xUnit & Moq
    └── BooksControllerTests.cs
