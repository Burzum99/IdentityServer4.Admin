FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
#
## Copy csproj and restore as distinct layers
COPY src/Skoruba.IdentityServer4.Admin.BusinessLogic.Identity/Skoruba.IdentityServer4.Admin.BusinessLogic.Identity.csproj /src/Skoruba.IdentityServer4.Admin.BusinessLogic.Identity/
COPY src/Skoruba.IdentityServer4.Admin.BusinessLogic.Shared/Skoruba.IdentityServer4.Admin.BusinessLogic.Shared.csproj /src/Skoruba.IdentityServer4.Admin.BusinessLogic.Shared/
COPY src/Skoruba.IdentityServer4.Admin.BusinessLogic/Skoruba.IdentityServer4.Admin.BusinessLogic.csproj /src/Skoruba.IdentityServer4.Admin.BusinessLogic/
COPY src/Skoruba.IdentityServer4.Admin.EntityFramework/Skoruba.IdentityServer4.Admin.EntityFramework.csproj /src/Skoruba.IdentityServer4.Admin.EntityFramework
COPY src/Skoruba.IdentityServer4.Admin.EntityFramework.Extensions/Skoruba.IdentityServer4.Admin.EntityFramework.Extensions.csproj /src/Skoruba.IdentityServer4.Admin.EntityFramework.Extensions
COPY src/Skoruba.IdentityServer4.Admin.EntityFramework.Identity/Skoruba.IdentityServer4.Admin.EntityFramework.Identity.csproj /src/Skoruba.IdentityServer4.Admin.EntityFramework.Identity
COPY src/Skoruba.IdentityServer4.Admin.EntityFramework.Shared/Skoruba.IdentityServer4.Admin.EntityFramework.Shared.csproj /src/Skoruba.IdentityServer4.Admin.EntityFramework.Shared
COPY src/Skoruba.IdentityServer4.Admin.Api/Skoruba.IdentityServer4.Admin.Api.csproj /src/Skoruba.IdentityServer4.Admin.Api/
COPY src/Skoruba.IdentityServer4.STS.Identity/Skoruba.IdentityServer4.STS.Identity.csproj /src/Skoruba.IdentityServer4.STS.Identity/
COPY src/Skoruba.IdentityServer4.Admin/Skoruba.IdentityServer4.Admin.csproj /src/Skoruba.IdentityServer4.Admin/
RUN dotnet restore /src/Skoruba.IdentityServer4.Admin/Skoruba.IdentityServer4.Admin.csproj
#
## Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o /app
#
## Build runtime image

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "Skoruba.IdentityServer4.Admin.dll"]