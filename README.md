# BearingStock - Blazor Web App with .NET 8 & MSSQL 🚀

This project is a **.NET 8-based Bearing Stock Management System** that provides an API and a Blazor-based frontend to manage bearings.  

## 📌 Features
- ✅ **View Bearings** - List bearings with details.
- ✅ **Create New Bearings** - Add a new bearing via a form.
- ✅ **Edit Bearings** - Update existing bearings.
- ✅ **Delete Bearings** - Remove bearings from the database.
- ✅ **MSSQL Database Integration** - Uses Entity Framework Core.
- ✅ **Blazor UI** - Built with **.NET 8 Interactive Server Mode**.

---

# 🔧 Setup & Configuration

## **1️⃣ Install Dependencies**
Ensure you have the following installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup) *(Optional for DB Management)*

---

## **2️⃣ Configure MSSQL Database**
1. Open **SQL Server Management Studio (SSMS)** or use the terminal.
2. Create a new database named **`BearingStock`**:

   ```sql
   CREATE DATABASE BearingStock;
   
---

## **3️⃣ Apply Migrations & Create Tables**
Run the following Entity Framework Core commands to set up the database schema:
- dotnet ef migrations add InitialCreate --project BearingStock.Core --startup-project BearingStock.Api 
- dotnet ef database update --project BearingStock.Core --startup-project BearingStock.Api
✅ This will create all required tables in the BearingStock database.

---

4️⃣ Update Configuration (appsettings.json)
Modify appsettings.json inside BearingStock.Api:
    "ConnectionStrings": {
      "DbConnection": "Server=DESKTOP-903LT1S;Database=BearingStock;Trusted_Connection=True;TrustServerCertificate=True;"
    }
Replace DESKTOP-903LT1S with your actual SQL Server name.
Ensure TrustServerCertificate=True; is included if using a self-signed certificate.

---

🚀 Running the Application
1️⃣ Start the API Server
Run the API project:

- cd BearingStock.Api
- dotnet run

✅ API will be available at:

http://localhost:5000/swagger (Swagger API Docs)
http://localhost:5000/api/bearings (Main Bearings Endpoint)

---

2️⃣ Start the Blazor Web App
Run the Blazor UI:

- cd BearingStock.Web
- dotnet run

✅ Frontend will be available at:

http://localhost:5000/bearings (Bearings List)

---

📡 API Endpoints
🟢 Bearings Endpoints (/api/bearings)
Method	Endpoint	Description
GET	/api/bearings	Get all bearings
GET	/api/bearings/{id}	Get a single bearing by ID
POST	/api/bearings	Create a new bearing
PUT	/api/bearings/{id}	Update an existing bearing
DELETE	/api/bearings/{id}	Delete a bearing

