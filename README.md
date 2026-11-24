# HelpDesk IA

API de mesa de ayuda (help desk) que recibe tickets de soporte y, al crearlos, **clasifica automáticamente la categoría y la prioridad y propone una primera respuesta** a partir del texto del problema.

Es un proyecto de portafolio. La clasificación usa reglas simples y está diseñada para poder cambiarse por un modelo de IA real sin tocar el resto de la aplicación.

## Tecnologías

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core 10 (base de datos primero)
- SQL Server
- C#

## Qué hace

- Crea tickets de soporte con título, descripción y solicitante.
- Si el ticket no trae categoría, la IA la propone según las palabras del texto.
- Asigna una prioridad (`Baja`, `Media` o `Alta`) según señales como "urgente" o "no puedo trabajar".
- Guarda una **sugerencia de primera respuesta** para el agente de soporte.
- Lista los tickets, del más reciente al más antiguo.

## Cómo funciona la IA

La lógica está detrás de una interfaz, `IAsistenteTickets`, que define un solo método: recibe el título y la descripción y devuelve categoría, prioridad y sugerencia.

```
TicketsController  ──usa──>  IAsistenteTickets  <──implementa──  AsistenteReglas
```

Hoy la implementación es `AsistenteReglas`, que busca palabras clave sin costo ni claves de API. Para usar un modelo de lenguaje real basta con crear otra clase que implemente la misma interfaz y registrarla en `Program.cs`.

| Id | Categoría |
|----|-----------|
| 1 | Hardware |
| 2 | Software |
| 3 | Redes |
| 4 | Accesos y contraseñas |
| 5 | Correo |

## Estructura

```
HelpDeskIA.Api/
├── Controllers/
│   └── TicketsController.cs   # endpoints de tickets
├── Models/                    # entidades generadas desde la base de datos
├── Servicios/
│   ├── IAsistenteTickets.cs   # contrato de la IA
│   └── AsistenteReglas.cs     # implementación basada en reglas
├── Program.cs
└── appsettings.json
database/
└── 01_crear_base_de_datos.sql
```

## Base de datos

Tablas: `Usuarios`, `Categorias`, `Tickets` y `Comentarios`. El script completo está en [`database/01_crear_base_de_datos.sql`](database/01_crear_base_de_datos.sql).

## Cómo ejecutarlo

**Requisitos:** .NET SDK 10, SQL Server (Express sirve) y SQL Server Management Studio o similar.

1. Ejecuta `database/01_crear_base_de_datos.sql` en tu servidor de SQL Server. Crea la base `HelpDeskIA`, las tablas y datos de ejemplo.
2. En `HelpDeskIA.Api/appsettings.json`, ajusta la cadena de conexión con el nombre de tu servidor:

   ```json
   "ConnectionStrings": {
     "Default": "Server=TU_SERVIDOR\\TU_INSTANCIA;Database=HelpDeskIA;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

3. Ejecuta el proyecto desde Visual Studio (F5) o con:

   ```bash
   dotnet run --project HelpDeskIA.Api
   ```

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| `POST` | `/api/tickets` | Crea un ticket y lo clasifica con la IA |
| `GET`  | `/api/tickets` | Lista los tickets |

El archivo `HelpDeskIA.Api.http` incluye peticiones de ejemplo que se pueden enviar desde Visual Studio.

### Ejemplo

Petición (sin categoría; la propone la IA):

```http
POST /api/tickets
Content-Type: application/json

{
  "titulo": "No puedo entrar al sistema",
  "descripcion": "Mi contraseña está bloqueada y es urgente",
  "solicitanteId": 2
}
```

Respuesta `201 Created`:

```json
{
  "id": 4,
  "categoriaId": 4,
  "prioridad": "Alta",
  "sugerenciaIa": "Verificar si la cuenta está bloqueada y restablecer la contraseña siguiendo el procedimiento de identidad."
}
```

## Estado del proyecto

**Hecho**
- [x] Modelo de datos en SQL Server
- [x] API para crear y listar tickets
- [x] Clasificación automática por reglas con interfaz intercambiable

**Pendiente**
- [ ] Pantalla web en Angular para crear y ver tickets
- [ ] Cambiar estado de un ticket, asignarlo y comentarlo
- [ ] Conectar un modelo de lenguaje real
- [ ] Publicar la demo en la nube (Azure y Vercel)
- [ ] Autenticación y roles

## Limitaciones

- La clasificación por palabras clave es sencilla: no entiende contexto y puede equivocarse con textos ambiguos.
- No tiene autenticación; es una demo y no debe usarse con datos reales.

## Autora

Ana Gabriel Ibarra Mojica · [github.com/anamojica613-alt](https://github.com/anamojica613-alt)
