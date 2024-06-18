# GroupQuizApp
This project aims to create an Application in C# with CRUD functionality.

![logo](https://res.cloudinary.com/dlulkctls/image/upload/v1718650282/GroupQuiz/logo_vamcmi.png)
# Quiz Application



[Click here to access the live project]()




## Table of Contents
1. [Screenshots](#screenshots)
2. [Design](#design)
    1. [Wireframes](#wireframes)
    2. [ER Diagram](#er-diagram)
    3. [Design Elements Overview](#design-elements-overview)
3. [Features](#features)
    
4. [User Experience (UX)](#user-experience-(ux))
  
    
5. [Security Features and Defensive Design](#security-features-and-defensive-design)
   
6. [Technologies Used](#technologies-used)
   
7. [Testing](#testing)
    1. []()
8. [Deployment](#deployment)
   
9. [Credits](#credits)

***

## Screenshots
---



[Back to Table of Contents ⇧](#table-of-contents)

## Design
---
### Project Structure
The project follows the standard ASP.NET MVC structure:

* Controllers: Contains the controllers for handling the requests.
* Models: Contains the model classes that define the data structure.
* Views: Contains the Razor views for the UI.
* wwwroot: Contains static files such as CSS, JavaScript, and images.
### Wireframes
You can download the wireframes for this website from this link []().
<details>
<summary>Wireframes Board</summary>
</details> 

---
### ER Diagram
<details>
<summary>ER Diagram</summary>

![ER Diagram](https://res.cloudinary.com/dlulkctls/image/upload/v1714994744/GroupQuiz/Er_diagram_hbb3jy.png)
</details> 

---


This code defines the structure of a relational database using the Database Markup Language (DBML). Each table in the database is defined with its attributes, data types, constraints, and relationships. Here's a brief description of each table and its purpose:



### Design Elements Overview
---



[Back to Table of Contents ⇧](#table-of-contents)


## Features
---
- **Home Page**: Welcomes users and offers navigation to start a quiz or create a new one.
- **Quiz Page**: Displays questions one at a time with options to navigate between them and submit the quiz at the last question.
- **Dashboard**: Allows users to view, edit, and delete questions.
- **Create/Edit Question**: Forms to add new questions or edit existing ones with validation.
- **Result Page**: 


[Back to Table of Contents ⇧](#table-of-contents)

## User Experience (UX)
---

* ### **User Stories**

- **As a user**, I want to start a quiz so that I can test my knowledge.
- **As a user**, I want to create and edit questions so that I can customize the quiz.
- **As a user**, I want to see my quiz results so that I can know my score.


---


### Development Methodology
---
* The development followed an Agile methodology on the [] ()

<details>
<summary>Agile board</summary>

![project]()
</details> 

[Back to Table of Contents ⇧](#table-of-contents)

## Security Features and Defensive Design
---

### Form Validation
---
Form validation is implemented to ensure the integrity and correctness of data entered by users. The following form validation is included on the Create and Edit pages:



[Back to Table of Contents ⇧](#table-of-contents)

## Technologies Used
---
### Languages Used
* [C#](https://en.wikipedia.org/wiki/C_Sharp_(programming_language)#:~:text=C%23%20(%2F%CB%8Csi%CB%90%20%CB%88,and%20component%2Doriented%20programming%20disciplines.)
* [HTML]()
* [CSS]()
* [javaScript]()

 ### Frameworks Programs Used
 ---
1. [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/):
    * C# is a general-purpose high-level programming language supporting multiple paradigms
1.  [ASP.NET](A free web framework for building great web applications and services with .NET and C#.)
    * 
1. [Bootstrap](Bootstrap is a powerful front-end framework for faster and easier web development.)
    * 
1. [GitHub](https://github.com/):
    * GitHub was used to store the site's code after being pushed from Gitpod.
1. [Grammerly](https://app.grammarly.com/):
    * Used to proofread the README.md.


### Packages
* [Microsoft.AspNetCore.Mvc.NewtonSoft]()
    * ASP.NET Core MVC features that use Newtonsoft.Json. Includes input and output formatters for JSON and JSON PATCH.

* [Microsoft.EntityFrameworkCore]()
    * Entity Framework Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL, and other databases through a provider plugin API.

    * Commonly Used Types:
        - Microsoft.EntityFrameworkCore.DbContext
        - Microsoft.EntityFrameworkCore.DbSet
* [Microsoft.EntityFrameworkCore.Tools]()
    * Entity Framework Core Tools for the NuGet Package Manager Console in Visual Studio. Enables these commonly used commands:
        - Add-Migration
        - Bundle-Migration
        - Drop-Database
        - Get-DbContext
        - Get-Migration
        - Optimize-DbContext
        - Remove-Migration
        - Scaffold-DbContext
        - Script-Migration
        - Update-Database
* [Pomelo.EntityFrameworkCore.MySql]()
    * Pomelo's MySQL database provider for Entity Framework Core.


[Back to Table of Contents ⇧](#table-of-contents)

### Libraries
* Microsoft.AspNetCore.Mvc
* System.Diagnostics
* Microsoft.EntityFrameworkCore


[Back to Table of Contents ⇧](#table-of-contents)


## Testing
---
### Validator Testing

1. Home Page: Tested the navigation buttons and links.
1. Quiz Page: Tested the question display, navigation between questions, and quiz submission.
1. Dashboard: Tested CRUD operations (Create, Read, Update, Delete) for questions

### Functional Testing

1. HTML Validation: Used the W3C HTML Validator to ensure there are no errors in the HTML code.
1. CSS Validation: Used the W3C CSS Validator to ensure the CSS file is error-free.
1. JavaScript Validation: Used JSHint to check for errors and potential problems in the JavaScript code.

### Unit Testing


[Back to Table of Contents ⇧](#table-of-contents)

## Deployment
---
### .NET Runtime
Deployed by localhost for development and testing purposes.
For future development, we hope to deploy it to a web server or a cloud service like Azure.

### **Making a Local Clone**
---
1. Clone the repository:

```sh
git clone https://github.com/kev-n14/GroupQuizApp.git
cd Group_Quiz
```
2. Open the project in Visual Studio.

3. Restore the NuGet packages:

```sh
dotnet restore
```
4. Update the database connection string in appsettings.json.

5. Run the application:

```sh
dotnet run
```

[Back to Table of Contents ⇧](#table-of-contents)


## Credits
---
* [Microsoft Documentation](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
* [Bootstrap Documentation](https://getbootstrap.com/docs/4.1/getting-started/introduction/)
* [W3school Resouces](https://www.w3schools.com/)


[Back to Table of Contents ⇧](#table-of-contents)
