# 📖 Fanfic Catalog — WPF Desktop Application

**Fanfic Catalog** is a Windows desktop application built with **C# / WPF + SQLite** for managing fanfictions. It allows users to create, browse, read, and edit fanfics with structured metadata such as categories, genres, tags, chapters, and authors — all in a stylish dark-themed GUI.

---

## 🏠 Main Window

🔍 **Search Bar**  
Search fanfics in real-time by title.

🎯 **Filters**  
Filter fanfics by:
- Category
- Genre (Pairing)
- Tags (Multi-select)

🧾 **Fanfic Cards**  
Each fanfic preview includes:
- **Title**, **Author**, **Category**, **Pairings**, and **Tags**
- Action buttons:
  - 📖 **Read** — Opens the fanfic reader window.
  - ❌ **Delete** — Removes the fanfic.
  - ➕ **Add Chapter** — Upload `.txt` chapter files.

---

## ✍️ Add Fanfic Window

Accessible via the **+ Add Fanfic** button.

### Required Fields:
- 📌 Title  
- 👤 Author (select from dropdown)  
- 🗂 Category  
- 🔗 Genre / Pairing  
- 🏷 Tags (multi-select)

### Optional:
- 📄 Add Chapters — Upload chapter files (`.txt`)

✅ Press **Save Fanfic** to store it in the database.
---

## 📚 Fanfic Reader

- Displays fanfic info and list of chapters.
- Click on a chapter to read it.
- **Click again** to collapse the content.
- 🖱 Right-click on a chapter to **delete** it.

---

## 👤 Authors Page

- View all authors with avatar and bio.
- 🔍 Filter by name.
- ➕ Add new authors via a popup window.
- ❌ Remove authors by clicking the **X** button next to them.
- 📂 Clicking on an author filters fanfics in the main window.

---

## 📦 Database Structure

| Table      | Fields                                                                 |
|------------|------------------------------------------------------------------------|
| **Fanfics**   | `Id`, `Title`, `Content`, `AuthorId`, `CategoryId`, `GenreId`, `DateAdded` |
| **Authors**   | `Id`, `Name`, `Bio`, `Avatar`                                        |
| **Categories** | `Id`, `Name`                                                       |
| **Genres**     | `Id`, `Name`                                                       |
| **Tags**       | `Id`, `Name`                                                       |
| **Chapters**   | `Id`, `FanficId`, `ChapterNumber`, `Title`, `FilePath`             |

> 📝 All chapter content is stored in `.txt` files on disk and linked via `FilePath`.

---

## 🖥 Technologies Used

- `C#`, `.NET 8.0`, `WPF`
- `SQLite` (local embedded DB)
- `XAML` for UI layout
- `MVVM-lite` pattern for separation (optional)

---


---

## 💡 Screenshots

![image](https://github.com/user-attachments/assets/82c3595d-b3c9-41b7-8d6f-67e50f9efe8c)

---

## 🧑‍💻 Author

Created by Bobka227(Yerokhov Denys).  
This project is a university coursework or personal hobby tool — feel free to fork or contribute!

---

