create table geolocations (
	id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	lastUpdate TIMESTAMP,  
	ip VARCHAR(256) NOT NULL UNIQUE,
	ipType VARCHAR(256),
    continentCode VARCHAR(256),
    continentName VARCHAR(256),
    countryCode VARCHAR(256),
    regionCode VARCHAR(256),
    regionName VARCHAR(256),
    city VARCHAR(256),
    zip VARCHAR(256),
    latitude DOUBLE,
    longitude DOUBLE);