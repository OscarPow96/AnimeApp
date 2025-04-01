----------------------READ ME-------------------------------------

1. Requisitos previos:

SQL Server instalado (o acceso a un servidor de bases de datos)

.NET Core SDK (versión 8.0 o superior)

Visual Studio 2022 (recomendado) o VS Code con extensión C#

2. Pasos de configuración:

Abrir el archivo y editar el appsettings.json

3. Reemplazar la cadena de conexión con sus propios valores:

Ejemplo de cadena de conexión para SQL Server Local

"DefaultConnection": "Server=su_servidor;Database=su_basedatos;User Id=su_usuario;Password=su_contraseña;Trusted_Connection=False;TrustServerCertificate=True;"

Ejemplo de cadena de conexión para desarrollo local:

"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AnimeAppDev;Trusted_Connection=True;TrustServerCertificate=True;"

Ejemplo de cadena de conexión para SQL Express

"DefaultConnection": "Server=.\\SQLEXPRESS;Database=AnimeDB;Trusted_Connection=True;"

4. Migraciones de Base de Datos 

Ejecutar en la consola del administrador de paquetes:

dotnet ef database update

----------------------------------------------------------------------

Ejecución del Proyecto

1. Visual Studio

   -Abrir el archivo .sln
   -Presionar F5 o el botón de inicio

2. Consola

   -dotnet run

----------------------------------------------------------------------

