# fams3

FAMS-search-api is a dotnet core rest service to execute people search accross multiple data providers.

## Project Structure

    .
    ├── app                     # Application Source Files.
    ├── .gitignore              # Git ignore.
    └── README.md               # This file.

## Run on Docker

download and install [Docker](https://www.docker.com)

Run

```shell
cd app/SearchApi
docker-compose up
```

Application health can be checked [here](http://localhost:5050/health).

## Run

download and install [dotnet core 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0)

Run

```shell
cd app/SearchApi/SearchApi.Web
dotnet run
```

Application health can be checked [here](http://localhost:5000/health).
