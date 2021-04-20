FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY *.sln .
COPY src/KIP_server_GET/*.csproj ./KIP_server_GET/
COPY src/KIP_POST_APP/*.csproj ./KIP_POST_APP/

RUN dotnet restore

COPY . .
WORKDIR src/KIP_server_GET
RUN dotnet build -c Release -o /app

WORKDIR src/KIP_POST_APP
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

LABEL maintainer="Alexei Bezchastnyi(alekseybezchastnyy9@gmail.com)"

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KIP_server_GET.dll"]