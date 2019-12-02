FROM mysql/mysql-server
ENV MYSQL_ROOT_PASSWORD admin
ENV MYSQL_DATABASE dbgeolocation
ENV MYSQL_USER user
ENV MYSQL_PASSWORD pass
ADD script.sql /docker-entrypoint-initdb.d
EXPOSE 3306