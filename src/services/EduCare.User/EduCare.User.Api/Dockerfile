FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore EduCare.User/EduCare.User.Api/EduCare.User.Api.csproj
RUN dotnet build EduCare.User/EduCare.User.Api/EduCare.User.Api.csproj
RUN dotnet publish EduCare.User/EduCare.User.Api/EduCare.User.Api.csproj -c Release -o /publish

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "EduCare.User.Api.dll"]