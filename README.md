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
ip Addres of MySql database is set staticly in web.config
docker run --name db -d -p3306:3306 dominikrzeplinski/geolocation-api:geolocationDatabase

