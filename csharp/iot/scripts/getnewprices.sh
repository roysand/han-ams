#! /bin/sh
tmux new -s iot-price
tmux send-keys -t iot 'ls -la' ENTER
tmux send-keys -t iot 'cd /home/roy/repo/han-ams/csharp/iot/tests/TestPriceClient/' ENTER
tmux send-keys -t iot 'dotnet run  &> ~/log/price.log' ENTER
tmux kill-session - iot-price
