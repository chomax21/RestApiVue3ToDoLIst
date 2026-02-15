FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RestApiVue3ToDoLIst.csproj", "."]
RUN dotnet restore "RestApiVue3ToDoLIst.csproj"
COPY . .
RUN dotnet publish "RestApiVue3ToDoLIst.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RestApiVue3ToDoLIst.dll"]