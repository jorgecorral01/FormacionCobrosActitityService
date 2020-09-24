FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
WORKDIR /src

COPY ["Charge.Activity.Service/Charge.Activity.Service.csproj", "Charge.Activity.Service/"]
COPY ["Charge.Activity.Service.Actions/Charge.Activity.Service.Action.csproj", "Charge.Activity.Service.Actions/"]
COPY ["Charge.Activity.Service.Bussines/Charge.Activity.Service.Bussines.csproj", "Charge.Activity.Service.Bussines/"]
COPY ["Charge.Activity.Service.Repository/Charge.Activity.Service.Repository.Entity.csproj", "Charge.Activity.Service.Repository/"]

RUN dotnet restore "Charge.Activity.Service/Charge.Activity.Service.csproj"

COPY . .
WORKDIR "/src"

FROM build AS publish

RUN dotnet publish "Charge.Activity.Service/Charge.Activity.Service.csproj" -c Release -o /app

# Build runtime image 
FROM base as final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Charge.Activity.Service.dll"]