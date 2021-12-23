FROM nginx:1.21.4
EXPOSE 80

RUN rm /etc/nginx/conf.d/default.conf
COPY ./*.conf /etc/nginx/conf.d/
