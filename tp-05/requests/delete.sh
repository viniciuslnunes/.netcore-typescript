#!/bin/bash

while read LINE; do
  COMMAND="export "
  ENV_VAR=$(echo $LINE | tr "=" "\n")

  for FRAGMENT in $ENV_VAR
  do
    COMMAND="${COMMAND}${FRAGMENT}="
  done

  eval ${COMMAND::-1}
done < ./.env

curl -i -k \
-X DELETE \
$API_URL/livros/1 \
--header "Content-Type: application/json" \
