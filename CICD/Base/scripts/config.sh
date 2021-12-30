#!/bin/bash
set -e

# DESCRIPTION
# --------------- 
# Configure the debian image:
#   - Update and upgrade the system
#   - Install some packages
#   - Fix some warnings and issues
#
# PREREQUISITES
# --------------- 
# None
#
# AUTHOR
# --------------- 
# Jolu Izquierdo

# This variable is used by the dialog package, if not set
# dialog will not work. (Dialog could be used by another tools
# to print messages in the terminal)
TERM=xterm

# Persist this environment variable forever
echo "TERM=xterm" > /etc/profile.d/term_config.sh

# Update repositories and upgrade system
apt-get update -y && apt-get upgrade -y > /dev/null
# Optionally install an editor of your liking and
# other optional dependencies.
apt-get install -y apt-utils neovim dialog > /dev/null
# This additional steps are performed for solving an error produced with 'debconf'
# For further information check: https://github.com/moby/moby/issues/27988
echo 'debconf debconf/frontend select Noninteractive' | debconf-set-selections
apt-get install -y -q > /dev/null
