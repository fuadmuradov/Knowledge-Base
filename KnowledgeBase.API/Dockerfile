#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KnowledgeBase.API/KnowledgeBase.API.csproj", "KnowledgeBase.API/"]
COPY ["KnowledgeBase.Service/KnowledgeBase.Service.csproj", "KnowledgeBase.Service/"]
COPY ["KnowledgeBase.Data/KnowledgeBase.Data.csproj", "KnowledgeBase.Data/"]
COPY ["KnowledgeBase.Core/KnowledgeBase.Core.csproj", "KnowledgeBase.Core/"]
RUN dotnet restore "KnowledgeBase.API/KnowledgeBase.API.csproj"
COPY . .
WORKDIR "/src/KnowledgeBase.API"
RUN dotnet build "KnowledgeBase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KnowledgeBase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KnowledgeBase.API.dll"]