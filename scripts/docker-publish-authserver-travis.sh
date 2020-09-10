#!/bin/bash
DOCKER_ENV=''
DOCKER_TAG=''

case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_ENV=production
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_ENV=development
    DOCKER_TAG=dev
    ;;    
esac

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD

docker build -t auth-server:$DOCKER_TAG -f ./src/AuthServer/src/AuthServer/Dockerfile ./src/AuthServer
docker build -t fibon-service:$DOCKER_TAG -f ./src/Fibon.Service/Dockerfile ./src/Fibon.Service 

docker tag auth-server:$DOCKER_TAG $DOCKER_USERNAME/auth-server:$DOCKER_TAG
docker tag fibon-service:$DOCKER_TAG $DOCKER_USERNAME/fibon-service:$DOCKER_TAG

docker push $DOCKER_USERNAME/auth-server:$DOCKER_TAG
docker push $DOCKER_USERNAME/fibon-service:$DOCKER_TAG