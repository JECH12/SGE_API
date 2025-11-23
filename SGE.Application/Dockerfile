FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SGE.Application/SGE.Application.csproj ./SGE.Application/
COPY SGE.Data/SGE.Data.csproj ./SGE.Data/
COPY SGE.Services/SGE.Services.csproj ./SGE.Services/
COPY SGE.Shared/SGE.Shared.csproj ./SGE.Shared/

RUN dotnet restore SGE.Application/SGE.Application.csproj

COPY . .

WORKDIR /src/SGE.Application
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "SGE.Application.dll"]