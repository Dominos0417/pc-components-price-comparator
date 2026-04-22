# PC Components Price Comparator 🖥️

## 📌 About the Project
This desktop application was developed to help users easily browse, aggregate, and compare prices of various PC components (e.g., CPUs, GPUs, Motherboards). The project demonstrates the ability to build clean desktop user interfaces and manage relational data within the .NET ecosystem.

## 🛠️ Tech Stack
* **Language:** C# (.NET)
* **UI Framework:** WPF (Windows Presentation Foundation) / XAML
* **Database:** Microsoft SQL Server
* **Data Querying:** LINQ (Language Integrated Query)
* **Architecture:** MVC / MVVM *(zostaw to, którego wzorca użyłeś, lub usuń ten punkt, jeśli nie stosowałeś konkretnego)*

## 🚀 Key Features
- **Data Aggregation:** Centralized database storing specifications and prices of computer hardware.
- **Advanced Filtering & Sorting:** Fast and efficient data querying implemented with **LINQ**, allowing users to instantly sort components by price, brand, or performance metrics.
- **Relational Database:** Well-structured MS SQL database ensuring data integrity and fast retrieval.
- **Interactive UI:** User-friendly and responsive desktop interface built with WPF.


## 📊 Database Structure
The application relies on a relational database design. 
- Included in the `Database/` folder is the `schema.sql` script containing the table structures and sample data necessary to run the application locally.

## ⚙️ How to Run Locally
1. Clone this repository to your local machine.
2. Open Microsoft SQL Server Management Studio (SSMS) and execute the `schema.sql` script from the `Database/` folder to set up the local database.
3. Open the solution file (`.sln`) in **Visual Studio**.
4. Update the `ConnectionString` in the `App.config` file to point to your local SQL Server instance.
5. Build and run the project (F5).
