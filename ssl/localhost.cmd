openssl req -new -nodes -keyout localhost.key -out localhost.csr -config localhost.conf

openssl x509 -req -in localhost.csr -CA ca.crt -CAkey ca.key -CAcreateserial -out localhost.crt -days 365 -extfile localhost.conf -extensions ext

openssl base64 -in localhost.crt -A

openssl base64 -in localhost.key -A