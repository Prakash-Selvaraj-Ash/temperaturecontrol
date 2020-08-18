@echo off
cd..
cd "eMTE.Temperature"
color 2
echo.
echo.
echo ==============================================================
echo Remove bin Directory
echo ==============================================================
echo.
echo.
if exist "bin\" rmdir /q  /s "bin"
echo Removed bin folder is done

echo.
echo.
echo ==============================================================
echo Build api solution 
echo ==============================================================
echo.
echo.
dotnet publish eMTE.Temperature.sln -c Release

echo.
echo.
echo ==============================================================
echo Copy Docker file to publich folder
echo ==============================================================
echo.
echo.
copy Dockerfile  .\bin\Release\netcoreapp3.1\publish

echo.
echo.
echo ==============================================================
echo Docker build new image
echo ==============================================================
echo.
echo.
docker build -t emte-health-tracker-api ./bin/release/netcoreapp3.1/publish/

echo.
echo.
echo ==============================================================
echo Docker login to Heroku
echo ==============================================================
echo.
echo.
docker login --username=moganvig90@gmail.com --password=48096ba2-45fb-48fc-a8be-b1ee0728d570 registry.heroku.com

echo.
echo.
echo ==============================================================
echo Docker tag image
echo ==============================================================
echo.
echo.
docker tag emte-health-tracker-api registry.heroku.com/emte-health-tracker-api/web
echo Docker tag image is done

echo.
echo.
echo ==============================================================
echo Docker push image to Heroku 
echo ==============================================================
echo.
echo.
docker push registry.heroku.com/emte-health-tracker-api/web

echo.
echo.
echo ==============================================================
echo Release
echo ==============================================================
echo.
echo.
heroku container:release web -a emte-health-tracker-api

echo ==============================================================
echo ----------------------ALL DONE--------------------------------
echo ==============================================================
PAUSE