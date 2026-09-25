# ChatApp

A messaging web application built with **C# and ASP.NET Core MVC**, featuring user authentication, contacts, private conversations, and messaging.

## Features

- User registration and login
- User authentication with **ASP.NET Core Identity**
- Contact management
- Private conversations
- Sending and receiving messages
- User-specific conversations and messages
- Database with **Entity Framework Core** and **SQL Server**

## Technologies

- **C#**
- **ASP.NET Core MVC**
- **ASP.NET Core Identity**
- **Entity Framework Core**
- **SQL Server**
- **Razor Views**
- **LINQ**

## Data Model

The application uses the following main entities:

- **ApplicationUser** — users
- **Contact** — user-to-user contact relationships
- **Conversation** — 
- **UserConversation** — associates users with conversations
- **Message** — messages sent within conversations

## Architecture

The project follows the **MVC (Model-View-Controller)** pattern.
