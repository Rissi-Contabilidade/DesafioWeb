FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DesafioWeb/DesafioWeb.csproj", "DesafioWeb/"]
RUN dotnet restore "DesafioWeb/DesafioWeb.csproj"
COPY . .
WORKDIR "/src/DesafioWeb"
RUN dotnet build "DesafioWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioWeb.dll"]
