# ASP.NET Core CRUD Maestro-Detalle con Repository, Unit of Work y Entity Framework Core

Este proyecto es un ejemplo de implementación de un CRUD maestro-detalle utilizando ASP.NET Core. Se enfoca en la gestión de órdenes (Order) y sus detalles (OrderDetails), aplicando los patrones de diseño Repository y Unit of Work con Entity Framework Core como ORM y SQL Server como base de datos.

## Características

- **Patrón de diseño Repository:** Facilita el acceso a datos mediante una capa de abstracción que permite un código más limpio y testeable.
- **Unit of Work:** Gestión de transacciones de manera centralizada, asegurando la integridad de los datos.
- **Entity Framework Core:** ORM para interacción con SQL Server.
- **CRUD Maestro-Detalle:** Gestión de órdenes y sus detalles en una estructura jerárquica.
- **ASP.NET Core MVC:** Framework utilizado para la construcción del front-end y la lógica del servidor.

## Requisitos

- .NET SDK 8.0 o superior
- SQL Server

## Configuración

1. Clona este repositorio.
2. Configura la cadena de conexión en `appsettings.json`.
3. Ejecuta las migraciones de Entity Framework Core para crear la base de datos.
4. Compila y ejecuta la aplicación.

## Uso

La aplicación permite crear, leer, actualizar y eliminar órdenes junto con sus detalles asociados. La interfaz está diseñada para manejar las operaciones maestro-detalle de manera eficiente.

## Agradecimientos

Este proyecto es posible gracias al apoyo continuo de los miembros de nuestro canal de YouTube, **InfoToolsSV**. Su compromiso y participación son fundamentales para que sigamos desarrollando contenido y herramientas útiles para la comunidad de desarrolladores. ¡Gracias por su apoyo constante y por ser parte de esta increíble comunidad!

Si aún no eres miembro, considera unirte para acceder a contenido exclusivo y ayudar a impulsar nuestro trabajo.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT.
