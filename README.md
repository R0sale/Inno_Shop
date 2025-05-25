# Inno_Shop

Inno_Shop is a system, contained of 2 microservices (UserService, ProductService), which are intended to manage users and their products.

# What Is Included Into Repository

Technologies Used
- ASP.NET Core 8
- Entity Framework Core (Code-First)
- SQL Server (in Docker)
- MediatR
- FluentValidation
- JWT Bearer Authentication
- Docker & Docker Compose

UserService implements:
- RESTful API for reading, creating, deleting, updating users.
- Authentication and authorization with JWT.
- Confirming user through email.
- Changing password with confirming action by following the link, sended onto email.

ProductService implements:
- RESTful API for reading, creating, deleting, updating users.
- Searching and filtering products via price and creation date parameters.
- Only authorized users may perfrm actions on products. They may manipulate only their products properties.

# Run The Project
You will need the following tools:
- [Visual Studio](https://visualstudio.microsoft.com/downloads/)
- [.NET Core 8 or later](https://dotnet.microsoft.com/download/dotnet-core/8)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

**Installing**
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)
1. Clone the repository
2. Go to root directory which include docker-compose.yml files, run below command:

```docker-compose up --build```

3. Wait for docker compose all microservices. That’s it! (some microservices need extra time to work so please wait if not worked in first shut)
4. You may use Postman to interact with microservices.

```http://localhost:5001/api/users```

**GET** Returns all the users on the current page (1 as default, size of page 10 as default).

```http://localhost:5001/api/users/{id}```

**GET** Returns a specified user.

**DELETE** Deletes a specified user.

**PUT** Updates user with new data.

**PATCH** Partially updates user.

```http://localhost:5001/api/authentication```

**POST** Registering(Creating) a new user. Confirming email.

```http://localhost:5001/api/authentication/login```

**POST** You may login and obtain a jwt token. If your email is not confirmed yet, you will receive a link to follow on your mail.

```http://localhost:5001/api/authentication/changePassword```

**POST** To change your password specify email and new password. After request, check your email follow the link and confirm action.

```http://localhost:5000/api/products``` for ProductService

**GET** Returns al the products on the current page (1 as default, size of page 10 as default).

**POST** You may create your product (if you are authorized through JWT).

```http://localhost:5000/api/products{id}```

**GET** Returns a specified product.

**DELETE** Deletes product with Id equals to id.

**PUT** Updates product with new data.

**PATCH** Partially updates product.



