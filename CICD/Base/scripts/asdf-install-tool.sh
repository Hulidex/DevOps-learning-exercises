#!/bin/bash
set -e

# DESCRIPTION
# --------------- 
# Install frameworks and tools, using asdf-vm tool version manager.
# For further information check:
#   https://asdf-vm.com/guide/introduction.html#how-it-works
#
# PREREQUISITES
# --------------- 
# Before using this script asdf-vm should be already installed according to its guide:
#   https://asdf-vm.com/guide/getting-started.html#_1-install-dependencies
#
# AUTHOR
# --------------- 
# Jolu Izquierdo
#

if [ ! $# -eq 3 ]; then
    err_msg="You need to provide exactly 3 arguments to the script.
Concretely:
    1. The plugin URL 
    2. The plugin name () 
    3. The plugin version you want to install and use globally in the system.
For example for installing NodeJS:
    v16.13.1

You can check a list of available plugins here:
    https://github.com/asdf-vm/asdf-plugins
"
    echo "$err_msg"
    exit 1
fi

# source bashrc in order to being able to use the command 'asdf'
. ~/.bashrc


# Add nodejs plugin to asdf
asdf plugin-add $2 $1


# Install a nodejs version
asdf install $2 $3 

# set global version of node
asdf global $2 $3
