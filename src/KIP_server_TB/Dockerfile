FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

WORKDIR /app
COPY *.csproj ./
RUN dotnet restore


COPY . ./
WORKDIR /app
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./

#ENV Telegram:ApiToken=1763897357:AAH_duVCuzMEMLq1FCrVsmN4fcPEAeW4b2I

LABEL maintainer="Alexei Bezchastnyi(alekseybezchastnyy9@gmail.com)"

ENTRYPOINT ["dotnet", "KIP_server_TB.dll"]