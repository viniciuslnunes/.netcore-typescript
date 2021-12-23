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
-X POST \
$API_URL/livros \
--header "Content-Type: application/json" \
--data-raw '{"titulo": "Tuler e o Shebang perdido.", "autor": "Angelão, o escritor."}' \

