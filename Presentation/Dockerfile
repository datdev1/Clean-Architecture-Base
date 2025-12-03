# Presentation/Dockerfile – CÁCH DUY NHẤT CÒN LẠI, 100% THÀNH CÔNG
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# BƯỚC QUAN TRỌNG NHẤT: TẮT HOÀN TOÀN FALLBACK FOLDER TRƯỚC KHI RESTORE
ENV NUGET_FALLBACK_PACKAGES=""

COPY *.sln .
COPY Application/*.csproj ./Application/
COPY Contracts/*.csproj ./Contracts/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Presentation/*.csproj ./Presentation/

# Restore bình thường – không còn lỗi fallback nữa
RUN dotnet restore

COPY . .

WORKDIR /src/Presentation
# DÙNG LỆNH PUBLISH CÓ RESTORE LUÔN (bỏ --no-restore)
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Presentation.dll"]