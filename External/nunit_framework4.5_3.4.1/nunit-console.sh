#!/bin/sh

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

if [ -z "$2" ]
then
    exec mono --debug $MONO_OPTIONS "${DIR}/nunit3-console.exe" $1 --noh --noresult
else
    exec mono --debug $MONO_OPTIONS "${DIR}/nunit3-console.exe" $1 --noh --noresult --where $2 
fi
