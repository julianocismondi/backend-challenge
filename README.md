## Backend Challenge

### Funcionalidad
App CRUD de usuarios con autenticación, autorización y roles: al registrar un usuario nuevo se genera con el rol de User. el único que puede otorgarle el rol de Admin es otro usuario admin que es capaz de ver y editar todos los usuarios de la base de datos.

### Tecnologías
Desarrollado en .Net 6
Code First con base de datos SQL Server

### Configuracion
Base de datos hosteada en server gratuito

Para base de datos en local, primero actualizar la cadena de conexión en appsettings.json y ejecutar el comando en package manager console:

`update-database`

### Create docker image
  `docker build -t backend-challenge .`

### Run docker container
  `docker run -p 7586:80 backend-challenge:latest`

### Usuario Admin
Email: admin@gmail.com

Pass: 123456
