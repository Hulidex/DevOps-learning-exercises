FROM hulidex/dotnet-angular:latest

COPY ./scripts/* /home/dev/scripts/
RUN chown -R dev:dev /home/dev/scripts && chmod -R 770 /home/dev/scripts/

# Use unprivileged user
USER dev

# Create the directory manually in the first place. Otherwise, the WORKDIR
# command will create the directory with root permissions and it won't be accessible.
RUN mkdir -p /home/dev/src
WORKDIR /home/dev/src

# Clone git repository to fetch the last version
# Concretely clone the specific branch 'exercise/CICD/02-Docker-create-development-environment'
RUN git clone -b exercise/CICD/02-Docker-create-development-environment https://github.com/Hulidex/DevOps-learning-exercises.git .
WORKDIR /home/dev/src/WebUI

# Publish solution, as a result of the dotnet + Angular template, both projects will 
# be compiled and the angular project will be embeded into the final solution.
RUN /home/dev/scripts/publish-solution.sh

WORKDIR /home/dev/src/WebUI/bin/WebUI
# Run the compiled solution
ENTRYPOINT [ "/bin/bash", "-c", "/home/dev/scripts/entrypoint.sh" ]