FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-environment
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o ./out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS web-environment
WORKDIR /app
COPY --from=build-environment /app/out/ ./

ENTRYPOINT [ "dotnet", "Tp04.WebApi.dll" ]

EXPOSE 80
