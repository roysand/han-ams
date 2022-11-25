#! /bin/sh
tmux new-session -d -s iot-price
# tmux detach -s iot-price ENTER
sleep 1
tmux send-keys -t iot-price 'ls -la' ENTER
tmux send-keys -t iot-price 'cd /home/roy/repo/han-ams/csharp/iot/tests/TestPriceClient/' ENTER
tmux send-keys -t iot-price 'dotnet run  &>> ~/log/price.log; tmux wait -S iot' ENTER
tmux wait iot
tmux kill-session -t iot-price
