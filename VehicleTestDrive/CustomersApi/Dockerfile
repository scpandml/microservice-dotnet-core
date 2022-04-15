#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CustomersApi/CustomersApi.csproj", "CustomersApi/"]
RUN dotnet restore "CustomersApi/CustomersApi.csproj"
COPY . .
WORKDIR "/src/CustomersApi"
RUN dotnet build "CustomersApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomersApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomersApi.dll"]