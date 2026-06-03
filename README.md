# NeedyNest

**NeedyNest** is a Windows desktop application that helps small organizations manage community resources, courses, and members. It handles donation intake and distribution, course enrollment (free & paid), member approval workflows, and payment tracking — all behind a clean, role-based interface.

Built with **C# / .NET Framework 4.7.2 (WinForms)** and **SQL Server**.

---

## ✨ Features

- **Role-based access** — Admin, Moderator, Distributor, and User (Needer) each get their own dashboard.
- **Member approval workflow** — new sign-ups stay *pending* until an admin approves them from a dedicated **Approve Members** screen, with a live red badge showing how many are waiting.
- **Resource sharing** — upload and download books and study materials (stored in the database).
- **Course management** — create free and paid courses, attach materials, and let members enroll.
- **Payments** — Bkash / Card checkout for paid courses, with full payment history for admins.
- **User management** — add, delete, promote, and review members.
- **Professional themed UI** — a centralized theme (gradient headers, consistent buttons, styled data grids) applied across every screen, with resizable / maximizable windows.

---

## 🧱 Tech Stack

| Layer | Technology |
|------|-------------|
| Language | C# |
| Framework | .NET Framework 4.7.2 (WinForms) |
| Database | SQL Server / LocalDB |
| IDE | Visual Studio 2022 |

---

## 🛠️ Prerequisites

- Windows 10 / 11
- Visual Studio 2022 (or Build Tools 2022) with the **.NET desktop development** workload
- **.NET Framework 4.7.2 Developer / Targeting Pack**
- SQL Server Express or LocalDB

---

## 🚀 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/Aiuohall/NeedyNest.git
cd NeedyNest
```

### 2. Restore the database
Import the included `NeedyNest.bacpac` into SQL Server using **SQL Server Management Studio**
(*Databases → right-click → Import Data-tier Application…*) and name the database **`NeedyNest`**.

### 3. Configure the connection string
The connection string lives in **`App.config`** — edit it once to point at your SQL Server instance:

```xml
<connectionStrings>
  <add name="NeedyNest"
       connectionString="Data Source=YOUR-PC\SQLEXPRESS;Initial Catalog=NeedyNest;Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```
> Replace `YOUR-PC\SQLEXPRESS` with your own server name. No recompiling of individual forms is needed — every screen reads this one value.

### 4. Apply the enrollment fix (one time)
Run **`DB/FixEnrolledPrimaryKey.sql`** in SSMS. It changes the `Enrolled` table's primary key to `(username, course id)` so multiple members can enroll in the same course.

### 5. Build & run
Open `NeedyNest.sln` in Visual Studio and press **F5**, or build from the command line:
```powershell
msbuild NeedyNest.sln /p:Configuration=Debug
```

---

## 🔑 Test Accounts (offline mode)

If the database is **not** connected, you can still explore the UI and navigation using the built-in test logins (defined in `Login.cs`). The login screen shows a hint bar when the database is offline.

| Username | Password | Role |
|----------|----------|------|
| `admin` | `admin123` | Admin |
| `moderator` | `mod123` | Moderator |
| `distributor` | `dist123` | Distributor |
| `user` | `user123` | User |

> ⚠️ These test accounts bypass the database for **login only** — features that read/write data (uploads, payments, approvals) still need a live database. **Remove the `TestAccounts` entries from `Login.cs` before a real release.**

---

## 👥 Roles

| Role | Can do |
|------|--------|
| **Admin** | Full control — manage & approve users, promote moderators, add/remove paid courses, view payment history, manage distribution. |
| **Moderator** | Manage categories, add course materials, add paid courses, manage distribution. |
| **Distributor** | Upload and manage course materials. |
| **User (Needer)** | Browse/receive books, enroll in paid courses, edit their profile. |

---

## 📁 Project Structure

```
NeedyNest/
├── App.config                 # single connection string
├── DbHelper.cs                # one connection factory (DbHelper.GetConnection)
├── Session.cs                 # logged-in user + role (Session.LoggedInRole)
├── NavigationHelper.cs        # role-based "back to my dashboard" routing
├── Login.cs / SignUp.cs       # auth
├── *DashBoard*.cs             # per-role dashboards
├── ApproveMembers.cs          # pending-member approval screen
├── Course / PaidCourse / …    # course & payment screens
├── Distribution / AddBooks /  # upload & file screens
│   Add Materials
├── UI/
│   ├── ThemeManager.cs        # colors, fonts, button & grid styling
│   ├── BaseForm.cs            # base form (theme + background-form cleanup)
│   ├── DashboardLayout.cs     # header + centered button stack + footer
│   ├── PageChrome.cs          # gradient header for content forms
│   └── UploadFormLayout.cs    # unified layout for upload screens
└── DB/
    └── FixEnrolledPrimaryKey.sql
```

**Design notes**
- All screens inherit `BaseForm`, which applies the theme and tidies up background windows automatically.
- The current user's role comes from `Session.LoggedInRole` (set at login) — not a per-action database lookup.
- UI layout is applied at runtime so the Visual Studio Designer files stay untouched.

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/my-change`)
3. Commit your changes with a clear message
4. Open a pull request describing what changed and why

---

## 📄 License

No license file is currently included. Add one (e.g. MIT) if you plan to open-source the project.
