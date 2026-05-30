# NeedyNest

NeedyNest is a Windows Forms desktop application for managing community resources, courses, and user roles. It streamlines donation intake and distribution, course enrollment, user approval workflows, and basic payment tracking to help small organizations coordinate aid efficiently.

**Features**
- **User Roles**: Admin, Moderator, Distributor, Needer — role-specific dashboards and actions.
- **Resource Management**: Add and track books and materials, record received items, and manage distributions.
- **Course Management**: Create and manage paid/free courses with enrollment and dashboards.
- **User Management**: Signup, approval flows, promote/demote moderators, and user administration.
- **Payments & History**: Track paid-course transactions and view payment histories.
- **Runtime Theming**: Centralized theming via [UI/ThemeManager.cs](UI/ThemeManager.cs) and `BaseForm` at [UI/BaseForm.cs](UI/BaseForm.cs) for a consistent, professional UI.

**Tech Stack**
- **Framework**: .NET Framework 4.7.2 (WinForms)
- **Language**: C# (WinForms Designer-generated forms)
- **Data**: Provided `NeedyNest.bacpac` (SQL Server export) for database import
- **Build**: Visual Studio 2022 or Visual Studio Build Tools (recommended)

**Prerequisites**
- **OS**: Windows 10/11
- **IDE/Tools**: Visual Studio 2022 (recommended) or Visual Studio Build Tools 2022
- **.NET Targeting Pack**: .NET Framework 4.7.2 Developer Pack / targeting pack
- **Database**: SQL Server or LocalDB to restore the included `NeedyNest.bacpac`

**Getting Started**
- Clone the repository:

```bash
git clone https://github.com/Aiuohall/NeedyNest.git
cd NeedyNest
```

- Open the solution in Visual Studio: open `NeedyNest.sln`.
- Restore/attach the database by importing `NeedyNest.bacpac` into SQL Server (use SQL Server Management Studio or `SqlPackage.exe`).
- Build and run using Visual Studio or via MSBuild:

```powershell
msbuild NeedyNest.sln /p:Configuration=Debug
```

**UI Theming**
- The app applies runtime theming so Designer files remain unchanged. See [UI/ThemeManager.cs](UI/ThemeManager.cs) and [UI/BaseForm.cs](UI/BaseForm.cs) to customize colors, fonts, and control styles globally.

**Development Notes**
- If you encounter compilation errors referencing C# language features such as string interpolation (`$"..."`), install the Visual Studio Build Tools 2022 (Roslyn) and the .NET Framework targeting packs.
- The project was retargeted to `.NETFramework,Version=v4.7.2` to simplify modern builds on current toolchains.

**Contributing**
- Fork the repo, create a feature branch, and submit a pull request describing your changes.

**License & Contact**
- No license file is included. Add a `LICENSE` file (e.g., MIT) if you wish to open-source this project.
- For questions or assistance, open an issue on the GitHub repository .
