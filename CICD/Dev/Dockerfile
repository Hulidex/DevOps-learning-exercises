FROM hulidex/dotnet-angular:latest

# Set dotnet core default environment
# the ENV statement will create this variable
# within the containers created from this image.
WORKDIR /home/dev/src/WebUI
COPY ./scripts/* /home/dev/scripts/
RUN chown -R dev:dev /home/dev/scripts && chmod -R 770 /home/dev/scripts/

# Use unprivileged user
USER dev

ENTRYPOINT [ "/bin/bash", "-c", "/home/dev/scripts/entrypoint.sh" ]