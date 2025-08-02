# 🚀 Stack Tutorial 📚  
A Full-Stack Learning Platform for Programming Tutorials

---

## 🌐 Live Demo
> [View Live Project](#)

---

## 📖 About the Project

Stack Tutorial is a modern, full-stack learning platform built using ASP.NET Core 8 MVC and SQL Server, designed to provide a structured and scalable way to manage technical tutorials. This platform is ideal for developers, trainers, and self-learners who want to publish, explore, or consume programming tutorials in a clean and categorized format.

At its core, the platform follows a 3-level hierarchical structure to organize content effectively:

🧱 Structured Content Hierarchy
The platform follows a 3-tier hierarchical model to logically organize tutorials:

🔹 Categories – e.g., C#, .NET Core, JavaScript

➡️ Topics – e.g., OOP, Collections, LINQ

➡️ Contents – e.g., Lessons, Examples, Code Snippets

---

##✨ Key Capabilities

- ✅ Clean 3-Tier Tutorial Structure: Category → Topic → Content
- 🛠️ Built with ASP.NET Core 8 MVC and Entity Framework Core (Code First)
- 📁 Modular architecture using Repository and Services Layer
- 🧠 Developer-friendly and highly extensible
- 🔍 Future-ready for features like search, authentication, and markdown or WYSIWYG editors
- 📄 Admin panel to manage tutorials, topics, and content dynamically
- 🌐 Responsive design with Razor-based views and static asset support
---

##🧑‍💻 Ideal For

- 🔍 Educators who want to manage and publish course material
- 🧠 Developers documenting code and tutorials in a reusable system
- 📄 Learners looking to explore well-structured programming content
- 🧑‍💻 Teams building internal documentation or training systems
---
## 🎯 Project Objectives

- ✅ Deliver structured, SEO-friendly programming content.
- ✅ Manage categories, topics, and content using a user-friendly interface.
- ✅ Follow best practices with clean architecture and Entity Framework Core.
- ✅ Build a scalable and extendable foundation for future LMS or documentation systems.

---

## 🛠️ Tech Stack

| Layer         | Technology                                                                 |
|---------------|-----------------------------------------------------------------------------|
| **Language**  | `C#` (Backend), `HTML`, `CSS`, `JavaScript` (Frontend)                      |
| **Framework** | `ASP.NET Core 8 MVC`                                                        |
| **Database**  | `SQL Server` (Code-First via Entity Framework Core)                         |
| **UI/UX**     | Razor Views, Bootstrap, Custom CSS                                          |
| **ORM**       | `Entity Framework Core`                                                     |
| **IDE**       | Visual Studio 2022+                                                         |
| **Version Control** | `Git & GitHub`                                                        |
| **Architecture** | MVC Pattern, Repository & Services Layer                                |

---

## 📁 Folder Structure

+ Stack-Tutorial/
+ ├── 📂 Controllers/
+ │   ├── 📜 HomeController.cs
+ │   ├── 📂 Admin/
+ │   │   ├── 📜 CategoryController.cs
+ │   │   ├── 📜 ContentController.cs
+ │   │   └── 📜 TopicController.cs
+ │   └── 📜 TutorialController.cs
+ │
+ ├── 📂 Data/
+ │   ├── 📜 ApplicationDbContext.cs
+ │   └── 📂 Migrations/
+ │       ├── 📜 20240601000000_InitialCreate.cs
+ │       └── 📜 ApplicationDbContextModelSnapshot.cs
+ │
+ ├── 📂 Models/
+ │   ├── 📜 CategoryModel.cs
+ │   ├── 📜 ContentModel.cs
+ │   └── 📜 TopicModel.cs
+ │
+ ├── 📂 Repositories/
+ │   ├── 📂 Interfaces/
+ │   │   ├── 📜 ICategoryRepository.cs
+ │   │   ├── 📜 IContentRepository.cs
+ │   │   └── 📜 ITopicRepository.cs
+ │   └── 📂 Implementations/
+ │       ├── 📜 CategoryRepository.cs
+ │       ├── 📜 ContentRepository.cs
+ │       └── 📜 TopicRepository.cs
+ │
+ ├── 📂 Services/
+ │   ├── 📂 Interfaces/
+ │   │   ├── 📜 ICategoryService.cs
+ │   │   ├── 📜 IContentService.cs
+ │   │   └── 📜 ITopicService.cs
+ │   └── 📂 Implementations/
+ │       ├── 📜 CategoryService.cs
+ │       ├── 📜 ContentService.cs
+ │       └── 📜 TopicService.cs
+ │
+ ├── 📂 wwwroot/
+ │   ├── 📂 css/
+ │   │   └── 📜 site.css
+ │   ├── 📂 js/
+ │   │   └── 📜 site.js
+ │   └── 📂 lib/
+ │       ├── 📂 bootstrap/
+ │       ├── 📂 font-awesome/
+ │       └── 📂 prism/
+ │
+ ├── 📂 Views/
+ │   ├── 📂 Shared/
+ │   │   ├── 📜 _Layout.cshtml
+ │   │   ├── 📜 _ValidationScriptsPartial.cshtml
+ │   │   └── 📜 Error.cshtml
+ │   ├── 📂 Admin/
+ │   │   ├── 📂 Category/
+ │   │   ├── 📂 Content/
+ │   │   └── 📂 Topic/
+ │   └── 📂 Tutorial/
+ │       ├── 📜 Index.cshtml
+ │       └── 📜 Content.cshtml
+ │
+ ├── 📜 appsettings.json
+ ├── 📜 Program.cs
+ └── 📜 Startup.cs

## **📸 Screenshots**  
**Here are a few screenshots showcasing the main features and layout of the Stack Tutorial platform:**

<img width="950" height="475" alt="Screenshot 2025-08-02 202418" src="https://github.com/user-attachments/assets/4b15defc-cd5e-4893-9b05-8b1c94b8b7f0" />

---

<img width="949" height="476" alt="Screenshot 2025-08-02 202536" src="https://github.com/user-attachments/assets/8a768e96-58d7-490c-b93d-79286a8e7807" />

---

<img width="945" height="479" alt="Screenshot 2025-08-02 202630" src="https://github.com/user-attachments/assets/252cd088-43da-4926-8f02-f2143aa5964c" />

---

<img width="956" height="472" alt="Screenshot 2025-08-02 202737" src="https://github.com/user-attachments/assets/e07b533a-5a0f-4ae1-8e74-a79cbd22f016" />

---

<img width="959" height="476" alt="Screenshot 2025-08-02 202829" src="https://github.com/user-attachments/assets/e66ddead-1e72-4833-aea3-6da74d867442" />





