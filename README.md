# My ASP.NET Core Web API with Entity Framework Core and PostgreSQL (GraphQL Server)

## Getting Started

1. Clone this repository to your local machine:

   ```shell
   git clone https://github.com/trthsa/test_asp_core_graphql
   ```

2. Sync dependencies:

   ```shell
   dotnet restore
   ```

3. Run to create migrations:

   ```shell
   dotnet ef migrations add InitialCreate
   ```

4. Run to update database:

   ```shell
   dotnet ef database update
   ```

5. Run to start server:

   ```shell
   dotnet run
   ```

6. Open browser and go to http://localhost:7155/graphql
