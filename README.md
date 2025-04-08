# collegeAppBackend

## SQL Connection String

- **Server Name**: vishal  
- **Username**: (Automatically used)  
- **Password**: (Automatically used)  
- **Database Name**: studentdb  
- **Integrated Security**: true  
- **Server Certificate**: true  

**Connection String**:  
`Data Source=vishal;Initial Catalog=studentdb;Integrated Security=True;Trust Server Certificate=True`

---

## Database Migration

To create a migration and update the database, use the following commands in the Package Manager Console:

1. `Add-Migration InitialCreate`  
2. `Update-Database`

---

## Patch Operation

To enable patch operations in your ASP.NET Core Web API, install the following NuGet packages:

1. `Microsoft.AspNetCore.JsonPatch`  
2. `Microsoft.AspNetCore.Mvc.NewtonsoftJson`

### Example for Patch Operation

Here is a short example of how to use the `JsonPatchDocument` for a patch operation in an ASP.NET Core Web API:
---

### Installing Packages Using NuGet Package Manager

To install the required packages, use the NuGet Package Manager in Visual Studio or run the following commands in the Package Manager Console: