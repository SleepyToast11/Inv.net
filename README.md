# Inv.net  

**Inv.net** is a small solo project I built to explore **C#** and the **.NET ecosystem**, with a focus on **Entity Framework Core**, **Domain-Driven Design (DDD)**, and service-oriented structuring.  

What started as a small experiment grew into a more feature-rich system when I saw an opportunity to improve a spreadsheet workflow for a local non-profit I help with. While a bit over-engineered for the original scope, the project became a valuable learning experience in applying modern .NET patterns.  

---

## ✨ Features
- Thin controllers that map directly to **domain operations** (not CRUD endpoints)  
- Rich domain models with **factories**, **aggregates**, and **value objects**  
- Multi-tenant data handling with tenant-aware repositories  
- Persistence powered by **Entity Framework Core**  
- Modular architecture designed with microservice boundaries in mind  

---

## 🛠️ Tech Stack
- **C# / .NET 8**  
- **Entity Framework Core** (PostgreSQL backend)  
- **DDD-inspired architecture** (repositories, factories, aggregates)  
- **Docker** (for local setup and service isolation)  

---

## 🚀 Why This Project
- Learn C# and the .NET ecosystem hands-on  
- Explore how to implement **DDD patterns** beyond a toy example  
- Get practical experience with **EF Core** and persistence abstraction  
- Replace and improve a spreadsheet workflow for a local non-profit  

---

## 📚 Lessons Learned

- Keeping controllers thin and pushing logic into the domain layer
- Using factories over services for object creation
-Wrapping EF Core entities inside rich domain models for persistence + change tracking
-Designing repositories to enforce multi-tenant read/write rules
-Exploring how DDD translates to practical microservice boundaries

---

## ℹ️ N.B.

This was a solo learning project, so I intentionally kept the workflow "casual". Expect:

-Commit messages that are… let’s say expressive, not corporate.
-Occasional feature-creep detours.
-Architecture that’s more for fun and exploration than strict “enterprise polish.”

C# can already feel business-y enough, so I let myself bend the rules to keep it fun.
