# GeolocationAPI
This is simple REST WEB API  that allows to check and store geolocation data based on IP or URL address. 

# Setup 
Its possible to use docker containers to run application.  
To run web applicatio use windows container:  
docker run --name app -d -p8000:8000 dominikrzeplinski/geolocation-api:geolocationApp

After successful start check his ip: 
docker exec app ipconfig

output will show on what ip is running application: 
IPv4 Address. . . . . . . . . . . :

To store results run linux container with MySql database.  

docker run --name db -d -p3306:3306 dominikrzeplinski/geolocation-api:geolocationDatabase

ip address of MySql database is set statically in web.config if yours docker machine for linux containers got different ip than 
192.168.99.1 do next 2 steps:  
attach to windows app container by: 
docker exec -ti app powershell
change ip: 
(Get-Content C:\inetpub\wwwroot\Web.config ).Replace('192.168.99.1','000.000.00.0') | Out-File -Encoding Ascii C:\inetpub\wwwroot\Web.config
where '000.000.00.0' is yours linux machine ip address. 
type 'exit'. 

#Tips 
If application still cant connect to database check yours firewall permisions.  
