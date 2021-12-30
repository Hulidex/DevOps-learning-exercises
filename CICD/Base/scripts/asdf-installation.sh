#!/bin/bash
set -e

# DESCRIPTION
# --------------- 
# Install  asdf-vm tool version manager.
# For further information check:
#   https://asdf-vm.com/guide/getting-started.html#_1-install-dependencies
#
# PREREQUISITES
# --------------- 
# None
#
# AUTHOR
# --------------- 
# Jolu Izquierdo

if [ ! $# -eq 1 ]; then
    err_msg="You need to provide only one argument to the script.
Concretely the asdf-vm version you want to install.
For example:
    v0.8.1

For further information please check:
https://asdf-vm.com/guide/getting-started.html#_2-download-asdf
"
    echo "$err_msg"
    exit 1
fi

# Install dependencies
apt-get install -y curl git > /dev/null

# Create asdf directories
mkdir -p $ASDF_DIR
mkdir -p $ASDF_DATA_DIR

# Clone git repository in the under /opt/asdf folder
git -c advice.detachedHead=false clone --progress --branch $1 https://github.com/asdf-vm/asdf.git $ASDF_DIR 

# Source asdf.sh each time a bash shell is created
echo ". $ASDF_DIR/asdf.sh" >> ~/.bashrc
