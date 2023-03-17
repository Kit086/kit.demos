FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://*:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ServerService/ServerService.csproj", "ServerService/"]
RUN dotnet restore "ServerService/ServerService.csproj"
COPY . .
WORKDIR "/src/ServerService"
RUN dotnet build "ServerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerService.dll"]
