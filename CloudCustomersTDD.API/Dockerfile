ARG EXECUTABLE=CloudCustomersTDD.dll
ARG ASPNETCORE_ENV=production
ARG APP_NAME=CloudCustomersTDD
ARG BASE_IMAGE

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG APP_NAME

WORKDIR /src
COPY . ./
RUN dotnet publish -c Release -o /app/publish $APP_NAME/*.csproj

#FROM 320721223658.dkr.ecr.us-east-1.amazonaws.com/base-image/net:tag-aspnet-6.0-alpine-amd64-1.0.5 as final
FROM $BASE_IMAGE
ARG EXECUTABLE
ARG ASPNETCORE_ENV
ARG APP_NAME

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENV
ENV APP=$EXECUTABLE
ENTRYPOINT dotnet $APP
