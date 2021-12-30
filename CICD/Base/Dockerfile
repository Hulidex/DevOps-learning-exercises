ARG DEB_VERSION=stable
FROM debian:${DEB_VERSION}

ARG ASDF_VERSION=v0.9.0
ARG NODEJS_VERSION=lts-gallium
ARG NETCORE_VERSION=6.0.101
ARG USER_GROUP_ID=1000

# Set working directory
WORKDIR /root

# Copy needed files and set execution and read permission
# only for the owner user
COPY ./scripts/* scripts/
RUN chmod -R 500 scripts

# Configure the image
RUN scripts/config.sh

ENV ASPNETCORE_ENVIRONMENT=Development

# Install asdf-vm for managing framework versions
ENV ASDF_DIR=/opt/asdf-vm
ENV ASDF_DATA_DIR=/srv/asdf
ENV ASDF_CONFIG_FILE=/srv/asdf/asdfrc
RUN scripts/asdf-installation.sh $ASDF_VERSION

# Install NodeJS dependencies
RUN apt-get install -y dirmngr gpg curl gawk > /dev/null
# Install NodeJS using asdf-vm
RUN scripts/asdf-install-tool.sh https://github.com/asdf-vm/asdf-nodejs.git nodejs $NODEJS_VERSION

# Install .NET core dependencies
RUN apt-get install -y libicu-dev > /dev/null
# Install .NET core Framework
RUN scripts/asdf-install-tool.sh https://github.com/emersonsoares/asdf-dotnet-core.git dotnet-core $NETCORE_VERSION

# Create unprivileged user
RUN scripts/create-unprivileged-user.sh $USER_GROUP_ID

# Default command to execute when creating containers
CMD ["bash"]