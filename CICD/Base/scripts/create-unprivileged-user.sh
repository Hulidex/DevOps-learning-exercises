#!/bin/bash
set -e

# DESCRIPTION
# --------------- 
#   - Create an unprivileged user whose ID should match with your
#     Host user id.
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
Concretely the USER GROUP ID you want to configure for this image
For example:
    1000

The 1000 ID is normally assigned in Linux based systems to the first created user,
Thus if your host operating system is based on Linux and
only has a sigle user, is very likely that it will has the ID 1000
"
    echo "$err_msg"
    exit 1
fi

# Create unprivileged group with custom GUID and name dev 
groupadd -g $1 dev

# Create an unprivileged user
useradd -u $1 -g $1 dev 

mkdir -p /home/dev

# Source asdf.sh each time a bash shell is created
# $ASDF_DIR is an environment variable
echo ". $ASDF_DIR/asdf.sh" >> /home/dev/.bashrc
cp /root/.tool-versions /home/dev/.

# Give permissions and ownership to the user directory
chown -R dev:dev /home/dev