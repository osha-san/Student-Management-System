# Student Management System

A Windows Forms desktop application for managing student records with full CRUD (Create, Read, Update, Delete) functionality.

## 📋 Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Troubleshooting](#troubleshooting)
- [Future Enhancements](#future-enhancements)
- [License](#license)

## ✨ Features

- **User Authentication**: Secure login system with username and password validation
- **Student Management**: Complete CRUD operations for student records
  - Add new students
  - View all students in a data grid
  - Update existing student information
  - Delete student records
- **Input Validation**: Ensures all required fields are filled before operations
- **Auto-populate Forms**: Click on any student record to automatically fill the form fields
- **Confirmation Dialogs**: Safety prompts before deleting records
- **Real-time Grid Updates**: Data grid automatically refreshes after any operation

## 🛠️ Tech Stack

| Component | Technology |
|-----------|-----------|
| **Frontend/UI** | WinForms (.NET Framework) |
| **Backend Language** | C# |
| **Database** | MySQL |
| **IDE** | Visual Studio (Community Edition) |
| **Database Connector** | MySQL.Data (.NET Connector) |
| **Database Management** | MySQL Workbench |

## 📦 Prerequisites

Before running this project, ensure you have the following installed:

1. **Visual Studio 2019/2022** (Community Edition or higher)
   - Download: https://visualstudio.microsoft.com/downloads/
   - Workload: .NET desktop development

2. **MySQL Server 8.0+**
   - Download: https://dev.mysql.com/downloads/mysql/

3. **MySQL Workbench** (Optional, for database management)
   - Download: https://dev.mysql.com/downloads/workbench/

4. **.NET Framework 4.7.2+**
   - Usually included with Visual Studio

## 🚀 Installation

### Step 1: Clone or Download the Project

```bash
git clone https://github.com/yourusername/StudentManagementSystem.git
```

Or download as ZIP and extract to your desired location.

### Step 2: Open Project in Visual Studio

1. Launch Visual Studio
2. Click **File → Open → Project/Solution**
3. Navigate to the project folder
4. Open `StudentManagementSystem.sln`

### Step 3: Install NuGet Package

The project requires the MySQL.Data package. To install:

**Method 1: NuGet Package Manager**
1. Right-click on the project in Solution Explorer
2. Select **Manage NuGet Packages**
3. Click the **Browse** tab
4. Search for `MySQL.Data`
5. Click **Install**

**Method 2: Package Manager Console**
```
Install-Package MySql.Data
```

## 🗄️ Database Setup

### Step 1: Create Database

Open MySQL Workbench and run the following SQL script:

```sql
-- Create database
CREATE DATABASE student_management_system;

-- Use the database
USE student_management_system;

-- Create users table
CREATE TABLE users (
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL UNIQUE,
    user_password VARCHAR(50) NOT NULL
);

-- Create students table
CREATE TABLE students (
    student_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    age INT NOT NULL,
    course VARCHAR(100) NOT NULL
);

-- Insert default admin user (password: admin123)
INSERT INTO users (username, user_password) VALUES ('admin', 'admin123');

-- Insert sample students (optional)
INSERT INTO students (first_name, last_name, age, course) VALUES
('Juan', 'Dela Cruz', 20, 'Computer Science'),
('Maria', 'Santos', 19, 'Information Technology'),
('Pedro', 'Reyes', 21, 'Engineering');
```

### Step 2: Verify Tables

Run these commands to verify:

```sql
-- Check users table
SELECT * FROM users;

-- Check students table
SELECT * FROM students;
```

## ⚙️ Configuration

### Database Connection Setup

1. Navigate to the `DAL` folder
2. Copy `Database.cs.example` and rename it to `Database.cs`
3. Open `Database.cs` and replace `YOUR_MYSQL_PASSWORD` with your actual MySQL root password:
```csharp
private static string connectionString = "server=localhost;database=student_management_system;uid=root;pwd=YOUR_ACTUAL_PASSWORD";
```

4. Save the file

**Note**: `Database.cs` is excluded from version control to protect your credentials.

### Update Database Connection String

Open `Database.cs` in the `DAL` folder and update the connection string with your MySQL credentials:

```csharp
private static string connectionString = "server=localhost;database=student_management_system;uid=root;pwd=YOUR_PASSWORD";
```

Replace:
- `YOUR_PASSWORD` with your MySQL root password
- `root` with your MySQL username if different
- `localhost` with your server address if different

## 📖 Usage

### Login

1. Run the application (Press `F5` or click the Start button)
2. Enter credentials:
   - **Username**: `admin`
   - **Password**: `admin123`
3. Click **Login**

### Managing Students

#### View Students
- Click the **Load** button to display all students in the data grid

#### Add New Student
1. Fill in all fields:
   - First Name
   - Last Name
   - Age (must be a number between 1-150)
   - Course
2. Click **Add**
3. Success message will appear
4. Grid automatically refreshes

#### Update Student
1. Click **Load** to display students
2. Click on a row in the data grid (fields will auto-populate)
3. Modify the desired fields
4. Click **Update**
5. Confirm the update

#### Delete Student
1. Click **Load** to display students
2. Click on a row in the data grid
3. Click **Delete**
4. Confirm deletion in the dialog box

## 📁 Project Structure

```
StudentManagementSystem/
│
├── DAL/
│   └── Database.cs                 # Database connection class
│
├── LoginForm.cs                    # Login form logic
├── LoginForm.Designer.cs           # Login form UI design
│
├── MainForm.cs                     # Main application logic
├── MainForm.Designer.cs            # Main form UI design
│
├── Program.cs                      # Application entry point
│
└── App.config                      # Application configuration
```

## 🔧 Troubleshooting

### Common Issues

#### 1. "Could not copy .exe file" Error
**Solution**: Close all running instances of the application
- Open Task Manager (`Ctrl + Shift + Esc`)
- End all `StudentManagementSystem.exe` processes
- Rebuild the project

#### 2. "Unknown column 'user_password'" Error
**Solution**: Verify database table structure
```sql
DESCRIBE users;
```
Ensure column names match exactly in your code

#### 3. MySQL Connection Error
**Solution**: 
- Verify MySQL service is running
- Check connection string credentials
- Ensure firewall allows MySQL connections

#### 4. Update/Delete Not Working
**Solution**: Configure DataGridView properties
```csharp
dataGridStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
dataGridStudents.MultiSelect = false;
```

#### 5. NuGet Package Error
**Solution**: Restore NuGet packages
- Right-click Solution in Solution Explorer
- Select **Restore NuGet Packages**

## 🚀 Future Enhancements

Potential features for future versions:

- [ ] Password hashing for secure storage
- [ ] User roles (Admin, Teacher, Student)
- [ ] Search and filter functionality
- [ ] Export data to Excel/PDF
- [ ] Student photo upload
- [ ] Attendance tracking
- [ ] Grade management
- [ ] Report generation
- [ ] Backup and restore functionality
- [ ] Email notifications
- [ ] Multi-language support

## 📝 Database Schema

### Users Table
| Column | Type | Description |
|--------|------|-------------|
| user_id | INT | Primary key, auto-increment |
| username | VARCHAR(50) | Unique username |
| user_password | VARCHAR(50) | User password |

### Students Table
| Column | Type | Description |
|--------|------|-------------|
| student_id | INT | Primary key, auto-increment |
| first_name | VARCHAR(100) | Student's first name |
| last_name | VARCHAR(100) | Student's last name |
| age | INT | Student's age |
| course | VARCHAR(100) | Enrolled course |

## 👥 Contributors

- **Your Name** - Initial work

## 📄 License

This project is licensed under the MIT License - see below for details:

```
MIT License

Copyright (c) 2025

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## 📧 Contact

For questions or support, please contact:
- Email: joshuasantiago17.22@gmail.com
- GitHub: [osha-san](https://github.com/osha-san)

## 🙏 Acknowledgments

- MySQL Documentation
- Microsoft .NET Framework Documentation
- Visual Studio Community

---

**Made using C# and MySQL**