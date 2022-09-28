#!/bin/sh
/usr/bin/tmux new-session -d -s iot-price
# tmux detach -s iot-price ENTER
sleep 1
/usr/bin/tmux send-keys -t iot-price 'ls -la' ENTER
/usr/bin/tmux send-keys -t iot-price 'cd /home/roy/repo/han-ams/csharp/iot/tests/TestPriceClient/' ENTER
/usr/bin/tmux send-keys -t iot-price '/snap/bin/dotnet run; tmux wait -S iot' ENTER
/usr/bin/tmux wait iot
/usr/bin/tmux kill-session -t iot-price
