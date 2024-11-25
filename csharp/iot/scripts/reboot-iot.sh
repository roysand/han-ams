#!/bin/bash

# Name of the tmux session
SESSION_NAME="iot"

# Directory containing the start.csproj file
TARGET_DIR="/home/roy/repo/han-ams/csharp/iot/src/Apps/MqttReader.Console"

# Check if the session already exists
tmux has-session -t $SESSION_NAME 2>/dev/null

# If the session does not exist, create it and start the .NET project
if [ $? != 0 ]; then
    # Create a new session and start it detached
    tmux new-session -d -s $SESSION_NAME

    # Send the command to change directory and run the project
    tmux send-keys -t $SESSION_NAME "cd $TARGET_DIR; dotnet run MqttReader.Console -c Release" C-m
fi

# Attach to the session
# tmux attach -t $SESSION_NAME
