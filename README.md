SGE_API — Prueba Técnica Backend .NET + Angular

Proyecto desarrollado en C# (.NET 8) y SQL
Server, aplicando principios de Clean Architecture, Automapper, Entity
Framework Core, y unit testing con xUnit.

------------------------------------------------------------------------

Objetivo

API que permite realizar operaciones CRUD sobre empleados,
implementando API Rest, devolviendo respuestas consistentes mediante un wrapper
genérico Result<T>.

------------------------------------------------------------------------

Requisitos previos

-   .NET 8 SDK
-   Docker (para ejecutar API si se desea)
-   Visual Studio 2022 o VS Code
-   Postman  (para probar EndPoints)

------------------------------------------------------------------------

-- Ejecución en local (con Docker)

1.  Requisitos previos

   - Asegúrate de tener instalados:

   - Docker Desktop

   - Visual Studio 2022 con soporte para .NET 8

    -Git

2. Clonar el repositorio

    --git https://github.com/JECH12/SGE_API.git
    cd TicketApp	


3️. Verificar el archivo docker-compose.yml


El proyecto ya incluye un docker-compose.yml configurado el servicio:


services:
  sgeapi:
    build:
      context: .
      dockerfile: ./SGE.Application/Dockerfile
    container_name: sg-api
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=tcp:sge-api.database.windows.net,1433;Initial Catalog=SGE-DB;Persist Security Info=False;User ID=adminUser;Password=Esteban123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
    ports:
      - "8080:8080"
    restart: unless-stopped

volumes:
  mssql-data:


4. Levantar la solución

	Verificar que Docker desktop este funcionando.

	Desde la raíz del proyecto: 

	docker-compose up --build

	Esto construira y la API.


5. Conexión de base de datos

	La API se conecta automáticamente a SGE-DB (Azure SQL Database) y aplica migraciones al iniciar.

	Cadena de conexión (en appsettings.Development.json):

	"DefaultConnection": "Server=tcp:sge-api.database.windows.net,1433;Initial Catalog=SGE-DB;Persist Security Info=False;User ID=adminUser;Password=Esteban123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

6 Endpoint

Una vez que el API este corriendo, abrir el navegador en:

http://localhost:8080/swagger

Ahí encontrara el playground de swagger para realizar consultas o si se desea hacerlo por postman.

------------------------------------------------------------------------

Endpoints

1. Obtener todos los empleados

   // GET: api/employees
   http://localhost:8080/api/Employees


2. Obtener empleado por ID: 

   // GET: api/employees
   http://localhost:8080/api/Employees/1

3. Crear Empleado

   // POST: api/employees
   http://localhost:8080/api/Employees

Body:
{
  "fullName": "string",
  "hireDate": "2025-11-24T03:47:55.285Z",
  "salary": 0,
  "departmentId": 0,
  "positionId": 0
}

4. Actualizar Empleado

   // PUT: api/employees/5
   http://localhost:8080/api/Employees/1

Body:
{
  "fullName": "string",
  "hireDate": "2025-11-24T03:49:24.888Z",
  "salary": 0,
  "departmentId": 0,
  "positionId": 0
}

5. Eliminar Empleado

   // DELETE: api/employees/5
   http://localhost:8080/api/Employees/1


------------------------------------------------------------------------------


Pruebas Unitarias

Ejecutar todas las pruebas con: dotnet test

Se utilizan xUnit, FluentAssertions, AutoMapper, y EF Core InMemory.
Cubre todos los métodos del servicio: GetAll, GetByIdAsync, CreateAsync,
UpdateAsync, DeleteAsync.

------------------------------------------------------------------------

 Tecnologías utilizadas

-   C# / .NET 8
-   API Rest
-   Entity Framework Core
-   Azure SQL Database
-   AutoMapper
-   xUnit + FluentAssertions
-   Docker

------------------------------------------------------------------------

El proyecto carece de endpoints para login, de autenticacion y autorizacion.
Es simple pero bastante flexible para nuevas implementaciones como:

- Autenticacion y Autoricacion con JWT.
- Ampliacion de endpoints y servicios para CRUD de departamentos y Cargos.
- Middleware de Autentiacion.

-- Autor --

Esteban Carrillo
Desarrollador .NET / Angular

------------------------------------------------------------------------