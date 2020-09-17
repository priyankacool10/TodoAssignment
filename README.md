# ToDoAPI
Description:
This solution exposes 3 endpoints: api/Authentication, api/TodoItem and api/TodoList
api/Authentication is HTTP Post that will autenticate the user.Users are configured in memory using a List.
Username: user1 and Password: pass1 for successfull login.

api/TodoItem and api/TodoList perform CRUD operations on TodoItem and TodoList

1. This solution is using EntityFramework along with In Memory Utilisation. SO if you run the application again, you need
to post the data again.
2. I have also shown how EF with SQL Server can be done by defining connection string and registering sql service.
3. Login Page is present in wwwroot folder which makes call to Web API for authentication using JavaScript
4. Logging is properly implement for all request response.

Steps to run Solution:
1. Create a Database named as TodoApiDatabase beforehand in SQL Database
2. Make sure you specify correct DB Server Name in appsettings.json
3. Open console in Visual Studio and type:
add-migration "Table Creation" and press Enter (It will create a migration file)
Then type
update-database (It will create Tables in Database named as TodoApiDatabase as created above using EF Code First Technique)
4. Run the solution. You will be presented with Swagger UI of all endpoints created in this solution.


In case of any queries, kindly contact Priyanka Kapoor (priyanka.kapoor@nagarro.com)